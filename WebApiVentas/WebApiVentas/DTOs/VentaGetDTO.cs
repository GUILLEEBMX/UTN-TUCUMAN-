using System;

namespace WebApiVentas.DTOs
{
    public class VentaGetDTO
    {
        public int id { get; set; }
        //public int comprador { get; set; }
        //public int vendedor { get; set; }
        public DateTime fecha { get; set; }
        public string factura { get; set; }
        public virtual PersonaVentaDTO Vendedor { get; set; }
        public virtual PersonaVentaDTO Comprador { get; set; }

    }
}
