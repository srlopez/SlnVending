using Xunit;
using System;
using System.Collections.Generic;
using Vending.Subsitemas.Monetarios;

namespace Vending
{
    public class EfectivoTests
    {

        [Theory]
        [InlineData(false, new int[] { 1 })]
        [InlineData(false, new int[] { 1, 2, 3 })]
        [InlineData(true, new int[] { 1, 2, 3, 4, 5 })]
        [InlineData(false, new int[] { 1, 2, 3, 4, 5, 6 })]
        [InlineData(false, new int[] { 1, 2, 3, -4, 5 })]
        public void Efectivo_Valido_Equal(bool esperado, int[] cantidad)
        {
            //
            var cash = new Efectivo(cantidad);
            Assert.Equal(esperado, cash.Valido);
        }

        [Theory]
        [InlineData(2, new int[] { 1, 0, 0, 0, 0 })]
        [InlineData(3, new int[] { 1, 1, 0, 0, 0 })]
        [InlineData(2.5, new int[] { 1, 0, 1, 0, 0 })]
        [InlineData(2.2, new int[] { 1, 0, 0, 1, 0 })]
        [InlineData(2.1, new int[] { 1, 0, 0, 0, 1 })]
        public void Efectivo_Valor_Equal(decimal esperado, int[] cantidad)
        {
            //
            var cash = new Efectivo(cantidad);
            Assert.Equal(esperado, cash.Importe);
        }
    }
}
