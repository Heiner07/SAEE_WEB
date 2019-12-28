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
        public async Task<ActionResult<IEnumerable<Grupos>>> GetGrupos(int id)
        {
            return await _context.Grupos.Include(grupo => grupo.EstudiantesXgrupos)
                .ThenInclude(EG => EG.IdEstudianteNavigation).Include(curso => curso.CursosGrupos)
                .Where(curso => curso.IdProfesor == id).ToListAsync();
        }

        // GET: api/Grupos/GetEstudiantes?id=IDGRUPO
        [HttpGet]
        [Route("GetEstudiantes")]
        public async Task<ActionResult<IEnumerable<Estudiantes>>> GetEstudiantes(int id)
        {
            var lista = _context.EstudiantesXgrupos.Where(x => x.IdGrupo == id).Include(z => z.IdEstudianteNavigation);

            if (lista == null)
            {
                return NotFound();
            }

            return await (from EG in lista
                          select EG.IdEstudianteNavigation).ToListAsync();
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
            _context.Grupos.Add(grupos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrupos", new { id = grupos.Id }, grupos);
        }

        // DELETE: api/Grupos/5
        [HttpDelete("{id}")]
        [Route("DeleteGrupos")]
        public async Task<ActionResult<Grupos>> DeleteGrupos(Grupos grupo)
        {
           
            if (grupo == null)
            {
                return NotFound();
            }

            _context.Grupos.Remove(grupo);
            await _context.SaveChangesAsync();

            return grupo;
        }

        private bool GruposExists(int id)
        {
            return _context.Grupos.Any(e => e.Id == id);
        }
    }
}
