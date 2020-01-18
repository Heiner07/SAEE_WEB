using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SAEE_WEB.Models
{
    public class CorreoElectronico
    {
        string to, from, password, subject, body;
        MailMessage mailMessage;
        SmtpClient smtpClient;

        public CorreoElectronico(Asignaciones asignacion, Profesores profesor, Cursos curso, Grupos grupo)
        {
            from = "servicio.saee@gmail.com";
            password = "Saee123#";
            to = profesor.Correo;
            subject = $"Recordatorio asignación: {asignacion.Nombre}";
            body = $"¡Saludos {profesor.Nombre} {profesor.PrimerApellido} {profesor.SegundoApellido}!<br/><br/>" +
                $"A continuación, se presenta el detalle de la asignación:<br/>" +
                $"<b>Curso: </b>{curso.Nombre}<br/>" +
                $"<b>Grupo: </b>{grupo.Grupo}<br/>" +
                $"<b>Nombre: </b>{asignacion.Nombre}<br/>" +
                $"<b>Descripción: </b>{asignacion.Descripcion}<br/>" +
                $"<b>Tipo: </b>{asignacion.Tipo}<br/>" +
                $"<b>Fecha: </b>{asignacion.Fecha.ToShortDateString()}<br/><br/>" +
                $"Sistema para la Administración de Evaluaciones de los Estudiantes.<br/>" +
                $"Nota: Por favor no responder al correo.";

            mailMessage = new MailMessage(from, to, subject, body)
            {
                IsBodyHtml = true
            };

            smtpClient = new SmtpClient("smtp.gmail.com")
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Port = 587,
                Credentials = new NetworkCredential(from, password)
            };

        }

        public void Enviar()
        {
            smtpClient.Send(mailMessage);
            smtpClient.Dispose();
        }
    }
}
