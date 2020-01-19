using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAEE_WEB.Models
{
    public class ContraseniaNueva
    {
        [Required(ErrorMessage = "La contraseña actual es requerida.")]
        public string Actual { get; set; }
        [Required(ErrorMessage = "La contraseña nueva es requerida.")]
        public string Nueva { get; set; }
        [Required(ErrorMessage = "La confirmación de la contraseña nueva es requerida")]
        public string Confirmada { get; set; }
    }
}
