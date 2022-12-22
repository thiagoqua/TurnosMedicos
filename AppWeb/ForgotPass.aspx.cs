using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb
{
    public partial class ForgotPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e){
            if (Validar.IsValidEmail(TextBox1.Text)){
                if(Validar.ExistingMail(TextBox1.Text)) {
                    TablesDataContext db = new TablesDataContext();

                    var servidor = from m in db.ServidorMail
                                   select m;
                    string emisor = servidor.FirstOrDefault().Mail;
                    string pass = servidor.FirstOrDefault().Pass;

                    //SE ENVIA UN CORREO CON UN LINK TEMPORAL 
                    EnviarMail.Enviar(emisor, pass, TextBox1.Text, true);
                    Session["requestChangePass"] = true;

                    //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Hemos enviado un mensaje a su correo. Siga las instrucciones.')", true);
                    Session["email"] = TextBox1.Text;     //recupero el mail ingresado para luego actualizar la base de datos en ChangePass

                    Response.Redirect("login.aspx", true);
                }
                else {
                    lblMsg.Text = "El mail ingresado no se encuentra en nuestra base de datos, es decir, no se encuentra registrado.";
                }
            }
            else
            {
                lblMsg.Text = "Complete el campo con su email e ingréselo de forma correcta.";
            }
            
            
        }
    }
}