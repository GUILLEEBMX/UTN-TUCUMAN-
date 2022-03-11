using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Servicios.VentasServices;

namespace WebApiVentas.Controllers.V2
{
    [Route("api/V2/ventas")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaServices ventaService;
        public VentasController(IVentaServices ventaService)
        {
            this.ventaService = ventaService;
        }
        /// <summary>
        /// Obtiene un listado de ventas.
        /// </summary>
        /// Las propiedades de las ventas estan acotadas a las requeridas por el modelo.
        /// <returns></returns>

        [HttpGet(Name = "ObtenerVentasV2")]
        [AllowAnonymous]
        public async Task<ActionResult<List<VentaGetDTO>>> Get()
        {
            return await ventaService.Get();
        }

        /// <summary>
        /// Obtiene una venta por su Id.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// 
        [HttpGet("int:id", Name = "ObtenerVentaXIdV2")]
        [AllowAnonymous]
        public async Task<ActionResult<VentaDTO>> GetxID(int id)
        {
            return await ventaService.GetxID(id);
        }
        /// <summary>
        /// Crea una venta en la BD.
        /// </summary>
        /// <remarks>
        /// Se solicitan los campos requeridos por el modelo.
        /// </remarks>           
        /// 
        [HttpPost(Name = "CrearVentaV2")]
        public async Task <ActionResult> Post (VentaCreacionDTO ventaCreacionDTO)
        {
            return await ventaService.Post(ventaCreacionDTO);
        }
        /// <summary>
        /// Actualiza una venta de la BD según el Id ingresado.
        /// </summary>
        /// <remarks>
        /// Actualiza una venta según ID ingresado.
        /// </remarks>
        [HttpPut("{id:int}", Name = "ActualizarVentaV2")]
        public async Task<ActionResult> Put (int id, VentaPutDTO ventaPutDTO)
        {
            return await ventaService.Put(id, ventaPutDTO);
        }
        /// <summary>
        /// Borra una venta de la BD según el Id ingresado.
        /// </summary>
        /// <remarks>
        /// Borra una venta según el ID ingresado.
        /// </remarks>
        [HttpDelete("{id:int}", Name = "BorrarVentaV2")]
        public async Task<ActionResult> Delete (int id)
        {
            return await ventaService.Delete(id);
        }

       



    }
}
