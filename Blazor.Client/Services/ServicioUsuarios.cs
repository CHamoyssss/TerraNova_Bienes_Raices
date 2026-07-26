using Blazor.Shared;
using System.Net.Http.Json;

namespace Blazor.Client.Services
{
    public class ServicioUsuarios
    {
        private HttpClient Http;

        public ServicioUsuarios(HttpClient http)
        {
            Http = http;
        }

        public async Task<List<UsuarioDTO>> Lista()
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<List<UsuarioDTO>>>("api/Usuario/Lista");

            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado.Mensaje);
            }
        }

        public async Task<int> Guardar(UsuarioDTO ObjUsuario)
        {
            var Resultado = await Http.PostAsJsonAsync("api/Usuario/Guardar", ObjUsuario);
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
            var Resultado = await Http.DeleteAsync($"api/Usuario/Eliminar/{Cod}");
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

        public async Task<UsuarioDTO> Buscar(int Cod)
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<UsuarioDTO>>($"api/Usuario/Buscar/{Cod}");
            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado?.Mensaje);
            }
        }

        public async Task<int> Modificar(UsuarioDTO NuevosDatos)
        {
            var Resultado = await Http.PutAsJsonAsync($"api/Usuario/Modificar/{NuevosDatos.Id}", NuevosDatos);
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

        public async Task<bool> ExisteNombreUsuario(string nombreUsuario, int idExcluir = 0)
        {
            return await Http.GetFromJsonAsync<bool>($"api/Usuario/ExisteNombreUsuario/{nombreUsuario}/{idExcluir}");
        }
    }
}