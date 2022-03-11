using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Models;

namespace WebApiVentas.Servicios.PersonasServices
{
    public class PersonaContext : IPersonaServices
    {
        private readonly bootcampContext context;
        private readonly IMapper mapper;

        public PersonaContext(bootcampContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ActionResult<List<PersonaIdDTO>>> Get()
        {
            var persona = await context.Personas.ToListAsync();

            return mapper.Map<List<PersonaIdDTO>>(persona);
        }

        public async Task<ActionResult<PersonaDTO>> GetxID (int id)
        {
            var existe = await context.Personas.AnyAsync(personasDB => personasDB.Id == id);

            if(!existe)
            {
                return new NotFoundObjectResult("ESA PERSONA NO EXISTE EN NUESTRA BASE DE DATOS...");
            }

            return mapper.Map<PersonaDTO>(await context.Personas.FirstOrDefaultAsync(personasDB => personasDB.Id == id));

        }

        public async Task<ActionResult> Post (PersonaCreacionDTO personaCreacionDTO)
        {
            var persona = mapper.Map<Persona>(personaCreacionDTO);

            context.Add(persona);

            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObtenerPersonaXIdV2",  new { id = persona.Id },mapper.Map<PersonaIdDTO>(persona));

           
        }

        public async Task<ActionResult> Put (int id, PersonaPutDTO personaPutDTO)
        {
            if (personaPutDTO.Id != id)
            {
              return new BadRequestObjectResult ("LOS IDs INGRESADOS DE LAS PERSONAS NO COINCIDEN");
            }

            var existe = await context.Personas.AnyAsync(personaDB => personaDB.Id == id);

            if (!existe)
            {
                return new NotFoundObjectResult("ESA PERSONA NO EXISTE EN NUESTRA BASE DE DATOS");

            }
           
                var persona = mapper.Map<Persona>(personaPutDTO);

                context.Update(persona);

                await context.SaveChangesAsync();


            var personaDTO = mapper.Map<PersonaIdDTO>(persona);

            return new CreatedAtRouteResult("ObtenerPersonaXIdV2", new { id = persona.Id},personaDTO);

        }

        public async Task<ActionResult> Delete (int id)
        {
            var existe = await context.Personas.AnyAsync(personasDB => personasDB.Id == id);

            if (!existe)
            {
               return new NotFoundObjectResult("ESA PERSONA NO EXISTE EN NUESTRA BASE DE DATOS");

            }
            else
            {
                context.Remove(new Persona() { Id = id });

                await context.SaveChangesAsync();
   
            }

            return new OkObjectResult("LA PERSONA CUYO ID ERA: " + id + " HA SIDO BORRADA DE MANERA CORRECTA ");
        }



    }
}
