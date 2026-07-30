using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared
{
    public class TrabajadorDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "La CI es obligatoria")]
        public string Ci { get; set; } = string.Empty;

        [Required(ErrorMessage = "El cargo es obligatorio")]
        public string Cargo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "Teléfono no válido")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Email no válido")]
        public string Email { get; set; } = string.Empty;

        public decimal PorcentajeComision { get; set; }

        public DateTime FechaContratacion { get; set; }

        public string? FotoUrl { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public bool Activo { get; set; }

        public virtual ICollection<PropiedadDTO> Propiedades { get; set; } = new List<PropiedadDTO>();

        public virtual ICollection<UsuarioDTO> Usuarios { get; set; } = new List<UsuarioDTO>();

        public virtual ICollection<VentaDTO> Venta { get; set; } = new List<VentaDTO>();

        public virtual ICollection<VisitaDTO> Visita { get; set; } = new List<VisitaDTO>();
    }
}
