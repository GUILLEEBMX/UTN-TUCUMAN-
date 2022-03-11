using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Filtros;
using WebApiVentas.Models;

namespace WebApiVentas.Controllers.V1
{
    [ApiController]
    [Route("api/v1/articulos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    //[CabeceraEstaPresente("x-version", "1")]

    public class ArticulosController : CustomBaseController
    {
        private readonly ILogger<ArticulosController> logger;

        public ArticulosController(bootcampContext context, IMapper mapper,
            ILogger<ArticulosController>logger)
            : base(context,mapper)
        {
            this.logger = logger;
        }



        /// <summary>
        /// Obtiene un listado de artículos.
        /// </summary>
        /// 
        [HttpGet (Name="ObtenerArticulosV1")]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        [AllowAnonymous]
        public async Task<List<ArticuloIdDTO>> Get()
        {
            logger.LogInformation("Estamos obteniendo lista de Articulos.");
            return await Get<Articulo, ArticuloIdDTO>();
        }



        /// <summary>
        /// Obtiene un artículo por su Id.
        /// </summary>
        /// 
        [HttpGet("{id:int}", Name = "ObtenerArticuloXIdV1")]
        [AllowAnonymous]
        public async Task<ActionResult<ArticuloPutDTO>> GetxID(int id)
        {
            return await Get<Articulo, ArticuloPutDTO>(id);
        }



        /// <summary>
        /// Crea un nuevo artículo en la BD.
        /// </summary>
        /// 
        [HttpPost (Name = "CrearArticuloV1")]
        public async Task<ActionResult> Post([FromBody] ArticuloCreacionDTO articulocreacionDTO)
        {
            return await Post<Articulo, ArticuloCreacionDTO>(articulocreacionDTO, "ObtenerArticuloXIdV1");
        }



        /// <summary>
        /// Actualiza un artículo de la BD.
        /// </summary>
        [HttpPut("{id:int}", Name = "ActualizarArticuloV1")]
        public async Task<ActionResult> Put(ArticuloPutDTO articuloDTO, int id)
        {
            return await Put<Articulo, ArticuloPutDTO, ArticuloIdDTO> (articuloDTO, id, "ObtenerArticuloXIdV1");
        }



        /// <summary>
        /// Borra un artículo de la BD.
        /// </summary>
        [HttpDelete("{id:int}", Name ="BorrarArticuloV1")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Articulo> (id);
        }
    }
}
