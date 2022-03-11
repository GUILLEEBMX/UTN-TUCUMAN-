using System;
using System.Collections.Generic;
using WebApiVentas.Models;

namespace WebApiVentas.DTOs
{
    public class VentaCreacionDTO
    {
        public int vendedor { get; set; }
        public int comprador { get; set; }
        public DateTime fecha { get; set; }
        public string factura { get; set; }



    }
}
