using Microsoft.EntityFrameworkCore;
using SAEE_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAEE_WEB.Data
{
    public class CursosServices
    {
        private readonly BDSAEEContext _context;

        public CursosServices(BDSAEEContext context)
        {
            _context = context;
        }

        public async Task<Cursos> GetCursos(int idProfesor, int idCurso)
        {
            return await _context.Cursos.FirstOrDefaultAsync(x => x.Id == idCurso);
        }

        public async Task<Cursos[]> GetCursos(int idProfesor)
        {
            
            return await _context.Cursos.Include(curso => curso.CursosGrupos)
                .ThenInclude(cursoGrupo => cursoGrupo.IdGrupoNavigation)
                .Where(curso => curso.IdProfesor == idProfesor).ToArrayAsync();
        }

        public async Task<Cursos> PostCursos(Cursos curso)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return await Task.FromResult(curso);
        }

        public async Task<Boolean> PutCursos(Cursos curso)
        {
            _context.Entry(curso).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<Boolean> DeleteCursos(Cursos curso)
        {
            _context.Remove(curso);
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<Boolean> RollbackCursos(Cursos curso)
        {
            _context.Entry(curso).CurrentValues.SetValues(_context.Entry(curso).OriginalValues);
            return await Task.FromResult(true);
        }

        private bool CursosExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }
    }
}
