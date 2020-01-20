using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SAEE_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAEE_WEB.Controllers
{
    public class ComprobacionSesion
    {
        static async Task<Profesores> ComprobarInicioSesion(Usuario usuario, BDSAEEContext _context)
        {
            Profesores profesor = await _context.Profesores.
                Where(P => P.Cedula.Equals(usuario.Profesor.Cedula) && P.Contrasenia.Equals(usuario.Profesor.Contrasenia)).
                FirstOrDefaultAsync();
            return profesor;
        }

        public static async Task<Profesores> ComprobarInicioSesion(IHeaderDictionary headers, BDSAEEContext _context)
        {
            Usuario usuario = new Usuario()
            {
                Profesor = new Profesores()
                {
                    Cedula = headers["cedula"],
                    Contrasenia = headers["contrasenia"]
                }
            };
            return await ComprobarInicioSesion(usuario, _context);
        }
    }
}
