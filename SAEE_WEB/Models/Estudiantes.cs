using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAEE_WEB.Models
{
    public partial class Estudiantes
    {
        public Estudiantes()
        {
            EstudiantesXgrupos = new HashSet<EstudiantesXgrupos>();
           
        }


        public int Id { get; set; }
        public int IdProfesor { get; set; }
        [Required]
        [Range(100000000, 9999999999, ErrorMessage = "La cédula no es válida.")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El primer apellido es requerido")]
        public string PrimerApellido { get; set; }
        [Required(ErrorMessage = "El segundo apellido es requerido")]
        public string SegundoApellido { get; set; }
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres")]
        public string Pin { get; set; }

        public virtual Profesores IdProfesorNavigation { get; set; }
        public virtual ICollection<EstudiantesXgrupos> EstudiantesXgrupos { get; set; }
    }
}
