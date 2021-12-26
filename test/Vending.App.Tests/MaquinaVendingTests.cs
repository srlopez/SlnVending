using Xunit;
using Vending;

using Vending.Subsitemas.Monetarios;
namespace Vending
{
    public class MaquinaVendingTests
    {
        [Theory]
        [InlineData(3, 2, PagoTipo.Insuficiente)]
        [InlineData(0.5, 12, PagoTipo.NoAdmintido)]
        [InlineData(0.1, 1, PagoTipo.SinCambios)]
        public void AplicarPago_NoValido(decimal precio, decimal pago, PagoTipo tipoEsperado)
        {
            //
            var ctrl = new ControlDePagos(new Efectivo(new int[] { 1, 1, 0, 1, 1 }));
            var sistema = new MaquinaVending(null, ctrl, null);
            var pagoEfectivo = new Efectivo(pago);
            // 
            var tipoResultante = sistema.ValidarPago(precio, new Efectivo(pago));
            //
            Assert.Equal(tipoEsperado, tipoResultante);
        }
    }
}
