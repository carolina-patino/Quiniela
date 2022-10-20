using Festival.Predicciones;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Festival.Apuestas
{
    public class ApuestaDto: AuditedEntityDto<Guid>
    {
        public Guid Tenant { get; set; }
        public string Alias { get; set; }
        public bool EstaPagada { get; set; }
        public IList<PrediccionDto> Predicciones { get; set; }
        public int PuntosObtenidos { get; set; }
        public string NombreUsuario { get; set; }

        public string EmailUsuario { get; set; }

    }
}
