using TerraNova_Bienes_Raices.Models;
using System.Linq; // Necesario para OrderByDescending, Take, etc.
using System.Collections.Generic;

namespace TerraNova_Bienes_Raices.Services
{
    public class DashboardService
    {
        private readonly ServicePropiedad _servicePropiedad;
        private readonly ServiceCliente _serviceCliente;
        private readonly ServiceVenta _serviceVenta;
        private readonly ServiceTrabajador _serviceTrabajador;
        private readonly ServiceVisita _serviceVisita; // Agregado para potenciar el dashboard

        public DashboardService(
            ServicePropiedad servicePropiedad,
            ServiceCliente serviceCliente,
            ServiceVenta serviceVenta,
            ServiceTrabajador serviceTrabajador,
            ServiceVisita serviceVisita)
        {
            _servicePropiedad = servicePropiedad;
            _serviceCliente = serviceCliente;
            _serviceVenta = serviceVenta;
            _serviceTrabajador = serviceTrabajador;
            _serviceVisita = serviceVisita;
        }

        public DashboardVM ObtenerDashboard()
        {
            // 1. Obtener los datos de los servicios (nota: usamos los nombres exactos de tus métodos)
            var propiedades = _servicePropiedad.ObtenerTodas();
            var clientes = _serviceCliente.ObtenerTodos();
            var ventas = _serviceVenta.ObtenerTodas();
            var trabajadores = _serviceTrabajador.ObtenerTodos();
            var visitas = _serviceVisita.ObtenerTodas();

            // 2. Construir la lista de "Actividad Reciente" con datos REALES
            var actividadesReales = new List<ActividadReciente>();

            // Agregamos las propiedades más recientes como actividad
            actividadesReales.AddRange(propiedades.Select(p => new ActividadReciente
            {
                Descripcion = $"Nueva propiedad registrada: {p.Titulo}",
                Icono = "bi-house-add",
                Fecha = p.FechaPublicacion
            }));

            // Agregamos las ventas como actividad
            actividadesReales.AddRange(ventas.Select(v => new ActividadReciente
            {
                Descripcion = $"Nueva transacción ({(v.FormaPago == "Alquiler" ? "Alquiler" : "Venta")}) registrada",
                Icono = "bi-currency-dollar",
                Fecha = v.Fecha
            }));

            // Agregamos las visitas programadas recientes
            actividadesReales.AddRange(visitas.Select(v => new ActividadReciente
            {
                Descripcion = $"Visita {v.Estado.ToLower()} para la propiedad ID: {v.IdPropiedad}",
                Icono = "bi-calendar-check",
                Fecha = v.FechaVisita
            }));

            // 3. Crear y retornar el ViewModel
            var dashboard = new DashboardVM
            {
                TotalPropiedades = propiedades.Count,
                TotalClientes = clientes.Count,
                TotalVentas = ventas.Count,
                TotalTrabajadores = trabajadores.Count,

                // Si quieres puedes agregar 'TotalVisitas = visitas.Count' a tu DashboardVM

                // Últimas 5 propiedades ordenadas por su Fecha de Publicación
                UltimasPropiedades = propiedades
                    .OrderByDescending(p => p.FechaPublicacion)
                    .Take(5)
                    .ToList(),

                // Tomamos las últimas 5 actividades reales ordenadas de la más reciente a la más antigua
                ActividadReciente = actividadesReales
                    .OrderByDescending(a => a.Fecha)
                    .Take(5)
                    .ToList()
            };

            return dashboard;
        }
    }
}