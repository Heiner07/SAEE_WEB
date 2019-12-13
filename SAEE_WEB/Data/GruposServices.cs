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

        public async Task<List<Grupos>> GetGrupos()
        {
            return await context.Grupos.Include(grupo => grupo.EstudiantesXgrupos)
                .ThenInclude(EG => EG.IdEstudianteNavigation).ToListAsync();
        }

        public async Task<List<Estudiantes>> GetEstudiantesXGrupos(Grupos grupo)
        {
             //SE NECESITA EL ID DEL PROFESOR
            var lista = context.EstudiantesXgrupos.Where(x => x.IdGrupo == grupo.Id && x.IdProfesor == 1).Include(z => z.IdEstudianteNavigation);
            List<Estudiantes> estudiantesGrupos = new List<Estudiantes>();
            return await (from EG in lista
                          select EG.IdEstudianteNavigation).ToListAsync();
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

        public async Task<Boolean> DeleteEstudianteXGrupos(int idEstudiante,Grupos grupo) {
            //TRAERME EL ID DEL PROFESOR
            var grupos = context.EstudiantesXgrupos.Where(x=>x.IdEstudiante==idEstudiante && x.IdGrupo==grupo.Id && x.IdProfesor==48)
                .FirstOrDefault();
            if (grupos == null)
            {
                return false;
            }
            try
            {
                context.EstudiantesXgrupos.Remove(grupos);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;


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
