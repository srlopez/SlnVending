using System.Collections.Generic;
using System.Linq;

namespace Vending.Subsitemas
{
    public class SeguridadValor : ISeguridad
    {
        int _valor;
        public SeguridadValor(int valor)
        {
            _valor = valor;
        }

        public bool EsPinValido(string pin)
        {
            try
            {
                if (pin.Length != 4) return false;
                var sum = pin.ToCharArray().Select(c => int.Parse(c.ToString())).Sum();
                return sum == _valor;
            }
            catch { return false; }
        }

    }
}
