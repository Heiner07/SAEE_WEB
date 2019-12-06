﻿using System;
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
            return await context.Grupos.ToListAsync();
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
        public async Task<Boolean> DeleteGrupo(int id)
        {
            var grupos = await context.Grupos.FindAsync(id);
            if (grupos == null)
            {
                return false;
            }
            try
            {
                context.Grupos.Remove(grupos);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }return true;
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