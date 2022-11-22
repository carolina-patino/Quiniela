using Festival.Apuestas;
using Festival.Partidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;


namespace Festival.Predicciones
{
    public class PrediccionAppService :
        CrudAppService<
            Prediccion, //The Book entity
            PrediccionDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdatePrediccionDto>, //Used to create/update a book
        IPrediccionAppService //implement the IBookAppService
    {
        private readonly IPrediccionRepository _prediccionRepository;

        public PrediccionAppService(IPrediccionRepository prediccionRepository)
            : base(prediccionRepository)
        {
            _prediccionRepository = prediccionRepository;
        }

        public async Task CalcularPuntos(PartidoDto Partido)
        {
            var predicciones = await _prediccionRepository.GetPredicciones(Partido.Id);
            var equipoGanador = "";
            
            if (Partido.ResultadoEquipoA > Partido.ResultadoEquipoB)
            {
                equipoGanador = "A";
            }
            else if (Partido.ResultadoEquipoA < Partido.ResultadoEquipoB)
            {
                equipoGanador = "B";
            }
            else if (Partido.ResultadoEquipoA == Partido.ResultadoEquipoB)
            {
                equipoGanador = "E";
            }
            
            foreach (var prediccion in predicciones)
            {
                prediccion.PuntosObtenidos = 0;
                if (Partido.ResultadoEquipoA == prediccion.PrediccionResultadoEquipoA && Partido.ResultadoEquipoB == prediccion.PrediccionResultadoEquipoB)
                {
                    prediccion.PuntosObtenidos = 3;
                }
                else if (equipoGanador == "A" && prediccion.PrediccionResultadoEquipoA > prediccion.PrediccionResultadoEquipoB)
                {
                    prediccion.PuntosObtenidos = 1;
                }
                else if (equipoGanador == "B" && prediccion.PrediccionResultadoEquipoB > prediccion.PrediccionResultadoEquipoA)
                {
                    prediccion.PuntosObtenidos = 1;
                }
                else if (equipoGanador == "E" && Partido.ResultadoEquipoA == prediccion.PrediccionResultadoEquipoA 
                    && Partido.ResultadoEquipoB == prediccion.PrediccionResultadoEquipoB)
                {
                    prediccion.PuntosObtenidos = 3;
                }
                else if (equipoGanador == "E" && prediccion.PrediccionResultadoEquipoA == prediccion.PrediccionResultadoEquipoB)
                {
                    prediccion.PuntosObtenidos = 1;
                }
            }

            await _prediccionRepository.UpdateManyAsync(predicciones);
        }

        public async Task EliminarPredicciones(Guid ApuestaId)
        {
            var predicciones = await _prediccionRepository.GetPrediccionesPorApuesta(ApuestaId);

            await _prediccionRepository.DeleteManyAsync(predicciones);

        }

    }
}
