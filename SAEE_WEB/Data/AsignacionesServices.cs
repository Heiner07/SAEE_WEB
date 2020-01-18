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
    public class AsignacionesServices
    {
        private readonly BDSAEEContext _context;

        public AsignacionesServices(BDSAEEContext context)
        {
            _context = context;
        }

        public async Task<Asignaciones> GetAsignaciones(int id)
        {
            return await _context.Asignaciones.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Asignaciones[]> GetAsignacionesProfesor(int idProfesor)
        {
            return await _context.Asignaciones.Where(asignacion => asignacion.Profesor == idProfesor)
                .Include(a => a.NotificacionesCorreo).ToArrayAsync();
        }
        public async Task<Asignaciones[]> GetAsignacionesDetallada(int idProfesor,int curso,int grupo,string tipo)
        {
            return await _context.Asignaciones.Where(asignacion => asignacion.Profesor == idProfesor && asignacion.Curso == curso &&
            asignacion.Tipo == tipo && asignacion.Grupo == grupo).ToArrayAsync();
        }
        public async Task<Asignaciones[]> GetAsignaciones()
        {
            return await _context.Asignaciones.ToArrayAsync();
        }
        /* private async Task<Profesores> GetProfesorCompleteData(int id)
         {
             return await _context.Profesores.Include(profesor => profesor.Cursos)
                 .Include(profesor => profesor.EstudiantesXgrupos)
                 .Include(profesor => profesor.Estudiantes)
                 .FirstOrDefaultAsync(x => x.Id == id);
         }*/

        public async Task<Boolean> PostAsignaciones(Asignaciones asignacion)
        {
            _context.Asignaciones.Add(asignacion);
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<Boolean> PutAsignaciones(Asignaciones asignacion)
        {
            _context.Entry(asignacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<Boolean> DeleteAsignaciones(Asignaciones asignacion)
        {

            _context.Remove(await GetAsignaciones(asignacion.Id));
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<Boolean> RollbackAsignaciones(Asignaciones asignacion)
        {
            _context.Entry(asignacion).CurrentValues.SetValues(_context.Entry(asignacion).OriginalValues);
            return await Task.FromResult(true);
        }

    }
}
