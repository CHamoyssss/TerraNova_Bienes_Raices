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

        public string Estado { get; set; } = null;

        public string Comentarios { get; set; }

        public virtual ClienteDTO IdClienteNavigation { get; set; } = null;

        public virtual PropiedadDTO IdPropiedadNavigation { get; set; } = null;

        public virtual TrabajadorDTO IdTrabajadorNavigation { get; set; } = null;
    }
}
