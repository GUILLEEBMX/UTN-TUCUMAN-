using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiVentas.Models;
using WebApiVentas.Utilidades;

namespace WebApiVentas.Test
{
    public class BasePruebas
    {
        protected bootcampContext ConstruirContext(string nombreDb)
        {
            var opciones = new DbContextOptionsBuilder<bootcampContext>()
                .UseInMemoryDatabase(nombreDb).Options;
            var dbContext = new bootcampContext(opciones);
            return dbContext;
        }
        protected IMapper ConfigurarAutoMapper()
        {
            var config = new MapperConfiguration(options =>
            { options.AddProfile(new AutoMapperProfiles());
            });

            return config.CreateMapper();
        }
    }
}
