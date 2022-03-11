using System;
using System.Collections.Generic;
using WebApiVentas.DTOs;

#nullable disable

namespace WebApiVentas.Models
{
    public partial class DetallesVenta: IId
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public int Articulo { get; set; }
        public int Cantidad { get; set; }

        public virtual Articulo ArticuloNavigation { get; set; }
        public virtual Venta IdVentaNavigation { get; set; }

    }
}
