using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Models;

namespace WebApiVentas.Servicios.DetalleServices
{
    public class DetalleContext : IDetalleServices
    {
        private readonly bootcampContext context;
        private readonly IMapper mapper;
        public DetalleContext(bootcampContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }

        public async Task<ActionResult<List<DetalleDTO>>> Get (int ventaid)
        {
            var existe_venta = await context.Ventas.AnyAsync(ventasDB => ventasDB.Id == ventaid);

            if (!existe_venta)
            {
                return new NotFoundObjectResult("Esa venta no existe en nuestra base da datos...");
            }

            var detalle = await context.DetallesVentas
                .Where(ventaDB => ventaDB.IdVenta == ventaid)
                .Include(detalleDB => detalleDB.ArticuloNavigation)
                .ToListAsync();

            return mapper.Map <List<DetalleDTO>>(detalle);

        }

        public async Task<ActionResult<DetalleDTO>> GetxID (int ventaId, int idDetalle)
        {
            var existe_venta = await context.Ventas.AnyAsync(ventaDB => ventaDB.Id == ventaId);

            if (!existe_venta)
            {
                return new NotFoundObjectResult("Esa venta no existe en nuestra base da datos...");
            }

            var existe_detalle = await context.DetallesVentas.AnyAsync(detalleDB => detalleDB.Id == idDetalle);

            if(!existe_detalle)
            {
                return new NotFoundObjectResult("Ese detalle no existe en nuestra base de datos...");
            }

            var detalle = await context.DetallesVentas
                .Where(ventaDB => ventaDB.IdVenta == ventaId)
                .Where(detalleDB => detalleDB.Id == idDetalle)
                .Include(vendedorDB => vendedorDB.ArticuloNavigation)
                .FirstOrDefaultAsync();

            return mapper.Map<DetalleDTO>(detalle);
        }

        public async Task<ActionResult> Post(int ventaId, DetalleCreacionDTO detalleCreacionDTO)
        {
            if (detalleCreacionDTO.IdVenta != ventaId)
            {
                return new BadRequestObjectResult("Los ids ingresados no coinciden ..");
            }

            var existe_venta = await context.DetallesVentas.AnyAsync(ventasDB => ventasDB.IdVenta == ventaId);

            if (!existe_venta)
            {
                return new NotFoundObjectResult("Esa venta no existe en nuestra base de datos...");
            }

            var existe_articulo = await context.Articulos.AnyAsync(articuloDB => articuloDB.Id == detalleCreacionDTO.Articulo);

            if (!existe_articulo)
            {
                return new NotFoundObjectResult("ESE ARTICULO NO EXISTE EN NUESTRA BASE DE DATOS...");
            }

            var detalle = mapper.Map<DetallesVenta>(detalleCreacionDTO);

            context.Add(detalle);
            await context.SaveChangesAsync();

            var detalleDTO = mapper.Map<DetalleIdDTO>(detalle);

            return new CreatedAtRouteResult("ObtenerDetallexID", new { ventaId = ventaId, idDetalle = detalle.Id }, detalleDTO);

        }

        public async Task<ActionResult> Put(DetallePutDTO detallePutDTO, int ventaId, int detalleId)
        {
            if (detallePutDTO.IdDetalle != detalleId)
            {
                return new BadRequestObjectResult("LOS IDs DE LOS DETALLES INGRESADOS NO COINCIDEN...");
            }

            if(detallePutDTO.IdVenta != ventaId)
            {
                return new BadRequestObjectResult("LOS IDs DE LAS VENTAS INGRESADAS NO COINCIDEN...");
            }

            var existeDetalle = await context.DetallesVentas.
                                Where(ventaDB => ventaDB.IdVenta == ventaId)
                               .Where(detalleDB => detalleDB.Id == detalleId)
                               .AnyAsync();

            if (!existeDetalle)
            {
                return new NotFoundObjectResult("ESE DETALLE NO EXISTE EN NUESTRA BASE DE DATOS...");
            }

            var existeVenta = await context.Ventas.AnyAsync(x => x.Id == ventaId);

            if (!existeVenta)
            {
                return new NotFoundObjectResult("ESA VENTA NO EXISTE EN NUESTRA BASE DE DATOS...");
            }


            var existe_articulo = await context.Articulos.AnyAsync(articuloDB => articuloDB.Id == detallePutDTO.Articulo);

            if (!existe_articulo)
            {
                return new NotFoundObjectResult("ESE ARTICULO NO EXISTE EN NUESTRA BASE DE DATOS...");
            }

            var detalle = mapper.Map<DetallesVenta>(detallePutDTO);

            context.Update(detalle);

            await context.SaveChangesAsync();

            var detalleDTO = mapper.Map<DetalleIdDTO>(detalle);

            return new CreatedAtRouteResult("ObtenerDetallexID", new { ventaId = ventaId, idDetalle = detalle.Id }, detalleDTO);

        }

        public async Task<ActionResult> Delete(int ventaId, int idDetalle)
        {
            var existeVenta = await context.Ventas.AnyAsync(x => x.Id == ventaId);

            if (!existeVenta)
            {
                return new BadRequestObjectResult("El id ingresado no corresponde a una venta activa.");
            }

            var existeDetalle = await context.DetallesVentas.
                              Where(ventaDB => ventaDB.IdVenta == ventaId)
                             .Where(detalleDB => detalleDB.Id == idDetalle)
                             .AnyAsync();

            if (!existeDetalle)
            {
                return new BadRequestObjectResult("El id ingresado no corresponde a un detalle activo.");
            }

            context.Remove(new DetallesVenta() { Id = idDetalle });
            await context.SaveChangesAsync();
            return new OkObjectResult("Detalle con Id nro. " + idDetalle + " eliminado correctamente.");

           
        }

       
    }
}
