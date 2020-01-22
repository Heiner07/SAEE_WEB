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
    public class EvaluacionesController : ControllerBase
    {
        private readonly BDSAEEContext _context;

        public EvaluacionesController(BDSAEEContext context)
        {
            _context = context;
        }
        // GET: api/Evaluaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evaluaciones>>> GetEvaluaciones()
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }

            return await _context.Evaluaciones.Where(evaluacion => evaluacion.Profesor == profesor.Id).ToArrayAsync();
        }

        // GET: api/Evaluaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evaluaciones>>> GetEvaluacionesXAsignatura(int asignacion,int periodo)
        {
           Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }

            return await _context.Evaluaciones.Where(evaluacion => evaluacion.Asignacion == asignacion && evaluacion.Periodo == periodo && evaluacion.Profesor == profesor.Id).ToArrayAsync();
        }

        // GET: api/Evaluaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evaluaciones>> GetEvaluaciones(int id)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }

            var evaluacion = await _context.Evaluaciones.FindAsync(id);

            if (evaluacion == null)
            {
                return NotFound();
            }

            return evaluacion;
        }

        // PUT: api/Evaluaciones/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluaciones(int id, Evaluaciones evaluacion)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }

            if (id != evaluacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(evaluacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluacionExists(id))
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

        // POST: api/Evaluaciones
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Evaluaciones>> PostEvaluaciones(Evaluaciones evaluacion)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }
            evaluacion.Profesor = profesor.Id;

            _context.Evaluaciones.Add(evaluacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsignaciones", new { id = evaluacion.Id }, evaluacion);
        }

        // DELETE: api/Evaluaciones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Evaluaciones>> DeleteEvaluaciones(int id)
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }

            var evaluacion = await _context.Evaluaciones.Where(asig => asig.Id == id).FirstOrDefaultAsync();
            if (evaluacion == null)
            {
                return NotFound();
            }

            _context.Evaluaciones.Remove(evaluacion);
            await _context.SaveChangesAsync();

            return evaluacion;
        }

        private bool EvaluacionExists(int id)
        {
            return _context.Evaluaciones.Any(e => e.Id == id);
        }
    }
}
