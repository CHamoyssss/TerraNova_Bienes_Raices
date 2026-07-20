namespace TerraNova_Bienes_Raices.Models
{
    public class CPropiedad
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;        // "Casa en Zona Sur"
        public string Tipo { get; set; } = string.Empty;          // Casa, Departamento, Terreno, Oficina, Local
        public string Direccion { get; set; } = string.Empty;
        public string Zona { get; set; } = string.Empty;          // Zona Sur, Centro, Norte, etc.
        public decimal Precio { get; set; }
        public double AreaM2 { get; set; }
        public int Habitaciones { get; set; }
        public int Banios { get; set; }
        public int Garajes { get; set; }
        public string Estado { get; set; } = "Disponible";        // Disponible, Vendida, Reservada, Alquilada
        public string TipoOperacion { get; set; } = "Venta";      // Venta, Alquiler
        public string Descripcion { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;
        public int IdTrabajador { get; set; }                     // agente asignado
        public DateTime FechaPublicacion { get; set; } = DateTime.Now;
    }
}
