using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiVentas.Utilidades
{
    public static class ServiceProviderServiceExtensions
    {
        public static object GetRequiredService(this IServiceProvider provider, Type serviceType)
        {
            var requiredServiceSupportingProvider = provider as ISupportRequiredService;
            if (requiredServiceSupportingProvider != null)
            {
                return requiredServiceSupportingProvider.GetRequiredService(serviceType);
            }

            var service = provider.GetService(serviceType);
            if (service == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            return service;
        }

    }
}
