using System;
using System.Collections.Generic;

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
        public string Nombre { get; set; }
        public int CantidadPeriodos { get; set; }

        public virtual Profesores IdProfesorNavigation { get; set; }
        public virtual ICollection<CursosGrupos> CursosGrupos { get; set; }
    }
}
