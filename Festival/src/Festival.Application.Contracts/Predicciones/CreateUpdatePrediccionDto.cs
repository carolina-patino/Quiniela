using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Festival.Predicciones
{
    public class CreateUpdatePrediccionDto
    {
        [Required(ErrorMessage = "El tenant es requerido")]
        public Guid Tenant { get; set; }

        [Required(ErrorMessage = "La predicción para el primer equipo es requerida")]
        public int PrediccionResultadoEquipoA { get; set; }

        [Required(ErrorMessage = "La predicción para el segundo equipo es requerida")]
        public int PrediccionResultadoEquipoB { get; set; }

        public int PuntosObtenidos { get; set; }

        [Required(ErrorMessage = "El partido es requerido")]
        public Guid PartidoId { get; set; }

        [Required(ErrorMessage = "La apuesta es requerida")]
        public Guid ApuestaId { get; set; }
        public string EquipoA { get; set; }
        public string EquipoB { get; set; }

        public string SiglasEquipoA { get; set; }

        public string SiglasEquipoB { get; set; }
    }
}
