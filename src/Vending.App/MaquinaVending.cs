using System.Collections.Generic;
using System.Linq;
using Vending.Modelos;
using Vending.Subsitemas;
using Vending.Subsitemas.Monetarios;

namespace Vending
{
    public class MaquinaVending
    {
        Dispensador _dispensador;
        ControlDePagos _ctrlPagos;
        ISeguridad _ctrlSeguridad;
        public MaquinaVending(Dispensador dispensador, ControlDePagos ctrlPagos, ISeguridad ctrlSeguridad)
        {
            _dispensador = dispensador;
            _ctrlPagos = ctrlPagos;
            _ctrlSeguridad = ctrlSeguridad;
        }

        // ===== DISPENSADOR ====

        public (int, int) Dimensiones
        {
            get => _dispensador.Dimensiones;
        }
        public Producto ObtenerInfoArticulo(in (int, int) coordenada, out int cantidad)
        {
            return _dispensador.ObtenerInfo(coordenada, out cantidad);
        }
        public int DescontarCantidad((int, int) coordenada) => _dispensador.DescontarCantidad(coordenada);
        public void RellernarParrilla() => _dispensador.RellernarParrilla();

        // ==== PAGOS ====
        public PagoTipo ValidarPago(decimal precio, Efectivo pago)
        {
            // Pago insuficiente
            if (pago.Importe < precio) return PagoTipo.Insuficiente;
            // Numero de monedas excesivo
            if (pago.Cantidad.Any(i => i > _ctrlPagos.MAX_MONEDAS)) return PagoTipo.NoAdmintido;
            if (pago.Cantidad.Sum() > _ctrlPagos.MAX_PAGO_MONEDAS) return PagoTipo.NoAdmintido;
            // Cambio no disponible
            var cambio = _ctrlPagos.ValidarPago(precio, pago);
            if (!cambio.Valido) return PagoTipo.SinCambios;
            // Pago Valido
            return PagoTipo.Valido;
        }
        public Efectivo AplicarPago(decimal precio, Efectivo pago) => _ctrlPagos.AplicarPago(precio, pago);
        public void ReestablecerCaja() => _ctrlPagos.ReestablecerCaja();
        public string Informe() => _ctrlPagos.ToString();
        // === SEGURIDAD ====
        public bool EsPinValido(string pin) => _ctrlSeguridad.EsPinValido(pin);
    }
}
