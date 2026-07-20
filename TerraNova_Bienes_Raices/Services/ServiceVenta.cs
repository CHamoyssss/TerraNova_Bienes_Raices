using System.Xml.Linq;
using TerraNova_Bienes_Raices.Models;

namespace TerraNova_Bienes_Raices.Services
{
    public class ServiceVenta
    {
        private List<CVenta> _ventas;
        private readonly ServicePropiedad _servicePropiedad;

        public ServiceVenta(ServicePropiedad servicePropiedad)
        {
            _servicePropiedad = servicePropiedad;

            // Datos de prueba precargados
            _ventas = new List<CVenta>
            {
                new CVenta
                {
                    Id = 1, IdCliente = 1, IdPropiedad = 4, IdTrabajador = 2,
                    Fecha = new DateTime(2026, 3, 15), Monto = 1200, ComisionGenerada = 48,
                    FormaPago = "Contado", Estado = "Completada", Observaciones = "Contrato de alquiler firmado."
                }
            };
        }

        // Obtener todas las ventas
        public List<CVenta> ObtenerTodas()
        {
            return _ventas;
        }

        // Obtener una venta por Id
        public CVenta? ObtenerPorId(int id)
        {
            return _ventas.FirstOrDefault(v => v.Id == id);
        }

        // Registrar una nueva venta
        public void Registrar(CVenta venta)
        {
            venta.Id = _ventas.Any() ? _ventas.Max(v => v.Id) + 1 : 1;

            // Calcular comisión automáticamente según el trabajador (se hará en la página, aquí solo guarda)
            _ventas.Add(venta);

            // Actualizar el estado de la propiedad vendida
            var estadoNuevo = venta.FormaPago == "Alquiler" ? "Alquilada" : "Vendida";
            _servicePropiedad.CambiarEstado(venta.IdPropiedad, estadoNuevo);
        }

        // Eliminar / cancelar una venta (revierte la propiedad a Disponible)
        public void Eliminar(int id)
        {
            var venta = ObtenerPorId(id);
            if (venta != null)
            {
                _servicePropiedad.CambiarEstado(venta.IdPropiedad, "Disponible");
                _ventas.Remove(venta);
            }
        }

        // Ventas por cliente (para DetalleCliente)
        public List<CVenta> ObtenerPorCliente(int idCliente)
        {
            return _ventas.Where(v => v.IdCliente == idCliente).ToList();
        }

        // Ventas por trabajador (para DetalleTrabajador)
        public List<CVenta> ObtenerPorTrabajador(int idTrabajador)
        {
            return _ventas.Where(v => v.IdTrabajador == idTrabajador).ToList();
        }

        // Ventas en un rango de fechas (para reportes)
        public List<CVenta> ObtenerPorRangoFecha(DateTime desde, DateTime hasta)
        {
            return _ventas.Where(v => v.Fecha >= desde && v.Fecha <= hasta).ToList();
        }
    }
}
