using System;
using System.Collections.Generic;
using System.Linq;
using Vending.Modelos;
using Vending.Subsitemas.Monetarios;

namespace Vending.UI.Consola
{
    enum ModoTerminal { Normal, Admin }

    public class Controlador
    {
        // ===== DEPENDENCIAS =====
        private MaquinaVending _sistema;
        private Vista _vista;

        // ===== MODO DE USO =====
        private string _usuario; // Usuario Logeado
        private ModoTerminal _modo; //Modo de la Interfaz
        private string[] _menuModo = { "Modo Admin", "Logout Admin" }; // Opciones del menu en funcion del modo

        // ===== CASOS DE USO ==== 
        private Dictionary<(string titulo, ModoTerminal modo), Func<bool>> _casosDeUso;
        public Controlador(MaquinaVending sistema, Vista vista)
        {
            _sistema = sistema;
            _vista = vista;
            _modo = ModoTerminal.Normal;
            _usuario = "Anonimo";
            _casosDeUso = new Dictionary<(string, ModoTerminal), Func<bool>>(){
                { ("Consultar Productos",ModoTerminal.Normal), ConsultarProducto },
                { ("Adquirir Producto",ModoTerminal.Normal), AdquirirProducto },
                { ("Reposición y Recaudación",ModoTerminal.Admin), ReestablecerMáquina },
                { ("Informe",ModoTerminal.Admin), Informe },
                { ("Apagar Máquina",ModoTerminal.Admin), ApagarMaquina },
                { (_menuModo[(int)_modo],ModoTerminal.Normal), establecerModoInterfaz },
            };
        }

        // ======== CICLO PRINCIPAL =====
        public void Run()
        {
            var ciclo = true;
            _vista.LimpiarPantalla();

            while (ciclo)
                try
                {
                    // Menu y Obtener opción 
                    var menu = _casosDeUso.Keys.Where(k => k.modo <= _modo).Select(k => k.titulo).ToList<String>();
                    var opcion = _vista.TryObtenerElementoDeLista($"Menu de Vending", menu, "Seleciona una opción");
                    // Ejecución de la opción escogida
                    _vista.Mostrar(""); // (opcion);
                    var casoDeUso = _casosDeUso.FirstOrDefault(k => k.Key.titulo == opcion).Value;
                    ciclo = casoDeUso.Invoke();
                    // Fin opción
                    _vista.MostrarYReturn("Pulsa <Return> para continuar", ConsoleColor.DarkGray);
                    _vista.LimpiarPantalla();
                }
                catch
                {
                    return;
                }
        }

