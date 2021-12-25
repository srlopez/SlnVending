namespace Vending.Modelos
{
    
    public class ParaFarma: Producto
    {
        public ParaFarma(string Nombre, decimal Precio) : base( Nombre, Precio)
        {
            Tipo = ProductoTipo.Farma;
        }
    }
}
