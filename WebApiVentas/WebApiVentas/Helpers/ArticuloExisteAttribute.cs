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
    public class ArticuloExisteAttribute : Attribute, IAsyncResourceFilter
    {
        private readonly bootcampContext Dbcontext;

        public ArticuloExisteAttribute(bootcampContext context)
        {
            this.Dbcontext = context;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var articuloIdObject = context.HttpContext.Request.RouteValues["id"];

            if (articuloIdObject == null)
            {
                return;
            }
            var ventaId = int.Parse(articuloIdObject.ToString());

            var existeVenta = await Dbcontext.Articulos.AnyAsync(x => x.Id == ventaId);

            if (!existeVenta)
            {
                context.Result = new NotFoundObjectResult("El id del articulo es inexistente");
            }
            else
            {
                await next();
            }
        }
    }
}
