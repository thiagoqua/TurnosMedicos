﻿using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace AppWeb{
    public partial class ForgotPass : System.Web.UI.Page{
        protected void Page_Load(object sender, EventArgs e){
            /*
             si un usuario, médico o paciente, cambia la url desde la barra de navegación y 
             quiere acceder a éste componente, se lo impido redirigiéndolo hacia su componente
             home correspondiente.
             el valor que corresponde a la key 'user' del session es distinto de null cada vez
             que un usuario se loguea, por lo que si es null no hay usuarios logueados.
            */
            Usuario trying = (Usuario)Session["user"];
            if(trying != null) {
                if(trying.isMedico)
                    Response.Redirect("~/WebMedicalHome.aspx");
                else
                    Response.Redirect("~/WebHome.aspx");
            }
        }

        //Se valida el email ingresado
        protected void Button1_Click(object sender, EventArgs e){
            if (Validar.IsValidEmail(TextBox1.Text)){
                if(Validar.ExistingMail(TextBox1.Text)) {
                    TablesDataContext db = new TablesDataContext();
                    string emisor, pass;

                    var servidor = from m in db.ServidorMail
                                   select m;
                    emisor = servidor.First().Mail;
                    pass = servidor.First().Pass;

                    EnviarMail.Enviar(emisor, pass, TextBox1.Text, true);

                    Session["requestChangePass"] = true;

                    //Se recupera el email ingresado para luego actualizar la base de datos en el form ChangePass
                    Session["email"] = TextBox1.Text;
                    Response.Redirect("login.aspx", true);
                }
                else {
                    lblMsg.Text = "El mail ingresado no se encuentra en nuestra base de datos, es decir, no se encuentra registrado.";
                }
            }
            else{
                lblMsg.Text = "Complete el campo con su email e ingréselo de forma correcta.";
            }
        }
    }
}