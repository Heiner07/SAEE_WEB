using System;
using System.Collections.Generic;

namespace SAEE_WEB.Models
{
    public partial class Profesores
    {
        public Profesores()
        {
            Cursos = new HashSet<Cursos>();
            Estudiantes = new HashSet<Estudiantes>();
            EstudiantesXgrupos = new HashSet<EstudiantesXgrupos>();
            Grupos = new HashSet<Grupos>();
        }

        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
        public bool Administrador { get; set; }

        public virtual ICollection<Cursos> Cursos { get; set; }
        public virtual ICollection<Estudiantes> Estudiantes { get; set; }
        public virtual ICollection<EstudiantesXgrupos> EstudiantesXgrupos { get; set; }
        public virtual ICollection<Grupos> Grupos { get; set; }
    }
}
