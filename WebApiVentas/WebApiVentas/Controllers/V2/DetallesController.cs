using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Servicios.DetalleServices;

namespace WebApiVentas.Controllers.V2
{
    [ApiController]
    [Route("api/v2/ventas/{ventaId:int}/detalles/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class DetallesController : ControllerBase
    {
        private readonly IDetalleServices detalleServices;
        public DetallesController(IDetalleServices detalleServices)
        {
            this.detalleServices = detalleServices;
        }
        /// <summary>
        /// Obtiene detalles de una venta por su Id.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        
        [HttpGet(Name = "ObtenerDetallesXVentaV2")]
        [AllowAnonymous]
        public async Task<ActionResult<List<DetalleDTO>>> Get (int ventaId)
        {
            return await detalleServices.Get(ventaId);
        }

        /// <summary>
        /// Obtiene un detalle mediante su Id y el Id de venta.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>

        [HttpGet("{idDetalle:int}", Name = "ObtenerDetallexID")]
        [AllowAnonymous]
        public async Task<ActionResult<DetalleDTO>> GetxID(int ventaId, int idDetalle)
        {
            return await detalleServices.GetxID(ventaId, idDetalle);
        }

        /// <summary>
        /// Crea un nuevo detalle según el Id de Venta ingresado.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>

        [HttpPost(Name = "CrearDetalleV2")]
        public async Task<ActionResult> Post(int ventaId, DetalleCreacionDTO detalleCreacionDTO)
        {
            return await detalleServices.Post(ventaId, detalleCreacionDTO);
        }

        /// <summary>
        /// Actualiza un detalle de la BD según el Id ingresado.
        /// </summary>
        /// <remarks>
        /// Actualiza detalle de venta según ID.
        /// </remarks>

        [HttpPut("{detalleId:int}", Name = "ActualizarDetalleV2")] 
        public async Task<ActionResult> Put (DetallePutDTO detallePutDTO, int ventaId, int detalleId)
        {
            return await detalleServices.Put(detallePutDTO,ventaId,detalleId);
        }

        /// <summary>
        /// Borra un detalle de la BD según el Id de venta y de Id de detalle ingresados.
        /// </summary>
        /// <remarks>
        /// Borra el detalle según el ID ingresado.
        /// </remarks>
        
        [HttpDelete("{idDetalle:int}", Name = "BorrarDetalleV2")]
        public async Task<ActionResult> Delete (int ventaId, int idDetalle)
        {
            return await detalleServices.Delete(ventaId, idDetalle);
        }
    }
}
