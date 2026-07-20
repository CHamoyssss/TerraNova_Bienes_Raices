namespace TerraNova_Bienes_Raices.Models
{
    public class CTrabajador
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string CI { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;     // Agente, Administrador, Recepcionista
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal PorcentajeComision { get; set; }
        public DateTime FechaContratacion { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;
    }
}
