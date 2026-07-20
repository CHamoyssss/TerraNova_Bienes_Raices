using TerraNova_Bienes_Raices.Models;

namespace TerraNova_Bienes_Raices.Services
{
    public class ServiceTrabajador
    {
        private List<CTrabajador> _trabajadores;

        public ServiceTrabajador()
        {
            // Datos de prueba precargados
            _trabajadores = new List<CTrabajador>
            {
                new CTrabajador { Id = 1, Nombre = "Ricardo", Apellido = "Mamani", CI = "4521369", Cargo = "Agente", Telefono = "70111222", Email = "ricardo.mamani@terranova.com", PorcentajeComision = 3.5m, FechaContratacion = new DateTime(2022, 3, 10), Activo = true },
                new CTrabajador { Id = 2, Nombre = "Daniela", Apellido = "Vargas", CI = "5632147", Cargo = "Agente", Telefono = "70222333", Email = "daniela.vargas@terranova.com", PorcentajeComision = 4.0m, FechaContratacion = new DateTime(2023, 6, 1), Activo = true },
                new CTrabajador { Id = 3, Nombre = "Fernando", Apellido = "Quispe", CI = "6741852", Cargo = "Administrador", Telefono = "70333444", Email = "fernando.quispe@terranova.com", PorcentajeComision = 0m, FechaContratacion = new DateTime(2021, 1, 15), Activo = true },
                new CTrabajador { Id = 4, Nombre = "Andrea", Apellido = "Choque", CI = "7852963", Cargo = "Recepcionista", Telefono = "70444555", Email = "andrea.choque@terranova.com", PorcentajeComision = 0m, FechaContratacion = new DateTime(2023, 9, 20), Activo = true }
            };
        }

        // Obtener todos los trabajadores
        public List<CTrabajador> ObtenerTodos()
        {
            return _trabajadores;
        }

        // Obtener solo los trabajadores activos (útil para selects de asignación)
        public List<CTrabajador> ObtenerActivos()
        {
            return _trabajadores.Where(t => t.Activo).ToList();
        }

        // Obtener un trabajador por Id
        public CTrabajador? ObtenerPorId(int id)
        {
            return _trabajadores.FirstOrDefault(t => t.Id == id);
        }

        // Registrar un nuevo trabajador
        public void Registrar(CTrabajador trabajador)
        {
            trabajador.Id = _trabajadores.Any() ? _trabajadores.Max(t => t.Id) + 1 : 1;
            _trabajadores.Add(trabajador);
        }

        // Actualizar un trabajador existente
        public void Actualizar(CTrabajador trabajador)
        {
            var existente = ObtenerPorId(trabajador.Id);
            if (existente != null)
            {
                existente.Nombre = trabajador.Nombre;
                existente.Apellido = trabajador.Apellido;
                existente.CI = trabajador.CI;
                existente.Cargo = trabajador.Cargo;
                existente.Telefono = trabajador.Telefono;
                existente.Email = trabajador.Email;
                existente.PorcentajeComision = trabajador.PorcentajeComision;
                existente.Activo = trabajador.Activo;
            }
        }

        // Eliminar un trabajador (baja lógica, más realista que borrar físicamente)
        public void Eliminar(int id)
        {
            var trabajador = ObtenerPorId(id);
            if (trabajador != null)
            {
                trabajador.Activo = false;
            }
        }

        // Buscar por nombre, apellido o cargo
        public List<CTrabajador> Buscar(string texto)
        {
            texto = texto.ToLower();
            return _trabajadores.Where(t =>
                t.Nombre.ToLower().Contains(texto) ||
                t.Apellido.ToLower().Contains(texto) ||
                t.Cargo.ToLower().Contains(texto)).ToList();
        }
    }
}
