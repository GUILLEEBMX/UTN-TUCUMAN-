using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Models;

namespace WebApiVentas.Servicios.ArticulosServices
{
    public class ArticuloContext:IArticuloServices
    {
        private readonly bootcampContext context;
        private readonly IMapper mapper;

        public ArticuloContext(bootcampContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ActionResult<List<ArticuloIdDTO>>> Get ()
        {
       
            return mapper.Map<List<ArticuloIdDTO>>(await context.Articulos.ToListAsync());
        }

        public async Task<ActionResult<ArticuloPutDTO>> GetxID (int id)
        {
            var existe = await context.Articulos.AnyAsync(articuloDB => articuloDB.Id == id);

            if (!existe)
            {
                return new NotFoundObjectResult("EL ARTICULO NO EXISTE EN NUESTRA BASE DE DATOS...");
            }

            return mapper.Map<ArticuloPutDTO>(await context.Articulos.FirstOrDefaultAsync(articulosDB => articulosDB.Id == id));
        }

        public async Task<ActionResult> Post (ArticuloCreacionDTO articuloCreacionDTO)
        {
             var articulo_entity = mapper.Map<Articulo>(articuloCreacionDTO);

            context.Add(articulo_entity);

            await context.SaveChangesAsync();

            return new  CreatedAtRouteResult("ObtenerArticuloXIdV2", new { id = articulo_entity.Id }, mapper.Map<ArticuloIdDTO>(articulo_entity));
        }

        public async Task<ActionResult> Put (int id, ArticuloPutDTO articuloPutDTO)
        {
            if (articuloPutDTO.Id != id)
            {
                return new BadRequestObjectResult("LOS IDs INGRESADOS NO COINCIDEN...");
            }

            var existe = await context.Articulos.AnyAsync(articuloDB => articuloDB.Id == id);

            if (!existe)
            {
                return new NotFoundObjectResult("ESE ARTICULO NO EXISTE EN NUESTRA BASE DE DATOS");
            }
          
                var articuloDB = mapper.Map<Articulo>(articuloPutDTO);

                context.Update(articuloDB);

                await context.SaveChangesAsync();

          
            return new CreatedAtRouteResult("ObtenerArticuloXIdV2", new { id = articuloDB.Id }, mapper.Map<ArticuloIdDTO>(articuloDB));

        }

        public async Task<ActionResult> Delete (int id)
        {
            var existe = await context.Articulos.AnyAsync(articuloDB => articuloDB.Id == id);

            if (!existe)
            {
                return new NotFoundObjectResult("EL ID INGRESADO NO COINCIDE CON NINGUN REGISTRO EN NUESTRA BASE DE DATOS...");
            }

            context.Remove(new Articulo ()  { Id = id });

            await context.SaveChangesAsync();

            return new OkObjectResult("EL ID BORRADO FUE:  " + id);

        }
        

    }
}
