using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace AppWeb
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e){
            
            //Código necesario para evitar problemas de compilación
            string JQueryVer = "1.7.1";
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-" + JQueryVer + ".min.js",
                DebugPath = "~/Scripts/jquery-" + JQueryVer + ".js",
                CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-" + JQueryVer + ".min.js",
                CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-" + JQueryVer + ".js",
                CdnSupportsSecureConnection = true,
                LoadSuccessExpression = "window.jQuery"
            });
        }

        void Session_Start(object s, EventArgs e) {
            //True si se pidió un cambio de contraseña y se envió el link al mail del usuario
            Session["requestChangePass"] = false;
            
            // Se guarda el mail ingresado por el usuario para cambiar la contraseña
            Session["email"] = "";
        }
    }
}