﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAEE_WEB.Models;

namespace SAEE_WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesoresController : ControllerBase
    {
        private readonly BDSAEEContext _context;

        public ProfesoresController(BDSAEEContext context)
        {
            _context = context;
        }

        // GET: api/Profesores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesores>>> GetProfesores()
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null && profesor.Administrador)
            {
                return BadRequest();
            }

            return await _context.Profesores.ToListAsync();
        }

        // GET: api/Profesores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesores>> GetProfesor(int id)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null && profesor.Administrador)
            {
                return BadRequest();
            }

            var profesores = await _context.Profesores.FindAsync(id);

            if (profesores == null)
            {
                return NotFound();
            }

            return profesores;
        }

        // PUT: api/Profesores/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesores(int id, Profesores profesores)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null && profesor.Administrador)
            {
                return BadRequest();
            }

            if (id != profesores.Id)
            {
                return BadRequest();
            }

            if(profesor.Id == profesores.Id)
            {
                // Se realiza de esta manera, ya que habría conflicto con la entidad entrante (son iguales),
                // por lo que se modifica la ultima retornada por el ef
                profesor.Cedula = profesores.Cedula;
                profesor.Nombre = profesores.Nombre;
                profesor.PrimerApellido = profesores.PrimerApellido;
                profesor.SegundoApellido = profesores.SegundoApellido;
                profesor.Correo = profesores.Correo;
                profesor.Contrasenia = profesores.Contrasenia;

                _context.Entry(profesor).State = EntityState.Modified;
            }
            else
            {
                _context.Entry(profesores).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesoresExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut]
        [Route("PutProfesor")]//Actualiza el perfil
        public async Task<IActionResult> PutProfesor(Profesores profesores)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null && profesor.Id != profesores.Id)
            {
                return BadRequest();
            }

            // Se realiza de esta manera, ya que habría conflicto con la entidad entrante (son iguales),
            // por lo que se modifica la ultima retornada por el ef
            profesor.Nombre = profesores.Nombre;
            profesor.PrimerApellido = profesores.PrimerApellido;
            profesor.SegundoApellido = profesores.SegundoApellido;
            profesor.Correo = profesores.Correo;
            profesor.Contrasenia = profesores.Contrasenia;

            _context.Entry(profesor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Profesores
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Profesores>> PostProfesores(Profesores profesores)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null && profesor.Administrador)
            {
                return BadRequest();
            }

            _context.Profesores.Add(profesores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfesores", new { id = profesores.Id }, profesores);
        }

        public async Task<Boolean> DeleteAsignacionesP(Asignaciones asignacion)
        {
            Evaluaciones[] evaluaciones = await _context.Evaluaciones.Where(x => x.Asignacion == asignacion.Id).ToArrayAsync();
            foreach (var i in evaluaciones)
            {
                _context.Evaluaciones.Remove(i);
                await _context.SaveChangesAsync();
            }
            return true;
        }
        private async Task<Profesores> GetProfesorCompleteData(int id)
        {

            var listaCursos = _context.Cursos.Include(curso => curso.CursosGrupos)
                .ThenInclude(cursoGrupo => cursoGrupo.IdGrupoNavigation).Where(x => x.IdProfesor == id);
            _context.RemoveRange(listaCursos);
            await _context.SaveChangesAsync();
            var listaAsignaciones = _context.Asignaciones.Include(z => z.NotificacionesCorreo).Where(x => x.Profesor == id).ToList();
            foreach (Asignaciones asignacion in listaAsignaciones)
            {
                await DeleteAsignacionesP(asignacion);
            }
            _context.Asignaciones.RemoveRange(listaAsignaciones);
            await _context.SaveChangesAsync();
            var listaGrupos = _context.Grupos.Include(EG => EG.EstudiantesXgrupos)
               .Where(x => x.IdProfesor == id);
            _context.Grupos.RemoveRange(listaGrupos);
            await _context.SaveChangesAsync();

            return await _context.Profesores.Include(z => z.Estudiantes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        // DELETE: api/Profesores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Profesores>> DeleteProfesores(int id)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null && profesor.Administrador)
            {
                return BadRequest();
            }

            var profesorborrar = await GetProfesorCompleteData(profesor.Id);
           

            if (profesorborrar == null)
            {
                return NotFound();
            }

            _context.Profesores.Remove(profesorborrar);
            await _context.SaveChangesAsync();

            return profesorborrar;
        }

        private bool ProfesoresExists(int id)
        {
            return _context.Profesores.Any(e => e.Id == id);
        }
    }
}
