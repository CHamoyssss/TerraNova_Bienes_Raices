namespace TerraNova_Bienes_Raices.Models
{
    public class CUsuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Rol { get; set; } = "Agente"; // "Admin" o "Agente"
    }
}
