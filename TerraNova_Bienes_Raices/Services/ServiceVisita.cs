using System.Xml.Linq;
using TerraNova_Bienes_Raices.Models;

namespace TerraNova_Bienes_Raices.Services
{
    public class ServiceVisita
    {
        private List<CVisita> _visitas;
        private readonly ServicePropiedad _servicePropiedad;

        public ServiceVisita(ServicePropiedad servicePropiedad)
        {
            _servicePropiedad = servicePropiedad;

            // Datos de prueba precargados
            _visitas = new List<CVisita>
            {
                new CVisita
                {
                    Id = 1, IdCliente = 1, IdPropiedad = 1, IdTrabajador = 1,
                    FechaVisita = new DateTime(2026, 7, 25, 10, 0, 0),
                    Estado = "Programada", Comentarios = "Cliente interesado, primera visita."
                },
                new CVisita
                {
                    Id = 2, IdCliente = 3, IdPropiedad = 2, IdTrabajador = 2,
                    FechaVisita = new DateTime(2026, 7, 20, 15, 30, 0),
                    Estado = "Realizada", Comentarios = "Le gustó, evaluando oferta."
                }
            };
        }

        // Obtener todas las visitas
        public List<CVisita> ObtenerTodas()
        {
            return _visitas;
        }

        // Obtener una visita por Id
        public CVisita? ObtenerPorId(int id)
        {
            return _visitas.FirstOrDefault(v => v.Id == id);
        }

        // Registrar una nueva visita
        public void Registrar(CVisita visita)
        {
            visita.Id = _visitas.Any() ? _visitas.Max(v => v.Id) + 1 : 1;
            _visitas.Add(visita);
        }

        // Actualizar una visita existente
        public void Actualizar(CVisita visita)
        {
            var existente = ObtenerPorId(visita.Id);
            if (existente != null)
            {
                existente.IdCliente = visita.IdCliente;
                existente.IdPropiedad = visita.IdPropiedad;
                existente.IdTrabajador = visita.IdTrabajador;
                existente.FechaVisita = visita.FechaVisita;
                existente.Estado = visita.Estado;
                existente.Comentarios = visita.Comentarios;
            }
        }

        // Cambiar el estado de una visita (Programada -> Realizada / Cancelada)
        public void CambiarEstado(int id, string nuevoEstado)
        {
            var visita = ObtenerPorId(id);
            if (visita != null)
            {
                visita.Estado = nuevoEstado;
            }
        }

        // Eliminar una visita
        public void Eliminar(int id)
        {
            var visita = ObtenerPorId(id);
            if (visita != null)
            {
                _visitas.Remove(visita);
            }
        }

        // Visitas por propiedad (para mostrar en DetallePropiedad)
        public List<CVisita> ObtenerPorPropiedad(int idPropiedad)
        {
            return _visitas.Where(v => v.IdPropiedad == idPropiedad).ToList();
        }

        // Visitas por trabajador (agenda del agente)
        public List<CVisita> ObtenerPorTrabajador(int idTrabajador)
        {
            return _visitas.Where(v => v.IdTrabajador == idTrabajador).ToList();
        }

        // Visitas próximas (Estado = Programada, ordenadas por fecha)
        public List<CVisita> ObtenerProximas()
        {
            return _visitas
                .Where(v => v.Estado == "Programada")
                .OrderBy(v => v.FechaVisita)
                .ToList();
        }
    }
}
