using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared
{
    public class PropiedadDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo es obligatorio")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La zona es obligatoria")]
        public string Zona { get; set; } = string.Empty;

        public decimal Precio { get; set; }

        public double AreaM2 { get; set; }

        public int Habitaciones { get; set; }

        public int Banios { get; set; }

        public int Garajes { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        public string Estado { get; set; } = "Disponible";

        [Required(ErrorMessage = "El tipo de operación es obligatorio")]
        public string TipoOperacion { get; set; } = "Venta";

        public string Descripcion { get; set; } = string.Empty;

        public string ImagenUrl { get; set; } = string.Empty;

        public int IdTrabajador { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public string NombreTrabajador { get; set; } = null;

        public virtual TrabajadorDTO? IdTrabajadorNavigation { get; set; }

        public virtual ICollection<VentaDTO> Venta { get; set; } = new List<VentaDTO>();

        public virtual ICollection<VisitaDTO> Visita { get; set; } = new List<VisitaDTO>();
    
    }
}
