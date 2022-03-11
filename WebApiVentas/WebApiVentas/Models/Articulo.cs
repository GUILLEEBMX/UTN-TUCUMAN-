using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiVentas.Validations;

#nullable disable

namespace WebApiVentas.Models
{
    public partial class Articulo: IId
    {
        public Articulo()
        {
            DetallesVenta = new HashSet<DetallesVenta>();

        }
        [Display(Name = "Id Articulo")]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [ValidarPrimeraLetraMayusAtributte]
        [StringLength(maximumLength: 30, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public string Codigo { get; set; }
        public string Marca { get; set; }
        public decimal Precio { get; set; }

        public virtual ICollection<DetallesVenta> DetallesVenta { get; set; }

       

    }
}
