﻿using Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AppWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cmdLogin.ServerClick += new EventHandler(CmdLogin_ServerClick);     //generan los eventos para el click de los botones
            //cmdForgot.ServerClick += new EventHandler(CmdForgot_ServerClick);
        }

        private bool ValidateUser(string email, string pass) {
            return Validar.Validate(email, pass) != null;
        }

        //Login button
        private void CmdLogin_ServerClick(object sender, EventArgs e)   //ojo con el nombre porque el boton es cmdLogin
        {
            Usuario trying = Validar.Validate(txtUserName.Value, txtUserPass.Value);
            if (trying != null){
                //la cookie persistente me guarda la sesion aun si corto la compilacion y salgo del navegador
                //si no uso cookie persistente, cuando salga del navegador y vuelva a entrar tengo que iniciar sesion nuevamente
                FormsAuthentication.SetAuthCookie(txtUserName.Value, chkPersistCookie.Checked);   
                Session["user"] = trying;
                if(chkPersistCookie.Checked) {
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
            {
                //Response.Redirect("login.aspx", true);
                lblMsg.Text = "Mail y/o contraseña incorrectos.";
            }
        }

        private void CmdForgot_ServerClick(object sender, EventArgs e)
        {
            lblMsg.Text = "Se clickeó olvidé mi contrasela";
        }

    }
}