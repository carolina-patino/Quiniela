using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Festival.Predicciones
{
    public interface IPrediccionRepository : IRepository<Prediccion, Guid>
    {
        Task<List<Prediccion>> GetPredicciones(Guid PartidoId);
    }
}
