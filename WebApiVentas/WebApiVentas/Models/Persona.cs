using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiVentas.Models
{
    public partial class Persona: IId
    {
        public Persona()
        {
            VentaCompradorNavigations = new HashSet<Venta>();
            VentaVendedorNavigations = new HashSet<Venta>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Domicilio { get; set; }
        public DateTime? Nacimiento { get; set; }

        public virtual ICollection<Venta> VentaCompradorNavigations { get; set; }
        public virtual ICollection<Venta> VentaVendedorNavigations { get; set; }
    }
}
