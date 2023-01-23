using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb
{
    public partial class Registro : System.Web.UI.Page
    {
        TablesDataContext db = new TablesDataContext(); //Tables contiene las tablas generadas con linq to SQL
        string email = "";
        static bool isMedico = false;
        static int IDPerfil = 1;
        int IDAfiliado = 0;

        public string getEmail()
        {
            return email;
        }
        public void setEmail(string mail)
        {
            email = mail;
        }

        public int getIDAfiliado()
        {
            return IDAfiliado;
        }

        public void setIDAfiliado(int afiliado)
        {
            IDAfiliado = afiliado;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var RequestIDAfiliado = Request.QueryString["IDAfiliado"];
            var RequestEmail = Request.QueryString["Email"];

            if ((RequestEmail != null) && (RequestIDAfiliado != null))
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
                setEmail(RequestEmail);
            }
        }

        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private void insertarUsuario(string email, string pass)
        {
            Usuario user = new Usuario();
            user.IDAfiliado = getIDAfiliado();
            user.IDPerfil = IDPerfil;
            user.isMedico = isMedico;
            user.UsuarioEmail = email;
            user.UsuarioContraseña = GetSHA256(pass);

            db.Usuario.InsertOnSubmit(user);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                //throw (ex);
            }
        }

        protected void btn_registrarse_Click(object sender, EventArgs e)
        {

            if (contraseña.Value.ToString().Trim() == "" || rep_contraseña.Value.ToString().Trim() == "")
            {
                Response.Write("<script>alert('Complete los campos faltantes.');</script>");
                return;
            }
            else if (contraseña.Value.ToString() == rep_contraseña.Value.ToString())
            {
                Response.Write("<script>alert('¡Su registro ha sido completado! Proceda a ingresar a su cuenta.');</script>");

                //ACTUALIZAR BDD INGRESANDO EL NVO USUARIO
                insertarUsuario(email, contraseña.Value.ToString());
                Response.Redirect(Constante.BaseUrl.baseurl + "Login.aspx?");
            }
            else
            {
                Response.Write("<script>alert('Las contraseñas no coinciden. Vuelva a intentarlo.');</script>");
                return;
            }
        }
    }
}