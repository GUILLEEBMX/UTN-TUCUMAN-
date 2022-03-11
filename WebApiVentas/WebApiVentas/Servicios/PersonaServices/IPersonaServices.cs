using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Models;

namespace WebApiVentas.Servicios.PersonasServices
{
    public interface IPersonaServices
    {
        public Task<ActionResult<List<PersonaIdDTO>>> Get();

        public Task<ActionResult<PersonaDTO>> GetxID (int id);

        public Task<ActionResult> Post(PersonaCreacionDTO personaCreacionDTO);

        public Task<ActionResult> Put(int id, PersonaPutDTO personaPutDTO);

        public Task<ActionResult> Delete(int id);

        
        

    }
}
