using System;
using System.Collections.Generic;

namespace Blazor.Server.Models;

public partial class Visita
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public int IdPropiedad { get; set; }

    public int IdTrabajador { get; set; }

    public DateTime FechaVisita { get; set; }

    public string Estado { get; set; } = null!;

    public string? Comentarios { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Propiedad IdPropiedadNavigation { get; set; } = null!;

    public virtual Trabajador IdTrabajadorNavigation { get; set; } = null!;
}
