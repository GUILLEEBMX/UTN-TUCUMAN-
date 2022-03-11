using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Models;
using WebApiVentas.Utilidades;

namespace WebApiVentas.Controllers.V1
{
    public class CustomBaseController: ControllerBase
    {
        private readonly bootcampContext context;
        private readonly IMapper mapper;

        public CustomBaseController(bootcampContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //GET Paginado
        protected async Task<List<TDTO>> Get<TEntidad, TDTO>(PaginacionDTO paginacionDTO) where TEntidad : class, IId
        {
            var queryable = context.Set<TEntidad>().AsQueryable();
            await HttpContext.InsertarParametroPaginacionEnCabecera(queryable);

            var entidad = await queryable.OrderBy(entidad => entidad.Id)
                .Paginar(paginacionDTO).ToListAsync();

            return mapper.Map<List<TDTO>>(entidad);
        }


        //se utiliza protected para que las clases derivadas puedan utilizarlo
        // GET
        protected async Task<List<TDTO>> Get<TEntidad, TDTO>() where TEntidad : class
        {
            var entidades = await context.Set<TEntidad>().AsNoTracking().ToListAsync();// AsNoTracking: tecnicas y querys mas rapidas (ademas ejemplo de como modificar en un solo lugar)
            var dtos = mapper.Map<List<TDTO>>(entidades);
            return dtos;
        }


        // GET X ID
        protected async Task<ActionResult<TDTO>> Get<TEntidad, TDTO>(int id) where TEntidad : class, IId
        {
            var entidad = await context.Set<TEntidad>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (entidad == null) 
            {
                return Ok("El Id ingresado no se encuentra activo en la base de datos");
            }

            return mapper.Map<TDTO>(entidad);
        }


        //POST
        protected async Task<ActionResult> Post<TEntidad, TDTO>
            (TDTO tDTO, string nombreRuta) where TEntidad : class, IId
        {

            var entidad = mapper.Map<TEntidad>(tDTO);

            context.Add(entidad);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult(nombreRuta, new { id = entidad.Id }, entidad);
        }


        //PUT
        protected async Task<ActionResult> Put<TEntidad, TDTO, TDTO2>
            (TDTO tDTO, int id, string nombreRuta) where TEntidad : class, IId
        {
            var entidad = mapper.Map<TEntidad>(tDTO);

            if (entidad.Id != id)
            {
                return BadRequest("El Id ingresado no coincide con el Id de la URL");
            }
            

            var existe = await context.Set<TEntidad>().AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return Ok("El Id ingresado no se encuentra activo en la base de datos");
            }

            var entidadDTO = mapper.Map<TDTO2>(entidad);
            context.Update(entidad);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult(nombreRuta, new { id = entidad.Id }, entidadDTO);
        }


        //DELETE
        protected async Task<ActionResult> Delete<TEntidad>(int id) where TEntidad : class, IId, new()// se utiliza constructor sin parametro asi que se debe avisar con new()
        {
            var existe = await context.Set<TEntidad>().AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return Ok("El Id ingresado no se encuentra activo en la base de datos");
            }

            context.Remove(new TEntidad() { Id = id });
            await context.SaveChangesAsync();
            return Ok("El id " + id + " fue borrado exitosamente");
        }

    }
}
