using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using WebApiVentas.Validations;

namespace WebApiVentas.Test.PruebasUnitarias
{
    [TestClass]
    public class ValidarPrimeraLetraMayusAtributteTest
    {
        [TestMethod]
        public void PrimeraLetraMinuscula_DevuelveError()
        {
            //Preparacion

            var primerLetraMayus = new ValidarPrimeraLetraMayusAtributte();
            var valor = "noelia";
            var valContext = new ValidationContext(new { Nombre = valor });

            //Ejecucion
            var resultado = primerLetraMayus.GetValidationResult(valor, valContext);


            //Verificacion
            Assert.AreEqual("La primera letra debe ser mayúscula",resultado.ErrorMessage);
        }



        [TestMethod]
        public void ValorNulo_NoDevuelveError()
        {
            //Preparacion

            var primerLetraMayus = new ValidarPrimeraLetraMayusAtributte();
            string valor = null;
            var valContext = new ValidationContext(new { Nombre = valor });

            //Ejecucion
            var resultado = primerLetraMayus.GetValidationResult(valor, valContext);


            //Verificacion
            Assert.IsNull(resultado);
        }



        [TestMethod]
        public void ValorConPrimeraLetraMayuscula_NoDevuelveError()
        {
            //Preparacion

            var primerLetraMayus = new ValidarPrimeraLetraMayusAtributte();
            var valor = "Noelia";
            var valContext = new ValidationContext(new { Nombre = valor });

            //Ejecucion
            var resultado = primerLetraMayus.GetValidationResult(valor, valContext);


            //Verificacion
            Assert.IsNull(resultado);
        }
    }
}