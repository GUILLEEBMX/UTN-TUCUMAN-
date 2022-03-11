using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.Models;

namespace WebApiVentas.Helpers
{
    public class VentaExisteAttribute : Attribute, IAsyncResourceFilter
    {
        private readonly bootcampContext Dbcontext;

        public VentaExisteAttribute(bootcampContext context)
        {
            this.Dbcontext = context;
        }
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var ventaIdObject = context.HttpContext.Request.RouteValues["id"];
            if (ventaIdObject == null)
            {
                return;
            }
            var ventaId = int.Parse(ventaIdObject.ToString());

            var venta = await Dbcontext.Ventas.Where(x => x.Id == ventaId).FirstOrDefaultAsync(); /*.ToListAsync()*/

            if (venta == null )
            {
                context.Result = new NotFoundObjectResult("El id de venta es inexistente");
            }
            else
            {
                await next();
            }

            ////var ventaIdObjec = context.HttpContext.
            //var existe_vendedor = await Dbcontext.Personas.Where(x => x.Id == ventaId).FirstOrDefaultAsync();

            //if (!existe_vendedor)
            //{
            //    context.Result = new NotFoundObjectResult("ESE VENDEDOR NO EXISTE EN NUESTRA BASE DE DATOS...");
            //}
            //else
            //{
            //    await next();
            //}
            //var existe_comprador = await Dbcontext.Personas.AnyAsync(compradorDB => compradorDB.Id == ventaPutDTO.Comprador);

            //if (!existe_comprador)
            //{
            //    context.Result = new NotFoundObjectResult("ESE COMPRADOR NO EXISTE EN NUESTRA BASE DE DATOS...");
            //}
            //else
            //{
            //    await next();
            //}

        }
    }
}
