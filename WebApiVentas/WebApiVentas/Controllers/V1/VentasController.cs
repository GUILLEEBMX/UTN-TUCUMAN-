using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Models;
using WebApiVentas.Utilidades;
using WebApiVentas.Validations;

namespace WebApiVentas.Controllers.V1
{
    [ApiController]
    [Route("api/v1/ventas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    
    public class VentasController : CustomBaseController
    {
        private readonly bootcampContext context;
        private readonly IMapper mapper;
        private readonly IVentaValidator _validacionVenta;

        public VentasController(bootcampContext context, IMapper mapper, IVentaValidator validacionVenta): base(context,mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this._validacionVenta = validacionVenta;
        }
        


        /// <summary>
        /// Obtiene un listado de ventas.
        /// </summary>
        /// 
        [HttpGet(Name = "ObtenerVentasV1")]
        [AllowAnonymous]
        public async Task<ActionResult<List<VentaGetDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Ventas.AsQueryable();
            await HttpContext.InsertarParametroPaginacionEnCabecera(queryable);

            //no generico por los navigation?
            var venta = await queryable.OrderBy(venta => venta.Id).Paginar(paginacionDTO)
                .Include(compradorDB => compradorDB.CompradorNavigation)
                .Include(vendedorDB => vendedorDB.VendedorNavigation).ToListAsync();

            return mapper.Map<List<VentaGetDTO>>(venta);
        }



        /// <summary>
        /// Obtiene una venta por su Id.
        /// </summary>
        /// 
        [HttpGet("{id}", Name = "ObtenerVentaXIdV1")]
        [AllowAnonymous]
       // [ServiceFilter(typeof(VentaExisteAttribute))]
        public async Task<ActionResult<VentaDTO>> Get(int id)
        {
            var ventaValida = await _validacionVenta.VentaValidaciones(id, null, null);
            if (ventaValida.Count != 0)
            {
                return BadRequest(ventaValida);
            }

            //No generico por los navigation? 
            var venta = await context.Ventas.Include(detalleDB => detalleDB.DetallesVenta)
                .ThenInclude(detalleDB => detalleDB.ArticuloNavigation)
                .Include(compradorDB => compradorDB.CompradorNavigation)
                .Include(vendedorDB => vendedorDB.VendedorNavigation).FirstOrDefaultAsync(g => g.Id == id);
            
            return mapper.Map<VentaDTO>(venta);
        }



        /// <summary>
        /// Crea una venta en la BD.
        /// </summary>
        /// 
        [HttpPost (Name = "CrearVentaV1")]
        public async Task<ActionResult> Post(VentaCreacionDTO ventaCreacionDTO)
        {
            var ventaValida = await _validacionVenta.VentaValidaciones(null, ventaCreacionDTO.vendedor, ventaCreacionDTO.comprador);
            if (ventaValida.Count != 0)
            {
                return BadRequest(ventaValida);
            }

            //no generico por el DateTime?
            var venta = mapper.Map<Venta>(ventaCreacionDTO);
            venta.Fecha = DateTime.Now;

            context.Add(venta);
            await context.SaveChangesAsync();

            var ventaDTO = mapper.Map<VentaIdDTO>(venta);

            return CreatedAtRoute("ObtenerVentaXIdV1", new { id = venta.Id }, ventaDTO);
        }



        /// <summary>
        /// Actualiza una venta de la BD según el Id ingresado.
        /// </summary>
        /// 
        [HttpPut("{id:int}", Name = "ActualizarVentaV1")]
        //[ServiceFilter(typeof(VentaExisteAttribute))]
        public async Task<ActionResult> Put(VentaPutDTO ventaPutDTO, int id)
        {
            if (ventaPutDTO.IdVenta != id)
            {
                return BadRequest("El id de la venta no coincide con el id de la URL");
            }
           
            var ventaValida = await _validacionVenta.VentaValidaciones(id, ventaPutDTO.Vendedor, ventaPutDTO.Comprador);
            if (ventaValida.Count != 0)
            {
                return BadRequest(ventaValida);
            }

            //generico a partir de aca?
            var venta = mapper.Map<Venta>(ventaPutDTO);

            context.Update(venta);
            await context.SaveChangesAsync();

            var ventaDTO = mapper.Map<VentaIdDTO>(venta);

            return CreatedAtRoute("ObtenerVentaXIdV1", new { id = venta.Id }, ventaDTO);
        }



        /// <summary>
        /// Borra una venta de la BD según el Id ingresado.
        /// </summary>
        /// 
        [HttpDelete("{id:int}", Name ="BorrarVentaV1")]
        //[ServiceFilter(typeof(VentaExisteAttribute))]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Venta>(id);
        }


    }
}