using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.DTOs;

namespace WebApiVentas.Servicios.VentasServices
{
    public interface IVentaServices
    {
        public Task<ActionResult<List<VentaGetDTO>>> Get();

        public Task<ActionResult<VentaDTO>> GetxID(int id);

        public Task<ActionResult> Post(VentaCreacionDTO ventaCreacionDTO);

        public Task<ActionResult> Put(int id, VentaPutDTO ventaPutDTO);

        public Task<ActionResult> Delete(int id);
    }
}
