using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiVentas.Validations
{
    public interface IVentaValidator
    {
        public Task <List<string>> VentaValidaciones(int? idVenta, int? idVendedor, int? idComprador);
    }
}