using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Threading.Tasks;
using WebApiVentas.DTOs;

namespace WebApiVentas.Servicios
{
    public class GeneradorEnlacesObjetoDTO<TObjetoDTO> : GeneradorEnlacesBase
        where TObjetoDTO : BaseDTO
    {
        public GeneradorEnlacesObjetoDTO(
            IAuthorizationService authorizationService, 
            IHttpContextAccessor httpContextAccessor, 
            IActionContextAccessor actionContextAccessor) : 
            base(authorizationService, httpContextAccessor, actionContextAccessor)
        {
        }

        public async Task GenerarEnlaces(TObjetoDTO objetoDTO, string nameRoute)
        {
            
            var esAdmin = await EsAdmin();
            var Url = ConstruirURLHelper();

            objetoDTO.Enlaces.Add(new DatoHATEOAS(
                enlace: Url.Link("Obtener" + nameRoute+ "XIdV2", new { id = objetoDTO.Id }),
                descripcion: "OBTENER "+ nameRoute,
                metodo: "GET"));

            if (esAdmin)
            {
                objetoDTO.Enlaces.Add(new DatoHATEOAS(
                    enlace: Url.Link("Obtener" + nameRoute + "XIdV2", new { id = objetoDTO.Id }),
                    descripcion: "ACTUALIZAR " + nameRoute,
                    metodo: "PUT"));

                objetoDTO.Enlaces.Add(new DatoHATEOAS(
                    enlace: Url.Link("Obtener" + nameRoute + "XIdV2", new { id = objetoDTO.Id }),
                    descripcion: "BORRAR " + nameRoute,
                    metodo: "DELETE"));
            }
        }
    }
}
