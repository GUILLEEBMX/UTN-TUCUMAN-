using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Servicios.ArticulosServices;
using WebApiVentas.Utilidades;

namespace WebApiVentas.Controllers.V2
{
    [ApiController]
    [Route("api/V2/articulos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class ArticulosController : ControllerBase
    {
        private readonly IArticuloServices articuloServices;

        public ArticulosController(IArticuloServices articuloServices)
        {
            this.articuloServices = articuloServices;
        }
        //GET: api/articulos
        /// <summary>
        /// Obtiene un listado de artículos.
        /// </summary>
        /// Las propiedades de los artículos estan acotadas a las requeridas por el modelo.
        /// <returns></returns>
        /// 
        [HttpGet(Name = "ObtenerArticulosV2")]
        [AllowAnonymous]
        [ServiceFilter(typeof(HATEOASArticulosDTOFilterAttribute))]
        public async Task<ActionResult<List<ArticuloIdDTO>>> Get ()
        {
            return await articuloServices.Get();
        }

        // GET: api/articulos/1
        /// <summary>
        /// Obtiene un artículo por su Id.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="id">Id de artículo.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado o ID no encontrado.</response>    

        [HttpGet ("int: id", Name = "ObtenerArticuloXIdV2")]
        [AllowAnonymous]
        [ServiceFilter(typeof(HATEOASArticulosDTOFilterAttribute))]
        public async Task<ActionResult<ArticuloPutDTO>> GetxID (int id)
        {
            return await articuloServices.GetxID(id);
        }

        // POST: api/articulos/
        /// <summary>
        /// Crea un nuevo artículo en la BD.
        /// </summary>
  
        [HttpPost(Name = "CrearArticuloV2")]

        public async Task<ActionResult> Post (ArticuloCreacionDTO articuloCreacionDTO)
        {
            return await articuloServices.Post(articuloCreacionDTO);
        }

        // PUT: api/articulos/1
        /// <summary>
        /// Actualiza un artículo de la BD.
        /// </summary>
        /// <remarks>
        /// Actualiza el artículo segun ID
        /// </remarks>
        /// <param name="id">Id de artículo.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Resultado de busqueda ID.</response> 
        /// 
        [HttpPut("{id:int}", Name = "ActualizarArticuloV2")]
        public async Task<ActionResult> Put (int id, ArticuloPutDTO articuloPutDTO)
        {
            return await articuloServices.Put(id, articuloPutDTO);
        }

        // DELETE: api/articulos/1
        /// <summary>
        /// Borra un artículo de la BD.
        /// </summary>
        /// <remarks>
        /// Borra el artículo indicado en el ID
        /// </remarks>
        /// <param name="id">Id de artículo.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Resultado de busqueda ID.</response> 

        [HttpDelete("{id:int}", Name = "BorrarArticuloV2")]
        public async Task<ActionResult> Delete (int id)
        {
            return await articuloServices.Delete(id);

        }

    }
}
