using AutoMapper;
using BibliotecaSys.Application.DataObjects;
using BibliotecaSys.Domain.Models;
using Serilog;

namespace BibliotecaSys.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        try
        {
            CreateMap<Libro, LibroDto>()
                .ForMember(libro => libro.Estado,
                    opt => opt.MapFrom(src => src.IdEstadoNavigation.Estado1));
            CreateMap<Reserva, ReservaDto>()
                .ForMember(dest => dest.TituloLibro,
                    opt => opt.MapFrom(src => src.IdLibroNavigation.Titulo));
        }
        catch (Exception ex)
        {
            var msg = "Something went wrong mapping, verify if the any navigation property was null";

            Log.Logger.Error(msg);
            throw new Exception(msg);
        }
    }
}