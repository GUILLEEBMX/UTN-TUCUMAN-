using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace WebApiVentas.Servicios
{
    public abstract class GeneradorEnlacesBase 
    {
        protected readonly IAuthorizationService authorizationService;
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly IActionContextAccessor actionContextAccessor;

        public GeneradorEnlacesBase(
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            IActionContextAccessor actionContextAccessor)
        {
            this.authorizationService = authorizationService;
            this.httpContextAccessor = httpContextAccessor;
            this.actionContextAccessor = actionContextAccessor;
        }

        protected IUrlHelper ConstruirURLHelper()
        {
            var factoria = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();

            return factoria.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        protected async Task<bool> EsAdmin()
        {
            var httpContext = httpContextAccessor.HttpContext;
            var resultado = await authorizationService.AuthorizeAsync(httpContext.User, "esAdmin");

            return resultado.Succeeded;
        }
    }
}
