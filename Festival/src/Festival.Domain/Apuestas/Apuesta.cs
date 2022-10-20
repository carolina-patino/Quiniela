using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Festival.Predicciones;

namespace Festival.Apuestas
{
    public class Apuesta : AuditedAggregateRoot<Guid>
    {
        public Guid Tenant { get; set; }
        public string Alias { get; set; }

        public bool EstaPagada { get; set; }

        public ICollection<Prediccion> Predicciones { get; set; }

    }
}
