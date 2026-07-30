using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Models;
using Blazor.Shared;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly TerraNovaDbContext Contexto;

        public UsuarioController(TerraNovaDbContext contexto)
        {
            Contexto = contexto;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var RespuestaApi = new ResponseAPI<List<UsuarioDTO>>();
            var ListaUsuarios = new List<UsuarioDTO>();
            try
            {
                foreach (var Usuario in await Contexto.Usuarios.ToListAsync())
                {
                    ListaUsuarios.Add(new UsuarioDTO
                    {
                        Id = Usuario.Id,
                        NombreUsuario = Usuario.NombreUsuario,
                        // Nunca se devuelve el hash de la contraseña al listar
                        ContraseñaHash = null,
                        Rol = Usuario.Rol,
                        IdTrabajador = Usuario.IdTrabajador,
                        Activo = Usuario.Activo
                    });
                }
                RespuestaApi.EsCorrecto = true;
                RespuestaApi.Valor = ListaUsuarios;
                RespuestaApi.Mensaje = "Lista de Usuarios Preparada";
            }
            catch (Exception ex)
            {
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = $"{ex.Message} | {ex.InnerException?.Message}";
            }
            return Ok(RespuestaApi);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(UsuarioDTO ObjUsuario)
        {
            var RespuestaAPI = new ResponseAPI<int>();
            try
            {
                var DatosUsuario = new Usuario
                {
                    NombreUsuario = ObjUsuario.NombreUsuario,
                    ContraseñaHash = BCrypt.Net.BCrypt.HashPassword(ObjUsuario.ContraseñaHash), // se hashea aquí
                    Rol = ObjUsuario.Rol,
                    IdTrabajador = ObjUsuario.IdTrabajador,
                    Activo = ObjUsuario.Activo
                };
                Contexto.Usuarios.Add(DatosUsuario);
                await Contexto.SaveChangesAsync();
                if (DatosUsuario.Id != 0)
                {
                    RespuestaAPI.EsCorrecto = true;
                    RespuestaAPI.Valor = DatosUsuario.Id;
                    RespuestaAPI.Mensaje = "Usuario registrado correctamente";
                }
                else
                {
                    RespuestaAPI.EsCorrecto = false;
                    RespuestaAPI.Mensaje = "Datos no guardados";
                }
            }
            catch (Exception ex)
            {
                RespuestaAPI.EsCorrecto = false;
                RespuestaAPI.Mensaje = $"{ex.Message} | {ex.InnerException?.Message}";
            }
            return Ok(RespuestaAPI);
        }

       

        [HttpDelete]
        [Route("Eliminar/{Cod}")]
        public async Task<IActionResult> Eliminar(int Cod)
        {
            // Baja lógica, igual que Trabajador
            var RespuestaApi = new ResponseAPI<int>();
            try
            {
                var UsuarioEliminar = await Contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == Cod);
                if (UsuarioEliminar != null)
                {
                    UsuarioEliminar.Activo = false;
                    await Contexto.SaveChangesAsync();
                    RespuestaApi.EsCorrecto = true;
                    RespuestaApi.Mensaje = "Usuario dado de baja";
                }
                else
                {
                    RespuestaApi.EsCorrecto = false;
                    RespuestaApi.Mensaje = "Usuario no encontrado";
                }
            }
            catch (Exception ex)
            {
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = $"{ex.Message} | {ex.InnerException?.Message}";
            }
            return Ok(RespuestaApi);
        }

        [HttpGet]
        [Route("Buscar/{Cod}")]
        public async Task<IActionResult> Buscar(int Cod)
        {
            var RespuestaApi = new ResponseAPI<UsuarioDTO>();
            var UsuarioBuscado = new UsuarioDTO();
            try
            {
                var UsuarioBd = await Contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == Cod);
                if (UsuarioBd != null)
                {
                    UsuarioBuscado.Id = UsuarioBd.Id;
                    UsuarioBuscado.NombreUsuario = UsuarioBd.NombreUsuario;
                    UsuarioBuscado.ContraseñaHash = null; // idem, nunca se expone
                    UsuarioBuscado.Rol = UsuarioBd.Rol;
                    UsuarioBuscado.IdTrabajador = UsuarioBd.IdTrabajador;
                    UsuarioBuscado.Activo = UsuarioBd.Activo;

                    RespuestaApi.EsCorrecto = true;
                    RespuestaApi.Valor = UsuarioBuscado;
                }
                else
                {
                    RespuestaApi.EsCorrecto = false;
                    RespuestaApi.Mensaje = "Usuario No Encontrado";
                }
            }
            catch (Exception ex)
            {
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = $"{ex.Message} | {ex.InnerException?.Message}";
            }
            return Ok(RespuestaApi);
        }

        [HttpPut]
        [Route("Modificar/{Cod}")]
        public async Task<IActionResult> Modificar(UsuarioDTO NuevosDatos, int Cod)
        {
            var RespuestaApi = new ResponseAPI<int>();
            try
            {
                var UsuarioBd = await Contexto.Usuarios.FirstOrDefaultAsync(u => u.Id == Cod);

                if (UsuarioBd != null)
                {
                    UsuarioBd.NombreUsuario = NuevosDatos.NombreUsuario;
                    UsuarioBd.Rol = NuevosDatos.Rol;
                    UsuarioBd.IdTrabajador = NuevosDatos.IdTrabajador;
                    UsuarioBd.Activo = NuevosDatos.Activo;
                    // La contraseña se actualiza aparte (ver endpoint CambiarContrasena),
                    // nunca dentro del Modificar general.

                    Contexto.Usuarios.Update(UsuarioBd);
                    await Contexto.SaveChangesAsync();

                    RespuestaApi.EsCorrecto = true;
                    RespuestaApi.Valor = UsuarioBd.Id;
                }
                else
                {
                    RespuestaApi.EsCorrecto = false;
                    RespuestaApi.Mensaje = "Usuario no encontrado";
                }
            }
            catch (Exception ex)
            {
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = $"{ex.Message} | {ex.InnerException?.Message}";
            }
            return Ok(RespuestaApi);
        }

        // Este endpoint es solo un placeholder para quien implemente el login real.
        // Por ahora NO hace hash de verdad -- eso debe resolverlo la persona que
        // arme la autenticación (ej. con BCrypt o Identity).
        [HttpGet]
        [Route("ExisteNombreUsuario/{NombreUsuario}/{IdExcluir}")]
        public async Task<IActionResult> ExisteNombreUsuario(string NombreUsuario, int IdExcluir)
        {
            var existe = await Contexto.Usuarios.AnyAsync(u => u.NombreUsuario == NombreUsuario && u.Id != IdExcluir);
            return Ok(existe);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestDTO datos)
        {
            var RespuestaApi = new ResponseAPI<UsuarioDTO>();
            try
            {
                var usuario = await Contexto.Usuarios
                    .Include(u => u.IdTrabajadorNavigation)
                    .FirstOrDefaultAsync(u => u.NombreUsuario == datos.NombreUsuario && u.Activo);

                if (usuario != null && BCrypt.Net.BCrypt.Verify(datos.Contrasena, usuario.ContraseñaHash))
                {
                    RespuestaApi.EsCorrecto = true;
                    RespuestaApi.Valor = new UsuarioDTO
                    {
                        Id = usuario.Id,
                        NombreUsuario = usuario.NombreUsuario,
                        Rol = usuario.Rol,
                        IdTrabajador = usuario.IdTrabajador,
                        Activo = usuario.Activo
                    };
                    RespuestaApi.Mensaje = "Inicio de sesión exitoso";
                }
                else
                {
                    RespuestaApi.EsCorrecto = false;
                    RespuestaApi.Mensaje = "Usuario o contraseña incorrectos";
                }
            }
            catch (Exception ex)
            {
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = $"{ex.Message} | {ex.InnerException?.Message}";
            }
            return Ok(RespuestaApi);
        }
    }
}