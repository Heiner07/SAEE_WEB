using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAEE_WEB.Models
{
    public partial class Grupos
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        public string Grupo { get; set; }
        public int Anio { get; set; }
    }
}
