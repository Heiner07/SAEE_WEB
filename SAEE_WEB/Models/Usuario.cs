using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAEE_WEB.Models
{
    public class Usuario
    {
        //public string Id { get; set; }
        //public string Nombre { get; set; }
        //public string Cedula { get; set; }

        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
