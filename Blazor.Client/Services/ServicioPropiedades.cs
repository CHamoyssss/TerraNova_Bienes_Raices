using Blazor.Shared;
using System.Net.Http.Json;

namespace Blazor.Client.Services
{
    public class ServicioPropiedades
    {
        private HttpClient Http;

        public ServicioPropiedades(HttpClient http)
        {
            Http = http;
        }

        public async Task<List<PropiedadDTO>> Lista()
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<List<PropiedadDTO>>>("api/Propiedad/Lista");

            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado.Mensaje);
            }
        }

        public async Task<List<PropiedadDTO>> Disponibles()
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<List<PropiedadDTO>>>("api/Propiedad/Disponibles");

            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado.Mensaje);
            }
        }

        public async Task<int> Guardar(PropiedadDTO ObjPropiedad)
        {
            var Resultado = await Http.PostAsJsonAsync("api/Propiedad/Guardar", ObjPropiedad);
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
            var Resultado = await Http.DeleteAsync($"api/Propiedad/Eliminar/{Cod}");
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

        public async Task<PropiedadDTO> Buscar(int Cod)
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<PropiedadDTO>>($"api/Propiedad/Buscar/{Cod}");
            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado?.Mensaje);
            }
        }

        public async Task<int> Modificar(PropiedadDTO NuevosDatos)
        {
            var Resultado = await Http.PutAsJsonAsync($"api/Propiedad/Modificar/{NuevosDatos.Id}", NuevosDatos);
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

        public async Task<int> CambiarEstado(int Cod, string NuevoEstado)
        {
            var Resultado = await Http.PutAsync($"api/Propiedad/CambiarEstado/{Cod}/{NuevoEstado}", null);
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