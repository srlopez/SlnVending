using System.Collections.Generic;

namespace Vending.Subsitemas
{
    public class SeguridadPin : ISeguridad
    {
        List<string> _pins;
        public SeguridadPin(List<string> pins)
        {
            _pins = pins;
        }

        public bool EsPinValido(string pin) => _pins.Contains(pin);

    }
}
