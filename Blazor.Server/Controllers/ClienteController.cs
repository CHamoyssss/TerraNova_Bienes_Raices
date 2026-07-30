using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Models;
using Blazor.Shared;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly TerraNovaDbContext Contexto;

        public ClienteController(TerraNovaDbContext contexto)
        {
            Contexto = contexto;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var RespuestaApi = new ResponseAPI<List<ClienteDTO>>();
            var ListaClientes = new List<ClienteDTO>();
            try
            {
                foreach (var Cliente in await Contexto.Clientes.ToListAsync())
                {
                    ListaClientes.Add(new ClienteDTO
                    {
                        Id = Cliente.Id,
                        Nombre = Cliente.Nombre,
                        Apellido = Cliente.Apellido,
                        Ci = Cliente.Ci,
                        Telefono = Cliente.Telefono,
                        Email = Cliente.Email,
                        Direccion = Cliente.Direccion,
                        FechaRegistro = Cliente.FechaRegistro,
                        TipoCliente = Cliente.TipoCliente
                    });
                }
                RespuestaApi.EsCorrecto = true;
                RespuestaApi.Valor = ListaClientes;
                RespuestaApi.Mensaje = "Lista de Clientes Preparada";
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
        public async Task<IActionResult> Guardar(ClienteDTO ObjCliente)
        {
            var RespuestaAPI = new ResponseAPI<int>();
            try
            {
                var DatosCliente = new Cliente
                {
                    Nombre = ObjCliente.Nombre,
                    Apellido = ObjCliente.Apellido,
                    Ci = ObjCliente.Ci,
                    Telefono = ObjCliente.Telefono,
                    Email = ObjCliente.Email,
                    Direccion = ObjCliente.Direccion,
                    FechaRegistro = ObjCliente.FechaRegistro,
                    TipoCliente = ObjCliente.TipoCliente
                };
                Contexto.Clientes.Add(DatosCliente);
                await Contexto.SaveChangesAsync();
                if (DatosCliente.Id != 0)
                {
                    RespuestaAPI.EsCorrecto = true;
                    RespuestaAPI.Valor = DatosCliente.Id;
                    RespuestaAPI.Mensaje = "Datos guardados correctamente";
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
            var RespuestaApi = new ResponseAPI<int>();
            try
            {
                var ClienteEliminar = await Contexto.Clientes.FirstOrDefaultAsync(c => c.Id == Cod);
                if (ClienteEliminar != null)
                {
                    Contexto.Clientes.Remove(ClienteEliminar);
                    await Contexto.SaveChangesAsync();
                    RespuestaApi.EsCorrecto = true;
                    RespuestaApi.Mensaje = "Datos del cliente eliminado";
                }
                else
                {
                    RespuestaApi.EsCorrecto = false;
                    RespuestaApi.Mensaje = "Cliente no encontrado";
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
            var RespuestaApi = new ResponseAPI<ClienteDTO>();
            var ClienteBuscado = new ClienteDTO();
            try
            {
                var ClienteBd = await Contexto.Clientes.FirstOrDefaultAsync(c => c.Id == Cod);
                if (ClienteBd != null)
                {
                    ClienteBuscado.Id = ClienteBd.Id;
                    ClienteBuscado.Nombre = ClienteBd.Nombre;
                    ClienteBuscado.Apellido = ClienteBd.Apellido;
                    ClienteBuscado.Ci = ClienteBd.Ci;
                    ClienteBuscado.Telefono = ClienteBd.Telefono;
                    ClienteBuscado.Email = ClienteBd.Email;
                    ClienteBuscado.Direccion = ClienteBd.Direccion;
                    ClienteBuscado.FechaRegistro = ClienteBd.FechaRegistro;
                    ClienteBuscado.TipoCliente = ClienteBd.TipoCliente;

                    RespuestaApi.EsCorrecto = true;
                    RespuestaApi.Valor = ClienteBuscado;
                }
                else
                {
                    RespuestaApi.EsCorrecto = false;
                    RespuestaApi.Mensaje = "Cliente No Encontrado";
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
        public async Task<IActionResult> Modificar(ClienteDTO NuevosDatos, int Cod)
        {
            var RespuestaApi = new ResponseAPI<int>();
            try
            {
                var ClienteBd = await Contexto.Clientes.FirstOrDefaultAsync(c => c.Id == Cod);

                if (ClienteBd != null)
                {
                    ClienteBd.Nombre = NuevosDatos.Nombre;
                    ClienteBd.Apellido = NuevosDatos.Apellido;
                    ClienteBd.Ci = NuevosDatos.Ci;
                    ClienteBd.Telefono = NuevosDatos.Telefono;
                    ClienteBd.Email = NuevosDatos.Email;
                    ClienteBd.Direccion = NuevosDatos.Direccion;
                    ClienteBd.TipoCliente = NuevosDatos.TipoCliente;

                    Contexto.Clientes.Update(ClienteBd);
                    await Contexto.SaveChangesAsync();

                    RespuestaApi.EsCorrecto = true;
                    RespuestaApi.Valor = ClienteBd.Id;
                }
                else
                {
                    RespuestaApi.EsCorrecto = false;
                    RespuestaApi.Mensaje = "Cliente no encontrado";
                }
            }
            catch (Exception ex)
            {
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = $"{ex.Message} | {ex.InnerException?.Message}";
            }
            return Ok(RespuestaApi);
        }

        // Validaciones de negocio: verificar duplicados antes de guardar/actualizar
        [HttpGet]
        [Route("ExisteCi/{Ci}/{IdExcluir}")]
        public async Task<IActionResult> ExisteCi(string Ci, int IdExcluir)
        {
            var existe = await Contexto.Clientes.AnyAsync(c => c.Ci == Ci && c.Id != IdExcluir);
            return Ok(existe);
        }

        [HttpGet]
        [Route("ExisteTelefono/{Telefono}/{IdExcluir}")]
        public async Task<IActionResult> ExisteTelefono(string Telefono, int IdExcluir)
        {
            var existe = await Contexto.Clientes.AnyAsync(c => c.Telefono == Telefono && c.Id != IdExcluir);
            return Ok(existe);
        }

        [HttpGet]
        [Route("ExisteEmail/{Email}/{IdExcluir}")]
        public async Task<IActionResult> ExisteEmail(string Email, int IdExcluir)
        {
            var existe = await Contexto.Clientes.AnyAsync(c => c.Email.ToLower() == Email.ToLower() && c.Id != IdExcluir);
            return Ok(existe);
        }
    }
}
