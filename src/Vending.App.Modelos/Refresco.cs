namespace Vending.Modelos
{
    public class Refresco : Producto
    {
        public decimal Centilitros { get; init;}

        public Refresco(string nombre, decimal precio, decimal centilitros = 100, int cantidad = 0) : base(nombre, precio, cantidad)
        {
            Centilitros = centilitros;
        }

    }
}
