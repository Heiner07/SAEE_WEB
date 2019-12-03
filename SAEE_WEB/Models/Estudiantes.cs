using System;
using System.Collections.Generic;

namespace SAEE_WEB.Models
{
    public partial class Estudiantes
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
    }
}
