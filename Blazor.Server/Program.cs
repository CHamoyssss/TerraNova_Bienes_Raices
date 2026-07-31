using Blazor.Server.Models;
using Blazor.Server.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
    {
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TerraNovaDbContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSql")));

builder.Services.AddTransient<ServicioEmail>();

builder.Services.AddCors(
    opciones => {
        opciones.AddPolicy("NuevaPolitica", app => {
            app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
    }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
