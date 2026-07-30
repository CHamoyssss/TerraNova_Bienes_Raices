using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Models;
using Blazor.Shared;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajadorController : ControllerBase
    {
        private readonly TerraNovaDbContext Contexto;

        public TrabajadorController(TerraNovaDbContext contexto)
        {
            Contexto = contexto;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var RespuestaApi = new ResponseAPI<List<TrabajadorDTO>>();
            var ListaTrabajadores = new List<TrabajadorDTO>();
            try
            {
                foreach (var Trabajador in await Contexto.Trabajadores.ToListAsync())
                {
                    ListaTrabajadores.Add(new TrabajadorDTO
                    {
                        Id = Trabajador.Id,
                        Nombre = Trabajador.Nombre,
                        Apellido = Trabajador.Apellido,
                        Ci = Trabajador.Ci,
                        Cargo = Trabajador.Cargo,
                        Telefono = Trabajador.Telefono,
                        Email = Trabajador.Email,
                        PorcentajeComision = Trabajador.PorcentajeComision,
                        FechaContratacion = Trabajador.FechaContratacion.ToDateTime(TimeOnly.MinValue),
                        Activo = Trabajador.Activo
                    });
                }
                RespuestaApi.EsCorrecto = true;
                RespuestaApi.Valor = ListaTrabajadores;
                RespuestaApi.Mensaje = "Lista de Trabajadores Preparada";
            }
            catch (Exception ex)
            {
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = $"{ex.Message} | {ex.InnerException?.Message}";
            }
            return Ok(RespuestaApi);
        }

        [HttpGet]
        [Route("Activos")]
        public async Task<IActionResult> Activos()
        {
            var RespuestaApi = new ResponseAPI<List<TrabajadorDTO>>();
            var ListaTrabajadores = new List<TrabajadorDTO>();
            try
            {
                var trabajadores = await Contexto.Trabajadores
                    .Where(t => t.Activo)
                    .ToListAsync();

                foreach (var Trabajador in trabajadores)
                {
                    ListaTrabajadores.Add(new TrabajadorDTO
                    {
                        Id = Trabajador.Id,
                        Nombre = Trabajador.Nombre,
                        Apellido = Trabajador.Apellido,
                        Ci = Trabajador.Ci,
                        Cargo = Trabajador.Cargo,
                        Telefono = Trabajador.Telefono,
                        Email = Trabajador.Email,
                        PorcentajeComision = Trabajador.PorcentajeComision,
                        FechaContratacion = Trabajador.FechaContratacion.ToDateTime(TimeOnly.MinValue),
                        Activo = Trabajador.Activo
                    });
                }
                RespuestaApi.EsCorrecto = true;
                RespuestaApi.Valor = ListaTrabajadores;
                RespuestaApi.Mensaje = "Trabajadores activos preparados";
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
        public async Task<IActionResult> Guardar(TrabajadorDTO ObjTrabajador)
        {
            var RespuestaAPI = new ResponseAPI<int>();
            try
            {
                var DatosTrabajador = new Trabajador
                {
                    Nombre = ObjTrabajador.Nombre,
                    Apellido = ObjTrabajador.Apellido,
                    Ci = ObjTrabajador.Ci,
                    Cargo = ObjTrabajador.Cargo,
                    Telefono = ObjTrabajador.Telefono,
                    Email = ObjTrabajador.Email,
                    PorcentajeComision = ObjTrabajador.PorcentajeComision,
                    FechaContratacion = DateOnly.FromDateTime(ObjTrabajador.FechaContratacion),
                    Activo = ObjTrabajador.Activo
                };
                Contexto.Trabajadores.Add(DatosTrabajador);
                await Contexto.SaveChangesAsync();
                if (DatosTrabajador.Id != 0)
                {
                    RespuestaAPI.EsCorrecto = true;
                    RespuestaAPI.Valor = DatosTrabajador.Id;
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
            // Baja lógica (igual que en la versión anterior en memoria):
            // se marca Activo = false en vez de borrar físicamente el registro,
            // porque puede tener Propiedades, Ventas o Visitas asociadas.
            var RespuestaApi = new ResponseAPI<int>();
            try
            {
                var TrabajadorEliminar = await Contexto.Trabajadores.FirstOrDefaultAsync(t => t.Id == Cod);
                if (TrabajadorEliminar != null)
                {
                    TrabajadorEliminar.Activo = false;
                    await Contexto.SaveChangesAsync();
                    RespuestaApi.EsCorrecto = true;
                    RespuestaApi.Mensaje = "Trabajador dado de baja";
                }
                else
                {
                    RespuestaApi.EsCorrecto = false;
                    RespuestaApi.Mensaje = "Trabajador no encontrado";
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
            var RespuestaApi = new ResponseAPI<TrabajadorDTO>();
            var TrabajadorBuscado = new TrabajadorDTO();
            try
            {
                var TrabajadorBd = await Contexto.Trabajadores.FirstOrDefaultAsync(t => t.Id == Cod);
                if (TrabajadorBd != null)
                {
                    TrabajadorBuscado.Id = TrabajadorBd.Id;
                    TrabajadorBuscado.Nombre = TrabajadorBd.Nombre;
                    TrabajadorBuscado.Apellido = TrabajadorBd.Apellido;
                    TrabajadorBuscado.Ci = TrabajadorBd.Ci;
                    TrabajadorBuscado.Cargo = TrabajadorBd.Cargo;
                    TrabajadorBuscado.Telefono = TrabajadorBd.Telefono;
                    TrabajadorBuscado.Email = TrabajadorBd.Email;
                    TrabajadorBuscado.PorcentajeComision = TrabajadorBd.PorcentajeComision;
                    TrabajadorBuscado.FechaContratacion = TrabajadorBd.FechaContratacion.ToDateTime(TimeOnly.MinValue);
                    TrabajadorBuscado.Activo = TrabajadorBd.Activo;

                    RespuestaApi.EsCorrecto = true;
                    RespuestaApi.Valor = TrabajadorBuscado;
                }
                else
                {
                    RespuestaApi.EsCorrecto = false;
                    RespuestaApi.Mensaje = "Trabajador No Encontrado";
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
        public async Task<IActionResult> Modificar(TrabajadorDTO NuevosDatos, int Cod)
        {
            var RespuestaApi = new ResponseAPI<int>();
            try
            {
                var TrabajadorBd = await Contexto.Trabajadores.FirstOrDefaultAsync(t => t.Id == Cod);

                if (TrabajadorBd != null)
                {
                    TrabajadorBd.Nombre = NuevosDatos.Nombre;
                    TrabajadorBd.Apellido = NuevosDatos.Apellido;
                    TrabajadorBd.Ci = NuevosDatos.Ci;
                    TrabajadorBd.Cargo = NuevosDatos.Cargo;
                    TrabajadorBd.Telefono = NuevosDatos.Telefono;
                    TrabajadorBd.Email = NuevosDatos.Email;
                    TrabajadorBd.PorcentajeComision = NuevosDatos.PorcentajeComision;
                    TrabajadorBd.FechaContratacion = DateOnly.FromDateTime(NuevosDatos.FechaContratacion);
                    TrabajadorBd.Activo = NuevosDatos.Activo;

                    Contexto.Trabajadores.Update(TrabajadorBd);
                    await Contexto.SaveChangesAsync();

                    RespuestaApi.EsCorrecto = true;
                    RespuestaApi.Valor = TrabajadorBd.Id;
                }
                else
                {
                    RespuestaApi.EsCorrecto = false;
                    RespuestaApi.Mensaje = "Trabajador no encontrado";
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