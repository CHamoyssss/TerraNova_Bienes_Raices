using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "La forma de pago es obligatoria")]
        public string FormaPago { get; set; } = "Contado";

        [Required(ErrorMessage = "El estado es obligatorio")]
        public string Estado { get; set; } = "Completada";

        public string Observaciones { get; set; } = string.Empty;

        public virtual ClienteDTO? IdClienteNavigation { get; set; }

        public virtual PropiedadDTO? IdPropiedadNavigation { get; set; }

        public virtual TrabajadorDTO? IdTrabajadorNavigation { get; set; }
    }
}
