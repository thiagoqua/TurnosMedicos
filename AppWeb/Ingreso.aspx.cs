using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace AppWeb{
    public partial class Ingreso : System.Web.UI.Page{
        protected void Page_Load(object sender, EventArgs e){

        }

        protected void ingresar_btn_Click(object sender, EventArgs e){
            Response.Redirect(BaseUrl.url + "Login.aspx");
        }

        protected void registrarse_btn_Click(object sender, EventArgs e){
            Response.Redirect(BaseUrl.url + "Verificacion.aspx");
        }

    }
}