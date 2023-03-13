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
            Page.Title = "Log In";
            /*
             si un usuario, médico o paciente, cambia la url desde la barra de navegación y 
             quiere acceder a éste componente, se lo impido redirigiéndolo hacia su componente
             home correspondiente.
             el valor que corresponde a la key user del session es distinto de null cada vez
             que un usuario se loguea, por lo que si es null no hay usuarios logueados.
            */
            Usuario trying = (Usuario)Session["user"];
            if(trying != null) {
                if(trying.isMedico)
                    Response.Redirect("~/WebMedicalHome.aspx");
                else
                    Response.Redirect("~/WebHome.aspx");
            }
            //genero los eventos para el click de los botones
            cmdLogin.ServerClick += new EventHandler(CmdLogin_ServerClick);     
        }

        //Se validan los datos ingresados y los campos seleccionados
        private void CmdLogin_ServerClick(object sender, EventArgs e){
            Usuario trying = Validar.Validate(txtUserName.Value, txtUserPass.Value);
            if (trying != null){
                FormsAuthentication.SetAuthCookie(txtUserName.Value, chkPersistCookie.Checked);   
                Session["user"] = trying;
                if(chkPersistCookie.Checked) {
                    /*
                      Se usan cookies persistentes para guardar la sesión (más específicamente
                      el ID del usuario que tiene la sesión iniciada), aún en los casos donde se corta la 
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

        }

    }
}