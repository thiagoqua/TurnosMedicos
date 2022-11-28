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

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Validar.IsValidEmail(TextBox1.Text))
            {
                TablesDataContext db = new TablesDataContext();

                var servidor = from m in db.ServidorMail
                               select m;
                string emisor = servidor.FirstOrDefault().Mail;
                string pass = servidor.FirstOrDefault().Pass;

                //CAMBIAR BODY Y AGREGAR UN LINK PARA QUE 
                EnviarMail.Enviar(emisor, pass, TextBox1.Text,true);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),"alertMessage","alert('Hemos enviado un mensaje a su correo. Siga las instrucciones.')",true);

                Response.Redirect("login.aspx", true);

            }
            else
            {
                lblMsg.Text = "Complete el campo con su email e ingréselo de forma correcta.";
            }
            
            
        }
    }
}