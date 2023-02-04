using Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AppWeb{
    public partial class Login : System.Web.UI.Page{
        protected void Page_Load(object sender, EventArgs e){
            //genero los eventos para el click de los botones
            cmdLogin.ServerClick += new EventHandler(CmdLogin_ServerClick);     
        }

        private void CmdLogin_ServerClick(object sender, EventArgs e){
            Usuario trying = Validar.Validate(txtUserName.Value, txtUserPass.Value);
            if (trying != null){
                FormsAuthentication.SetAuthCookie(txtUserName.Value, chkPersistCookie.Checked);   
                Session["user"] = trying;
                if(chkPersistCookie.Checked) {
                    /*
                      usamos cookies persistentes para guardar la sesión (más específicamente
                      el ID del usuario que tiene la sesión iniciada) aún si se corta la 
                      compilación o el usuario reinicia el navegador
                    */
                    Response.Cookies.Add(new HttpCookie("userID") {
                        Value = trying.UsuarioID.ToString(),
                        Expires = DateTime.Now.AddDays(1)
                    });
                }

                if(trying.isMedico)
                    Response.Redirect("WebMedicalHome.aspx");
                else
                    Response.Redirect("WebHome.aspx");
            }
            else
                lblMsg.Text = "Mail y/o contraseña incorrectos.";
        }

        private void CmdForgot_ServerClick(object sender, EventArgs e){
            lblMsg.Text = "Se clickeó olvidé mi contrasela";
        }

    }
}