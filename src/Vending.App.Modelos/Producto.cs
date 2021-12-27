using System.Linq;

namespace Vending.Modelos
{
    public abstract class Producto
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public Producto(string nombre, decimal? precio, int cantidad = 0)
        {
            if (nombre is null) throw new System.ArgumentNullException("Debe indicar un nombre");
            if (precio is null) throw new System.ArgumentNullException("Debe indicar un precio");
            if (precio <= 0) new System.ArgumentException("El precio no puede ser 0 ni inferior");
            Nombre = nombre;
            Precio = precio ?? 0;
            Cantidad = cantidad;
        }

        public string Tipo { get => this.GetType().ToString().Split(".").Last(); }

        public override string ToString() => $"{Tipo}: '{Nombre}' {Precio:#.00€}";

    }
}
