using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAEE_WEB.Models
{
    public partial class Profesores
    {
        public Profesores()
        {
            Estudiantes = new HashSet<Estudiantes>();
            EstudiantesXgrupos = new HashSet<EstudiantesXgrupos>();
            Grupos = new HashSet<Grupos>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "La cédula es requerida")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El primer apellido es requerido")]
        public string PrimerApellido { get; set; }
        [Required(ErrorMessage = "El segundo apellido es requerido")]
        public string SegundoApellido { get; set; }
        public string Correo { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Contrasenia { get; set; }
        public bool Administrador { get; set; }

        public virtual ICollection<Estudiantes> Estudiantes { get; set; }
        public virtual ICollection<EstudiantesXgrupos> EstudiantesXgrupos { get; set; }
        public virtual ICollection<Grupos> Grupos { get; set; }
    }
}
