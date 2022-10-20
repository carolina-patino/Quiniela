using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Festival.Predicciones;

namespace Festival.Apuestas
{
    public class CreateUpdateApuestaDto
    {
        [Required(ErrorMessage = "El tenant es requerido")]
        public Guid Tenant { get; set; }

        [Required(ErrorMessage = "El alias es requerido")]
        [StringLength(ApuestaConsts.AliasMaxLength, ErrorMessage = "El alias es muy largo")]
        public string Alias { get; set; }

        public bool EstaPagada { get; set; } = false;

        public IList<PrediccionDto> Predicciones { get; set; }

    }
}
