
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.Validations;

namespace WebApiVentas.DTOs
{
    public class PersonaCreacionDTO
    {
        [Required(ErrorMessage = "Debe ingresar su Nombre")]
        [ValidarPrimeraLetraMayusAtributte]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar su apellido")] 
        [ValidarPrimeraLetraMayusAtributte]
        public string Apellido { get; set; }

        public int? Dni { get; set; }// null tip Mayco
        public string Telefono { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Domicilio { get; set; }
        public DateTime? Nacimiento { get; set; }
      
    }
}
