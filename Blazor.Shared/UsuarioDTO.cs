using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public string NombreUsuario { get; set; } = null;

        public string ContraseñaHash { get; set; } = null;

        public string Rol { get; set; } = null;

        public int? IdTrabajador { get; set; }

        public bool Activo { get; set; }

        public virtual TrabajadorDTO? IdTrabajadorNavigation { get; set; }
    }
}
