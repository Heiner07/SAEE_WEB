using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SAEE_WEB.Data;
using SAEE_WEB.Models;

namespace SAEE_WEB
{
    public class LoginModel : PageModel
    {
        public string ReturnUrl { get; set; }
        //NC: Número de Cédula
        //CON: Contraseña
        public async Task<IActionResult> OnGetAsync(string NC, string CON)
        {
            string inicioProfesor = Url.Content("~/true");
            string inicioEstudiante = Url.Content("~/evaluaciones-estudiante/true");
            try
            {
                // Clear the existing external cookie
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }
            Profesores profesor;
            Estudiantes estudiante = null;
            using (BDSAEEContext context = new BDSAEEContext())
            {
                profesor = await context.Profesores.Where(P => P.Cedula.Equals(NC) && P.Contrasenia.Equals(CON)).FirstOrDefaultAsync();
                if(profesor == null)
                {
                    estudiante = await context.Estudiantes.Where(P => P.Cedula.Equals(NC) && P.Pin.Equals(CON)).FirstOrDefaultAsync();
                }
            }

            List<Claim> claims = null;

            if (profesor != null)
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, profesor.Nombre),
                    new Claim("Ap1", profesor.PrimerApellido),
                    new Claim("Ap2", profesor.SegundoApellido),
                    new Claim("P", true.ToString()),
                    new Claim("E", false.ToString()),
                    (profesor.Administrador) ?
                    new Claim("Rol", "Administrador")
                    : new Claim("Rol", "Profesor"),
                    new Claim("Id", profesor.Id.ToString())
                };
            }
            else if (estudiante != null)
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, estudiante.Nombre),
                    new Claim("Ap1", estudiante.PrimerApellido),
                    new Claim("Ap2", estudiante.SegundoApellido),
                    new Claim("P", false.ToString()),
                    new Claim("E", true.ToString()),
                    new Claim("Rol", "Estudiante"),
                    new Claim("Id", estudiante.Id.ToString())
                };
            }

            if (claims != null)
            {
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = this.Request.Host.Value
                };
                try
                {
                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }

                if (profesor == null)
                {
                    return LocalRedirect(inicioEstudiante);
                }
                else
                {
                    return LocalRedirect(inicioProfesor);
                }
            }
            else
            {
                return LocalRedirect("/Inicio-Sesion/true");
            }
        }
    }
}