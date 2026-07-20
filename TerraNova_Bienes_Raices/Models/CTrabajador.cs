using System.ComponentModel.DataAnnotations;

namespace TerraNova_Bienes_Raices.Models
{
    public class CTrabajador
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "La CI es obligatoria")]
        public string CI { get; set; } = string.Empty;

        [Required(ErrorMessage = "El cargo es obligatorio")]
        public string Cargo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "Teléfono no válido")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Email no válido")]
        public string Email { get; set; } = string.Empty;
        public decimal PorcentajeComision { get; set; }
        public DateTime FechaContratacion { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;
    }
}
