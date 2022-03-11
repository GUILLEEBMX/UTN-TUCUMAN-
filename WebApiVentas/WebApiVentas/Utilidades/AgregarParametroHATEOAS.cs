using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiVentas.Utilidades
{
    public class AgregarParametroHATEOAS : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var result = context.ApiDescription.ActionDescriptor.AttributeRouteInfo.Name;

            if (  result != "ObtenerArticulosV2" && result != "ObtenerArticuloXIdV2" 
                && result != "ObtenerPersonasV2" && result != "ObtenerPersonaXIdV2")
            { 
                return;
            }

           
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "IncluirHATEOAS",
                In = ParameterLocation.Header,
                Required = false
            });
            


        }
    }
}
