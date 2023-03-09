using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;

namespace AppWeb {
    public partial class WebMedicalHome : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                TablesDataContext db = new TablesDataContext();
                Usuario logged = (Usuario)Session["user"];
                /*
                  si logged es null, significa que el Login no guardó en la sesión al usuario, por lo que
                  tengo que ir a buscarlo a la cookie ya que se trata de un reinicio del navegador
                */
                if(logged == null) {
                    int UsuarioId = Convert.ToInt32(Request.Cookies["userID"].Value);
                    logged = (from user in db.Usuario
                              where user.UsuarioID == UsuarioId
                              select user).First();

                    Session["user"] = logged;
                }
                /*
                    si un usuario paciente cambia la url desde la barra de navegación y quiere
                    acceder a éste componente, se lo impido redirigiéndolo hacia su componente
                    home
                */
                if(!logged.isMedico)
                    Response.Redirect("~/WebHome.aspx");

                Session["medico"] = (from medico in db.Medico
                                     where medico.IDUsuario == logged.UsuarioID
                                     select medico).First();
            }
        }

        protected void signOutButton_Click(object sender, EventArgs e) {
            Session["user"] = Session["afiliado"] = Session["medico"] = null;
            FormsAuthentication.SignOut();
            Response.Redirect("login.aspx", true);
        }
    }
}