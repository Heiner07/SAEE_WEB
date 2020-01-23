using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAEE_WEB.Models
{
    public class EvaluacionesTodoInfo
    {
        public int Id { get; set; }
        public Cursos Curso { get; set; }
        public Grupos Grupo { get; set; }
        public Profesores Profesor { get; set; }
        public Asignaciones Asignacion { get; set; }
        //public int Estudiante { get; set; }
        public Decimal Puntos { get; set; }
        public Decimal Porcentaje { get; set; }
        public int Nota { get; set; }
        //public string Estado { get; set; }
        public int Periodo { get; set; }
    }
}
