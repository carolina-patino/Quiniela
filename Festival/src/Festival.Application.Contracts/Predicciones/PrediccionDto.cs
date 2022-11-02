using Festival.Partidos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Festival.Predicciones
{
    public class PrediccionDto : AuditedEntityDto<Guid>
    {
        public Guid Tenant { get; set; }

        [Range(0, 99, ErrorMessage = "No se aceptan valores negativos")]
        public int PrediccionResultadoEquipoA { get; set; }

        [Range(0, 99, ErrorMessage = "No se aceptan valores negativos")]
        public int PrediccionResultadoEquipoB { get; set; }

        public int PuntosObtenidos { get; set; }

        public PartidoDto Partido { get; set; }

        public Guid ApuestaId { get; set; }

    }
}
