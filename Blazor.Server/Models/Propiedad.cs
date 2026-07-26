using System;
using System.Collections.Generic;

namespace Blazor.Server.Models;

public partial class Propiedad
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Zona { get; set; } = null!;

    public decimal Precio { get; set; }

    public double AreaM2 { get; set; }

    public int Habitaciones { get; set; }

    public int Banios { get; set; }

    public int Garajes { get; set; }

    public string Estado { get; set; } = null!;

    public string TipoOperacion { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? ImagenUrl { get; set; }

    public int IdTrabajador { get; set; }

    public DateTime FechaPublicacion { get; set; }

    public virtual Trabajador IdTrabajadorNavigation { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();

    public virtual ICollection<Visita> Visita { get; set; } = new List<Visita>();
}
