using System.Collections.Generic;

namespace Vending.Subsitemas
{
    using Vending.Modelos;
    public class Dispensador
    {
        internal Producto[,] Parrilla { get; init; }
        internal int[,] Cantidad { get; init; }

        public (int, int) Dimensiones
        {
            get => (Parrilla.GetLength(0), Parrilla.GetLength(1));
        }
        public Dispensador(Producto[,] parrilla)
        {
            Parrilla = parrilla;
            Cantidad = new int[Parrilla.GetLength(0), Parrilla.GetLength(1)];
            RellernarParrilla();
        }

        public Producto ObtenerInfo(in (int x, int y) coordenada, out int cantidad)
        {
            cantidad = Cantidad[coordenada.x, coordenada.y];
            return Parrilla[coordenada.x, coordenada.y];
        }

        public void RellernarParrilla(int cantidad = 5)
        {
            var filas = Cantidad.GetLength(0);
            var columnas = Cantidad.GetLength(1);
            for (var f = 0; f < filas; f++)
                for (var c = 0; c < columnas; c++)
                    Cantidad[f, c] = cantidad;
        }
        public int DescontarCantidad ((int f, int c) coordenada) => --Cantidad[coordenada.f, coordenada.c];
       
        public override string ToString() => $"Dispensador({Parrilla.GetLength(0)}x{Parrilla.GetLength(1)})";
    }
}
