using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace WebApiVentas.Utilidades
{
    public class SwaggerAgrupaPorVersion : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var namespaceControlador = controller.ControllerType.Namespace; // da el nombre del controlador
            var versionAPI = namespaceControlador.Split(separator: ".").Last().ToLower(); // da el nro de version
            controller.ApiExplorer.GroupName = versionAPI;


        }
    }
}
