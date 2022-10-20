using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Festival.Equipos
{
    public class CreateUpdateEquipoDto 
    {
        [Required(ErrorMessage = "El tenant es requerido")]
        public Guid TenantId { get; set; }

        [Required (ErrorMessage = "El grupo es requerido")]
        [StringLength(EquipoConsts.GrupoMaxLength, ErrorMessage = "El Grupo del equipo es muy largo")]
        public string Grupo { get; set; }

        [Required(ErrorMessage = "El país es requerido")]
        [StringLength(EquipoConsts.PaisMaxLength, ErrorMessage = "El nombre del país es muy largo")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "Las siglas del país son requeridas")]
        [StringLength(EquipoConsts.SiglasMaxLength, ErrorMessage = "Las siglas del país son muy largas")]
        public string Siglas { get; set; }
    }
}
