using Festival.Equipos;
using Festival.Partidos;
using Festival.Predicciones;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Festival.Apuestas
{
    public class ApuestaAppService : FestivalAppService, IApuestaAppService
    {
        private readonly IApuestaRepository _apuestaRepository;
        private readonly IRepository<Prediccion, Guid> _prediccionRepository;
        private readonly IRepository<Equipo, Guid> _equipoRepository;

        public ApuestaAppService(IApuestaRepository apuestaRepository
            , IRepository<Prediccion, Guid> prediccionRepository
            , IRepository<Equipo, Guid> equipoRepository)
        {
            _apuestaRepository = apuestaRepository;
            _prediccionRepository = prediccionRepository;
            _equipoRepository = equipoRepository;
        }

        public async Task<PagedResultDto<ApuestaDto>> GetApuestas()
        {
            var apuestas = await _apuestaRepository.GetApuestas(CurrentUser.Id);
            var apuestasDtos = ObjectMapper.Map<IReadOnlyList<Apuesta>, IReadOnlyList<ApuestaDto>>(apuestas);
            var equipos = await _equipoRepository.GetQueryableAsync();

            foreach (var dto in apuestasDtos)
            {
                var query = from prediccion in dto.Predicciones
                            join equipoA in await _equipoRepository.GetQueryableAsync() on prediccion.Partido.EquipoAId equals equipoA.Id
                            join equipoB in await _equipoRepository.GetQueryableAsync() on prediccion.Partido.EquipoBId equals equipoB.Id
                            select new { prediccion, equipoA, equipoB};
                dto.Predicciones = query.Select(p =>
                {

                    var prediccion = p.prediccion;
                    prediccion.Partido.EquipoA = p.equipoA.Pais;
                    prediccion.Partido.EquipoB = p.equipoB.Pais;
                    prediccion.Partido.SiglasEquipoA = p.equipoA.Siglas;
                    prediccion.Partido.SiglasEquipoB = p.equipoB.Siglas;
                    return prediccion;
                }).ToList();

                dto.PuntosObtenidos = dto.Predicciones.Sum(pr => pr.PuntosObtenidos);
            }


            var totalCount = await _apuestaRepository.CountAsync();

            return new PagedResultDto<ApuestaDto>(
                     totalCount,
                     apuestasDtos
                    );
        }

        public async Task<ApuestaDto> GetApuesta(Guid id)
        {
            var apuesta = await _apuestaRepository.GetApuesta(id);
            var apuestaDto = ObjectMapper.Map<Apuesta,ApuestaDto>(apuesta);
            var equipos = await _equipoRepository.GetQueryableAsync();

            var query = from prediccion in apuestaDto.Predicciones
                            join equipoA in await _equipoRepository.GetQueryableAsync() on prediccion.Partido.EquipoAId equals equipoA.Id
                            join equipoB in await _equipoRepository.GetQueryableAsync() on prediccion.Partido.EquipoBId equals equipoB.Id
                            select new { prediccion, equipoA, equipoB };
            apuestaDto.Predicciones = query.Select(p =>
            {
                var prediccion = p.prediccion;
                prediccion.Partido.EquipoA = p.equipoA.Pais;
                prediccion.Partido.EquipoB = p.equipoB.Pais;
                prediccion.Partido.SiglasEquipoA = p.equipoA.Siglas;
                prediccion.Partido.SiglasEquipoB = p.equipoB.Siglas;
                return prediccion;
            }).ToList();

            apuestaDto.PuntosObtenidos = apuestaDto.Predicciones.Sum(pr => pr.PuntosObtenidos);

            return apuestaDto;
        }

        public async Task<ApuestaDto> CreateApuesta(CreateUpdateApuestaDto input)
        {

            var nuevaApuesta = ObjectMapper.Map<CreateUpdateApuestaDto, Apuesta>(input);
            nuevaApuesta.Tenant = (Guid)CurrentTenant.Id;

            var apuesta = await _apuestaRepository.InsertAsync(nuevaApuesta);

            return ObjectMapper.Map<Apuesta, ApuestaDto>(apuesta);
        }

        public async Task<List<CreateUpdatePrediccionDto>>AgregarPredicciones(List<CreateUpdatePrediccionDto> predicciones)
        {
            var prediccionesDto = ObjectMapper.Map<List<CreateUpdatePrediccionDto>, List<Prediccion>>(predicciones);

            await _prediccionRepository.InsertManyAsync(prediccionesDto);

            return predicciones;

        }

        public async Task<List<ApuestaDto>> GetRanking()
        {
            var query = await _apuestaRepository.GetRanking();
            var apuestas = query.ToList();
            var apuestasDto = ObjectMapper.Map<List<ApuestaWithNavigationProperties>, List<ApuestaDto>>(apuestas);

            apuestasDto = apuestasDto.Select(p =>
            {
                p.PuntosObtenidos = p.Predicciones.Sum(pr => pr.PuntosObtenidos);
                return p;
             }).OrderByDescending(p => p.PuntosObtenidos).ToList();
            return apuestasDto;
        }

        public async Task EditarApuesta(ApuestaDto input)
        {
            var apuesta = await _apuestaRepository.GetApuesta(input.Id);
            apuesta.Alias = input.Alias;
            await _apuestaRepository.UpdateAsync(apuesta);
            foreach (var prediccion in apuesta.Predicciones)
            {
                var prediccionEditada = input.Predicciones.Where(x => x.Id == prediccion.Id).FirstOrDefault();
                prediccion.PrediccionResultadoEquipoA = prediccionEditada.PrediccionResultadoEquipoA;
                prediccion.PrediccionResultadoEquipoB = prediccionEditada.PrediccionResultadoEquipoB;
            }
        }

        public async Task<PremioDto> GetTotalPremio()
        {
            var totalApuestas = await _apuestaRepository.GetTotalApuestas();
            var premio = new PremioDto();
            var total = totalApuestas * 2;
            premio.PrimerLugar = Math.Round(total * 0.70);
            premio.SegundaLugar = Math.Round(total * 0.20);
            premio.TercerLugar = Math.Round(total * 0.10);
            return premio;
        }

        

    }
}

