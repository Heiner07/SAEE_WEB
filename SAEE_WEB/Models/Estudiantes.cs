using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAEE_WEB.Models
{
    public partial class Estudiantes
    {
        public int Id { get; set; }
        [Required]
        [Range(100000000,9999999999,ErrorMessage="La cédula no es válida.")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        public string PrimerApellido { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        public string SegundoApellido { get; set; }
        public String Pin { get; set; }
    }
}
