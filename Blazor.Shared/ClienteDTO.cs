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

        public string Nombre { get; set; } = null;

        public string Apellido { get; set; } = null;

        public string Ci { get; set; } = null;

        public string Telefono { get; set; } = null;

        public string Email { get; set; } = null;

        public string Direccion { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string TipoCliente { get; set; } = null;

        public virtual ICollection<VentaDTO> Venta { get; set; } = new List<VentaDTO>();

        public virtual ICollection<VisitaDTO> Visita { get; set; } = new List<VisitaDTO>();
    }
}
