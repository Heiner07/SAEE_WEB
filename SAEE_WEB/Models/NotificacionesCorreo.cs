using System;
using System.Collections.Generic;

namespace SAEE_WEB.Models
{
    public partial class NotificacionesCorreo
    {
        public int Id { get; set; }
        public int IdAsignacion { get; set; }
        public int DiasAnticipacion { get; set; }

        public virtual Asignaciones IdAsignacionNavigation { get; set; }
    }
}
