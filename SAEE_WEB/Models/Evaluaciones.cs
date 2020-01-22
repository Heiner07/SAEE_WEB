using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAEE_WEB.Models
{
    public class Evaluaciones
    {
        public Evaluaciones() {
            /*   
               Estudiantes = new HashSet<Estudiantes>();
               Cursos = new HashSet<Cursos>();
               EstudiantesXgrupos = new HashSet<EstudiantesXgrupos>();
               Grupos = new HashSet<Grupos>();*/
        }

        public int Id { get; set;}
        public int Profesor { get; set; }
        public int Asignacion { get; set; }
        public int Estudiante { get; set; }
        public Decimal Puntos { get; set; }
        public Decimal Porcentaje { get; set; }
        public int Nota { get; set; }
        public string Estado { get; set; }
        public int Periodo { get; set; }
        public virtual Estudiantes IdEstudianteNavigation { get; set; }

        /*   public virtual ICollection<Estudiantes> Estudiantes { get; set; }
        public virtual ICollection<Cursos> Cursos { get; set; }
        public virtual ICollection<EstudiantesXgrupos> EstudiantesXgrupos { get; set; }
           public virtual ICollection<Grupos> Grupos { get; set; }*/
    }

}
