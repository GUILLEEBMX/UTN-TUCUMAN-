using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiVentas.Controllers.V2;
using WebApiVentas.DTOs;
using WebApiVentas.Models;
using WebApiVentas.Servicios.ArticulosServices;

namespace WebApiVentas.Test.PruebasUnitarias
{   
    [TestClass]
    public class ArticulosControllerTests: BasePruebas
    {
        [TestMethod]
        public async  Task ObtenerArticulos()
        {
            //preparación
            var nombreDb = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();

            contexto.Articulos.Add(new Models.Articulo() { Nombre = "art1" });
            contexto.Articulos.Add(new Models.Articulo() { Nombre = "art2" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreDb);

            //prueba
            var controller = new ArticuloContext(contexto2, mapper);
            var respuesta = await controller.Get();
            //verificación
            var articulos = respuesta.Value;
            Assert.AreEqual(2, articulos.Count);
                
        }

        [TestMethod]
        public async Task ObtenerArticuloPorIdExistente()
        {
            //preparación
            var nombreDb = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();

            contexto.Articulos.Add(new Articulo() { Nombre = "art1" });
            contexto.Articulos.Add(new Articulo() { Nombre = "art2" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreDb);
            var controller = new ArticuloContext(contexto2, mapper);

            var id = 1;
            var respuesta = await controller.GetxID(id);
            var resultado = respuesta.Value;
            Assert.AreEqual(id, resultado.Id);
        }
        [TestMethod]
        public async Task CrearArticulo()
        {
            var nombreDb = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();

            var nuevoArticulo = new ArticuloCreacionDTO() { Nombre = "nuevo Art" };
            var controller = new ArticuloContext(contexto, mapper);

            var respuesta = await controller.Post(nuevoArticulo);
            var resultado = respuesta as CreatedAtRouteResult;
            Assert.IsNotNull(resultado);

            var contexto2 = ConstruirContext(nombreDb);
            var cantidad = await contexto2.Articulos.CountAsync();
            Assert.AreEqual(1, cantidad);
        }
        [TestMethod]
        public async Task ActualizarArticulo()
        {
            var nombreDb = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDb);
            var mapper = ConfigurarAutoMapper();

            contexto.Articulos.Add(new Articulo(){ Nombre = "Art1"});
            await contexto.SaveChangesAsync();
            
            var contexto2 = ConstruirContext(nombreDb);
            var controller = new ArticuloContext(contexto2, mapper);

            var articuloPutDto = new ArticuloPutDTO() { Id = 1, Nombre ="Nombrenuevo"};

            var id = 1;
            var respuesta = await controller.Put(id,articuloPutDto);

            //Assert.AreEqual(204, resultado.StatusCode);
            //Ahora devuelve 201
            var resultado = respuesta as CreatedAtRouteResult;
            Assert.IsNotNull(resultado);

            var contexto3 = ConstruirContext(nombreDb);
            var existe = await contexto3.Articulos.AnyAsync(x => x.Nombre == "Nombrenuevo");
            Assert.IsTrue(existe);
        }
        [TestMethod]
        public async Task IntentaBorrarArticuloNoExistente()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            var controller = new ArticuloContext(contexto, mapper);

            var respuesta = await controller.Delete(1);
            var resultado = respuesta as NotFoundObjectResult;
            //var resultado = respuesta as StatusCodeResult; No devuelve el status 404 
            Assert.AreEqual(404, resultado.StatusCode);
        }
        [TestMethod]
        public async Task IntentaBorrarArticuloExistente()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();

            contexto.Articulos.Add(new Articulo() { Nombre = "Articulo1" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);
            var controller = new ArticuloContext(contexto2, mapper);

            var respuesta = await controller.Delete(1);
            var resultado = respuesta as OkObjectResult;
            Assert.AreEqual(200, resultado.StatusCode);

            var contexto3 = ConstruirContext(nombreBD);
            var existe = await contexto3.Articulos.AnyAsync();
            Assert.IsFalse(existe);

        }

    }
}
