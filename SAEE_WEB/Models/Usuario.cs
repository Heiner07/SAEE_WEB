using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAEE_WEB.Models
{
    public class Usuario
    {
        public Profesores Profesor { get; set; }

        public string HttpHeader { get; set; }
    }
}
