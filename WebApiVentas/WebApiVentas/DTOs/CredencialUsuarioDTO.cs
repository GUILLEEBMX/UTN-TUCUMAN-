
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiVentas.DTOs
{
    public class CredencialUsuarioDTO
    {
        [EmailAddress]
        public string email { get; set; }

        public string password { get; set; }

        
    }
}
