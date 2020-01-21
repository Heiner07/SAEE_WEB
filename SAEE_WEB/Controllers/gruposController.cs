using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAEE_WEB.Models;

namespace SAEE_WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Gruposcontroller : ControllerBase
    {


        private readonly BDSAEEContext _context;

        public Gruposcontroller(BDSAEEContext context)
        {
            _context = context;
        }

        // GET: api/Grupos/GetGrupos?id=IDPROFESOR
        [HttpGet]
        [Route("GetGrupos")]
        public async Task<ActionResult<IEnumerable<Grupos>>> GetGrupos()
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }
            return await _context.Grupos.Where(grupo => grupo.IdProfesor == profesor.Id).Include(grupo => grupo.EstudiantesXgrupos).ToListAsync();
        }
       
        // GET: api/Grupos/GetEstudiantes?id=IDGRUPO
        [HttpGet]
        [Route("GetEG")]
        public async Task<ActionResult<IEnumerable<EstudiantesXgrupos>>> GetEG(int id)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }
            var lista = _context.EstudiantesXgrupos.Where(x => x.IdGrupo == id).Include(z => z.IdEstudianteNavigation).ToListAsync();
            return await lista;
        }

        [HttpGet]
        [Route("GetEGOffline")]
        public async Task<ActionResult<IEnumerable<EstudiantesXgrupos>>> GetEGOffline()
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }
            var lista = _context.EstudiantesXgrupos.Include(z => z.IdEstudianteNavigation).ToListAsync();
            return await lista;
        }



        // PUT: api/Grupos/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Route("PutGrupos")]
        public async Task<IActionResult> PutGrupos(Grupos grupos)
        {

            try
            {
                Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
                if (profesor == null)
                {
                    return BadRequest();
                }
                _context.Entry(grupos).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Grupos/PostGrupos?
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Route("PostGrupos")]
        public async Task<ActionResult<Grupos>> PostGrupos(Grupos grupos)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }
            grupos.IdProfesor = profesor.Id;
            _context.Grupos.Add(grupos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrupos", new { id = grupos.Id }, grupos);
        }

        // DELETE: api/Grupos/5
        [HttpDelete("{id}")]
        [Route("DeleteGrupos")]
        public async Task<ActionResult<Grupos>> DeleteGrupos(Grupos grupo)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }
            grupo.EstudiantesXgrupos = _context.EstudiantesXgrupos.Where(x => x.IdGrupo == grupo.Id).Include(z => z.IdEstudianteNavigation).ToList();
            if (grupo == null)
            {
                return NotFound();
            }

            _context.Grupos.Remove(grupo);
            await _context.SaveChangesAsync();

            return grupo;
        }
        [HttpDelete]
        [Route("DeleteEG")]
        public async Task<Boolean> DeleteEG(EstudiantesXgrupos eg)
        {
           // var grupos = _context.EstudiantesXgrupos.Where(x => x.IdEstudiante == idEstudiante && x.IdGrupo == idGrupo)
            //.FirstOrDefault();
            if (eg == null)
            {
                return false;
            }
            try
            {
                _context.EstudiantesXgrupos.Remove(eg);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;


        }
        [HttpPost]
        [Route("PostEG")]
        public async Task<ActionResult<EstudiantesXgrupos>> PostEG(EstudiantesXgrupos eg)
        {

            _context.EstudiantesXgrupos.Add(eg);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEG", new { id = eg.Id }, eg);
        }
       
    }
}
