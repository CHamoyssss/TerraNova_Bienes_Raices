using System;
using System.Collections.Generic;

namespace Blazor.Server.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string ContraseñaHash { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public int? IdTrabajador { get; set; }

    public bool Activo { get; set; }

    public virtual Trabajador? IdTrabajadorNavigation { get; set; }
}
