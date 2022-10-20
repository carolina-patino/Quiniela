using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Festival.Equipos;
using Festival.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace Festival.Partidos
{
    public class PartidoAppService : CrudAppService<
            Partido, //The Book entity
            PartidoDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdatePartidoDto>, //Used to create/update a book
        IPartidoAppService //implement the IBookAppService
    {
        private readonly IRepository<Equipo, Guid> _equipoRepository;
        private readonly IPartidoRepository _partidoRepository;
        public PartidoAppService(IPartidoRepository partidoRepository, IRepository<Equipo, Guid> equipoRepository)
           : base(partidoRepository)
        {
            _equipoRepository = equipoRepository;  
            _partidoRepository = partidoRepository;
        }

        [Authorize(FestivalPermissions.Partidos.Create)]
        public override async Task<PartidoDto> CreateAsync(CreateUpdatePartidoDto input)
        {
            var partido = ObjectMapper.Map<CreateUpdatePartidoDto, Partido>(input);
            partido.Tenant = (Guid)CurrentTenant.Id;

            await Repository.InsertAsync(partido);

            return ObjectMapper.Map<Partido, PartidoDto>(partido);
        }

        [Authorize(FestivalPermissions.Partidos.Consultar)]
        public async Task<List<PartidoDto>> GetPartidosAsync()
        {
            var queryable = await _partidoRepository.GetPartidos();

            var query = from partido in queryable
                        join equipoA in await _equipoRepository.GetQueryableAsync() on partido.EquipoAId equals equipoA.Id 
                        join equipoB in await _equipoRepository.GetQueryableAsync() on partido.EquipoBId equals equipoB.Id
                        select new { partido, equipoA, equipoB};

            var partidoDtos = query.Select(x =>
            {
                var partidoDto = ObjectMapper.Map<Partido, PartidoDto>(x.partido);
                partidoDto.EquipoA = x.equipoA.Pais;
                partidoDto.EquipoB = x.equipoB.Pais;
                partidoDto.SiglasEquipoA = x.equipoA.Siglas;
                partidoDto.SiglasEquipoB = x.equipoB.Siglas;
                partidoDto.Grupo = x.equipoA.Grupo;
                return partidoDto;
            }).ToList();

            partidoDtos = partidoDtos.OrderBy(x => x.Grupo).ToList();

            return partidoDtos;
        }

        [Authorize(FestivalPermissions.Partidos.CargarResultados)]
        public async Task CargarResultados(PartidoDto input)
        {
            Partido partido = await Repository.GetAsync(input.Id);

            partido.ResultadoEquipoA = input.ResultadoEquipoA;
            partido.ResultadoEquipoB = input.ResultadoEquipoB;
            await Repository.UpdateAsync(partido);
        }

        [Authorize(FestivalPermissions.Partidos.Consultar)]
        public async Task<CreateUpdatePartidoDto> GetPartido(Guid Id)
        {
            Partido partido = await Repository.GetAsync(Id);

            return ObjectMapper.Map<Partido, CreateUpdatePartidoDto>(partido);
        }

        public async Task<List<PartidoDto>> GetPartidosDelDia()
        {
            var queryable = await _partidoRepository.GetPartidosDelDia();

            var query = from partido in queryable
                        join equipoA in await _equipoRepository.GetQueryableAsync() on partido.EquipoAId equals equipoA.Id
                        join equipoB in await _equipoRepository.GetQueryableAsync() on partido.EquipoBId equals equipoB.Id
                        select new { partido, equipoA, equipoB };

            var partidoDtos = query.Select(x =>
            {
                var partidoDto = ObjectMapper.Map<Partido, PartidoDto>(x.partido);
                partidoDto.EquipoA = x.equipoA.Pais;
                partidoDto.EquipoB = x.equipoB.Pais;
                partidoDto.SiglasEquipoA = x.equipoA.Siglas;
                partidoDto.SiglasEquipoB = x.equipoB.Siglas;
                return partidoDto;
            }).ToList();

            return partidoDtos;
        }
    }
}

