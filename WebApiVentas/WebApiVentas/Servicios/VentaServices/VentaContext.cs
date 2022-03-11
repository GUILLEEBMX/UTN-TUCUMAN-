using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Models;

namespace WebApiVentas.Servicios.VentasServices
{
    public class VentaContext : IVentaServices
    {
        private readonly bootcampContext context;
        private readonly IMapper mapper;
        public VentaContext(bootcampContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ActionResult<List<VentaGetDTO>>> Get ()
        {
                 var ventas = await context.Ventas
                .Include(vendedorDB => vendedorDB.VendedorNavigation)
                .Include(compradorDB => compradorDB.CompradorNavigation)
                .ToListAsync();

            return mapper.Map<List<VentaGetDTO>>(ventas);
        }

        public async Task<ActionResult<VentaDTO>> GetxID(int id)
        {
            var existe = await context.Ventas.AnyAsync(ventasDB => ventasDB.Id == id);

            if (!existe)
            {
                return new NotFoundObjectResult("ESA VENTA NO EXISTE EN NUESTRA BASE DE DATOS...");
            }

            var venta = await context.Ventas
                .Include(vendedorDB => vendedorDB.VendedorNavigation)
                .Include(compradorDB => compradorDB.CompradorNavigation)
                .Include(detallesDB => detallesDB.DetallesVenta)
                .ThenInclude(articulosDB => articulosDB.ArticuloNavigation)
                .FirstOrDefaultAsync(ventaDB => ventaDB.Id == id);

            return mapper.Map<VentaDTO>(venta);
        }

        public async Task<ActionResult> Post (VentaCreacionDTO ventaCreacionDTO)
        {
            var existe_vendedor = await context.Personas.AnyAsync(vendedorDB => vendedorDB.Id == ventaCreacionDTO.vendedor);
            
            if (!existe_vendedor)
            {
                return new NotFoundObjectResult("ESE VENDEDOR NO EXISTE EN NUESTRA BASE DE DATOS...");
            }

            var existe_comprador = await context.Personas.AnyAsync(compradorDB => compradorDB.Id == ventaCreacionDTO.comprador);
            
            if(!existe_comprador)
            {
                return new NotFoundObjectResult("ESE COMPRADOR NO EXISTE EN NUESTRA BASE DE DATOS...");
            }



            var venta = mapper.Map<Venta>(ventaCreacionDTO);
            venta.Fecha = DateTime.Now;

            context.Add(venta);

            await context.SaveChangesAsync();

            var ventaDTO = mapper.Map<VentaIdDTO>(venta);

            return new CreatedAtRouteResult("ObtenerVentaXIdV2", new { id = venta.Id }, ventaDTO);
        }

        public async Task<ActionResult> Put (int id, VentaPutDTO ventaPutDTO)
        {
           if (ventaPutDTO.IdVenta != id)
            {
                return new BadRequestObjectResult("LOS IDs INGRESADOS NO COINCIDEN...");
            }

            var existe = await context.Ventas.AnyAsync(ventasDB => ventasDB.Id == id);

            if(!existe)
            {
                return new NotFoundObjectResult("ESA VENTA NO EXISTE EN NUESTRA BASE DE DATOS...");
            }

            var existe_vendedor = await context.Personas.AnyAsync(vendedorDB => vendedorDB.Id == ventaPutDTO.Vendedor);

            if (!existe_vendedor)
            {
                return new NotFoundObjectResult("ESE VENDEDOR NO EXISTE EN NUESTRA BASE DE DATOS...");
            }

            var existe_comprador = await context.Personas.AnyAsync(compradorDB => compradorDB.Id == ventaPutDTO.Comprador);

            if (!existe_comprador)
            {
                return new NotFoundObjectResult("ESE COMPRADOR NO EXISTE EN NUESTRA BASE DE DATOS...");
            }


            var venta = mapper.Map<Venta>(ventaPutDTO);

            context.Update(venta);

            await context.SaveChangesAsync();

            var ventaDTO = mapper.Map<VentaIdDTO>(venta);

            return new CreatedAtRouteResult("ObtenerVentaXIdV2", new { id = venta.Id }, ventaDTO);

        }


        public async Task<ActionResult> Delete (int id)
        {
            var existe = await context.Ventas.AnyAsync(ventasDB => ventasDB.Id == id);

            if (!existe)
            {
                return new NotFoundObjectResult("ESA VENTA NO EXISTE EN NUESTRA BASE DE DATOS...");
            }

            context.Remove(new Venta() { Id = id });

            await context.SaveChangesAsync();

            return new OkObjectResult("EL ID " + id + " FUE BORRADO DE MANERA EXITOSA...");

        }


    }
}
