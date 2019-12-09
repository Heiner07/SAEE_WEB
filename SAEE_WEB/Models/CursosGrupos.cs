using System;
using System.Collections.Generic;

namespace SAEE_WEB.Models
{
    public partial class CursosGrupos
    {
        public int Id { get; set; }
        public int IdCurso { get; set; }
        public int IdGrupo { get; set; }

        public virtual Cursos IdCursoNavigation { get; set; }
        public virtual Grupos IdGrupoNavigation { get; set; }
    }
}
