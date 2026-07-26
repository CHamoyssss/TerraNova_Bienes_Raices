using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared
{
    public class TrabajadorDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null;

        public string Apellido { get; set; } = null;

        public string Ci { get; set; } = null;

        public string Cargo { get; set; } = null;

        public string Telefono { get; set; } = null;

        public string Email { get; set; } = null;

        public decimal PorcentajeComision { get; set; }

        public DateTime FechaContratacion { get; set; }

        public bool Activo { get; set; }

        public virtual ICollection<PropiedadDTO> Propiedades { get; set; } = new List<PropiedadDTO>();

        public virtual ICollection<UsuarioDTO> Usuarios { get; set; } = new List<UsuarioDTO>();

        public virtual ICollection<VentaDTO> Venta { get; set; } = new List<VentaDTO>();

        public virtual ICollection<VisitaDTO> Visita { get; set; } = new List<VisitaDTO>();
    }
}
