﻿using System;
using System.Collections.Generic;

namespace SAEE_WEB.Models
{
    public partial class Grupos
    {
        public Grupos()
        {
            CursosGrupos = new HashSet<CursosGrupos>();
            EstudiantesXgrupos = new HashSet<EstudiantesXgrupos>();
        }

        public int Id { get; set; }
        public int IdProfesor { get; set; }
        public string Grupo { get; set; }
        public int Anio { get; set; }

        public virtual Profesores IdProfesorNavigation { get; set; }
        public virtual ICollection<CursosGrupos> CursosGrupos { get; set; }
        public virtual ICollection<EstudiantesXgrupos> EstudiantesXgrupos { get; set; }
    }   
}
