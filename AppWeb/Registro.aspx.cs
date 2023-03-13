using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb{
    public partial class Registro : System.Web.UI.Page{
        private TablesDataContext db;
        private string email = "";
        private static bool isMedico = false;
        private static int IDPerfil = 1;
        private int IDAfiliado = 0;

        protected void Page_Load(object sender, EventArgs e) {
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

            Page.Title = "Sign Up";
            if(!IsPostBack) {
                db = new TablesDataContext();
                Session["database"] = db;
            }
            else {
                db = (TablesDataContext) Session["database"];
            }
            var RequestIDAfiliado = Request.QueryString["IDAfiliado"];
            var RequestEmail = Request.QueryString["Email"];

            /*
              seteo el ID del afiliado si se cumplen las condiciones, sino muestro un mensaje 
              y redirecciono a la pantalla de verificación
            */
            if((RequestEmail != null) && (RequestIDAfiliado != null)) {
                int IDAfiliado_Parser;
                int.TryParse(RequestIDAfiliado, out IDAfiliado_Parser);
                if(IDAfiliado_Parser > 0) {
                    setIDAfiliado(IDAfiliado_Parser);
                }
                else {
                    Response.Write("<script>alert('No se pudo obtener el código de Afiliado, por favor vuelva a registrarse.');</script>");
                    Response.Redirect(BaseUrl.url + "Verificacion.aspx?");
                }
                setEmail(RequestEmail);
            }
        }

        public void setEmail(string mail){
            email = mail;
        }

        public int getIDAfiliado(){
            return IDAfiliado;
        }

        public void setIDAfiliado(int afiliado){
            IDAfiliado = afiliado;
        }

        private void insertarUsuario(string email, string pass){
            Usuario user = new Usuario();
            user.IDAfiliado = getIDAfiliado();
            user.IDPerfil = IDPerfil;
            user.isMedico = isMedico;
            user.UsuarioEmail = email;
            user.UsuarioContraseña = Encriptar.GetSHA256(pass);

            db.Usuario.InsertOnSubmit(user);
            try{
                db.SubmitChanges();
            }
            catch (Exception){
            }
        }

        protected void btn_registrarse_Click(object sender, EventArgs e){
            if (contraseña.Value.ToString().Trim() == "" || 
                rep_contraseña.Value.ToString().Trim() == ""){
                Response.Write("<script>alert('Complete los campos faltantes.');</script>");
                return;
            }
            else if (contraseña.Value.ToString() == rep_contraseña.Value.ToString()){
                Response.Write("<script>alert('¡Su registro ha sido completado! Proceda a ingresar a su cuenta.');</script>");
                insertarUsuario(email, contraseña.Value.ToString());
                Response.Redirect(BaseUrl.url + "Login.aspx?");
            }
            else{
                Response.Write("<script>alert('Las contraseñas no coinciden. Vuelva a intentarlo.');</script>");
                return;
            }
        }
    }
}