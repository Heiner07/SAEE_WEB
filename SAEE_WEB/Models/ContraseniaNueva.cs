using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAEE_WEB.Models
{
    public class ContraseniaNueva
    {
        [Required]
        public string Actual { get; set; }
        [Required]
        public string Nueva { get; set; }
        [Required]
        public string Confirmada { get; set; }
    }
}
