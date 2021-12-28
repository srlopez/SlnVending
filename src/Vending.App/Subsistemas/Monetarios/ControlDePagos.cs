using System.Collections.Generic;
using System.Linq;
using System;

namespace Vending.Subsitemas.Monetarios
{
    using Vending.Modelos;
    public class ControlDePagos
    {
        Efectivo _caja;
        decimal _cajon;

        // cantidad máxima de cada moneda en caja
        public int MaxMonedasPorCanal { get; init; }
        // cantidad máxima de monedas admitas en el pago
        public int MaxMonedasPorPago { get; init; }
        public decimal Valor
        {
            get => _caja.Importe + _cajon;
        }

        public ControlDePagos(Efectivo caja = null)
        {
            var config = new AppConfig().Get();

            MaxMonedasPorCanal = config.MaxMonedasPorCanal;
            MaxMonedasPorPago = config.MaxMonedasPorPago;
            ReestablecerCaja(caja);
        }
        public void ReestablecerCaja(Efectivo caja = null)
        {
            _caja = caja ?? new Efectivo(new int[] { 5, 5, 5, 5, 5 });
            _cajon = 0;
        }
        public Efectivo ValidarPago(decimal precio, Efectivo pago)
        {
            var fakeCaja = new Efectivo(_caja);
            var fakeCajon = 0M;
            var cambio = IntegrarImporteYCalcularCambio(pago.Importe - precio, pago, fakeCaja, ref fakeCajon);
            return cambio;
        }
        public Efectivo AplicarPago(decimal precio, Efectivo pago)
        {
            var cambio = IntegrarImporteYCalcularCambio(pago.Importe - precio, pago, _caja, ref _cajon);
            return cambio;
        }

        private Efectivo IntegrarImporteYCalcularCambio(decimal aDevolver, Efectivo importe, Efectivo caja, ref decimal resto)
        {
            // Integrar importe
            for (var i = 0; i < caja.Length; i++)
            {
                caja.Cantidad[i] += importe.Cantidad[i];
                if (caja.Cantidad[i] > MaxMonedasPorCanal)
                {
                    resto += (caja.Cantidad[i] - MaxMonedasPorCanal) * caja.Valor[i];
                    caja.Cantidad[i] = MaxMonedasPorCanal;
                }
            }
            // Calcular Cambio
            var cambio = new Efectivo(new int[] { 0, 0, 0, 0, 0 });
            // i = 1 -> Saltamos las monedas de 2€
            for (int i = 1; i < caja.Length; i++)
            {
                while (aDevolver >= caja.Valor[i] && caja.Cantidad[i] > 0)
                {
                    aDevolver -= caja.Valor[i];
                    cambio.Cantidad[i]++;
                    caja.Cantidad[i]--;
                }
            }
            if (aDevolver != 0) cambio = new Efectivo(new int[] { -1 });
            return cambio;
        }

        public override string ToString() => $"Caja: {_caja} + {_cajon}€";

    }
}
