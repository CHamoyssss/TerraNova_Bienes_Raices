using System;
using System.Collections.Generic;

namespace Blazor.Server.Models;

public partial class Venta
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public int IdPropiedad { get; set; }

    public int IdTrabajador { get; set; }

    public DateTime Fecha { get; set; }

    public decimal Monto { get; set; }

    public decimal ComisionGenerada { get; set; }

    public string FormaPago { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string? Observaciones { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Propiedad IdPropiedadNavigation { get; set; } = null!;

    public virtual Trabajador IdTrabajadorNavigation { get; set; } = null!;
}
