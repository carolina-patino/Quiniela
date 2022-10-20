using Festival.Predicciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.Apuestas
{
    public class ApuestaWithNavigationProperties
    {
      public Guid Id { get; set; }
      public Guid Tenant { get; set; }
      public string Alias { get; set; }

      public bool EstaPagada { get; set; }

      public ICollection<Prediccion> Predicciones { get; set; }

      public string NombreUsuario { get; set; }

      public string CorreoUsuario { get; set; }

    }
}
