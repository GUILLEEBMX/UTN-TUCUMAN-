using System;
using System.Collections.Generic;
using WebApiVentas.Models;

namespace WebApiVentas.DTOs
{
    public class VentaDTO
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string factura { get; set; }

        public virtual PersonaVentaDTO Vendedor { get; set; }
        public virtual PersonaVentaDTO Comprador { get; set; }
        public virtual ICollection<DetalleDTO> DetallesdeVenta { get; set; }
        
        


    }
}
