using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAEE_WEB.Models;

namespace SAEE_WEB.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposController : ControllerBase
    {

        BDSAEEContext _context = new BDSAEEContext();

        // GET: api/Grupos
        [HttpGet]
        public Task<IEnumerable<Grupos>> GetGrupos()
        {
            
            return Task.FromResult<IEnumerable<Grupos>>(_context.Grupos); 
        }

        // GET: api/Grupos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grupos>> GetGrupos(int id)
        {
            var grupos = await _context.Grupos.FindAsync(id);

            if (grupos == null)
            {
                return NotFound();
            }

            return grupos;
        }

        // PUT: api/Grupos/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrupos(int id, Grupos grupos)
        {
            if (id != grupos.Id)
            {
                return BadRequest();
            }

            _context.Entry(grupos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GruposExists(id))
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

        // POST: api/Grupos
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Grupos>> PostGrupos(Grupos grupos)
        {
            _context.Grupos.Add(grupos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrupos", new { id = grupos.Id }, grupos);
        }

        // DELETE: api/Grupos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Grupos>> DeleteGrupos(int id)
        {
            var grupos = await _context.Grupos.FindAsync(id);
            if (grupos == null)
            {
                return NotFound();
            }

            _context.Grupos.Remove(grupos);
            await _context.SaveChangesAsync();

            return grupos;
        }

        private bool GruposExists(int id)
        {
            return _context.Grupos.Any(e => e.Id == id);
        }
    }
}
