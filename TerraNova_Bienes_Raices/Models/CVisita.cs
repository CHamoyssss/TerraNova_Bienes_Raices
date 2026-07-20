namespace TerraNova_Bienes_Raices.Models
{
    public class CVisita
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdPropiedad { get; set; }
        public int IdTrabajador { get; set; }
        public DateTime FechaVisita { get; set; }
        public string Estado { get; set; } = "Programada"; // Programada, Realizada, Cancelada
        public string Comentarios { get; set; } = string.Empty;
    }
}
