﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAEE_WEB.Models;

namespace SAEE_WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionesController : ControllerBase
    {
        private readonly BDSAEEContext _context;

        public AsignacionesController(BDSAEEContext context)
        {
            _context = context;
        }

        // GET: api/Asignaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignaciones>>> GetAsignaciones()
        {
           Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }
            
            return await _context.Asignaciones.Where(asignacion => asignacion.Profesor == profesor.Id).ToListAsync();
        }

        // GET: api/Asignaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignaciones>> GetAsignaciones(int id)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }

            var asignacion = await _context.Asignaciones.FindAsync(id);

            if (asignacion == null)
            {
                return NotFound();
            }

            return asignacion;
        }

        // PUT: api/Asignaciones/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignaciones(int id, Asignaciones asignacion)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }

            if (id != asignacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(asignacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignacionExists(id))
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

        // POST: api/Asignaciones
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Asignaciones>> PostAsignaciones(Asignaciones asignacion)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }
            asignacion.Profesor = profesor.Id;
            _context.Asignaciones.Add(asignacion);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAsignaciones", new { id = asignacion.Id }, asignacion);
        }

        // DELETE: api/Asignaciones/5
        [HttpDelete("{id}")]
       
        public async Task<ActionResult<Asignaciones>> DeleteAsignaciones(int id)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }                                                                    
            var evaluaciones = await _context.Evaluaciones.Where(eva => eva.Asignacion == id).ToListAsync();
            _context.Evaluaciones.RemoveRange(evaluaciones);
            await _context.SaveChangesAsync();
            var asignacion = await _context.Asignaciones.Where(asig => asig.Id == id).FirstOrDefaultAsync();
            if (asignacion == null)
            {
                return NotFound();
            }

            _context.Asignaciones.Remove(asignacion);
            await _context.SaveChangesAsync();

            return asignacion;
        }

        //OFFLINE
       // [HttpDelete]
        [Route("DeleteAllAsignaciones")]
        public async Task<Boolean> DeleteAllAsignaciones()
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return false;
            }
            try
            {
                
                var evaluaciones = await _context.Evaluaciones.Where(eva => eva.Profesor == profesor.Id).ToListAsync();
                _context.Evaluaciones.RemoveRange(evaluaciones);
                await _context.SaveChangesAsync();

                var listaAsignaciones = await _context.Asignaciones.Include(x=>x.NotificacionesCorreo)
                    .Where(x => x.Profesor == profesor.Id).ToListAsync();
                _context.Asignaciones.RemoveRange(listaAsignaciones);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;


        }
        private bool AsignacionExists(int id)
        {
            return _context.Asignaciones.Any(e => e.Id == id);
        }
    }
}
