using System.ComponentModel.DataAnnotations;

namespace TerraNova_Bienes_Raices.Models
{
    public class CPropiedad
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo es obligatorio")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La zona es obligatoria")]
        public string Zona { get; set; } = string.Empty;

        [Range(1, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "El área debe ser mayor a 0")]
        public double AreaM2 { get; set; }

        [Range(0, 20, ErrorMessage = "Cantidad de habitaciones no válida")]
        public int Habitaciones { get; set; }

        [Range(0, 20, ErrorMessage = "Cantidad de baños no válida")]
        public int Banios { get; set; }

        [Range(0, 10, ErrorMessage = "Cantidad de garajes no válida")]
        public int Garajes { get; set; }

        public string Estado { get; set; } = "Disponible";
        public string TipoOperacion { get; set; } = "Venta";
        public string Descripcion { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un agente")]
        public int IdTrabajador { get; set; }

        public DateTime FechaPublicacion { get; set; } = DateTime.Now;
    }
}
