﻿using System;
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

        public async Task<List<Estudiantes>> GetEstudiantes(int idProfesor)
        {
            return await context.Estudiantes.Include(grupo => grupo.EstudiantesXgrupos)
                .Where(curso => curso.IdProfesor == idProfesor).ToListAsync();
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
        public async Task<Boolean> DeleteEstudiante(Estudiantes estudiante)
        {
            if (estudiante == null)
            {
                return false;
            }
            try
            {
                var listaAsignaciones = context.Evaluaciones.Where(x => x.Estudiante == estudiante.Id).ToList();
                context.Evaluaciones.RemoveRange(listaAsignaciones);
                await context.SaveChangesAsync();
                Estudiantes estudianteEliminar = context.Estudiantes.Where(x => x.Id == estudiante.Id).Include(EG => EG.EstudiantesXgrupos)
                    .FirstOrDefault();
                    
                context.Remove(estudianteEliminar);
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
        public string GenerarContrasenia() {
            Random rdn = new Random();
            string caracteres = "1234567890";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 5;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }
            return contraseniaAleatoria;
        }

    }
}
