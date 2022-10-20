using Festival.Equipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Festival.Partidos
{
    public class Partido: AuditedAggregateRoot<Guid>
    {
        public Guid Tenant { get; set; }
        public int ResultadoEquipoA { get; set; }

        public int ResultadoEquipoB { get; set; }

        public DateTime FechaPartido { get; set; }

        public Guid EquipoAId { get; set; }

        public Guid EquipoBId { get; set; }

    }
}
