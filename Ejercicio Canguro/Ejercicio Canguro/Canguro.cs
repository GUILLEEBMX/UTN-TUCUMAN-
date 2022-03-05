using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_Canguro
{
    class Canguro
    {
        public int Velocidad { get; set; }
        public int Ubicacion { get; set; }

        public int Saltar(int velocidad)
        {
            int posicion_actual = this.Ubicacion += velocidad;
            return posicion_actual;
        }

    }
}
