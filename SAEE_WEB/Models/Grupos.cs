using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAEE_WEB.Models
{
    public partial class Grupos
    {
        public Grupos()
        {
            EstudiantesXgrupos = new HashSet<EstudiantesXgrupos>();
        }

        public int Id { get; set; }
        public int IdProfesor { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Grupo { get; set; }
        public int Anio { get; set; }

        public virtual Profesores IdProfesorNavigation { get; set; }
        public virtual ICollection<EstudiantesXgrupos> EstudiantesXgrupos { get; set; }
    }
}
