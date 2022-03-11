using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiVentas.Controllers.V1;
using WebApiVentas.Controllers.v2;
using WebApiVentas.Test.Mocks;

namespace WebApiVentas.Test
{
    [TestClass]
    public class RootControllerTest
    {
        [TestMethod]

        public async Task SiUsuarioEsAdmin_Obtenemos7Links()
        {
            //preparacion
            var authorizationService = new AuthorizationServiceMock();
            authorizationService.resultado = AuthorizationResult.Success();
            var rootController = new RootController(authorizationService);
            rootController.Url = new URLHelperMock();

            //ejecucion
            var resultado = await rootController.Get();

            //verificacion
            Assert.AreEqual(7, resultado.Value.Count());

        }

        [TestMethod]

        public async Task SiUsuarioNoEsAdmin_Obtenemos4Links()
        {
            //preparacion
            var authorizationService = new AuthorizationServiceMock();
            authorizationService.resultado = AuthorizationResult.Failed();
            var rootController = new RootController(authorizationService);
            rootController.Url = new URLHelperMock();

            //ejecucion
            var resultado = await rootController.Get();

            //verificacion
            Assert.AreEqual(4, resultado.Value.Count());

        }


        [TestMethod]

        public async Task SiUsuarioNoEsAdmin_Obtenemos4Links_UsandoMoq()
        {
            //preparacion
            var authorizationService = new AuthorizationServiceMock();
            authorizationService.resultado = AuthorizationResult.Failed();
            var rootController = new RootController(authorizationService);
            rootController.Url = new URLHelperMock();

            //ejecucion
            var resultado = await rootController.Get();

            //verificacion
            Assert.AreEqual(4, resultado.Value.Count());

        }

    }
}
