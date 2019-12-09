using System;
using System.Collections.Generic;

namespace SAEE_WEB.Models
{
    public partial class EstudiantesXgrupos
    {
        public int Id { get; set; }
        public int IdProfesor { get; set; }
        public int IdGrupo { get; set; }
        public int IdEstudiante { get; set; }

        public virtual Estudiantes IdEstudianteNavigation { get; set; }
        public virtual Grupos IdGrupoNavigation { get; set; }
        public virtual Profesores IdProfesorNavigation { get; set; }
    }
}
