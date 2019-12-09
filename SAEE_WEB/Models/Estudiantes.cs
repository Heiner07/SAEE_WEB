using System;
using System.Collections.Generic;

namespace SAEE_WEB.Models
{
    public partial class Estudiantes
    {
        public Estudiantes()
        {
            EstudiantesXgrupos = new HashSet<EstudiantesXgrupos>();
        }

        public int Id { get; set; }
        public int IdProfesor { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres")]
        public string Pin { get; set; }

        public virtual Profesores IdProfesorNavigation { get; set; }
        public virtual ICollection<EstudiantesXgrupos> EstudiantesXgrupos { get; set; }
    }
}
