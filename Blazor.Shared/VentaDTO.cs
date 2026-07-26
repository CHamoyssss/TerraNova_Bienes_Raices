using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared
{
    public class VentaDTO
    {
        public int Id { get; set; }

        public int IdCliente { get; set; }

        public int IdPropiedad { get; set; }

        public int IdTrabajador { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Monto { get; set; }

        public decimal ComisionGenerada { get; set; }

        public string FormaPago { get; set; } = null;

        public string Estado { get; set; } = null;

        public string Observaciones { get; set; }

        public virtual ClienteDTO IdClienteNavigation { get; set; } = null;

        public virtual PropiedadDTO IdPropiedadNavigation { get; set; } = null;

        public virtual TrabajadorDTO IdTrabajadorNavigation { get; set; } = null;
    }
}
