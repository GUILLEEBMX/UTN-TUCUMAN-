using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiVentas.DTOs
{
    public class ArticuloPutDTO: BaseDTO
    {

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public string Codigo { get; set; }
        public string Marca { get; set; }
        public decimal Precio { get; set; }
    }
}
