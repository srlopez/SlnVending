using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Vending.Data
{
    using Vending.Modelos;

    public interface IRepoDispensador
    {
        void Inicializar();
        Producto[,] Cargar();
        void Guardar(Producto[,] parrilla);
    }
}