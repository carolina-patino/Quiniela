using Festival.Partidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Festival.Equipos
{
    public class Equipo: AuditedAggregateRoot<Guid>
    {
        public Guid Tenant { get; set; }
        public string Grupo { get; set; }

        public string Pais { get; set; }

        public string Siglas { get; set; }

    }
}
