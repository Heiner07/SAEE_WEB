using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAEE_WEB.Models;
namespace SAEE_WEB.Data
{
    public class EstudiantesServices
    {
        private readonly BDSAEEContext context;

        public EstudiantesServices(BDSAEEContext context)
        {
            this.context = context;
        }

        public async Task<List<Estudiantes>> GetEstudiantes()
        {
            return await context.Estudiantes.ToListAsync();
        }


        [HttpGet("{id}", Name = "obtenerEstudiante")]
        public async Task<Estudiantes> GetEstudiante(int id)
        {
            return await context.Estudiantes.FirstOrDefaultAsync(x => x.Id == id);
        }


        [HttpPut("{id}")]
        public async Task<Boolean> PutEstudiante(Estudiantes estudiante)
        {
            context.Add(estudiante);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            new CreatedAtRouteResult("obtenerEstudiante", new { id = estudiante.Id }, estudiante);
            return true;
        }

        public async Task<Boolean> EditarEstudiante(Estudiantes estudiante)
        {
            try
            {
                context.Entry(estudiante).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;

        }

        // DELETE: api/Estudiantes/5
        [HttpDelete("{id}")]
        public async Task<Boolean> DeleteEstudiante(int id)
        {
            var estudiante = await context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return false;
            }
            try
            {
                context.Estudiantes.Remove(estudiante);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }
        public async Task<Boolean> RollbackEstudiantes(Estudiantes estudiante)
        {
            context.Entry(estudiante).CurrentValues.SetValues(context.Entry(estudiante).OriginalValues);
            return await Task.FromResult(true);
        }
    }
}
