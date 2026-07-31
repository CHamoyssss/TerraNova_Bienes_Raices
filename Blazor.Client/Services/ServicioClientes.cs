using Blazor.Shared;
using System.Net.Http.Json;

namespace Blazor.Client.Services
{
    public class ServicioClientes
    {
        private HttpClient Http;

        public ServicioClientes(HttpClient http)
        {
            Http = http;
        }

        public async Task<List<ClienteDTO>> Lista()
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<List<ClienteDTO>>>("api/Cliente/Lista");

            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado.Mensaje);
            }
        }

        public async Task<int> Guardar(ClienteDTO ObjCliente)
        {
            var Resultado = await Http.PostAsJsonAsync("api/Cliente/Guardar", ObjCliente);
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
            var Resultado = await Http.DeleteAsync($"api/Cliente/Eliminar/{Cod}");
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

        public async Task<ClienteDTO> Buscar(int Cod)
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<ClienteDTO>>($"api/Cliente/Buscar/{Cod}");
            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado?.Mensaje);
            }
        }

        public async Task<int> Modificar(ClienteDTO NuevosDatos)
        {
            var Resultado = await Http.PutAsJsonAsync($"api/Cliente/Modificar/{NuevosDatos.Id}", NuevosDatos);
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

        // Validaciones de negocio (equivalentes a tus endpoints ExisteCi/ExisteTelefono/ExisteEmail del Controller)
        public async Task<bool> ExisteCi(string ci, int idExcluir = 0)
        {
            return await Http.GetFromJsonAsync<bool>($"api/Cliente/ExisteCi/{ci}/{idExcluir}");
        }

        public async Task<bool> ExisteTelefono(string telefono, int idExcluir = 0)
        {
            return await Http.GetFromJsonAsync<bool>($"api/Cliente/ExisteTelefono/{telefono}/{idExcluir}");
        }

        public async Task<bool> ExisteEmail(string email, int idExcluir = 0)
        {
            return await Http.GetFromJsonAsync<bool>($"api/Cliente/ExisteEmail/{email}/{idExcluir}");
        }
    }
}