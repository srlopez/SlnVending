namespace Vending.Modelos
{
    public class Golosina : Producto
    {
        public Golosina(string Nombre, decimal Precio) : base(Nombre, Precio)
        {
            Tipo = ProductoTipo.Golosina;
        }
    }
}
