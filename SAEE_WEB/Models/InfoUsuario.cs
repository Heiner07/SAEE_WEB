using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAEE_WEB.Models
{
    public class InfoUsuario
    {
        [Required]
        public string Cedula { get; set; }
        [Required]
        public string Contrasenia { get; set; }
    }
}
