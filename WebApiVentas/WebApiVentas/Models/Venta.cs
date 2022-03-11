using System;
using System.Collections.Generic;
using WebApiVentas.DTOs;

#nullable disable

namespace WebApiVentas.Models
{
    public partial class Venta: IId
    {
        public Venta()
        {
            DetallesVenta = new HashSet<DetallesVenta>();
        }

        public int Id { get; set; }
        public int Comprador { get; set; }
        public int Vendedor { get; set; }
        public DateTime Fecha { get; set; }
        public string Factura { get; set; }


        public virtual Persona CompradorNavigation { get; set; }
        public virtual Persona VendedorNavigation { get; set; }

        public virtual ICollection<DetallesVenta> DetallesVenta { get; set; }

    }
}
