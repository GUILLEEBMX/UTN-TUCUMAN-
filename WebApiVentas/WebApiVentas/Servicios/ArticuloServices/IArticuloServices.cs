using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.DTOs;

namespace WebApiVentas.Servicios.ArticulosServices
{
    public interface IArticuloServices
    {
        public Task<ActionResult<List<ArticuloIdDTO>>> Get();

        public Task<ActionResult<ArticuloPutDTO>> GetxID(int id);

        public Task<ActionResult> Post(ArticuloCreacionDTO articuloCreacionDTO);

        public Task<ActionResult> Put(int id, ArticuloPutDTO articuloPutDTO);

        public Task<ActionResult> Delete(int id);



    }
}
