using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Servicios.PersonasServices;
using WebApiVentas.Utilidades;

namespace WebApiVentas.Controllers.V2
{
    [ApiController]
    [Route("api/V2/personas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaServices personaServices;

        public PersonasController(IPersonaServices personaServices)
        {
            this.personaServices = personaServices;
        }

        //GET: api/personas
        /// <summary>
        /// Obtiene un listado de personas
        /// </summary>
        /// Las propiedades de las personas estan acotadas a las requeridas por el modelo
        /// <returns></returns>

        [HttpGet(Name = "ObtenerPersonasV2")]
        [AllowAnonymous]
        [ServiceFilter(typeof(HATEOASPersonasDTOFilterAttribute))]

        public async Task<ActionResult<List<PersonaIdDTO>>> Get()
        {
          return await personaServices.Get();

        }

        /// <summary>
        /// Obtiene una persona por su ID.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id de persona.</param>

        [HttpGet("{id}", Name = "ObtenerPersonaXIdV2")]
        [AllowAnonymous]
        [ServiceFilter(typeof(HATEOASPersonasDTOFilterAttribute))]
        public async Task<ActionResult<PersonaDTO>> GetxID(int id)
        {
            return await personaServices.GetxID(id);
        }

        /// <summary>
        /// Crea una persona en la BD.
        /// </summary>
        /// <remarks>
        /// Crea una persona en la BD.
        /// </remarks>

        [HttpPost(Name = "CrearPersonaV2")]
        public async Task<ActionResult<PersonaDTO>> Post (PersonaCreacionDTO personaCreacionDTO)
        {
            
            return await personaServices.Post(personaCreacionDTO);
        }


        /// <summary>
        /// Actualiza una persona de la BD.
        /// </summary>
        /// <remarks>
        /// Actualiza la persona segun ID
        /// </remarks>
        /// <param name="id">Id de persona.</param>


        [HttpPut("{id}", Name = "ActualizarPersonaV2")]
        public async Task<ActionResult> Put (int id, PersonaPutDTO personaPutDTO)
        {
         
            return await personaServices.Put(id, personaPutDTO);

        }

        /// <summary>
        /// Borra una persona de la BD.
        /// </summary>
        /// <remarks>
        /// Borra una persona según el ID indicado.
        /// </remarks>

        [HttpDelete("{id}", Name = "BorrarPersonaV2")]
        public async Task<ActionResult> Delete (int id)
        {
            return await personaServices.Delete(id);
        }
    }
}
