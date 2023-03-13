using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;

namespace AppWeb{
    public partial class Verificacion : System.Web.UI.Page{
        TablesDataContext db;

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

            Page.Title = "Verificación";
            if(!IsPostBack){
                db = new TablesDataContext();
                var obraSocial = from obra in db.ObraSocial          
                                 select obra;

                obrasocial_combo.DataTextField = "ObraSocialDescripcion";
                obrasocial_combo.DataValueField = "ObraSocialId";
                obrasocial_combo.DataSource = obraSocial;
                obrasocial_combo.DataBind();

                CargarComboPlan(sender, e);
                Session["database"] = db;
            }
            else
                db = (TablesDataContext)Session["database"];
        }

        private void CargarComboPlan(object sender, EventArgs e){
            int obrasocial_id = Convert.ToInt32(obrasocial_combo.SelectedItem.Value);
            var planlist = from plan in db.PlanObraSocial
                           where plan.IDObraSocial == obrasocial_id
                           select plan;

            plan_combo.ClearSelection();
            plan_combo.DataTextField = "PlanDescripcion";
            plan_combo.DataValueField = "PlanId";
            plan_combo.DataSource = planlist;
            plan_combo.DataBind();
        }

        protected void obrasocial_combo_SelectedIndexChanged(object sender, EventArgs e){
            CargarComboPlan(sender, e);
        }

        protected void plan_combo_SelectedIndexChanged(object sender, EventArgs e){

        }

        protected void volver_btn_Click(object sender, EventArgs e){

        }

        protected void btn_verificar(object sender, EventArgs e){
            if (dni.Text.Trim() == "" || nroafiliado.Text.Trim() == ""){
                Response.Write("<script>alert('Complete los campos faltantes.');</script>");
                return;
            }
            int dni_int, IDAfiliado, plan_id;
            string nroAfiliado_txt;
            dni_int = IDAfiliado = plan_id = 0;

            int.TryParse(dni.Text, out dni_int);

            if (dni_int == 0){
                Response.Write("<script>alert('Error.');</script>");
                return;
            }

            nroAfiliado_txt = nroafiliado.Text;
            var checkDNIandNroAfil = from afil in db.Afiliado
                                     where afil.nroDNI == dni_int
                                     select afil;
            var checkafiliado = checkDNIandNroAfil.First()?.AfiliadoID;

            if (checkafiliado.HasValue)
                IDAfiliado = checkafiliado.Value;

            if (IDAfiliado == 0)
                Response.Write("<script>alert('Probando...');</script>");

            int.TryParse(plan_combo.Text, out plan_id);
            if (plan_id == 0){
                Response.Write("<script>alert('Error.');</script>");
                return;
            }

            if (checkDNIandNroAfil.Count() == 1){
                var checkPlanAndObra = from plan in db.PlanObraSocial
                                       where plan.PlanId == plan_id &&
                                             plan.PlanId == checkDNIandNroAfil.First().IDPlan
                                       select plan;

                if (checkPlanAndObra.Count() == 1)
                    Response.Redirect(BaseUrl.url + "VerificarEmail.aspx?IDAfiliado=" + IDAfiliado);
                else
                    Response.Write("<script>alert('Los datos ingresados y/o seleccionados no son correctos.');</script>");
                    return;
            }
            else
            {
                Response.Write("<script>alert('Los datos ingresados y/o seleccionados no son correctos.');</script>");
                return;
            }

        }

        protected void Volver_btn_Click(object sender, EventArgs e){
            dni.Text = String.Empty;
            nroafiliado.Text = String.Empty;
            Response.Redirect(BaseUrl.url + "Ingreso.aspx");
        }
    }
}
