using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Usuario, UsuarioDTOs>().ReverseMap();

            
            CreateMap<Agua, AguaDTOs>()
                .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.IdUsuario))
                .ReverseMap();

           
            CreateMap<Reporte, ReporteDTOs>()
                .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.UsuarioId))
                .ReverseMap();

            ShouldMapMethod = m => false;  

            
        }
    }
}
