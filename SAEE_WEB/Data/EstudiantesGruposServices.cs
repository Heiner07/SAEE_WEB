using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAEE_WEB.Models;


namespace SAEE_WEB.Data
{
    public class EstudiantesGruposServices 
    {
        private readonly BDSAEEContext context;

        public EstudiantesGruposServices(BDSAEEContext context)
        {
            this.context = context;
        }

        public async Task<List<Estudiantes>> GetEstudiantesXGrupos(Grupos grupo)
        {
            var lista = context.EstudiantesXgrupos.Where(x => x.IdGrupo == grupo.Id).Include(z => z.IdEstudianteNavigation);
            return await lista.Select(x => x.IdEstudianteNavigation).ToListAsync();
        }

        public async Task<Boolean> DeleteEstudianteXGrupos(int idEstudiante, Grupos grupo)
        {
            var grupos = context.EstudiantesXgrupos.Where(x => x.IdEstudiante == idEstudiante && x.IdGrupo == grupo.Id)
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

    }
}
