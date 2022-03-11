using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiVentas.DTOs
{
    public class ColeccionDeRecursos <T> : BaseDTO where T : BaseDTO
    {
        public List <T> Valores { get; set; }
    }
}
