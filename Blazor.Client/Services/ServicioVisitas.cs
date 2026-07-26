using Blazor.Shared;
using System.Net.Http.Json;

namespace Blazor.Client.Services
{
    public class ServicioVisitas
    {
        private HttpClient Http;

        public ServicioVisitas(HttpClient http)
        {
            Http = http;
        }

        public async Task<List<VisitaDTO>> Lista()
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<List<VisitaDTO>>>("api/Visita/Lista");

            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado.Mensaje);
            }
        }

        public async Task<List<VisitaDTO>> Proximas()
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<List<VisitaDTO>>>("api/Visita/Proximas");

            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado.Mensaje);
            }
        }

        public async Task<int> Guardar(VisitaDTO ObjVisita)
        {
            var Resultado = await Http.PostAsJsonAsync("api/Visita/Guardar", ObjVisita);
            var Respuesta = await Resultado.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            if (Respuesta!.EsCorrecto)
            {
                return Respuesta.Valor;
            }
            else
            {
                throw new Exception(Respuesta.Mensaje);
            }
        }

        public async Task<bool> Eliminar(int Cod)
        {
            var Resultado = await Http.DeleteAsync($"api/Visita/Eliminar/{Cod}");
            var Respuesta = await Resultado.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            if (Respuesta!.EsCorrecto)
            {
                return Respuesta.EsCorrecto;
            }
            else
            {
                throw new Exception(Respuesta?.Mensaje);
            }
        }

        public async Task<VisitaDTO> Buscar(int Cod)
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<VisitaDTO>>($"api/Visita/Buscar/{Cod}");
            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado?.Mensaje);
            }
        }

        public async Task<int> Modificar(VisitaDTO NuevosDatos)
        {
            var Resultado = await Http.PutAsJsonAsync($"api/Visita/Modificar/{NuevosDatos.Id}", NuevosDatos);
            var Respuesta = await Resultado.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            if (Respuesta!.EsCorrecto)
            {
                return Respuesta.Valor;
            }
            else
            {
                throw new Exception(Respuesta?.Mensaje);
            }
        }

        public async Task<List<VisitaDTO>> PorPropiedad(int idPropiedad)
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<List<VisitaDTO>>>($"api/Visita/PorPropiedad/{idPropiedad}");
            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado?.Mensaje);
            }
        }
    }
}