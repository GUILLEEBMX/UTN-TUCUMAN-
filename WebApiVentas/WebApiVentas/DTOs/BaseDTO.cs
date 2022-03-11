using System.Collections.Generic;

namespace WebApiVentas.DTOs
{
    public abstract class BaseDTO
    {
        public int Id { get; set; }
        public List<DatoHATEOAS> Enlaces { get; set; } = new List<DatoHATEOAS>();
    }
}
