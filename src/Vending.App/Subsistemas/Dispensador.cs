using System.Collections.Generic;


namespace Vending.Subsitemas
{
    using Vending.Modelos;
    using Vending.Data;
    
    public class Dispensador
    {
        internal Producto[,] Parrilla { get; init; }
        internal IRepoDispensador _repositorio;
        public (int, int) Dimensiones
        {
            get => (Parrilla.GetLength(0), Parrilla.GetLength(1));
        }
        
        public Dispensador(IRepoDispensador repo){
            _repositorio = repo;
            _repositorio.Inicializar();
            Parrilla = _repositorio.Cargar();
        }
        public Producto ObtenerInfo(in (int x, int y) coordenada, out int cantidad)
        {
            cantidad = Parrilla[coordenada.x, coordenada.y].Cantidad;
            return Parrilla[coordenada.x, coordenada.y];
        }
        public void RellernarParrilla(int cantidad = 5)
        {
            var filas = Parrilla.GetLength(0);
            var columnas = Parrilla.GetLength(1);
            for (var f = 0; f < filas; f++)
                for (var c = 0; c < columnas; c++)
                    Parrilla[f, c].Cantidad = cantidad;
        }
        public int DescontarCantidad ((int f, int c) coordenada) => --Parrilla[coordenada.f, coordenada.c].Cantidad;
       
        // === OPERATIVA ====
        public void Guardar(){
            _repositorio.Guardar(Parrilla);
        }

        public override string ToString() => $"Dispensador({Parrilla.GetLength(0)}x{Parrilla.GetLength(1)})";
    }
}
