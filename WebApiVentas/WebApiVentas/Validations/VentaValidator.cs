using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.Models;

namespace WebApiVentas.Validations
{
    public class VentaValidator: IVentaValidator
    {
        private readonly bootcampContext context;

        public VentaValidator(bootcampContext context)
        {
            this.context = context;
        }

        public async Task <List<string>> VentaValidaciones(int? idVenta, int? idVendedor, int? idComprador)
        {
            List<string> errores = new List<string>();

            if (idVenta!=null)
            {
                var existeVenta = await context.Ventas.AnyAsync(x => x.Id == idVenta);
                if (!existeVenta)
                {
                    errores.Add("No existe Venta");
                }
            }
            //existeVenta = (idVenta == null) ? existeVenta= await context.Ventas.AnyAsync(x => x.Id == idVenta): false;
            if (idVendedor!= null)
            {
                var existeVendedor = await context.Personas.AnyAsync(x => x.Id == idVendedor);
                if (!existeVendedor)
                {
                    errores.Add("No existe vendedor");
                }
            }
            if (idComprador!=null)
            {
                var existeComprador = await context.Personas.AnyAsync(x => x.Id == idComprador);
                if (!existeComprador)
                {
                    errores.Add("No existe Comprador");
                }
            }
            
            return errores;
        }


    }
}
