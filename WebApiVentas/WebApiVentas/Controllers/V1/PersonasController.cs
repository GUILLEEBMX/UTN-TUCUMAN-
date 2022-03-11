using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Models;

namespace WebApiVentas.Controllers.V1
{
    [ApiController]
    [Route("api/v1/personas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class PersonasController : CustomBaseController
    {
        private readonly bootcampContext context;

        private readonly IMapper mapper;
        private readonly IAuthorizationService authorizationService;

        public PersonasController(bootcampContext context, IMapper mapper, IAuthorizationService authorizationService): base(context,mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.authorizationService = authorizationService;
        }



        /// <summary>
        /// Obtiene un listado de personas
        /// </summary>
        /// 
        [HttpGet(Name = "ObtenerPersonasv1")]
        [AllowAnonymous]
        public async Task<ActionResult<List<PersonaIdDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            return await Get<Persona, PersonaIdDTO>(paginacionDTO);
        }



        /// <summary>
        /// Obtiene una persona por su ID.
        /// </summary>
        /// 
        [HttpGet("{id}", Name = "ObtenerPersonaXIdv1")]
        [AllowAnonymous]
        public async Task<ActionResult<PersonaDTO>> GetId(int id)
        {
            return await Get<Persona,PersonaDTO>(id);
        }



        /// <summary>
        /// Crea una persona en la BD.
        /// </summary>
        /// 
        [HttpPost(Name = "CrearPersonav1")]
        public async Task<ActionResult> Post(PersonaCreacionDTO personaCreacionDTO)
        {
            return await Post<Persona, PersonaCreacionDTO>(personaCreacionDTO, "ObtenerPersonaXIdv1");
        }



        /// <summary>
        /// Actualiza una persona de la BD.
        /// </summary>
        [HttpPut("{id}", Name = "ActualizarPersonaV1")]
        public async Task<ActionResult> Put(int id, PersonaPutDTO PersonaPutDTO)
        {
            return await Put<Persona, PersonaPutDTO, PersonaIdDTO>(PersonaPutDTO, id, "ObtenerPersonaXIdv1");
        }



        /// <summary>
        /// Borra una persona de la BD.
        /// </summary>
        /// 
        [HttpDelete("int:id", Name = "BorrarPersonav1")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Persona>(id);
        }

    }
}