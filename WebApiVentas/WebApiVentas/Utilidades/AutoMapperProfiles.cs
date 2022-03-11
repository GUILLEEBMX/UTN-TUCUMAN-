using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVentas.DTOs;
using WebApiVentas.Models;

namespace WebApiVentas.Utilidades
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            //PersonaMap
            CreateMap<PersonaCreacionDTO, Persona>();
            CreateMap<Persona, PersonaDTO>();
            CreateMap<Persona, PersonaIdDTO>();
            CreateMap<Persona, PersonaVentaDTO>();
            CreateMap<PersonaPutDTO, Persona>();

            //ArticuloMap
            CreateMap<ArticuloCreacionDTO, Articulo>();
            CreateMap<Articulo, ArticuloIdDTO>();
            CreateMap<Articulo, ArticuloPutDTO>();
            CreateMap<ArticuloPutDTO, Articulo>();
            CreateMap<Articulo, DetalleArticuloDTO>();


            //VentaMap
            CreateMap<Venta,VentaDTO>()
                .ForMember(d => d.Comprador, x=> x.MapFrom(o=> o.CompradorNavigation))
                .ForMember(d => d.Vendedor, x=> x.MapFrom(o=> o.VendedorNavigation))
                .ForMember(d=> d.DetallesdeVenta, x=> x.MapFrom(o=> o.DetallesVenta));
            CreateMap<Venta, VentaGetDTO>()
                 .ForMember(d => d.Comprador, x => x.MapFrom(o => o.CompradorNavigation))
                 .ForMember(d => d.Vendedor, x => x.MapFrom(o => o.VendedorNavigation));
            CreateMap<VentaPutDTO, Venta>()
                .ForMember(d => d.Id, x => x.MapFrom(o => o.IdVenta));
            CreateMap<VentaCreacionDTO, Venta>();
            CreateMap<Venta, VentaIdDTO>()
                .ForMember(o => o.IdVenta, x => x.MapFrom (o => o.Id));
           
            
            //DetalleMap
            CreateMap<DetallesVenta, DetalleDTO>()
                .ForMember(d=> d.Articulos,x=> x.MapFrom(o=> o.ArticuloNavigation));
            CreateMap<DetalleCreacionDTO, DetallesVenta>();
            CreateMap<DetallePutDTO, DetallesVenta>()
                .ForMember(d => d.Id, x => x.MapFrom(o => o.IdDetalle));
            CreateMap<DetallesVenta,DetalleIdDTO>()
                .ForMember(d => d.IdDetalle, x => x.MapFrom(o => o.Id));

        }



        //private List<DetallesVenta> MapVentasDetalles (VentaCreacionDTO ventaCreacionDTO)  
        //{
        //    var resultado = new List<DetallesVenta> ();

        //    resultado.Add(new DetallesVenta());


        //    return resultado;
        //}

    }
}
