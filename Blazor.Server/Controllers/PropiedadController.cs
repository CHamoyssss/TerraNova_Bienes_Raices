using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Models;
using Blazor.Shared;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class PropiedadController : ControllerBase
    {
        private readonly TerraNovaDbContext Contexto;

        public PropiedadController(TerraNovaDbContext contexto)
        {
            Contexto = contexto;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var RespuestaApi = new ResponseAPI<List<PropiedadDTO>>();
            var ListaPropiedades = new List<PropiedadDTO>();
            try
            {
                var propiedades = await Contexto.Propiedades
                    .Include(p => p.IdTrabajadorNavigation)
                    .ToListAsync();

                foreach (var Propiedad in propiedades)
                {
                    ListaPropiedades.Add(new PropiedadDTO
                    {
                        Id = Propiedad.Id,
                        Titulo = Propiedad.Titulo,
                        Tipo = Propiedad.Tipo,
                        Direccion = Propiedad.Direccion,
                        Zona = Propiedad.Zona,
                        Precio = Propiedad.Precio,
                        AreaM2 = Propiedad.AreaM2,
                        Habitaciones = Propiedad.Habitaciones,
                        Banios = Propiedad.Banios,
                        Garajes = Propiedad.Garajes,
                        Estado = Propiedad.Estado,
                        TipoOperacion = Propiedad.TipoOperacion,
                        Descripcion = Propiedad.Descripcion,
                        ImagenUrl = Propiedad.ImagenUrl,
                        IdTrabajador = Propiedad.IdTrabajador,
                        FechaPublicacion = Propiedad.FechaPublicacion,
                        NombreTrabajador = Propiedad.IdTrabajadorNavigation.Nombre + " " + Propiedad.IdTrabajadorNavigation.Apellido,
                        Latitud = Propiedad.Latitud,
                        Longitud = Propiedad.Longitud
                    });
                }
                RespuestaApi.EsCorrecto = true;
                RespuestaApi.Valor = ListaPropiedades;
                RespuestaApi.Mensaje = "Lista de Propiedades Preparada";
            }
            catch (Exception ex)
            {
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = ex.Message;
            }
            return Ok(RespuestaApi);
        }

        [HttpGet]
        [Route("Disponibles")]
        public async Task<IActionResult> Disponibles()
        {
            var RespuestaApi = new ResponseAPI<List<PropiedadDTO>>();
            var ListaPropiedades = new List<PropiedadDTO>();
            try
            {
                var propiedades = await Contexto.Propiedades
                    .Include(p => p.IdTrabajadorNavigation)
                    .Where(p => p.Estado == "Disponible")
                    .ToListAsync();

                foreach (var Propiedad in propiedades)
                {
                    ListaPropiedades.Add(new PropiedadDTO
                    {
                        Id = Propiedad.Id,
                        Titulo = Propiedad.Titulo,
                        Tipo = Propiedad.Tipo,
                        Direccion = Propiedad.Direccion,
                        Zona = Propiedad.Zona,
                        Precio = Propiedad.Precio,
                        AreaM2 = Propiedad.AreaM2,
                        Habitaciones = Propiedad.Habitaciones,
                        Banios = Propiedad.Banios,
                        Garajes = Propiedad.Garajes,
                        Estado = Propiedad.Estado,
                        TipoOperacion = Propiedad.TipoOperacion,
                        Descripcion = Propiedad.Descripcion,
                        ImagenUrl = Propiedad.ImagenUrl,
                        IdTrabajador = Propiedad.IdTrabajador,
                        FechaPublicacion = Propiedad.FechaPublicacion,
                        NombreTrabajador = Propiedad.IdTrabajadorNavigation.Nombre + " " + Propiedad.IdTrabajadorNavigation.Apellido,
                        Latitud = Propiedad.Latitud,
                        Longitud = Propiedad.Longitud
                    });
                }
                RespuestaApi.EsCorrecto = true;
                RespuestaApi.Valor = ListaPropiedades;
                RespuestaApi.Mensaje = "Propiedades disponibles preparadas";
            }
            catch (Exception ex)
            {
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = ex.Message;
            }
            return Ok(RespuestaApi);
        }

        [HttpGet]
        [Route("Buscar/{Cod}")]
        public async Task<IActionResult> Buscar(int Cod)
        {
            var RespuestaApi = new ResponseAPI<PropiedadDTO>();
            var PropiedadBuscada = new PropiedadDTO();
            try
            {
                var PropiedadBd = await Contexto.Propiedades
                    .Include(p => p.IdTrabajadorNavigation)
                    .FirstOrDefaultAsync(p => p.Id == Cod);

                if (PropiedadBd != null)
                {
                    PropiedadBuscada.Id = PropiedadBd.Id;
                    PropiedadBuscada.Titulo = PropiedadBd.Titulo;
                    PropiedadBuscada.Tipo = PropiedadBd.Tipo;
                    PropiedadBuscada.Direccion = PropiedadBd.Direccion;
                    PropiedadBuscada.Zona = PropiedadBd.Zona;
                    PropiedadBuscada.Precio = PropiedadBd.Precio;
                    PropiedadBuscada.AreaM2 = PropiedadBd.AreaM2;
                    PropiedadBuscada.Habitaciones = PropiedadBd.Habitaciones;
                    PropiedadBuscada.Banios = PropiedadBd.Banios;
                    PropiedadBuscada.Garajes = PropiedadBd.Garajes;
                    PropiedadBuscada.Estado = PropiedadBd.Estado;
                    PropiedadBuscada.TipoOperacion = PropiedadBd.TipoOperacion;
                    PropiedadBuscada.Descripcion = PropiedadBd.Descripcion;
                    PropiedadBuscada.ImagenUrl = PropiedadBd.ImagenUrl;
                    PropiedadBuscada.IdTrabajador = PropiedadBd.IdTrabajador;
                    PropiedadBuscada.FechaPublicacion = PropiedadBd.FechaPublicacion;
                    PropiedadBuscada.NombreTrabajador = PropiedadBd.IdTrabajadorNavigation.Nombre + " " + PropiedadBd.IdTrabajadorNavigation.Apellido;
                    PropiedadBuscada.Latitud = PropiedadBd.Latitud;
                    PropiedadBuscada.Longitud = PropiedadBd.Longitud;

                    RespuestaApi.EsCorrecto = true;
                    RespuestaApi.Valor = PropiedadBuscada;
                }
                else
                {
                    RespuestaApi.EsCorrecto = false;
                    RespuestaApi.Mensaje = "Propiedad No Encontrada";
                }
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
    public async Task<IActionResult> Guardar(PropiedadDTO ObjPropiedad)
    {
        var RespuestaAPI = new ResponseAPI<int>();
        try
        {
            var DatosPropiedad = new Propiedad
            {
                Titulo = ObjPropiedad.Titulo,
                Tipo = ObjPropiedad.Tipo,
                Direccion = ObjPropiedad.Direccion,
                Zona = ObjPropiedad.Zona,
                Precio = ObjPropiedad.Precio,
                AreaM2 = ObjPropiedad.AreaM2,
                Habitaciones = ObjPropiedad.Habitaciones,
                Banios = ObjPropiedad.Banios,
                Garajes = ObjPropiedad.Garajes,
                Estado = ObjPropiedad.Estado,
                TipoOperacion = ObjPropiedad.TipoOperacion,
                Descripcion = ObjPropiedad.Descripcion,
                ImagenUrl = ObjPropiedad.ImagenUrl,
                IdTrabajador = ObjPropiedad.IdTrabajador,
                FechaPublicacion = ObjPropiedad.FechaPublicacion,
                Latitud = ObjPropiedad.Latitud,
                Longitud = ObjPropiedad.Longitud
            };
            Contexto.Propiedades.Add(DatosPropiedad);
            await Contexto.SaveChangesAsync();
            if (DatosPropiedad.Id != 0)
            {
                RespuestaAPI.EsCorrecto = true;
                RespuestaAPI.Valor = DatosPropiedad.Id;
                RespuestaAPI.Mensaje = "Propiedad guardada correctamente";
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
            RespuestaAPI.Mensaje = ex.Message;
        }
        return Ok(RespuestaAPI);
    }

    [HttpPut]
    [Route("Modificar/{Cod}")]
    public async Task<IActionResult> Modificar(PropiedadDTO NuevosDatos, int Cod)
    {
        var RespuestaApi = new ResponseAPI<int>();
        try
        {
            var PropiedadBd = await Contexto.Propiedades.FirstOrDefaultAsync(p => p.Id == Cod);
            if (PropiedadBd != null)
            {
                PropiedadBd.Titulo = NuevosDatos.Titulo;
                PropiedadBd.Tipo = NuevosDatos.Tipo;
                PropiedadBd.Direccion = NuevosDatos.Direccion;
                PropiedadBd.Zona = NuevosDatos.Zona;
                PropiedadBd.Precio = NuevosDatos.Precio;
                PropiedadBd.AreaM2 = NuevosDatos.AreaM2;
                PropiedadBd.Habitaciones = NuevosDatos.Habitaciones;
                PropiedadBd.Banios = NuevosDatos.Banios;
                PropiedadBd.Garajes = NuevosDatos.Garajes;
                PropiedadBd.Estado = NuevosDatos.Estado;
                PropiedadBd.TipoOperacion = NuevosDatos.TipoOperacion;
                PropiedadBd.Descripcion = NuevosDatos.Descripcion;
                PropiedadBd.ImagenUrl = NuevosDatos.ImagenUrl;
                PropiedadBd.IdTrabajador = NuevosDatos.IdTrabajador;
                PropiedadBd.Latitud = NuevosDatos.Latitud;
                PropiedadBd.Longitud = NuevosDatos.Longitud;

                Contexto.Propiedades.Update(PropiedadBd);
                await Contexto.SaveChangesAsync();

                RespuestaApi.EsCorrecto = true;
                RespuestaApi.Valor = PropiedadBd.Id;
            }
            else
            {
                RespuestaApi.EsCorrecto = false;
                RespuestaApi.Mensaje = "Propiedad no encontrada";
            }
        }
        catch (Exception ex)
        {
            RespuestaApi.EsCorrecto = false;
            RespuestaApi.Mensaje = ex.Message;
        }
        return Ok(RespuestaApi);
    }
}