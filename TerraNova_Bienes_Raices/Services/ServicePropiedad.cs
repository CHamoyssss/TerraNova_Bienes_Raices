using System.Xml.Linq;
using TerraNova_Bienes_Raices.Models;

namespace TerraNova_Bienes_Raices.Services
{
    public class ServicePropiedad
    {
        private List<CPropiedad> _propiedades;

        public ServicePropiedad()
        {
            // Datos de prueba precargados
            _propiedades = new List<CPropiedad>
            {
                new CPropiedad
                {
                    Id = 1, Titulo = "Casa en Zona Sur", Tipo = "Casa", Direccion = "Calle 21 de Calacoto #100",
                    Zona = "Sur", Precio = 185000, AreaM2 = 220, Habitaciones = 4, Banios = 3, Garajes = 2,
                    Estado = "Disponible", TipoOperacion = "Venta", Descripcion = "Casa amplia con jardín y terraza.",
                    ImagenUrl = "", IdTrabajador = 1, FechaPublicacion = new DateTime(2026, 1, 10)
                },
                new CPropiedad
                {
                    Id = 2, Titulo = "Departamento Equipetrol", Tipo = "Departamento", Direccion = "Av. San Martín #450",
                    Zona = "Equipetrol", Precio = 95000, AreaM2 = 110, Habitaciones = 2, Banios = 2, Garajes = 1,
                    Estado = "Disponible", TipoOperacion = "Venta", Descripcion = "Departamento moderno, edificio con piscina.",
                    ImagenUrl = "", IdTrabajador = 2, FechaPublicacion = new DateTime(2026, 2, 5)
                },
                new CPropiedad
                {
                    Id = 3, Titulo = "Terreno Urubó", Tipo = "Terreno", Direccion = "Condominio Las Palmas",
                    Zona = "Urubó", Precio = 60000, AreaM2 = 500, Habitaciones = 0, Banios = 0, Garajes = 0,
                    Estado = "Reservada", TipoOperacion = "Venta", Descripcion = "Terreno plano, listo para construir.",
                    ImagenUrl = "", IdTrabajador = 1, FechaPublicacion = new DateTime(2025, 11, 20)
                },
                new CPropiedad
                {
                    Id = 4, Titulo = "Oficina Centro", Tipo = "Oficina", Direccion = "Calle Bolívar #300",
                    Zona = "Centro", Precio = 1200, AreaM2 = 60, Habitaciones = 0, Banios = 1, Garajes = 0,
                    Estado = "Vendida", TipoOperacion = "Alquiler", Descripcion = "Oficina en edificio corporativo.",
                    ImagenUrl = "", IdTrabajador = 2, FechaPublicacion = new DateTime(2025, 12, 1)
                }
            };
        }

        // Obtener todas las propiedades
        public List<CPropiedad> ObtenerTodas()
        {
            return _propiedades;
        }

        // Obtener solo las disponibles (para el select de RegistrarVenta)
        public List<CPropiedad> ObtenerDisponibles()
        {
            return _propiedades.Where(p => p.Estado == "Disponible").ToList();
        }

        // Obtener una propiedad por Id
        public CPropiedad? ObtenerPorId(int id)
        {
            return _propiedades.FirstOrDefault(p => p.Id == id);
        }

        // Registrar una nueva propiedad
        public void Registrar(CPropiedad propiedad)
        {
            propiedad.Id = _propiedades.Any() ? _propiedades.Max(p => p.Id) + 1 : 1;
            _propiedades.Add(propiedad);
        }

        // Actualizar una propiedad existente
        public void Actualizar(CPropiedad propiedad)
        {
            var existente = ObtenerPorId(propiedad.Id);
            if (existente != null)
            {
                existente.Titulo = propiedad.Titulo;
                existente.Tipo = propiedad.Tipo;
                existente.Direccion = propiedad.Direccion;
                existente.Zona = propiedad.Zona;
                existente.Precio = propiedad.Precio;
                existente.AreaM2 = propiedad.AreaM2;
                existente.Habitaciones = propiedad.Habitaciones;
                existente.Banios = propiedad.Banios;
                existente.Garajes = propiedad.Garajes;
                existente.Estado = propiedad.Estado;
                existente.TipoOperacion = propiedad.TipoOperacion;
                existente.Descripcion = propiedad.Descripcion;
                existente.ImagenUrl = propiedad.ImagenUrl;
                existente.IdTrabajador = propiedad.IdTrabajador;
            }
        }

        // Cambiar solo el estado (se usará al registrar una venta)
        public void CambiarEstado(int id, string nuevoEstado)
        {
            var propiedad = ObtenerPorId(id);
            if (propiedad != null)
            {
                propiedad.Estado = nuevoEstado;
            }
        }

        // Eliminar una propiedad
        public void Eliminar(int id)
        {
            var propiedad = ObtenerPorId(id);
            if (propiedad != null)
            {
                _propiedades.Remove(propiedad);
            }
        }

        // Filtrar por tipo, zona, estado o rango de precio
        public List<CPropiedad> Filtrar(string? tipo = null, string? zona = null, string? estado = null, decimal? precioMax = null)
        {
            var resultado = _propiedades.AsEnumerable();

            if (!string.IsNullOrEmpty(tipo))
                resultado = resultado.Where(p => p.Tipo == tipo);

            if (!string.IsNullOrEmpty(zona))
                resultado = resultado.Where(p => p.Zona == zona);

            if (!string.IsNullOrEmpty(estado))
                resultado = resultado.Where(p => p.Estado == estado);

            if (precioMax.HasValue)
                resultado = resultado.Where(p => p.Precio <= precioMax.Value);

            return resultado.ToList();
        }
    }
}
