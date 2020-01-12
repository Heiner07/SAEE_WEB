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
    public class CursosGruposController : ControllerBase
    {
        private readonly BDSAEEContext _context;

        public CursosGruposController(BDSAEEContext context)
        {
            _context = context;
        }

        // GET: api/CursosGrupos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursosGrupos>>> GetCursosGrupos()
        {
            Profesores profesor = await InicioSesionController.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }

            return await _context.CursosGrupos.ToListAsync();
        }

        // GET: api/CursosGrupos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CursosGrupos>>> GetCursosGrupos(int id)
        {
            Profesores profesor = await InicioSesionController.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }

            var cursosGrupos = await _context.CursosGrupos.Include(cursoGrupo => cursoGrupo.IdGrupoNavigation).
                Where(cursoGrupo => cursoGrupo.IdCurso == id).ToListAsync();

            if (cursosGrupos == null)
            {
                return NotFound();
            }

            return cursosGrupos;
        }

        // PUT: api/CursosGrupos/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCursosGrupos(int id, CursosGrupos cursosGrupos)
        {
            Profesores profesor = await InicioSesionController.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }

            if (id != cursosGrupos.Id)
            {
                return BadRequest();
            }

            _context.Entry(cursosGrupos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursosGruposExists(id))
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
        [Route("DeleteCursosGrupos")]
        public async Task<IActionResult> DeleteCursosGrupos(List<CursosGrupos> cursosGrupos)
        {
            Profesores profesor = await InicioSesionController.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }

            _context.CursosGrupos.RemoveRange(cursosGrupos);

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

        // POST: api/CursosGrupos
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<CursosGrupos>> PostCursosGrupos(CursosGrupos cursosGrupos)
        //{
        //    _context.CursosGrupos.Add(cursosGrupos);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCursosGrupos", new { id = cursosGrupos.Id }, cursosGrupos);
        //}
        [HttpPut]
        [Route("PostCursosGrupos")]
        public async Task<IActionResult> PostCursosGrupos(List<CursosGrupos> cursosGrupos)
        {
            Profesores profesor = await InicioSesionController.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }

            _context.CursosGrupos.AddRange(cursosGrupos);

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

        // DELETE: api/CursosGrupos/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<CursosGrupos>> DeleteCursosGrupos(int id)
        //{
        //    var cursosGrupos = await _context.CursosGrupos.FindAsync(id);
        //    if (cursosGrupos == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.CursosGrupos.Remove(cursosGrupos);
        //    await _context.SaveChangesAsync();

        //    return cursosGrupos;
        //}

        private bool CursosGruposExists(int id)
        {
            return _context.CursosGrupos.Any(e => e.Id == id);
        }
    }
}
