using System;

namespace WebApiVentas.DTOs
{
    public class PersonaDTO : BaseDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Domicilio { get; set; }
        public DateTime? Nacimiento { get; set; }
    }
}

