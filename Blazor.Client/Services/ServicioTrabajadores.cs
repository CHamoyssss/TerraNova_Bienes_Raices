using Blazor.Shared;
using System.Net.Http.Json;

namespace Blazor.Client.Services
{
    public class ServicioTrabajadores
    {
        private HttpClient Http;

        public ServicioTrabajadores(HttpClient http)
        {
            Http = http;
        }

        public async Task<List<TrabajadorDTO>> Lista()
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<List<TrabajadorDTO>>>("api/Trabajador/Lista");

            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado.Mensaje);
            }
        }

        public async Task<List<TrabajadorDTO>> Activos()
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<List<TrabajadorDTO>>>("api/Trabajador/Activos");

            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado.Mensaje);
            }
        }

        public async Task<int> Guardar(TrabajadorDTO ObjTrabajador)
        {
            var Resultado = await Http.PostAsJsonAsync("api/Trabajador/Guardar", ObjTrabajador);
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
            var Resultado = await Http.DeleteAsync($"api/Trabajador/Eliminar/{Cod}");
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

        public async Task<TrabajadorDTO> Buscar(int Cod)
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<TrabajadorDTO>>($"api/Trabajador/Buscar/{Cod}");
            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado?.Mensaje);
            }
        }

        public async Task<int> Modificar(TrabajadorDTO NuevosDatos)
        {
            var Resultado = await Http.PutAsJsonAsync($"api/Trabajador/Modificar/{NuevosDatos.Id}", NuevosDatos);
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
    }
}