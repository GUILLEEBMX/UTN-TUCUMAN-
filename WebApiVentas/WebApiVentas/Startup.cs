using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using WebApiVentas.Utilidades;
using WebApiVentas.Configuration;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace WebApiVentas
{
    public class Startup
    {
        
            public Startup(IConfiguration configuration)
            {
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                Configuration = configuration;

            }

            public IConfiguration Configuration { get; }

            // This method gets called by the runtime. Use this method to add services to the container.
            public void ConfigureServices(IServiceCollection services)
            {

                services.RegisterServices(Configuration); // Registro de servicios

                services.AddSwaggerDocumentation(Configuration);//Documents extensions
                
                services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_CONNECTIONSTRING"]);



            }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
            {

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseSwaggerDoc(Configuration);

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseCors();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }

        }

    }


