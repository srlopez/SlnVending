namespace Vending.Modelos
{
    public class RefrescoDietetico : Refresco
    {

        public RefrescoDietetico(string nombre, decimal precio, decimal centilitros = 50, int cantidad = 0) : base(nombre, precio, centilitros, cantidad)
        {
        }

    }
}
