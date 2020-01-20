using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAEE_WEB.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace SAEE_WEB.Data
{
    public class EvaluacionesServices
    {
        private readonly BDSAEEContext _context;

        public EvaluacionesServices(BDSAEEContext context)
        {
            _context = context;
        }

        public async Task<Evaluaciones> GetEvaluaciones(int id)
        {
            return await _context.Evaluaciones.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Evaluaciones[]> GetEvaluacionesporAsignacion(int id)
        {
            return await _context.Evaluaciones.Where(x => x.Asignacion == id).ToArrayAsync();
        }
        public async Task<Evaluaciones[]> GetEvaluaciones()
        {
            return await _context.Evaluaciones.ToArrayAsync();
        }

        public async Task<Evaluaciones[]> GetEvaluacionesXAsignacion(int asignacion,int periodo,int profesor)
        {
            return await _context.Evaluaciones.Where(evaluacion => evaluacion.Asignacion == asignacion && evaluacion.Periodo == periodo && evaluacion.Profesor == profesor).ToArrayAsync();
        }

        /* private async Task<Profesores> GetProfesorCompleteData(int id)
         {
             return await _context.Profesores.Include(profesor => profesor.Cursos)
                 .Include(profesor => profesor.EstudiantesXgrupos)
                 .Include(profesor => profesor.Estudiantes)
                 .FirstOrDefaultAsync(x => x.Id == id);
         }*/

        public async Task<Boolean> PostEvaluaciones(Evaluaciones evaluacion)
        {
            _context.Evaluaciones.Add(evaluacion);
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<Boolean> PutEvaluaciones(Evaluaciones evaluacion)
        {
            _context.Entry(evaluacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<Boolean> DeleteEvaluaciones(Evaluaciones evaluacion)
        {

            _context.Remove(await GetEvaluaciones(evaluacion.Id));
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }



        public async Task<Boolean> RollbackEvaluaciones(Evaluaciones evaluacion)
        {
            _context.Entry(evaluacion).CurrentValues.SetValues(_context.Entry(evaluacion).OriginalValues);
            return await Task.FromResult(true);
        }

    }
}
