using Xunit;
using System;
using System.Collections.Generic;
using Vending.Subsitemas.Monetarios;

namespace Vending
{
    public class EfectivoTests
    {

        [Theory]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 })]
        [InlineData(new int[] { 1, 2, 3, -4, 5 })]
        public void Efectivo_Invalido( int[] cantidad)
        {
            //
            var cash = new Efectivo(cantidad);
            Assert.Equal(false, cash.Valido);
        }

        [Theory]
        [InlineData(2, new int[] { 1, 0, 0, 0, 0 })]
        [InlineData(3, new int[] { 1, 1, 0, 0, 0 })]
        [InlineData(2.5, new int[] { 1, 0, 1, 0, 0 })]
        [InlineData(2.2, new int[] { 1, 0, 0, 1, 0 })]
        [InlineData(2.1, new int[] { 1, 0, 0, 0, 1 })]
        public void Valor_De_Efectivo_De_Array(decimal esperado, int[] cantidad)
        {
            //
            var cash = new Efectivo(cantidad);
            Assert.Equal(esperado, cash.Importe);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3.10)]
        [InlineData(4.20)]
        [InlineData(6.30)]
        [InlineData(7.50)]
        [InlineData(8.60)]
        public void Valor_De_Efectivo_De_Decimal(decimal esperado)
        {
            //
            var cash = new Efectivo(esperado);
            Assert.Equal(esperado, cash.Importe);
        }
    }
}
