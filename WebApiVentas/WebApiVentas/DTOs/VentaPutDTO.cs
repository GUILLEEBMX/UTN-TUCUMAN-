using System;

namespace WebApiVentas.DTOs
{
    public class VentaPutDTO
    {
        public int IdVenta { get; set; }
        public int Vendedor { get; set; }
        public int Comprador { get; set; }
        public DateTime Fecha { get; set; }
        public string Factura { get; set; }
    }
}
