using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiVentas.Utilidades
{   
    public class HATEOASFilterAttribute: ResultFilterAttribute
    {     
        protected bool DebeIncluirHATEOAS (ResultExecutingContext context)
        {
            var resultado = context.Result as ObjectResult;

            if(!EsRespuestaExitosa(resultado))
            {
                return false;
            }
            
            var cabecera = context.HttpContext.Request.Headers["incluirHATEOAS"];

            if(cabecera.Count == 0)
            {
                return false;
            }

            var valor = cabecera [0];

            if(!valor.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            return true;
        }

        private bool EsRespuestaExitosa (ObjectResult resultado)
        {
            var exito = false;

            if(resultado?.Value != null )//&& resultado.StatusCode >= 200 && resultado.StatusCode < 300)
            {
                exito = true;
            }

            return exito;
        }
    }
}
