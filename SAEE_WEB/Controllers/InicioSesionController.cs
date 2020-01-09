using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAEE_WEB.Models;

namespace SAEE_WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InicioSesionController : ControllerBase
    {
        private readonly BDSAEEContext _context;

        public InicioSesionController(BDSAEEContext context)
        {
            _context = context;
        }

        // POST: api/InicioSesion
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostInicioSesion(Usuario usuario)
        {
            Profesores profesor = await ComprobarInicioSesion(usuario, _context);
            if (profesor == null)
            {
                return BadRequest();
            }
            else
            {
                usuario.Id = profesor.Id;
                usuario.Administrador = profesor.Administrador;
                usuario.Nombre = profesor.Nombre;
                usuario.Apellido1 = profesor.PrimerApellido;
                usuario.Apellido2 = profesor.SegundoApellido;
                return usuario;
            }
        }

        static async Task<Profesores> ComprobarInicioSesion(Usuario usuario, BDSAEEContext _context)
        {
            Profesores profesor = await _context.Profesores.
                Where(P => P.Cedula.Equals(usuario.Cedula) && P.Contrasenia.Equals(usuario.Contrasenia)).FirstOrDefaultAsync();
            return profesor;
        }

        static async Task<Profesores> ComprobarInicioSesion(IHeaderDictionary headers, BDSAEEContext _context)
        {
            Usuario usuario = new Usuario()
            {
                Cedula = headers["cedula"],
                Contrasenia = headers["contrasenia"]
            };
            return await ComprobarInicioSesion(usuario, _context);
        }
    }
}
