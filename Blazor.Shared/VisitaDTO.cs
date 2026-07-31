using System.ComponentModel.DataAnnotations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared
{
    public class VisitaDTO
    {
        public int Id { get; set; }

        public int IdCliente { get; set; }

        public int IdPropiedad { get; set; }

        public int IdTrabajador { get; set; }

        public DateTime FechaVisita { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        public string Estado { get; set; } = "Programada";

        public string Comentarios { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
        public virtual ClienteDTO? IdClienteNavigation { get; set; }

        [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
        public virtual PropiedadDTO? IdPropiedadNavigation { get; set; }

        [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
        public virtual TrabajadorDTO? IdTrabajadorNavigation { get; set; }
    }
}
