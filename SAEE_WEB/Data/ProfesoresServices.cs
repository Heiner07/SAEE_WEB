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

        private bool ProfesoresExists(int id)
        {
            return _context.Profesores.Any(e => e.Id == id);
        }
    }
}
