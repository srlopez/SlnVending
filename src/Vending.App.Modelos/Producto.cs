namespace Vending.Modelos
{
    public enum ProductoTipo { Golosina, Refresco, Farma }
    public abstract class Producto
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public ProductoTipo Tipo { get; set; }

        public Producto(string nombre, decimal? precio)
        {
            if (nombre is null) throw new System.ArgumentNullException("Debe indicar un nombre");
            if (precio is null) throw new System.ArgumentNullException("Debe indicar un precio");
            if (precio <= 0) new System.ArgumentException("El precio no puede ser 0 ni inferior");
            Nombre = nombre;
            Precio = precio??0;
        }

        public virtual bool EsDietetico() => false;

        public override string ToString() => $"{Nombre,12} {Precio}";
    }
}
