using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAEE_WEB.Models
{
    public partial class Cursos
    {
        public Cursos()
        {
            CursosGrupos = new HashSet<CursosGrupos>();
        }

        public int Id { get; set; }
        public int IdProfesor { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La cantidad de periodos es requerida")]
        public int CantidadPeriodos { get; set; }

        public virtual Profesores IdProfesorNavigation { get; set; }
        public virtual ICollection<CursosGrupos> CursosGrupos { get; set; }
    }
}
