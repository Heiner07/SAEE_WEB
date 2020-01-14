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
    public class ProfesoresController : ControllerBase
    {
        private readonly BDSAEEContext _context;

        public ProfesoresController(BDSAEEContext context)
        {
            _context = context;
        }

        // GET: api/Profesores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesores>>> GetProfesores()
        {
            Profesores profesor = await InicioSesionController.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null && profesor.Administrador)
            {
                return BadRequest();
            }

            return await _context.Profesores.ToListAsync();
        }

        // GET: api/Profesores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesores>> GetProfesor(int id)
        {
            Profesores profesor = await InicioSesionController.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null && profesor.Administrador)
            {
                return BadRequest();
            }

            var profesores = await _context.Profesores.FindAsync(id);

            if (profesores == null)
            {
                return NotFound();
            }

            return profesores;
        }

        // PUT: api/Profesores/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesores(int id, Profesores profesores)
        {
            Profesores profesor = await InicioSesionController.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null && profesor.Administrador)
            {
                return BadRequest();
            }

            if (id != profesores.Id)
            {
                return BadRequest();
            }

            _context.Entry(profesores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesoresExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut]
        [Route("PutProfesor")]//Actualiza el perfil
        public async Task<IActionResult> PutProfesor(Profesores profesores)
        {
            Profesores profesor = await InicioSesionController.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null && profesor.Id != profesores.Id)
            {
                return BadRequest();
            }

            _context.Entry(profesores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Profesores
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Profesores>> PostProfesores(Profesores profesores)
        {
            Profesores profesor = await InicioSesionController.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null && profesor.Administrador)
            {
                return BadRequest();
            }

            _context.Profesores.Add(profesores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfesores", new { id = profesores.Id }, profesores);
        }

        // DELETE: api/Profesores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Profesores>> DeleteProfesores(int id)
        {
            Profesores profesor = await InicioSesionController.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null && profesor.Administrador)
            {
                return BadRequest();
            }

            var profesores = await _context.Profesores.Include(profesor => profesor.Cursos)
                .Include(profesor => profesor.EstudiantesXgrupos)
                .Include(profesor => profesor.Estudiantes)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (profesores == null)
            {
                return NotFound();
            }

            _context.Profesores.Remove(profesores);
            await _context.SaveChangesAsync();

            return profesores;
        }

        private bool ProfesoresExists(int id)
        {
            return _context.Profesores.Any(e => e.Id == id);
        }
    }
}
