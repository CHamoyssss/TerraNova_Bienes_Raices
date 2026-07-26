using System;
using System.Collections.Generic;

namespace Blazor.Server.Models;

public partial class Trabajador
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Ci { get; set; } = null!;

    public string Cargo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Email { get; set; } = null!;

    public decimal PorcentajeComision { get; set; }

    public DateOnly FechaContratacion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Propiedad> Propiedades { get; set; } = new List<Propiedad>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();

    public virtual ICollection<Visita> Visita { get; set; } = new List<Visita>();
}
