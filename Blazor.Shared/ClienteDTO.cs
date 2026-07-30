using System.ComponentModel.DataAnnotations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "La CI es obligatoria")]
        public string Ci { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "Teléfono no válido")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Email no válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        public string Direccion { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; }

        [Required(ErrorMessage = "El tipo de cliente es obligatorio")]
        public string TipoCliente { get; set; } = "Comprador";

        public virtual ICollection<VentaDTO> Venta { get; set; } = new List<VentaDTO>();

        public virtual ICollection<VisitaDTO> Visita { get; set; } = new List<VisitaDTO>();
    }
}
