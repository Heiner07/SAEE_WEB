using System;
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
    public class Iniciar_sesionController : ControllerBase
    {
        private readonly BDSAEEContext _context;

        public Iniciar_sesionController(BDSAEEContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Profesores>> GetProfesor()
        {
            Profesores profesor = await ComprobacionSesion.ComprobarInicioSesion(HttpContext.Request.Headers, _context);
            if (profesor == null)
            {
                return BadRequest();
            }
            return profesor;
        }
    }
}
