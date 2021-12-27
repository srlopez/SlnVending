namespace Vending.Modelos
{

    public class ParaFarma : Producto
    {
        public ParaFarma(string nombre, decimal? precio, int cantidad = 0) : base(nombre, precio, cantidad)
        {
        }
    }
}
