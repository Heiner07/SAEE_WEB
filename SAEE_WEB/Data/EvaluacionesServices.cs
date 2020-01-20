﻿using System;
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


        public void CrearPdf(List<EstudianteEvaluacion> estudiantes, Asignaciones asignacion, Profesores profesor, String curso, String grupo, int anio, int periodo)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(estudiantes.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdftable.DefaultCell.BorderWidth = 1;

            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);


            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Notas.pdf", FileMode.Create));
            doc.Open();
            Paragraph linea = new Paragraph("Primera Linea");
            doc.Add(linea);
            doc.Close();

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
