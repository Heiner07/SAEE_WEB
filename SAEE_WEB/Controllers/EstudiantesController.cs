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
    public class EstudiantesController : ControllerBase
    {
        private readonly BDSAEEContext _context;

        public EstudiantesController(BDSAEEContext context)
        {
            _context = context;
        }

        // GET: api/Estudiantes
        [HttpGet]
        [Route("GetEstudiantes")]
        public async Task<ActionResult<IEnumerable<Estudiantes>>> GetEstudiantes(int id)
        {
            return await _context.Estudiantes.Include(grupo => grupo.EstudiantesXgrupos)
                .Where(curso => curso.IdProfesor == id).ToListAsync();
        }

        // GET: api/Estudiantes/5
        [HttpGet]
        [Route("GetEstudiante")]
        public async Task<ActionResult<Estudiantes>> GetEstudiante(int id)
        {
            var estudiantes = await _context.Estudiantes.FindAsync(id);

            if (estudiantes == null)
            {
                return NotFound();
            }

            return estudiantes;
        }

        // PUT: api/Estudiantes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        [Route("PutEstudiantes")]
        public async Task<IActionResult> PutEstudiantes(Estudiantes estudiantes)
        {
            try
            {
                _context.Entry(estudiantes).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Estudiantes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Route("PostEstudiantes")]
        public async Task<ActionResult<Estudiantes>> PostEstudiantes(Estudiantes estudiantes)
        {
            _context.Estudiantes.Add(estudiantes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstudiantes", new { id = estudiantes.Id }, estudiantes);
        }

        // DELETE: api/Estudiantes/5
        [HttpDelete]
        [Route("DeleteEstudiantes")]
        public async Task<ActionResult<Estudiantes>> DeleteEstudiantes(Estudiantes estudiante)
        {
           
            if (estudiante == null)
            {
                return NotFound();
            }

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();

            return estudiante;
        }

        private bool EstudiantesExists(int id)
        {
            return _context.Estudiantes.Any(e => e.Id == id);
        }
    }
}
