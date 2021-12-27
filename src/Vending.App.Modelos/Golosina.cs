namespace Vending.Modelos
{
    public class Golosina : Producto
    {
        public decimal Gramos { get; init;}
        public Golosina(string nombre, decimal? precio, decimal gramos = 50, int cantidad = 0) : base(nombre, precio, cantidad)
        {
            Gramos = gramos;
        }
    }
}
