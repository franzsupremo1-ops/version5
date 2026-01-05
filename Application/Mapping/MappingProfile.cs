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
            // Usuario
            CreateMap<Usuario, UsuarioDTOs>().ReverseMap();

            // Agua
            CreateMap<Agua, AguaDTOs>()
                .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.IdUsuario))
                .ReverseMap();

            // Reporte
            CreateMap<Reporte, ReporteDTOs>()
                .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.UsuarioId))
                .ReverseMap();

            ShouldMapMethod = m => false;  // ← Agregar esta línea

            // resto mappings...
        }
    }
}
