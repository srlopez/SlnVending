using System;
using System.Collections.Generic;
using System.Linq;

namespace Vending.Subsitemas.Monetarios
{
    public class Efectivo
    {
        // ESTATICOS
        internal decimal[] Valor = { 2, 1, .5M, .2M, .1M };
        // INTERNOS
        internal int[] Cantidad = { 0, 0, 0, 0, 0 };
        public int Length { get => Valor.Length; }
        public decimal Importe
        {
            get => Cantidad.ToList().Select((v, i) => v * Valor[i]).Sum();
        }
        // Evitamos un Excepción indicando Valido=false;
        public bool Valido { get; } = false;

        public Efectivo(){
        }
        
        public Efectivo(int[] cantidad)
        {
            Valido = false;
            if (cantidad.Length != Length) return;
            if (cantidad.Any(i => i < 0)) return;
            cantidad.CopyTo(Cantidad, 0);
            Valido = true;
        }

        public Efectivo(Efectivo efectivo)
        {
            efectivo.Cantidad.CopyTo(Cantidad, 0);
            Valido = efectivo.Valido;
        }

        public Efectivo(decimal importe, int idx = 1)
        {
            // Este método lo uso en los tests
            var cambio = new Efectivo(new int[] { 0, 0, 0, 0, 0 });
            // idx = 1 salta monedas de 2€
            // idx = 0 usa monedas de 2€
            for (int i = idx; i < Length; i++)
            {
                while (importe >= Valor[i])
                {
                    importe -= Valor[i];
                    cambio.Cantidad[i]++;
                }
            }
            cambio.Cantidad.CopyTo(Cantidad, 0);
            Valido = true;
        }

        public override string ToString() => $"{Importe:#.00}€ [" + string.Join(",", Cantidad) + "]";
    }
}
