using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb {
    public partial class ChangePass : Page
    {
        private static readonly int minLength = 7;
        readonly TablesDataContext db = new TablesDataContext();

        protected void Page_Load(object sender, EventArgs e) {
            //En las 2 primeras condiciones reviso si el usuario tiene autorizacion para estar en la pagina actual
            if(Session["requestChangePass"] == null) {
                Response.Redirect("login.aspx", true);
            }
            else if((bool)Session["requestChangePass"] == false) {
                Response.Redirect("login.aspx", true);
            }
            else{
                //Se obtiene el id del usuario mediante el email
                var getId = from id in db.Usuario
                            where Session["email"].ToString() == id.UsuarioEmail
                            select id.UsuarioID;

                //Se obtiene la fecha donde se pidió el cambio de contraseña a partir del id
                if(getId.Count() > 0) {
                    TimeSpan span;
                    var fechaConsulta = from fc in db.NuevaContraseña
                                        where getId.First() == fc.IDUsuario
                                        select fc.Creacion;

                    span = fechaConsulta.First().Subtract(DateTime.Now);
                    if(span.Hours > 1){
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage",
                            "alert('La fecha se venció.')", true);
                    }
                }
            }
        }

        //Se validan las contraseñas ingresadas
        protected void Button1_Click(object sender, EventArgs e) {
            if(TextBox1.Text.Trim() == "" || TextBox2.Text.Trim() == "") {
                lblMsg.Text = "Complete los campos faltantes.";
            }
            else if(TextBox1.Text.Length < minLength || TextBox2.Text.Length < minLength) {
                lblMsg.Text = "La contraseña debe tener al menos 7 caracteres.";
            }
            else if(TextBox1.Text.Equals(TextBox2.Text) == false) {
                lblMsg.Text = "Las contraseñas deben coincidir";
            }
            else {
                lblMsg.Text = Session["email"].ToString();      
                ConsultasBdd.ActualizarUsuario(Session["email"].ToString(), TextBox2.Text);
                Session["email"] = "";
                Session["requestChangePass"] = false;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", 
                    "alert('Su contraseña ha sido actualizada. Intente loguearse.')", true);
                Response.Redirect("login.aspx", true);
            }

        }
    }
}