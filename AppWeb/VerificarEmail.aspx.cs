using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;

namespace AppWeb
{
    public partial class VerificarEmail : System.Web.UI.Page
    {
        private string emisor;
        private string pass;
        private int IDAfiliado = -1;
        private int nroVerif = 0;
        TablesDataContext db = new TablesDataContext();
        public string texto;

        protected void Page_Load(object sender, EventArgs e)
        {
            var RequestIDAfiliado = Request.QueryString["IDAfiliado"];
            if (RequestIDAfiliado != null)
            {
                int IDAfiliado_Parser = 0;
                int.TryParse(RequestIDAfiliado, out IDAfiliado_Parser);
                if (IDAfiliado_Parser > 0)
                {
                    setIDAfiliado(IDAfiliado_Parser);
                }
                else
                {
                    Response.Write("<script>alert('No se pudo obtener el código de Afiliado, por favor vuelva a registrarse.');</script>");
                    Response.Redirect(Constante.BaseUrl.baseurl + "Verificacion.aspx?");
                }
            }
        }
        public void setIDAfiliado(int afiliado)
        {
            IDAfiliado = afiliado;
        }

        public int getIDAfiliado()
        {
            return IDAfiliado;
        }
        public int getNroVerif()
        {
            return nroVerif;
        }
        public void setNroVerif(int em)
        {
            nroVerif = em;
        }

        public bool isValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }


        public bool checkEmail(string email)
        {

            var mail = from e in db.Usuario
                       where email == e.UsuarioEmail
                       select e;
            if (mail.Count() >= 1)
            {
                return true;
            }
            return false;
        }


        public bool Verificacion(TextBox Codigo)
        {
            if (getNroVerif() != 0)            //SE ENVIÓ EL CORREO
            {
                if (nroVerif.ToString() == Codigo.Text)         //SI EL NRO ES IGUAL ESTÁ VERIFICADO
                {
                    Response.Write("<script>alert('¡Verificacion exitosa!');</script>");
                    return true;
                }
                else
                {
                    Response.Write("<script>alert('Codigo no válido. Reintente');</script>");
                    return false;
                }
            }
            else
            {
                Response.Write("<script>alert('Codigo no válido. Reintente');</script>");
                return false;
            }
        }

        protected void texto_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void DatosEmail()
        {
            var servidor = from m in db.ServidorMail
                           select m;
            emisor = servidor.FirstOrDefault().Mail;
            pass = servidor.FirstOrDefault().Pass;

        }

        protected void btn_verificar_Click(object sender, EventArgs e)
        {
            if (isValidEmail(email.Text) == false)
            {
                Response.Write("<script>alert('Ingrese un mail válido.');</script>");
            }
            else if (checkEmail(email.Text) == true)
            {
                Response.Write("<script>alert('El mail ingresado ya se encuentra registrado. Pruebe con otro mail o proceda a loguearse.');</script>");
            }
            else
            {
                DatosEmail();
                setNroVerif(EnviarMail.Enviar(emisor, pass, email.Text, false));

                Session.Add("CodVerif_Session", getNroVerif());

                Response.Write("<script>alert('Hemos enviado un mensaje a su correo. Siga las instrucciones.');</script>");
                btn_confirmar.Enabled = true;
            }


        }

        protected void btn_confirmar_Click(object sender, EventArgs e)
        {
            setNroVerif((int)Session["CodVerif_Session"]);
            if (codigo.Text.Trim() == "")
            {
                Response.Write("<script>alert('Ingrese el código de verificación.');</script>");
            }

            DatosEmail();
            EnviarMail.Enviar(emisor, pass, email.Text, false);

            if (getNroVerif() != 0)            //SE ENVIÓ EL CORREO
            {
                if (nroVerif.ToString() == codigo.Text)         //SI EL NRO ES IGUAL ESTÁ VERIFICADO
                {
                    Response.Write("<script>alert('Verificación exitosa.');</script>");

                    bool verificacion = false;

                    verificacion = Verificacion(codigo);

                    if (verificacion == true)
                    {
                        Response.Write("<script>alert('Verificacion');</script>");

                        Response.Redirect(Constante.BaseUrl.baseurl + "Registro.aspx?Email=" + email.Text + "&IDAfiliado=" + IDAfiliado.ToString());
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Código no válido. Reintente.');</script>");
            }
        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            codigo.Text = String.Empty;
            email.Text = String.Empty;
            Response.Redirect(Constante.BaseUrl.baseurl + "Verificacion.aspx");
        }

    }
}