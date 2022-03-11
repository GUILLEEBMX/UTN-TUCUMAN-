using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Models;

namespace WebApiVentas.Controllers.V1
{
    [ApiController]
    [Route("api/v1/ventas/{ventaId:int}/detalles/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    //[Route("api/{ventaId:int}/detalles/")]
    //[CabeceraEstaPresente("x-version", "1")]


    public class DetallesController : CustomBaseController
    {
        private readonly bootcampContext context;
        private readonly IMapper mapper;

        public DetallesController(bootcampContext context, IMapper mapper): base(context,mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }



        /// <summary>
        /// Obtiene detalles de una venta por su Id.
        /// </summary>
        /// 
        [HttpGet(Name = "ObtenerDetallesXVentaV1")]
        [AllowAnonymous]
        public async Task<ActionResult<List<DetalleDTO>>> Get(int ventaId)
        {
            var existeVenta = await context.Ventas.AnyAsync(ventaDB => ventaDB.Id == ventaId);

            if (!existeVenta)
            {
                return Ok("La venta solicitada es inexistente.");
            }

            //no generico por where??
            var detalles = await context.DetallesVentas.
                Where(detalleDB => detalleDB.IdVenta == ventaId)
                .Include(articuloDB => articuloDB.ArticuloNavigation)
                .ToListAsync();

            return mapper.Map<List<DetalleDTO>>(detalles);
        }



        /// <summary>
        /// Obtiene un detalle mediante su Id y el Id de venta.
        /// </summary>
        /// 
        [HttpGet("{idDetalle:int}", Name = "ObtenerDetalleV1")]
        [AllowAnonymous]
        public async Task<ActionResult<DetalleDTO>> GetxID(int ventaId, int idDetalle)
        {
            //NO GENERICO
            var existeVenta = await context.Ventas.AnyAsync(ventasDB => ventasDB.Id == ventaId);

            if (!existeVenta)
            {
                return NotFound("El id ingresado no corresponde a una venta activa.");
            }

            var existeDetalle = await context.DetallesVentas.
                               Where(ventaDB => ventaDB.IdVenta == ventaId)
                              .Where(detalleDB => detalleDB.Id == idDetalle)
                              .AnyAsync();

            if (!existeDetalle)
            {
                return NotFound("El id ingresado no corresponde a un detalle de la venta.");
            }

            var detalles = await context.DetallesVentas.
                Where(ventaDB => ventaDB.IdVenta == ventaId)
                .Where(detalleDB => detalleDB.Id == idDetalle)
                .Include(articuloDB => articuloDB.ArticuloNavigation)
                .FirstOrDefaultAsync();



            return mapper.Map<DetalleDTO>(detalles);
        }



        /// <summary>
        /// Crea un nuevo detalle según el Id de Venta ingresado.
        /// </summary>
        /// 
        [HttpPost (Name = "CrearDetalleV1")]
        public async Task<ActionResult> Post(DetalleCreacionDTO detalleCreacionDTO, int ventaId)
        {
            if (detalleCreacionDTO.IdVenta != ventaId)
            {
                return BadRequest("El id ingresado no corresponde a una venta activa");
            }

            var existeVenta = await context.Ventas.AnyAsync(ventaDB => ventaDB.Id == ventaId);

            if (!existeVenta)
            {
                return NotFound("El id ingresado no corresponde a una venta activa");
            }

            var existe_articulo = await context.Articulos.AnyAsync(articulosDB => articulosDB.Id == detalleCreacionDTO.Articulo);

            if(!existe_articulo)
            {
                return NotFound("El id de articulo ingresado no se encuentra activo");
            }

            //se puede realizar generico a partir de aca?  ID detalle para crear ruta
            var detalle = mapper.Map<DetallesVenta>(detalleCreacionDTO);
            context.Add(detalle);
            await context.SaveChangesAsync();

            var detalleDTO = mapper.Map<DetalleIdDTO>(detalle);

            return CreatedAtRoute("ObtenerDetalleV1", new { ventaId = ventaId, idDetalle = detalle.Id },detalleDTO);
        }



        /// <summary>
        /// Actualiza un detalle de la BD según el Id ingresado.
        /// </summary>
        /// 
        [HttpPut ("{detalleId:int}", Name = "ActualizarDetalleV1")]
        public async Task<ActionResult> Put(DetallePutDTO detallePutDTO, int ventaId, int detalleId)
        {
            if (detallePutDTO.IdDetalle != detalleId)
            {
                return BadRequest("El id de detalle ingresado no coincide con el id de la URL");
            }

            if (detallePutDTO.IdVenta != ventaId)
            {
                return BadRequest("El id de venta ingresado no coincide con el id de la URL");
            }

            var existeDetalle = await context.DetallesVentas.
                                Where(ventaDB => ventaDB.IdVenta == ventaId)
                               .Where(detalleDB => detalleDB.Id == detalleId)
                               .AnyAsync();

            if (!existeDetalle)
            {
                return NotFound("El id ingresado no corresponde a un detalle activo.");
            }

            var existeVenta = await context.DetallesVentas.AnyAsync(ventasDB => ventasDB.IdVenta == ventaId);

            if (!existeVenta)
            {
                return NotFound("El id ingresado no corresponde a una venta activa.");
            }

            var existe_articulo = await context.Articulos.AnyAsync(articulosDB => articulosDB.Id == detallePutDTO.Articulo);

            if (!existe_articulo)
            {
                return NotFound("El id de articulo ingresado no se encuentra activo");
            }

            //se puede realizar generico a partir de aca?  ID detalle para crear ruta

            var detalle = mapper.Map<DetallesVenta>(detallePutDTO);

            context.Update(detalle);
            await context.SaveChangesAsync();

            var detalleDTO = mapper.Map<DetalleIdDTO>(detalle);

            return CreatedAtRoute("ObtenerDetalleV1", new { ventaId = ventaId, idDetalle = detalle.Id }, detalleDTO);
        }



        /// <summary>
        /// Borra un detalle de la BD según el Id de venta y de Id de detalle ingresados.
        /// </summary>
        /// 
        [HttpDelete ("{idDetalle:int}", Name = "BorrarDetalleV1")]
        public async Task<ActionResult> Delete(int ventaId, int idDetalle)
        {
            //NO GENERICO
            var existeVenta = await context.Ventas.AnyAsync(x => x.Id == ventaId);

            if (!existeVenta)
            {
                return NotFound("El id ingresado no corresponde a una venta activa");
            }

            var existeDetalle = await context.DetallesVentas.
                              Where(ventaDB => ventaDB.IdVenta == ventaId)
                             .Where(detalleDB => detalleDB.Id == idDetalle)
                             .AnyAsync();

            if (!existeDetalle)
            {
                return NotFound("El id ingresado no corresponde a un detalle activo");
            }

            context.Remove(new DetallesVenta() { Id = idDetalle });
            await context.SaveChangesAsync();
            return Ok("Detalle con Id nro. " +idDetalle+" eliminado correctamente.");
        }
    
    }

 }
