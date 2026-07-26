using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Models;
using Blazor.Shared;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly TerraNovaDbContext Contexto;

        public VentaController(TerraNovaDbContext contexto)
        {
            Contexto = contexto;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var RespuestaApi = new ResponseAPI<List<VentaDTO>>();
            var ListaVentas = new List<VentaDTO>();
            try
            {
                var ventas = await Contexto.Ventas
                    .Include(v => v.IdClienteNavigation)
                    .Include(v => v.IdPropiedadNavigation)
                    .Include(v => v.IdTrabajadorNavigation)
                    .ToListAsync();

                foreach (var Venta in ventas)
                {
                    ListaVentas.Add(new VentaDTO
                    {
                        Id = Venta.Id,
                        IdCliente = Venta.IdCliente,
                        IdPropiedad = Venta.IdPropiedad,
                        IdTrabajador = Venta.IdTrabajador,
                        Fecha = Venta.Fecha,
                        Monto = Venta.Monto,
                        ComisionGenerada = Venta.ComisionGenerada,
                        FormaPago = Venta.FormaPago,
                        Estado = Venta.Estado,
                        Observaciones = Venta.Observaciones,
                        IdClienteNavigation = new ClienteDTO
                        {
                            Id = Venta.IdClienteNavigation.Id,
                            Nombre = Venta.IdClienteNavigation.Nombre,
                            Apellido = Venta.IdClienteNavigation.Apellido
                        },
                        IdPropiedadNavigation = new PropiedadDTO
                        {
                            Id = Venta.IdPropiedadNavigation.Id,
                            Titulo = Venta.IdPropiedadNavigation.Titulo
                        },
                        IdTrabajadorNavigation = new TrabajadorDTO
                        {
                            Id = Venta.IdTrabajadorNavigation.Id,
                            Nombre = Venta.IdTrabajadorNavigation.Nombre,
                            Apellido = Venta.IdTrabajadorNavigation.Apellido
                        }
                    });
                }
                RespuestaApi.EsCorrecto = true;
                RespuestaApi.Valor = ListaVentas;
                RespuestaApi.Mensaje = "Lista de Ventas Preparada";
            }
            catch (Exception ex)
            {
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = ex.Message;
            }
            return Ok(RespuestaApi);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(VentaDTO ObjVenta)
        {
            var RespuestaAPI = new ResponseAPI<int>();

            // Usamos una transacción: si algo falla a mitad de camino
            // (guardar la venta o actualizar la propiedad), se revierte todo.
            using var transaccion = await Contexto.Database.BeginTransactionAsync();
            try
            {
                var DatosVenta = new Venta
                {
                    IdCliente = ObjVenta.IdCliente,
                    IdPropiedad = ObjVenta.IdPropiedad,
                    IdTrabajador = ObjVenta.IdTrabajador,
                    Fecha = ObjVenta.Fecha,
                    Monto = ObjVenta.Monto,
                    ComisionGenerada = ObjVenta.ComisionGenerada,
                    FormaPago = ObjVenta.FormaPago,
                    Estado = ObjVenta.Estado,
                    Observaciones = ObjVenta.Observaciones
                };
                Contexto.Ventas.Add(DatosVenta);
                await Contexto.SaveChangesAsync();

                // Actualizar el estado de la propiedad vendida/alquilada
                var propiedad = await Contexto.Propiedades.FirstOrDefaultAsync(p => p.Id == ObjVenta.IdPropiedad);
                if (propiedad != null)
                {
                    propiedad.Estado = ObjVenta.FormaPago == "Alquiler" ? "Alquilada" : "Vendida";
                    await Contexto.SaveChangesAsync();
                }

                await transaccion.CommitAsync();

                if (DatosVenta.Id != 0)
                {
                    RespuestaAPI.EsCorrecto = true;
                    RespuestaAPI.Valor = DatosVenta.Id;
                    RespuestaAPI.Mensaje = "Venta registrada correctamente";
                }
                else
                {
                    RespuestaAPI.EsCorrecto = false;
                    RespuestaAPI.Mensaje = "Datos no guardados";
                }
            }
            catch (Exception ex)
            {
                await transaccion.RollbackAsync();
                RespuestaAPI.EsCorrecto = false;
                RespuestaAPI.Mensaje = ex.Message;
            }
            return Ok(RespuestaAPI);
        }

        [HttpDelete]
        [Route("Eliminar/{Cod}")]
        public async Task<IActionResult> Eliminar(int Cod)
        {
            var RespuestaApi = new ResponseAPI<int>();
            using var transaccion = await Contexto.Database.BeginTransactionAsync();
            try
            {
                var VentaEliminar = await Contexto.Ventas.FirstOrDefaultAsync(v => v.Id == Cod);
                if (VentaEliminar != null)
                {
                    // Al cancelar la venta, la propiedad vuelve a estar Disponible
                    var propiedad = await Contexto.Propiedades.FirstOrDefaultAsync(p => p.Id == VentaEliminar.IdPropiedad);
                    if (propiedad != null)
                    {
                        propiedad.Estado = "Disponible";
                    }

                    Contexto.Ventas.Remove(VentaEliminar);
                    await Contexto.SaveChangesAsync();
                    await transaccion.CommitAsync();

                    RespuestaApi.EsCorrecto = true;
                    RespuestaApi.Mensaje = "Venta eliminada y propiedad liberada";
                }
                else
                {
                    RespuestaApi.EsCorrecto = false;
                    RespuestaApi.Mensaje = "Venta no encontrada";
                }
            }
            catch (Exception ex)
            {
                await transaccion.RollbackAsync();
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = ex.Message;
            }
            return Ok(RespuestaApi);
        }

        [HttpGet]
        [Route("Buscar/{Cod}")]
        public async Task<IActionResult> Buscar(int Cod)
        {
            var RespuestaApi = new ResponseAPI<VentaDTO>();
            var VentaBuscada = new VentaDTO();
            try
            {
                var VentaBd = await Contexto.Ventas.FirstOrDefaultAsync(v => v.Id == Cod);
                if (VentaBd != null)
                {
                    VentaBuscada.Id = VentaBd.Id;
                    VentaBuscada.IdCliente = VentaBd.IdCliente;
                    VentaBuscada.IdPropiedad = VentaBd.IdPropiedad;
                    VentaBuscada.IdTrabajador = VentaBd.IdTrabajador;
                    VentaBuscada.Fecha = VentaBd.Fecha;
                    VentaBuscada.Monto = VentaBd.Monto;
                    VentaBuscada.ComisionGenerada = VentaBd.ComisionGenerada;
                    VentaBuscada.FormaPago = VentaBd.FormaPago;
                    VentaBuscada.Estado = VentaBd.Estado;
                    VentaBuscada.Observaciones = VentaBd.Observaciones;

                    RespuestaApi.EsCorrecto = true;
                    RespuestaApi.Valor = VentaBuscada;
                }
                else
                {
                    RespuestaApi.EsCorrecto = false;
                    RespuestaApi.Mensaje = "Venta No Encontrada";
                }
            }
            catch (Exception ex)
            {
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = ex.Message;
            }
            return Ok(RespuestaApi);
        }

        [HttpGet]
        [Route("PorCliente/{IdCliente}")]
        public async Task<IActionResult> PorCliente(int IdCliente)
        {
            var RespuestaApi = new ResponseAPI<List<VentaDTO>>();
            try
            {
                var ventas = await Contexto.Ventas
                    .Where(v => v.IdCliente == IdCliente)
                    .Select(v => new VentaDTO
                    {
                        Id = v.Id,
                        IdCliente = v.IdCliente,
                        IdPropiedad = v.IdPropiedad,
                        IdTrabajador = v.IdTrabajador,
                        Fecha = v.Fecha,
                        Monto = v.Monto,
                        ComisionGenerada = v.ComisionGenerada,
                        FormaPago = v.FormaPago,
                        Estado = v.Estado,
                        Observaciones = v.Observaciones
                    })
                    .ToListAsync();

                RespuestaApi.EsCorrecto = true;
                RespuestaApi.Valor = ventas;
                RespuestaApi.Mensaje = "Ventas del cliente preparadas";
            }
            catch (Exception ex)
            {
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = ex.Message;
            }
            return Ok(RespuestaApi);
        }

        [HttpGet]
        [Route("PorTrabajador/{IdTrabajador}")]
        public async Task<IActionResult> PorTrabajador(int IdTrabajador)
        {
            var RespuestaApi = new ResponseAPI<List<VentaDTO>>();
            try
            {
                var ventas = await Contexto.Ventas
                    .Where(v => v.IdTrabajador == IdTrabajador)
                    .Select(v => new VentaDTO
                    {
                        Id = v.Id,
                        IdCliente = v.IdCliente,
                        IdPropiedad = v.IdPropiedad,
                        IdTrabajador = v.IdTrabajador,
                        Fecha = v.Fecha,
                        Monto = v.Monto,
                        ComisionGenerada = v.ComisionGenerada,
                        FormaPago = v.FormaPago,
                        Estado = v.Estado,
                        Observaciones = v.Observaciones
                    })
                    .ToListAsync();

                RespuestaApi.EsCorrecto = true;
                RespuestaApi.Valor = ventas;
                RespuestaApi.Mensaje = "Ventas del trabajador preparadas";
            }
            catch (Exception ex)
            {
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = ex.Message;
            }
            return Ok(RespuestaApi);
        }

        [HttpPut]
    [Route("Modificar/{Cod}")]
    public async Task<IActionResult> Modificar(VentaDTO NuevosDatos, int Cod)
    {
        var RespuestaApi = new ResponseAPI<int>();
        using var transaccion = await Contexto.Database.BeginTransactionAsync();
        try
        {
            var VentaBd = await Contexto.Ventas.FirstOrDefaultAsync(v => v.Id == Cod);
            if (VentaBd != null)
            {
                VentaBd.IdCliente = NuevosDatos.IdCliente;
                VentaBd.IdPropiedad = NuevosDatos.IdPropiedad;
                VentaBd.IdTrabajador = NuevosDatos.IdTrabajador;
                VentaBd.Fecha = NuevosDatos.Fecha;
                VentaBd.Monto = NuevosDatos.Monto;
                VentaBd.ComisionGenerada = NuevosDatos.ComisionGenerada;
                VentaBd.FormaPago = NuevosDatos.FormaPago;
                VentaBd.Estado = NuevosDatos.Estado;
                VentaBd.Observaciones = NuevosDatos.Observaciones;

                Contexto.Ventas.Update(VentaBd);
                await Contexto.SaveChangesAsync();
                await transaccion.CommitAsync();

                RespuestaApi.EsCorrecto = true;
                RespuestaApi.Valor = VentaBd.Id;
            }
            else
            {
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = "Venta no encontrada";
            }
        }
        catch (Exception ex)
        {
            await transaccion.RollbackAsync();
            RespuestaApi.EsCorrecto = false;
            RespuestaApi.Mensaje = ex.Message;
        }
        return Ok(RespuestaApi);
    }
}
