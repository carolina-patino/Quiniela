using Festival.Apuestas;
using Festival.Partidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Festival.Predicciones
{
    public class Prediccion: AuditedAggregateRoot<Guid>
    {
        public Guid Tenant { get; set; }

        public int PrediccionResultadoEquipoA { get; set; }

        public int PrediccionResultadoEquipoB { get; set; }

        public int PuntosObtenidos { get; set; }

        public Guid ApuestaId { get; set; }

        public Guid PartidoId { get; set; }

        public Partido Partido { get; set; }

        public Apuesta Apuesta { get; set; }
    }
}
