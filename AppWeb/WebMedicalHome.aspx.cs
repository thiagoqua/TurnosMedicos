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
            signOutButton.ServerClick += new EventHandler(signOutButton_Click);
            if(!IsPostBack) {
                TablesDataContext db = new TablesDataContext();
                int UsuarioID = (Session["user"] as Usuario).UsuarioID;
                Session["medico"] = (from medico in db.Medico
                                     where medico.IDUsuario == UsuarioID
                                     select medico).FirstOrDefault();
            }
        }

        protected void signOutButton_Click(object sender, EventArgs e) {
            FormsAuthentication.SignOut();
            Response.Redirect("login.aspx", true);
        }
    }
}