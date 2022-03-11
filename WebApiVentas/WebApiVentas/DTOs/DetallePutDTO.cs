namespace WebApiVentas.DTOs
{
    public class DetallePutDTO
    {
        public int IdVenta { get; set; }
        public int IdDetalle { get; set; }
        public int Articulo { get; set; }
        public int Cantidad { get; set; }
    }
}
