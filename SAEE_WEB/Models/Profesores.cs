using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Ingrese la cédula")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese el primer apellido")]
        public string PrimerApellido { get; set; }
        [Required(ErrorMessage = "Ingrese el segundo apellido")]
        public string SegundoApellido { get; set; }
        [Required(ErrorMessage = "Ingrese el correo")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Correo { get; set; }
        [Required(ErrorMessage = "Ingrese la contraseña")]
        public string Contrasenia { get; set; }
        public bool Administrador { get; set; }

        public virtual ICollection<Cursos> Cursos { get; set; }
        public virtual ICollection<Estudiantes> Estudiantes { get; set; }
        public virtual ICollection<EstudiantesXgrupos> EstudiantesXgrupos { get; set; }
        public virtual ICollection<Grupos> Grupos { get; set; }
    }
}
