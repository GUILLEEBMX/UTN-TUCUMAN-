using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.DTOs;

namespace WebApiVentas.Servicios.DetalleServices
{
    public interface IDetalleServices
    {
        public Task<ActionResult<List<DetalleDTO>>> Get(int ventaid);

        public Task<ActionResult<DetalleDTO>> GetxID(int ventaId, int idDetalle);

        public Task<ActionResult> Post(int ventaId, DetalleCreacionDTO detalleCreacionDTO);

        public Task<ActionResult> Put(DetallePutDTO detallePutDTO, int ventaId, int detalleId);

        public Task<ActionResult> Delete(int ventaId, int idDetalle);

    }
}
