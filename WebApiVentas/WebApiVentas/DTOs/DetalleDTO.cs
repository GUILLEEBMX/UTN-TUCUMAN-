using WebApiVentas.Models;

namespace WebApiVentas.DTOs
{
    public class DetalleDTO
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public virtual DetalleArticuloDTO Articulos { get; set; }
    }
}
