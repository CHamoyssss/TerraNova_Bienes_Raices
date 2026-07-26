using System;
using System.Collections.Generic;

namespace Blazor.Server.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Ci { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Direccion { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string TipoCliente { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();

    public virtual ICollection<Visita> Visita { get; set; } = new List<Visita>();
}
