using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb {
    public partial class ChangePass : System.Web.UI.Page {
        private static readonly int minLength = 7;
        TablesDataContext db = new TablesDataContext();

        protected void Page_Load(object sender, EventArgs e) {

            if(Session["requestChangePass"] == null) {
                Response.Redirect("login.aspx", true);
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Es null.')", true);

            }
            else if((bool)Session["requestChangePass"] == false) {
                Response.Redirect("login.aspx", true);
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Es false.')", true);

            }
            else  //chequear que la hora esté dentro del rango
            {

                //obtengo el id del usuario mediante el email
                var getId = from id in db.Usuario
                            where Session["email"].ToString() == id.UsuarioEmail
                            select id.UsuarioID;

                //obtengo la fecha donde se pidio el cambio de contraseña a partir del id

                if(getId.Count() > 0) {
                    var fechaConsulta = from fc in db.NuevaContraseña
                                        where getId.FirstOrDefault() == fc.ID
                                        select fc.Creacion;

                    TimeSpan span = fechaConsulta.FirstOrDefault().Subtract(DateTime.Now);

                    //lblMsg.Text = span.Hours.ToString();

                    if(span.Hours > 1)  //si es mayor a cero es una fecha posterior (es decir, se venció el tiempo)
                    {
                        //Response.Redirect("login.aspx", true);
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('La fecha se venció.')", true);
                        //lblMsg.Text = span.Hours.ToString();
                    }

                }

            }

        }

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
                lblMsg.Text = Session["email"].ToString();      //guarda algo session ???
                ConsultasBdd.ActualizarUsuario(Session["email"].ToString(), TextBox2.Text);
                Session["email"] = "";
                Session["requestChangePass"] = false;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Su contraseña ha sido actualizada. Intente loguearse.')", true);
                Response.Redirect("login.aspx", true);

            }

        }
    }
}