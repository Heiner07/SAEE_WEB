using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAEE_WEB.Models
{
    public class ConstructorMensajeMatSnackBar
    {
        public static bool AsignarValoresSnackBar(int Success, ref string TextoMsg,
            ref string IconoMsg, ref string ColorIcono)
        {
            if (Success == 1)
            {
                TextoMsg = "Agregado correctamente";
                IconoMsg = "check_circle";
                ColorIcono = "color:green;";
                return true;
            }
            if (Success == 2)
            {
                TextoMsg = "Guardado correctamente";
                IconoMsg = "save";
                ColorIcono = "color:blue;";
                return true;
            }
            if (Success == 3)
            {
                TextoMsg = "Borrado correctamente";
                IconoMsg = "delete";
                ColorIcono = "color:red;";
                return true;
            }
            return false;
        }
    }
}
