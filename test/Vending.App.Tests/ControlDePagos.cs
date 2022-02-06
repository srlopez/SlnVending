using Xunit;
using System;
using System.Collections.Generic;
using Vending.Subsitemas.Economicos;

namespace Vending.Subsitemas.Monetarios
{
    public class ControlDePagosTests
    {

        [Theory]
        [InlineData(19.0, null)]
        [InlineData(2.0, new int[] { 1, 0, 0, 0, 0 })]
        [InlineData(3.0, new int[] { 1, 1, 0, 0, 0 })]
        [InlineData(2.5, new int[] { 1, 0, 1, 0, 0 })]
        public void Inicializacion_New_Control(decimal esperado, int[] value)
        {
            //
            Efectivo efectivo = null;
            if (value != null) efectivo = new Efectivo(value);
            var c = new ControlDePagos(efectivo);
            // "Caja: 19.00€ [5,5,5,5,5] + 0€";
            //
            Assert.Equal(esperado, c.Valor);
        }


        [Fact]
        public void AplicarPago()
        {
            //
            var c = new ControlDePagos();
            //                 "Caja: 19.00€ [5,5,5,5,5] + 0€";

            var pago = new int[] { 2, 2, 2, 2, 2 };// 7,60
            var precio = 3M;
            var cambioEsperado = 4.6M;
            var cajaEsperada = 22.0M;
            var ctrlEsperado = "Caja: 14.40€ [5,1,4,5,4] + 7.6€";
            // 
            var cambioResult = c.AplicarPago(precio, new Efectivo(pago));
            //
            Assert.Equal(cambioEsperado, cambioResult.Importe);
            Assert.Equal(ctrlEsperado, c.ToString());
            Assert.Equal(cajaEsperada, c.Valor);

            //
            pago = new int[] { 1, 1, 1, 1, 1 };// 3.80
            precio = 1.5M;
            cambioEsperado = 2.3M;
            cajaEsperada = 23.5M;
            ctrlEsperado = "Caja: 13.70€ [5,0,5,4,4] + 9.8€";
            //
            cambioResult = c.AplicarPago(precio, new Efectivo(pago));
            //
            Assert.Equal(cambioEsperado, cambioResult.Importe);
            Assert.Equal(ctrlEsperado, c.ToString());
            Assert.Equal(cajaEsperada, c.Valor);
        }

        [Theory]
        [InlineData(3, 7.6, 4.6)]
        [InlineData(0.5, 7.6, 7.1)]
        [InlineData(1.5, 2.6, 1.1)]
        public void AplicarPago_Valido(decimal precio, decimal pago, decimal cambio)
        {
            //
            var ctrl = new ControlDePagos();
            var pagoEfectivo = new Efectivo(pago, 0);
            // 
            var cambioEfectivo = ctrl.AplicarPago(precio, new Efectivo(pago));
            //
            Assert.Equal(cambio, cambioEfectivo.Importe);
        }

    }
}
