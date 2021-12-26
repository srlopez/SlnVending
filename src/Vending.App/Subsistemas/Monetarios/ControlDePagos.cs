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
        public int MAX_MONEDAS { get; init; }
        // cantidad máxima de monedas admitas en el pago
        public int MAX_PAGO_MONEDAS { get; init; }
        decimal Valor
        {
            get => _caja.Importe + _cajon;
        }

        public ControlDePagos()
        {
            MAX_MONEDAS = Configuracion.MAX_MONEDAS;
            MAX_PAGO_MONEDAS = Configuracion.MAX_PAGO_MONEDAS;
            ReestablecerCaja();
        }
        public void ReestablecerCaja()
        {
            _caja = new Efectivo(new int[] { 5, 5, 5, 5, 5 });
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
                if (caja.Cantidad[i] > MAX_MONEDAS)
                {
                    resto += (caja.Cantidad[i] - MAX_MONEDAS) * caja.Valor[i];
                    caja.Cantidad[i] = MAX_MONEDAS;
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
