using Blazor.Shared;
using System.Net.Http.Json;

namespace Blazor.Client.Services
{
    public class ServicioVentas
    {
        private HttpClient Http;

        public ServicioVentas(HttpClient http)
        {
            Http = http;
        }

        public async Task<List<VentaDTO>> Lista()
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<List<VentaDTO>>>("api/Venta/Lista");

            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado.Mensaje);
            }
        }

        public async Task<int> Guardar(VentaDTO ObjVenta)
        {
            var Resultado = await Http.PostAsJsonAsync("api/Venta/Guardar", ObjVenta);
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
            var Resultado = await Http.DeleteAsync($"api/Venta/Eliminar/{Cod}");
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

        public async Task<VentaDTO> Buscar(int Cod)
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<VentaDTO>>($"api/Venta/Buscar/{Cod}");
            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado?.Mensaje);
            }
        }

        public async Task<List<VentaDTO>> PorCliente(int idCliente)
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<List<VentaDTO>>>($"api/Venta/PorCliente/{idCliente}");
            if (Resultado!.EsCorrecto)
            {
                return Resultado.Valor;
            }
            else
            {
                throw new Exception(Resultado?.Mensaje);
            }
        }

        public async Task<List<VentaDTO>> PorTrabajador(int idTrabajador)
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<List<VentaDTO>>>($"api/Venta/PorTrabajador/{idTrabajador}");
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