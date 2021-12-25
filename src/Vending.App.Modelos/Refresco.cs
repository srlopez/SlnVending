namespace Vending.Modelos
{
    public class Refresco : Producto
    {
        public bool Zero { get; set; }

        public Refresco(string Nombre, decimal Precio, bool Zero = false) : base(Nombre, Precio)
        {
            this.Zero = Zero;
            Tipo = ProductoTipo.Refresco;
        }

        public override bool EsDietetico() => Zero == true;
    }
}
