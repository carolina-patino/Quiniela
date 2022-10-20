using AutoMapper;
using Festival.Equipos;
using Festival.Partidos;
using Festival.Apuestas;
using Festival.Predicciones;

namespace Festival;

public class FestivalApplicationAutoMapperProfile : Profile
{
    public FestivalApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Equipo, EquipoDto>();
        CreateMap<CreateUpdateEquipoDto, Equipo>();

        CreateMap<Partido, PartidoDto>();
        CreateMap<CreateUpdatePartidoDto, Partido>();
        CreateMap<PartidoDto, CreateUpdatePartidoDto>();

        CreateMap<Apuesta, ApuestaDto>();
        CreateMap<CreateUpdateApuestaDto, Apuesta>();
        CreateMap<ApuestaWithNavigationProperties, ApuestaDto>();

        CreateMap<Prediccion, PrediccionDto>();
        CreateMap<CreateUpdatePrediccionDto, Prediccion>();


    }
}
