using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Servicios;

namespace WebApiVentas.Utilidades
{
    public class HATEOASArticulosDTOFilterAttribute : HATEOASFilterAttribute
    {
        private readonly GeneradorEnlacesObjetoDTO<BaseDTO> generadorEnlacesPersonasDTO;

        public HATEOASArticulosDTOFilterAttribute(GeneradorEnlacesObjetoDTO<BaseDTO>
            generadorEnlacesPersonasDTO)
        {
            this.generadorEnlacesPersonasDTO = generadorEnlacesPersonasDTO;
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var debeIncluir = DebeIncluirHATEOAS(context);

            if (!debeIncluir)
            {
                await next();
                return;
            }

            var resultado = context.Result as ObjectResult;
            var articuloDTO = resultado.Value as ArticuloPutDTO;

            if (articuloDTO == null)
            {
                var articuloIdDto = resultado.Value as List<ArticuloIdDTO>
                    ?? throw new ArgumentException("Se esperaba una instancia de articulo o un List<articulos>");


                articuloIdDto.ForEach(async personaIdDTO => await generadorEnlacesPersonasDTO.GenerarEnlaces(personaIdDTO, "Articulo"));

                resultado.Value = articuloIdDto;
            }
            else
            {
                await generadorEnlacesPersonasDTO.GenerarEnlaces(articuloDTO, "Articulo");
            }
            await next();

        }
    }
}
