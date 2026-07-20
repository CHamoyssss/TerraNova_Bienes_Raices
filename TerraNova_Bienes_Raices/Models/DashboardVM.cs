namespace TerraNova_Bienes_Raices.Models
{
    public class DashboardVM
    {
        public int TotalPropiedades { get; set; }
        public int TotalClientes { get; set; }
        public int TotalVentas { get; set; }
        public int TotalTrabajadores { get; set; }

    public List<CPropiedad> UltimasPropiedades { get; set; } = new();

    // Lista opcional de actividad reciente (simulada por ahora)
    public List<ActividadReciente> ActividadReciente { get; set; } = new();
  }
    public class ActividadReciente
    {
        public string Descripcion { get; set; } = string.Empty;
        public string Icono { get; set; } = "bi-info-circle";
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
