using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiVentas.DTOs;

namespace WebApiVentas.Controllers.v2
{
    [ApiController]
    [Route("api")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class RootController: ControllerBase
    {
        private readonly IAuthorizationService authorizationService;

        public RootController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        [HttpGet(Name ="ObtenerRoot")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DatoHATEOAS>>> Get()
        {
            var datoHateoas = new List<DatoHATEOAS>();

            var esAdmin = await authorizationService.AuthorizeAsync(User, "esAdmin");

            datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("ObtenerRoot", new { }), 
                descripcion: "Root", metodo: "GET"));
            
            datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("ObtenerVentasV2", new { }),
                descripcion: "Ventas", metodo: "GET"));

            datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("ObtenerArticulosV2", new { }),
                descripcion: "Articulos", metodo: "GET"));

            datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("ObtenerPersonasV2", new { }),
                descripcion: "Personas", metodo: "GET"));

            //datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("ObtenerDetallesXVentaV1", new { }),
           // descripcion: "Detalles", metodo: "GET"));


            if (esAdmin.Succeeded)
            {
                datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("CrearVentaV2", new { }),
                    descripcion: "Ventas-Crear", metodo: "POST"));

                datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("CrearArticuloV2", new { }),
                    descripcion: "Articulos-Crear", metodo: "POST"));

                datoHateoas.Add(new DatoHATEOAS(enlace: Url.Link("CrearPersonaV2", new { }),
                    descripcion: "Personas-Crear", metodo: "POST"));

            }

            return datoHateoas;
        }

    }
}
