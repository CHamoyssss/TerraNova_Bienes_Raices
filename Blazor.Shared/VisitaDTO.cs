using System.Text.Json.Serialization;

namespace Blazor.Shared
{
    public class VisitaDTO
    {
        public int Id { get; set; }

        public int IdCliente { get; set; }

        public int IdPropiedad { get; set; }

        public int IdTrabajador { get; set; }

        public DateTime FechaVisita { get; set; }

        public string? Estado { get; set; }

        public string? Comentarios { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ClienteDTO? IdClienteNavigation { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PropiedadDTO? IdPropiedadNavigation { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TrabajadorDTO? IdTrabajadorNavigation { get; set; }
    }
}
