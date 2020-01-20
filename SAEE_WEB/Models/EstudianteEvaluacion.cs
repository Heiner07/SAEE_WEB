using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAEE_WEB.Models
{
    public class EstudianteEvaluacion
    {
        public EstudianteEvaluacion() {
          /*  Cursos = new HashSet<Cursos>();
            Estudiantes = new HashSet<Estudiantes>();
            EstudiantesXgrupos = new HashSet<EstudiantesXgrupos>();
            Grupos = new HashSet<Grupos>();*/
        }

        public Evaluaciones evaluacion { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public Decimal Puntos { get; set; }
        public Decimal Porcentaje { get; set; }
        public int Nota { get; set; }
        public string Estado { get; set; }

        /*   public virtual ICollection<Cursos> Cursos { get; set; }
           public virtual ICollection<Estudiantes> Estudiantes { get; set; }
           public virtual ICollection<EstudiantesXgrupos> EstudiantesXgrupos { get; set; }
           public virtual ICollection<Grupos> Grupos { get; set; }*/
    }

}
