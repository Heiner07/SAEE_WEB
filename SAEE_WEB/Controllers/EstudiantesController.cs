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
        public async Task<ActionResult<IEnumerable<Estudiantes>>> GetEstudiantes()
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }
            return await _context.Estudiantes.Include(grupo => grupo.EstudiantesXgrupos)
                .Where(curso => curso.IdProfesor == profesor.Id).ToListAsync();
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

        [HttpPut]
        [Route("PutEstudiantes")]
        public async Task<IActionResult> PutEstudiantes(Estudiantes estudiantes)
        {
            try
            {
                Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
                if (profesor == null)
                {
                    return BadRequest();
                }
                _context.Entry(estudiantes).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost]
        [Route("PostEstudiantes")]
        public async Task<ActionResult<Estudiantes>> PostEstudiantes(Estudiantes estudiantes)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }
            estudiantes.IdProfesor = profesor.Id;
            _context.Estudiantes.Add(estudiantes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstudiantes", new { id = estudiantes.Id }, estudiantes);
        }

        // DELETE: api/Estudiantes/5
        [HttpDelete]
        [Route("DeleteEstudiantes")]
        public async Task<ActionResult<Estudiantes>> DeleteEstudiantes(Estudiantes estudiante)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }
            var listaAsignaciones = _context.Evaluaciones.Where(x => x.Estudiante == estudiante.Id).ToList();
            _context.Evaluaciones.RemoveRange(listaAsignaciones);
            await _context.SaveChangesAsync();
            Estudiantes estudianteEliminar = _context.Estudiantes.Where(x => x.Id == estudiante.Id).Include(EG => EG.EstudiantesXgrupos)
                    .FirstOrDefault();

            _context.Remove(estudianteEliminar);
            await _context.SaveChangesAsync();

            return estudiante;
        }



        //DELETE TODOS LOS ESTUDIANTES
        [HttpDelete]
        [Route("DeleteAllEstudiantes")]
        public async Task<Boolean> DeleteAllEstudiantes()
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return false;
            }
             try
            {
                var listaEstudiantes = _context.Estudiantes.Where(x => x.IdProfesor == profesor.Id).Include(EG => EG.EstudiantesXgrupos);
                _context.Estudiantes.RemoveRange(listaEstudiantes);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;

        }


    }
}
