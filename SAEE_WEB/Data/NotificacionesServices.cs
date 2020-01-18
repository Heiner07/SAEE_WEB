using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SAEE_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SAEE_WEB.Data
{
    public class NotificacionesServices : IHostedService, IDisposable
    {
        private IServiceProvider serviceProvider;
        private Timer timer;

        public NotificacionesServices(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(VerificarNotificaciones, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            return Task.CompletedTask;
        }

        private async void VerificarNotificaciones(object objetc)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                BDSAEEContext context = scope.ServiceProvider.GetRequiredService<BDSAEEContext>();
                List<NotificacionesCorreo> notificaciones =
                    await context.NotificacionesCorreo.Include(n => n.IdAsignacionNavigation).ToListAsync();
                DateTime fechaAsignacion;
                int diasAnticipacion;
                Profesores profesor;
                Cursos curso;
                Grupos grupo;
                foreach(NotificacionesCorreo notificacion in notificaciones)
                {
                    fechaAsignacion = notificacion.IdAsignacionNavigation.Fecha;
                    diasAnticipacion = notificacion.DiasAnticipacion;
                    if (fechaAsignacion.AddDays(diasAnticipacion * -1).Date.Equals(DateTime.Today.Date))
                    {
                        profesor = await context.Profesores.FindAsync(notificacion.IdAsignacionNavigation.Profesor);
                        grupo = await context.Grupos.FindAsync(notificacion.IdAsignacionNavigation.Grupo);
                        curso = await context.Cursos.FindAsync(notificacion.IdAsignacionNavigation.Curso);
                        new CorreoElectronico(notificacion.IdAsignacionNavigation, profesor, curso, grupo).Enviar();
                        context.Remove(notificacion);
                        try
                        {
                            await context.SaveChangesAsync();
                        }
                        catch (Exception)
                        {
                            //Manejar el error
                        }
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
