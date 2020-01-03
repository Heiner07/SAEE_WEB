using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAEE_WEB.Models
{
    public class Asignaciones
    {
        public Asignaciones() {
          /*  Cursos = new HashSet<Cursos>();
            Estudiantes = new HashSet<Estudiantes>();
            EstudiantesXgrupos = new HashSet<EstudiantesXgrupos>();
            Grupos = new HashSet<Grupos>();*/
        }
        
        public int Id { get; set;}
        public string Tipo { get; set; }
        public int Profesor { get; set; }
        public int Curso { get; set; }
        public int Grupo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
        public Decimal Puntos { get; set; }
        public Decimal Porcentaje { get; set; }

     /*   public virtual ICollection<Cursos> Cursos { get; set; }
        public virtual ICollection<Estudiantes> Estudiantes { get; set; }
        public virtual ICollection<EstudiantesXgrupos> EstudiantesXgrupos { get; set; }
        public virtual ICollection<Grupos> Grupos { get; set; }*/
    }

}