        // ======= CASOS DE USO ========
        private bool ConsultarProducto()
        {
            // Obtener parrilla
            var limites = _sistema.Dimensiones;
            // Transformar a ViewModel
            var parrillaVM = ParrillaToVM(limites, _modo);
            // Mostar parrilla
            _vista.MostrarParrilla("Artículos en Venta", parrillaVM);
            return true;

            List<string>[,] ParrillaToVM((int filas, int columnas) limites, ModoTerminal modo)
            {
                var parrillaVM = new List<string>[limites.filas, limites.columnas];
                for (var f = 0; f < limites.filas; f++)
                    for (var c = 0; c < limites.columnas; c++)
                    {
                        int cantidad;
                        var art = _sistema.ObtenerInfoArticulo((f, c), out cantidad);
                        parrillaVM[f, c] = ProductoVM(art, cantidad, modo);
                    }
                return parrillaVM;

                List<string> ProductoVM(Producto p, int cantidad, ModoTerminal modo)
                {
                    // switch expresion + guardas
                    // type casting
                    // reflexion para tipo
                    var prefix = p.Tipo switch
                    {
                        "ParaFarma" => "💊",
                        "RefrescoDietetico" => "🍉",
                        // Número mágico -> refactorización
                        "Refresco" when ((Refresco)p).Centilitros < 60 => "🥤",
                        "Refresco"  => "🍶",
                        _ => "🍬",
                    };
                    var data = new List<string> {
                        prefix+" "+p.Nombre,
                        p.Precio.ToString("#.00€"),
                    };
                    if (modo == ModoTerminal.Admin)
                        data.Add("              " + cantidad.ToString("0"));
                    return data;
                }

            }

        }
        private bool AdquirirProducto()
        {
            string username = "";
            while (true)
            {
                // Include 'ConsultarProducto'
                ConsultarProducto();
                try
                {
                    // Variables del Caso de Uso            
                    (int x, int y) coordenadas;
                    Producto articulo;
                    int cantidad;
                    Efectivo pago;

                    // 1.- Obtener coordenada de parrilla
                    while (true)
                    {
                        coordenadas = _vista.TryObtenerTuplaInt("Indica la posición x,y", _sistema.Dimensiones);
                        // 2.- Verificar la coordenada: Cantidad y Articulo
                        articulo = _sistema.ObtenerInfoArticulo(coordenadas, out cantidad);
                        if (cantidad > 0) break;
                        _vista.Mostrar($"'{articulo.Nombre}' no disponible en estos momentos");
                    }
                    
                    // 3.- Informar
                    _vista.Mostrar($"Has seleccionado {articulo}");
                    // _vista.Mostrar($"Has seleccionado '{articulo.Nombre}'");
                    // _vista.Mostrar($"Importe {articulo.Precio:0.00}€");

                    // 4.- -> Si Producto Parafarmacia obtener usuario
                    if (articulo.Tipo == "ParaFarma" && username == "")
                    {
                        _vista.Mostrar($"Este producto requiere que te identifiques");
                        username = _vista.TryObtenerDatoDeTipo<string>("Indica tu nombre");
                    }

                    // 5.- Obtener importe del Pago
                    while (true)
                    {
                        var monedas = _vista.TryObtenerArrayInt("Introduce el número de monedas de 2€,1€,0.5€,0.2€ y 0.1€ separadas por ','", 5, ',');
                        pago = new Efectivo(monedas);
                        if (pago.Valido) break;
                        _vista.Mostrar($"Importe introducido no válido", ConsoleColor.DarkRed);
                    }

                    // 6.- Verificar Pago
                        var pagoStatus = _sistema.ValidarPago(articulo.Precio, pago);
                    if (pagoStatus == PagoTipo.Insuficiente)
                        _vista.Mostrar($"Importe introducido insuficiente", ConsoleColor.DarkRed);
                    if (pagoStatus == PagoTipo.NoAdmintido)
                        _vista.Mostrar($"Cantidad de monedas no adminitdas", ConsoleColor.DarkRed);
                    if (pagoStatus == PagoTipo.SinCambios)
                        _vista.Mostrar($"Cambio no disponible", ConsoleColor.DarkRed);
                    if (pagoStatus != PagoTipo.Valido)
                    {
                        _vista.Mostrar($"Recoge tu dinero {pago.Importe:0.00}€");
                        break; // Salimos
                    }
                    
                    // 7.- Proceder a la entrega de producto
                    _vista.Mostrar($"Expulsando '{articulo.Nombre}' a la bandeja de recogida ...");
                    _sistema.DescontarCantidad(coordenadas);
                    
                    // 8.- Proceder a actualizar Caja 
                    var cambio = _sistema.AplicarPago(articulo.Precio, pago);
                    
                    // 9.- Devolver Cambios
                    if (cambio.Importe > 0)
                        _vista.Mostrar($"Su cambio {cambio}");

                    // 10.- Operaciones de Finalización
                    // _sistema.VentaFinalizada(articulo.Precio)
                    // ventas++
                    // total+=precio

                    // 11.- Fin ciclo o Continue ...
                    // if (username != "")
                    // {
                    //     var sn = _vista.TryObtenerCaracterDeString("Desea seguir comprando", "SN", 'N');
                    //     if (sn == 'S') continue;
                    // }
                    // break;
                    if (username == "") break;
                    var sn = _vista.TryObtenerCaracterDeString("Desea seguir comprando", "SN", 'N');
                    if (sn != 'S') break;
                }
                catch (Exception)
                {
                    _vista.Mostrar("UC: Cancelación de usuario", ConsoleColor.DarkRed);
                    break;
                }
            }
            return true;
        }
        private bool ReestablecerMáquina()
        {
            _sistema.RellernarParrilla();
            _sistema.ReestablecerCaja();
            //_sistema.RestablecerVentas();
            return true;
        }
        private bool Informe()
        {
            //var caja = _sistema.InfoCaja();
            var info = _sistema.Informe();
            _vista.Mostrar(info);
            return true;
        }
        private bool ApagarMaquina()
        {
            var sn = _vista.TryObtenerCaracterDeString("Desea imprimir informe", "SN", 'S');
            if (sn == 'S') Informe();
            _sistema.ApagarMaquina();
            _vista.Mostrar("Apagando máquina ...");
            return false;
        }

        // ====== MODO DEL TERMINAL =====
        private bool establecerModoInterfaz()
        {
            switch (_modo)
            {
                case ModoTerminal.Admin:
                    establecerNormal();
                    break;
                case ModoTerminal.Normal:
                    try
                    {
                        var pin = _vista.TryObtenerDatoDeTipo<string>("Pin").ToLower().Trim();
                        if (!_sistema.EsPinValido(pin))
                            _vista.Mostrar("Acceso no permitido", ConsoleColor.DarkRed);
                        else
                            establecerAdmin("Admin");
                    }
                    catch { return true; };
                    break;
            }
            return true;
            void establecerNormal()
            {
                _usuario = "anomious";
                _modo = ModoTerminal.Normal;
                establecerOpcionDeMenu(_menuModo[(int)_modo]);
            };
            void establecerAdmin(string username)
            {
                _usuario = username.ToLower().Trim();
                _modo = ModoTerminal.Admin;
                establecerOpcionDeMenu(_menuModo[(int)_modo]);
            };
            void establecerOpcionDeMenu(string opcion)
            {
                var modoKey = _casosDeUso.FirstOrDefault(x => x.Value == establecerModoInterfaz).Key;
                _casosDeUso.Remove(modoKey);
                _casosDeUso.Add((opcion, _modo), establecerModoInterfaz);
            }
        }
    }
}