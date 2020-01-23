using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAEE_WEB.Models;
using System.IO;
using System.Net;
using System.Net.Mail;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Drawing;
using System.Data;
using Syncfusion.Pdf.Tables;

namespace SAEE_WEB.Data
{

    public class EvaluacionesServices
    {
        private readonly BDSAEEContext _context;

        public EvaluacionesServices(BDSAEEContext context)
        {
            _context = context;
        }


        public MemoryStream CrearPdf(List<EstudianteEvaluacion> estudiantes, Asignaciones asignacion, Profesores profesor, String curso, String grupo, int anio, int periodo)
        {
            using (PdfDocument pdfDocument = new PdfDocument())
            {

                int paragraphAfterSpacing = 8;
                int cellMargin = 8;

                //Add page to the PDF document
                PdfPage page = pdfDocument.Pages.Add();

                //Create a new font
                PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);

                //Create a text element to draw a text in PDF page
                PdfTextElement title = new PdfTextElement("Informe de notas "+grupo, font, PdfBrushes.Black);
                PdfLayoutResult result = title.Draw(page, new PointF(0, 0));
                string fecha = DateTime.Now.ToShortDateString();

                PdfStandardFont contentFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
                PdfTextElement content = new PdfTextElement("Profesor: " + profesor.Nombre + " " + profesor.PrimerApellido + " " + profesor.SegundoApellido + "             Fecha: " + fecha + "\n" +
                "Curso: " + curso + "     Grupo: " + grupo + "      Anio: " + anio + "     Rubro: " + asignacion.Tipo + "    Asignación: " + asignacion.Nombre + "\n" +
                "Puntos: " + asignacion.Puntos + "      Porcentaje: " + asignacion.Porcentaje + "\n" +
                "Periodo: " + periodo, contentFont, PdfBrushes.Black);
                PdfLayoutFormat format = new PdfLayoutFormat();
                format.Layout = PdfLayoutType.Paginate;

                //Draw a text to the PDF document
                result = content.Draw(page, new RectangleF(0, result.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);

                PdfGrid pdfGrid = new PdfGrid();

                pdfGrid.Style.CellPadding.Left = cellMargin;
                pdfGrid.Style.CellPadding.Right = cellMargin;

                //Applying built-in style to the PDF grid
                pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
                // Initialize DataTable to assign as DateSource to the light table.

                DataTable table = new DataTable();

                //Include columns to the DataTable.

                table.Columns.Add("Cédula");

                table.Columns.Add("Nombre");

                table.Columns.Add("Puntos");

                table.Columns.Add("Porcentaje");

                table.Columns.Add("Nota");
                //Include rows to the DataTable.
                foreach(EstudianteEvaluacion estu in estudiantes)
                {
                    table.Rows.Add(new object[] { estu.Cedula,estu.Nombre,estu.Puntos.ToString(),estu.Porcentaje.ToString(),estu.Nota.ToString() });
                }


                //Assign data source.

                pdfGrid.DataSource = table;

                pdfGrid.Style.Font = contentFont;

                //Draw PDF grid into the PDF page
                pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));

                using (MemoryStream stream = new MemoryStream())
                {
                    //Saving the PDF document into the stream
                    pdfDocument.Save(stream);
                    //Closing the PDF document
                    pdfDocument.Close(true);
                    return stream;

                }
            }

            }

        public async Task<Evaluaciones> GetEvaluaciones(int id)
        {
            return await _context.Evaluaciones.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Evaluaciones[]> GetEvaluacionesEstudiante(int id)
        {
            return await _context.Evaluaciones.Where(x => x.Estudiante == id).ToArrayAsync();
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
