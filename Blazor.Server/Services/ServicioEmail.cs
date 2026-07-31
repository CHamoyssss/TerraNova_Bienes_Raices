using Blazor.Server.Models;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace Blazor.Server.Services
{
    public class ServicioEmail
    {
        private readonly IConfiguration _config;
        private readonly TerraNovaDbContext _contexto;

        public ServicioEmail(IConfiguration config, TerraNovaDbContext contexto)
        {
            _config = config;
            _contexto = contexto;
        }

        public async Task EnviarNotificacionVisita(int idVisita)
        {
            var visita = await _contexto.Visitas
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdPropiedadNavigation)
                .Include(v => v.IdTrabajadorNavigation)
                .FirstOrDefaultAsync(v => v.Id == idVisita);

            if (visita == null) return;

            var servidor = _config["Smtp:Servidor"];
            var puerto = int.Parse(_config["Smtp:Puerto"]!);
            var usuario = _config["Smtp:Usuario"];
            var clave = _config["Smtp:Clave"];

            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("TerraNova Bienes Raices", usuario));
            mensaje.To.Add(new MailboxAddress(
                $"{visita.IdClienteNavigation.Nombre} {visita.IdClienteNavigation.Apellido}",
                visita.IdClienteNavigation.Email));
            mensaje.Cc.Add(new MailboxAddress(
                $"{visita.IdTrabajadorNavigation.Nombre} {visita.IdTrabajadorNavigation.Apellido}",
                visita.IdTrabajadorNavigation.Email));
            mensaje.Subject = "Nueva Visita Programada - TerraNova Bienes Raices";

            var fecha = visita.FechaVisita.ToString("dd/MM/yyyy");
            var hora = visita.FechaVisita.ToString("HH:mm");
            var comentarios = string.IsNullOrEmpty(visita.Comentarios) ? "Sin comentarios" : visita.Comentarios;

            mensaje.Body = new TextPart("html")
            {
                Text = $@"
<html>
<body style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
    <h2 style='color: #1a5276;'>TerraNova Bienes Raices</h2>
    <h3>Nueva Visita Programada</h3>
    <table style='border-collapse: collapse; width: 100%; max-width: 500px;'>
        <tr><td style='padding: 8px; font-weight: bold;'>Cliente:</td><td style='padding: 8px;'>{visita.IdClienteNavigation.Nombre} {visita.IdClienteNavigation.Apellido}</td></tr>
        <tr><td style='padding: 8px; font-weight: bold;'>Propiedad:</td><td style='padding: 8px;'>{visita.IdPropiedadNavigation.Titulo}</td></tr>
        <tr><td style='padding: 8px; font-weight: bold;'>Fecha:</td><td style='padding: 8px;'>{fecha}</td></tr>
        <tr><td style='padding: 8px; font-weight: bold;'>Hora:</td><td style='padding: 8px;'>{hora}</td></tr>
        <tr><td style='padding: 8px; font-weight: bold;'>Agente:</td><td style='padding: 8px;'>{visita.IdTrabajadorNavigation.Nombre} {visita.IdTrabajadorNavigation.Apellido}</td></tr>
        <tr><td style='padding: 8px; font-weight: bold;'>Comentarios:</td><td style='padding: 8px;'>{comentarios}</td></tr>
    </table>
    <hr style='margin-top: 20px; border: 1px solid #eee;' />
    <p style='font-size: 12px; color: #888;'>Este es un mensaje automatico del sistema TerraNova Bienes Raices.</p>
</body>
</html>"
            };

            using var cliente = new SmtpClient();
            await cliente.ConnectAsync(servidor, puerto, MailKit.Security.SecureSocketOptions.StartTls);
            await cliente.AuthenticateAsync(usuario, clave);
            await cliente.SendAsync(mensaje);
            await cliente.DisconnectAsync(true);
        }
    }
}
