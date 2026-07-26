using Blazor.Shared;
using Blazored.LocalStorage;
using System.Net.Http.Json;

namespace Blazor.Client.Services
{
    public class ServicioAutenticacion
    {
        private readonly HttpClient Http;
        private readonly ILocalStorageService LocalStorage;
        private const string ClaveStorage = "usuarioActual";

        public UsuarioDTO? UsuarioActual { get; private set; }
        public bool EstaLogueado => UsuarioActual != null;
        public bool EsAdmin => UsuarioActual?.Rol == "Admin";

        public ServicioAutenticacion(HttpClient http, ILocalStorageService localStorage)
        {
            Http = http;
            LocalStorage = localStorage;
        }

        // Se llama al iniciar la app, para recuperar la sesión si el usuario ya estaba logueado
        public async Task InicializarAsync()
        {
            UsuarioActual = await LocalStorage.GetItemAsync<UsuarioDTO>(ClaveStorage);
        }

        public async Task<bool> Login(string nombreUsuario, string contrasena)
        {
            var resultado = await Http.PostAsJsonAsync("api/Usuario/Login", new LoginRequestDTO
            {
                NombreUsuario = nombreUsuario,
                Contrasena = contrasena
            });

            var respuesta = await resultado.Content.ReadFromJsonAsync<ResponseAPI<UsuarioDTO>>();

            if (respuesta!.EsCorrecto)
            {
                UsuarioActual = respuesta.Valor;
                await LocalStorage.SetItemAsync(ClaveStorage, UsuarioActual);
                return true;
            }

            return false;
        }

        public async Task CerrarSesion()
        {
            UsuarioActual = null;
            await LocalStorage.RemoveItemAsync(ClaveStorage);
        }
    }
}