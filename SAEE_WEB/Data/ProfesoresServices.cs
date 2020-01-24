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
    public class ProfesoresServices
    {
        private readonly BDSAEEContext _context;

        public ProfesoresServices(BDSAEEContext context)
        {
            _context = context;
        }

        public async Task<Profesores> GetProfesores(int id)
        {
            return await _context.Profesores.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Profesores[]> GetProfesores()
        {
            return await _context.Profesores.ToArrayAsync();
        }

        private async Task<Profesores> GetProfesorCompleteData(int id)
        {
           
            var listaCursos = _context.Cursos.Include(curso => curso.CursosGrupos)
                .ThenInclude(cursoGrupo => cursoGrupo.IdGrupoNavigation).Where(x=>x.IdProfesor == id);
            _context.RemoveRange(listaCursos);
            await _context.SaveChangesAsync();
            var listaAsignaciones = _context.Asignaciones.Include(z => z.NotificacionesCorreo).Where(x => x.Profesor == id).ToList();
            foreach (Asignaciones asignacion in listaAsignaciones)
            {
                await DeleteAsignacionesP(asignacion);
            }
            _context.RemoveRange(listaAsignaciones);
            await _context.SaveChangesAsync();
            var listaGrupos = _context.Grupos.Include(EG => EG.EstudiantesXgrupos)
               .Where(x => x.IdProfesor == id);
            _context.RemoveRange(listaGrupos);
            await _context.SaveChangesAsync();

            return await _context.Profesores.Include(z => z.Estudiantes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Boolean> DeleteAsignacionesP(Asignaciones asignacion)
        {
            Evaluaciones[] evaluaciones = await _context.Evaluaciones.Where(x => x.Asignacion == asignacion.Id).ToArrayAsync();
            foreach (var i in evaluaciones)
            {
                _context.Remove(i);
                await _context.SaveChangesAsync();
            }
            return true;
        }
        public async Task<Profesores> PostProfesores(Profesores profesor)
        {
            _context.Profesores.Add(profesor);
            await _context.SaveChangesAsync();

            return await Task.FromResult(profesor);
        }

        public async Task<Boolean> PutProfesores(Profesores profesor)
        {
            _context.Entry(profesor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<Boolean> DeleteProfesores(Profesores profesor)
        {
            var profesorborrar = await GetProfesorCompleteData(profesor.Id);
            _context.Remove(profesorborrar);
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<Boolean> RollbackProfesores(Profesores profesor)
        {
            _context.Entry(profesor).CurrentValues.SetValues(_context.Entry(profesor).OriginalValues);
            return await Task.FromResult(true);
        }

        private bool ProfesoresExists(int id)
        {
            return _context.Profesores.Any(e => e.Id == id);
        }
    }
}
