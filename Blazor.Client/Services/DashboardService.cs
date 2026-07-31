using Blazor.Shared;
using Blazor.Client.Models; 

namespace Blazor.Client.Services
{
    public class DashboardService
    {
        private readonly ServicioPropiedades _servicioPropiedades;
        private readonly ServicioClientes _servicioClientes;
        private readonly ServicioVentas _servicioVentas;
        private readonly ServicioTrabajadores _servicioTrabajadores;
        private readonly ServicioVisitas _servicioVisitas;

        public DashboardService(
            ServicioPropiedades servicioPropiedades,
            ServicioClientes servicioClientes,
            ServicioVentas servicioVentas,
            ServicioTrabajadores servicioTrabajadores,
            ServicioVisitas servicioVisitas)
        {
            _servicioPropiedades = servicioPropiedades;
            _servicioClientes = servicioClientes;
            _servicioVentas = servicioVentas;
            _servicioTrabajadores = servicioTrabajadores;
            _servicioVisitas = servicioVisitas;
        }

        public async Task<DashboardVM> ObtenerDashboardAsync()
        {
            // Se piden todos los datos en paralelo para que cargue más rápido
            var tareaPropiedades = _servicioPropiedades.Lista();
            var tareaClientes = _servicioClientes.Lista();
            var tareaVentas = _servicioVentas.Lista();
            var tareaTrabajadores = _servicioTrabajadores.Lista();
            var tareaVisitas = _servicioVisitas.Lista();

            await Task.WhenAll(tareaPropiedades, tareaClientes, tareaVentas, tareaTrabajadores, tareaVisitas);

            var propiedades = tareaPropiedades.Result;
            var clientes = tareaClientes.Result;
            var ventas = tareaVentas.Result;
            var trabajadores = tareaTrabajadores.Result;
            var visitas = tareaVisitas.Result;

            var actividadesReales = new List<ActividadReciente>();

            actividadesReales.AddRange(propiedades.Select(p => new ActividadReciente
            {
                Descripcion = $"Nueva propiedad registrada: {p.Titulo}",
                Icono = "bi-house-add",
                Fecha = p.FechaPublicacion
            }));

            actividadesReales.AddRange(ventas.Select(v => new ActividadReciente
            {
                Descripcion = $"Nueva transacción ({(v.FormaPago == "Alquiler" ? "Alquiler" : "Venta")}) registrada",
                Icono = "bi-currency-dollar",
                Fecha = v.Fecha
            }));

            actividadesReales.AddRange(visitas.Select(v => new ActividadReciente
            {
                Descripcion = $"Visita {v.Estado.ToLower()} para la propiedad ID: {v.IdPropiedad}",
                Icono = "bi-calendar-check",
                Fecha = v.FechaVisita
            }));

            var dashboard = new DashboardVM
            {
                TotalPropiedades = propiedades.Count,
                TotalClientes = clientes.Count,
                TotalVentas = ventas.Count,
                TotalTrabajadores = trabajadores.Count,
                UltimasPropiedades = propiedades
                    .OrderByDescending(p => p.FechaPublicacion)
                    .Take(5)
                    .ToList(),
                ActividadReciente = actividadesReales
                    .OrderByDescending(a => a.Fecha)
                    .Take(5)
                    .ToList()
            };

            return dashboard;
        }
    }
}
