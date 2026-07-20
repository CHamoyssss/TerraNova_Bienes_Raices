using TerraNova_Bienes_Raices.Models;
namespace TerraNova_Bienes_Raices.Services
{
    public class ServiceSesion
    {
        public CUsuario UsuarioActual { get; set; } = new CUsuario
        {
            Id = 1,
            NombreUsuario = "admin_prueba",
            Rol = "Admin" // cambiar a "Agente" para probar la vista restringida
        };

        public bool EsAdmin => UsuarioActual.Rol == "Admin";
    }
}
