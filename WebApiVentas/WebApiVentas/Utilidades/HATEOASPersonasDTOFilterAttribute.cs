using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Servicios;

namespace WebApiVentas.Utilidades
{
    public class HATEOASPersonasDTOFilterAttribute : HATEOASFilterAttribute
    {
        private readonly GeneradorEnlacesObjetoDTO<BaseDTO> generadorEnlacesPersonasDTO;

        public HATEOASPersonasDTOFilterAttribute(GeneradorEnlacesObjetoDTO<BaseDTO> generadorEnlacesPersonasDTO)
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
            var personaDTO = resultado.Value as PersonaDTO;

            if (personaDTO == null)
            {
                var personasIdDTO = resultado.Value as List<PersonaIdDTO>
                    ?? throw new ArgumentException("Se esperaba una instancia de personaIdDTO o un List<personsIdDTO>");


                personasIdDTO.ForEach(async personaIdDTO => await generadorEnlacesPersonasDTO.GenerarEnlaces(personaIdDTO, "Persona"));

                resultado.Value = personasIdDTO;
            }
            else
            {
                await generadorEnlacesPersonasDTO.GenerarEnlaces(personaDTO, "Persona");
            }
            await next();

        }
    }
}