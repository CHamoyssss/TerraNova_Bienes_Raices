using System.Xml.Linq;
using TerraNova_Bienes_Raices.Models;

namespace TerraNova_Bienes_Raices.Services
{
    public class ServiceCliente
    {
        private List<CCliente> _clientes;

        public ServiceCliente()
        {
            // Datos de prueba precargados
            _clientes = new List<CCliente>
            {
                new CCliente { Id = 1, Nombre = "Juan", Apellido = "Pérez", CI = "5874123", Telefono = "70012345", Email = "juan.perez@mail.com", Direccion = "Av. Arce #123", TipoCliente = "Comprador" },
                new CCliente { Id = 2, Nombre = "María", Apellido = "Gonzales", CI = "6231458", Telefono = "70023456", Email = "maria.gonzales@mail.com", Direccion = "Calle Sucre #456", TipoCliente = "Vendedor" },
                new CCliente { Id = 3, Nombre = "Carlos", Apellido = "Fernández", CI = "7458963", Telefono = "70034567", Email = "carlos.fernandez@mail.com", Direccion = "Av. Busch #789", TipoCliente = "Comprador" },
                new CCliente { Id = 4, Nombre = "Valentina", Apellido = "Rojas", CI = "8123654", Telefono = "70045678", Email = "valentina.rojas@mail.com", Direccion = "Calle Comercio #321", TipoCliente = "Ambos" }
            };
        }

        // Obtener todos los clientes
        public List<CCliente> ObtenerTodos()
        {
            return _clientes;
        }

        // Obtener un cliente por Id
        public CCliente? ObtenerPorId(int id)
        {
            return _clientes.FirstOrDefault(c => c.Id == id);
        }

        // Registrar un nuevo cliente
        public void Registrar(CCliente cliente)
        {
            cliente.Id = _clientes.Any() ? _clientes.Max(c => c.Id) + 1 : 1;
            _clientes.Add(cliente);
        }

        // Actualizar un cliente existente
        public void Actualizar(CCliente cliente)
        {
            var existente = ObtenerPorId(cliente.Id);
            if (existente != null)
            {
                existente.Nombre = cliente.Nombre;
                existente.Apellido = cliente.Apellido;
                existente.CI = cliente.CI;
                existente.Telefono = cliente.Telefono;
                existente.Email = cliente.Email;
                existente.Direccion = cliente.Direccion;
                existente.TipoCliente = cliente.TipoCliente;
            }
        }

        // Eliminar un cliente
        public void Eliminar(int id)
        {
            var cliente = ObtenerPorId(id);
            if (cliente != null)
            {
                _clientes.Remove(cliente);
            }
        }

        // Buscar por nombre, apellido o CI
        public List<CCliente> Buscar(string texto)
        {
            texto = texto.ToLower();
            return _clientes.Where(c =>
                c.Nombre.ToLower().Contains(texto) ||
                c.Apellido.ToLower().Contains(texto) ||
                c.CI.Contains(texto)).ToList();
        }

        // Verifica si la CI ya existe (excluyendo al propio cliente si se está editando)
        public bool ExisteCI(string ci, int idExcluir = 0)
        {
            return _clientes.Any(c => c.CI == ci && c.Id != idExcluir);
        }

        // Verifica si el Teléfono ya existe
        public bool ExisteTelefono(string telefono, int idExcluir = 0)
        {
            return _clientes.Any(c => c.Telefono == telefono && c.Id != idExcluir);
        }

        // Verifica si el Email ya existe
        public bool ExisteEmail(string email, int idExcluir = 0)
        {
            return _clientes.Any(c => c.Email.ToLower() == email.ToLower() && c.Id != idExcluir);
        }
    }
}
