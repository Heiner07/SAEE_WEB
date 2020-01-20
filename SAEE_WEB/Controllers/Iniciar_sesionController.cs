using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAEE_WEB.Models;

namespace SAEE_WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Iniciar_sesionController : ControllerBase
    {
        private readonly BDSAEEContext _context;

        public Iniciar_sesionController(BDSAEEContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Profesores>> GetProfesor()
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                BadRequest();
            }
            return profesor;
        }

        // POST: api/Iniciar_sesion
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostIniciar_sesion(Usuario usuario)
        {
            Profesores profesor = await _context.Profesores.
                Where(P => P.Cedula.Equals(usuario.Profesor.Cedula) && P.Contrasenia.Equals(usuario.Profesor.Contrasenia)).
                FirstOrDefaultAsync();
            if (profesor == null)
            {
                BadRequest();
            }
            usuario.Profesor = profesor;

            return usuario;
        }
    }
}
