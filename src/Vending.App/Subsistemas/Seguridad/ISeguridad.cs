using System.Collections.Generic;

namespace Vending.Subsitemas
{
    public interface ISeguridad
    {
        public bool EsPinValido(string pin);
    }
}
