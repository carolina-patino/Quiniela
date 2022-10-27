using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Festival.Partidos
{
    public class PartidoDto: AuditedEntityDto<Guid>
    {
        public Guid Tenant { get; set; }

        [Range(0, 99, ErrorMessage = "No se aceptan valores negativos")]
        public int ResultadoEquipoA { get; set; }

        [Range(0, 99, ErrorMessage = "No se aceptan valores negativos")]
        public int ResultadoEquipoB { get; set; }

        public DateTime FechaPartido { get; set; }

        public string EquipoA { get; set; }

        public string EquipoB { get; set; }

        public Guid EquipoAId { get; set; }

        public Guid EquipoBId { get; set; }

        public string SiglasEquipoA { get; set; }

        public string SiglasEquipoB { get; set; }

        public string Grupo { get; set; }

    }
}
