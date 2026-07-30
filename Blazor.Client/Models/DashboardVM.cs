using Blazor.Shared;

namespace Blazor.Client.Models
{
    public class DashboardVM
    {
        public int TotalPropiedades { get; set; }
        public int TotalClientes { get; set; }
        public int TotalVentas { get; set; }
        public int TotalTrabajadores { get; set; }
        public List<PropiedadDTO> UltimasPropiedades { get; set; } = new();
        public List<ActividadReciente> ActividadReciente { get; set; } = new();
    }

    public class ActividadReciente
    {
        public string Descripcion { get; set; } = string.Empty;
        public string Icono { get; set; } = "bi-info-circle";
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
