using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiVentas.DTOs;
using WebApiVentas.Filtros;
using WebApiVentas.Helpers;
using WebApiVentas.Models;
using WebApiVentas.Servicios;
using WebApiVentas.Servicios.ArticulosServices;
using WebApiVentas.Servicios.DetalleServices;
using WebApiVentas.Servicios.PersonasServices;
using WebApiVentas.Servicios.VentasServices;
using WebApiVentas.Utilidades;
using WebApiVentas.Validations;

namespace WebApiVentas.Configuration
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            //    /*
            //     * TIPS: Configuración de repositorios en el contenedor de dependencias.
            //     *
            //     * Formato:
            //     * services.AddTransient<I[Model]Repository, [Tecnologia][Model]Repository>();
            //     *
            //     * Ejemplos:
            //     * services.AddTransient<IUsuarioRepository, MySqlUsuarioRepository>();
            //     * services.AddTransient<IUsuarioRepository, CiDiUsuarioRepository>();
            //     * services.AddTransient<IUsuarioRepository, ActiveDirectorysUsuarioRepository>();
            //     * services.AddTransient<IUsuarioRepository, MockUsuarioRepository>();
            //     */

            //    //services.AddTransient<ILicenciaRepository, MsSqlServerLicenciaRepository>();


            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
          
            services.AddDbContext<bootcampContext>(options => options.UseSqlServer(configuration.GetConnectionString("defaultConnection")));
            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<GeneradorEnlacesObjetoDTO<BaseDTO>>();
            services.AddTransient<HATEOASPersonasDTOFilterAttribute>();
            services.AddTransient<HATEOASArticulosDTOFilterAttribute>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IPersonaServices, PersonaContext>();
            services.AddTransient<IArticuloServices, ArticuloContext>();
            services.AddTransient<IVentaServices, VentaContext>();
            services.AddTransient<IDetalleServices, DetalleContext>();
            
            services.AddTransient<MiFiltroDeAccion>();

            services.AddScoped<VentaExisteAttribute>();
            services.AddScoped<IVentaValidator, VentaValidator>();
            services.AddScoped<ArticuloExisteAttribute>();


            services.AddCors(opciones =>
            {
                opciones.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://webapiventas.azurewebsites.net/").AllowAnyMethod().AllowAnyHeader();
                });
            });

            //services.AddControllers(opciones =>
            //{
            //    opciones.Filters.Add(typeof(FiltroDeExcepcion));
            //    //opciones.Filters.Add(typeof(MiFiltroDeAccion));

            //});

            return services;
        }

        public static IServiceCollection RegisterApplicationValidators(this IServiceCollection services)
        {
            /*
             * TIPS: Configuración de validadores en el contenedor de dependencias. No todos los modelos tendrán un validador.
             *
             * Formato:
             * services.AddTransient<IValidator<[Model]Model>, [Model]Validator>();
             *
             * Ejemplos:
             * services.AddTransient<IValidator<UsuarioModel>, UsuarioValidator>();
             */

           // services.AddScoped<IValidator<ProductoModel>, ProductoValidator>();
        
            return services;
        }
    }

}

