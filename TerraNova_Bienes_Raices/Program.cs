using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TerraNova_Bienes_Raices;
using TerraNova_Bienes_Raices.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<ServiceCliente>();
builder.Services.AddScoped<ServiceTrabajador>();
builder.Services.AddScoped<ServicePropiedad>();
builder.Services.AddScoped<ServiceVenta>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<ServiceVisita>();
await builder.Build().RunAsync();
