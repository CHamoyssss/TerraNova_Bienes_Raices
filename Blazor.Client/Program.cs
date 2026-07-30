using Blazor.Client;
using Blazor.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ServicioClientes>();
builder.Services.AddScoped<ServicioTrabajadores>();
builder.Services.AddScoped<ServicioPropiedades>();
builder.Services.AddScoped<ServicioVentas>();
builder.Services.AddScoped<ServicioVisitas>();
builder.Services.AddScoped<ServicioUsuarios>();
builder.Services.AddScoped<ServicioAutenticacion>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<Blazor.Client.Services.ToastService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5150/") });

await builder.Build().RunAsync();
