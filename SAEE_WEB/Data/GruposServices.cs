using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAEE_WEB.Models;

namespace SAEE_WEB.Data
{

    public class GruposServices
    {
        private readonly BDSAEEContext context;

        public GruposServices(BDSAEEContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<List<Grupos>> GetGrupos(int idProfesor)
        {  
            return await context.Grupos.Include(grupo => grupo.EstudiantesXgrupos)
                .ThenInclude(EG => EG.IdEstudianteNavigation).Include(curso => curso.CursosGrupos)
                .Where(curso => curso.IdProfesor == idProfesor).ToListAsync();
        }

        
        [HttpGet("{id}", Name = "obtenerGrupo")]
        public async Task<Grupos> GetGrupo(int id)
        {
            return await context.Grupos.FirstOrDefaultAsync(x => x.Id == id);
        }


        [HttpPut("{id}")]
        public async Task<Boolean> PutGrupo(Grupos grupo)
        {
            context.Add(grupo);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            new CreatedAtRouteResult("obtenerGrupo", new { id = grupo.Id }, grupo);
            return true;
        }

        public async Task<Boolean> EditarGrupo(Grupos grupo) {
            try
            {
                context.Entry(grupo).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        
        }
        
        // DELETE: api/Grupos/5
        [HttpDelete("{id}")]
        public async Task<Boolean> DeleteGrupo(Grupos grupo)
        {
            if (grupo == null)
            {
                return false;
            }
            try
            {
                context.Remove(grupo);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }return true;
        }

        
        public async Task<Boolean> RollbackGrupos(Grupos grupo)
        {
            context.Entry(grupo).CurrentValues.SetValues(context.Entry(grupo).OriginalValues);
            return await Task.FromResult(true);
        }
        private bool GruposExists(int id)
        {
            return context.Grupos.Any(e => e.Id == id);
        }
    }
}
