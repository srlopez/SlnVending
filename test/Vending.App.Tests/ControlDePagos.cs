using Xunit;
using System;
using System.Collections.Generic;
using Vending.Subsitemas.Monetarios;

namespace Vending.Subsitemas.Monetarios
{
    public class ControlDePagosTests
    {

        [Fact]
        public void New_Control()
        {
            //
            var c = new ControlDePagos();
            var esperado = "Caja: 19.00€ [5,5,5,5,5] + 0€";
            //            //
            Assert.Equal(esperado, c.ToString());
        }

        [Fact]
        public void AplicarPago()
        {
            //
            var c = new ControlDePagos();
            var pago = new int[] { 2, 2, 2, 2, 2 };// 7,60
            var precio = 3M;
            var cambioEsperado = 4.6M;
            //                 "Caja: 19.00€ [5,5,5,5,5] + 0€";
            var ctrlEsperado = "Caja: 14.40€ [5,1,4,5,4] + 7.6€";
            // 
            var cambioResult = c.AplicarPago(precio, new Efectivo(pago));
            //
            Assert.Equal(cambioEsperado, cambioResult.Importe);
            Assert.Equal(ctrlEsperado, c.ToString());

            //
            pago = new int[] { 1, 1, 1, 1, 1 };// 3.80
            precio = 1.5M;
            cambioEsperado = 2.3M; 
            ctrlEsperado = "Caja: 13.70€ [5,0,5,4,4] + 9.8€";
            //
            cambioResult = c.AplicarPago(precio, new Efectivo(pago));
            //
            Assert.Equal(cambioEsperado, cambioResult.Importe);
            Assert.Equal(ctrlEsperado, c.ToString());
        }

    }
}
