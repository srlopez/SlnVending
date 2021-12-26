namespace Vending.Modelos
{
    public class Golosina : Producto
    {
        decimal Gramos { get; init;}
        public Golosina(string Nombre, decimal Precio, decimal gramos = 50) : base(Nombre, Precio)
        {
            Gramos = gramos;
        }
    }
}
