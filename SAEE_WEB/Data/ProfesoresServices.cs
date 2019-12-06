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

        public async Task<Profesores[]> GetProfesores()
        {
            return await _context.Profesores.ToArrayAsync();
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
            _context.Remove(profesor);
            await _context.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        private bool ProfesoresExists(int id)
        {
            return _context.Profesores.Any(e => e.Id == id);
        }
    }
}
