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

            if (Resultado != null && Resultado.EsCorrecto)
            {
                return Resultado.Valor!;
            }
            else
            {
                throw new Exception(Resultado?.Mensaje ?? "Error al obtener la lista de ventas");
            }
        }

        public async Task<int> Guardar(VentaDTO ObjVenta)
        {
            var Resultado = await Http.PostAsJsonAsync("api/Venta/Guardar", ObjVenta);

            if (!Resultado.IsSuccessStatusCode)
            {
                var errorBody = await Resultado.Content.ReadAsStringAsync();
                throw new Exception($"Error del servidor ({(int)Resultado.StatusCode}): {errorBody}");
            }

            var Respuesta = await Resultado.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            if (Respuesta != null && Respuesta.EsCorrecto)
            {
                return Respuesta.Valor;
            }
            else
            {
                throw new Exception(Respuesta?.Mensaje ?? "Error desconocido al registrar la venta");
            }
        }

        public async Task<bool> Eliminar(int Cod)
        {
            var Resultado = await Http.DeleteAsync($"api/Venta/Eliminar/{Cod}");
            var Respuesta = await Resultado.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            if (Respuesta != null && Respuesta.EsCorrecto)
            {
                return Respuesta.Valor > 0;
            }
            else
            {
                throw new Exception(Respuesta?.Mensaje ?? "Error al eliminar la venta");
            }
        }

        public async Task<VentaDTO> Buscar(int Cod)
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<VentaDTO>>($"api/Venta/Buscar/{Cod}");
            if (Resultado != null && Resultado.EsCorrecto)
            {
                return Resultado.Valor!;
            }
            else
            {
                throw new Exception(Resultado?.Mensaje ?? "Error al buscar la venta");
            }
        }

        public async Task<int> Modificar(VentaDTO NuevosDatos)
        {
            var Resultado = await Http.PutAsJsonAsync($"api/Venta/Modificar/{NuevosDatos.Id}", NuevosDatos);
            if (!Resultado.IsSuccessStatusCode)
            {
                var errorBody = await Resultado.Content.ReadAsStringAsync();
                throw new Exception($"Error del servidor ({(int)Resultado.StatusCode}): {errorBody}");
            }
            var Respuesta = await Resultado.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            if (Respuesta != null && Respuesta.EsCorrecto)
            {
                return Respuesta.Valor;
            }
            else
            {
                throw new Exception(Respuesta?.Mensaje ?? "Error al modificar la venta");
            }
        }

        public async Task<List<VentaDTO>> PorCliente(int idCliente)
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<List<VentaDTO>>>($"api/Venta/PorCliente/{idCliente}");
            if (Resultado != null && Resultado.EsCorrecto)
            {
                return Resultado.Valor!;
            }
            else
            {
                throw new Exception(Resultado?.Mensaje ?? "Error al obtener ventas del cliente");
            }
        }

        public async Task<List<VentaDTO>> PorTrabajador(int idTrabajador)
        {
            var Resultado = await Http.GetFromJsonAsync<ResponseAPI<List<VentaDTO>>>($"api/Venta/PorTrabajador/{idTrabajador}");
            if (Resultado != null && Resultado.EsCorrecto)
            {
                return Resultado.Valor!;
            }
            else
            {
                throw new Exception(Resultado?.Mensaje ?? "Error al obtener ventas del trabajador");
            }
        }
    }
}