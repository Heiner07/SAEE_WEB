using Microsoft.EntityFrameworkCore;
using SAEE_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAEE_WEB.Data
{
    public class CursosGruposServices
    {
        private readonly BDSAEEContext _context;

        public CursosGruposServices(BDSAEEContext context)
        {
            _context = context;
        }

        public async Task<CursosGrupos> GetCursosGrupos(int id)
        {
            return await _context.CursosGrupos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CursosGrupos[]> GetCursosGrupos()
        {
            return await _context.CursosGrupos.ToArrayAsync();
        }
    }
}
