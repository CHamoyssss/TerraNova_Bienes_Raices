using TerraNova_Bienes_Raices.Models;
namespace TerraNova_Bienes_Raices.Services
{
    public class ServiceSesion
    {
        // Por ahora, hardcodeado para poder probar. Cuando exista login real,
        // esto se llenará con los datos del usuario que inició sesión.
        public CUsuario UsuarioActual { get; set; } = new CUsuario
        {
            Id = 1,
            NombreUsuario = "admin_prueba",
            Rol = "Admin" // cambia a "Agente" para probar la vista restringida
        };

        public bool EsAdmin => UsuarioActual.Rol == "Admin";
    }
}
