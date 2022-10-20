using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Festival.Partidos
{
    public class CreateUpdatePartidoDto
    {
        public int ResultadoEquipoA { get; set; }

        public int ResultadoEquipoB { get; set; }

        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime FechaPartido { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El equipo A es requerido")]
        public Guid EquipoAId { get; set; }

        [Required(ErrorMessage = "El equipo B es requerido")]
        public Guid EquipoBId { get; set; }

    }
}
