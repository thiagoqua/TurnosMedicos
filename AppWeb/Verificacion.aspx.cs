using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;

namespace AppWeb
{
    public partial class Verificacion : System.Web.UI.Page
    {
        TablesDataContext db = new TablesDataContext(); //'TABLES' CONTIENE LAS TABLAS GENERADAS CON LINQ-TO-SQL
        private string strAfiliado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var obraSocial = from obra in db.ObraSocial          //CARGO DATOS DE OBRA SOCIAL
                                 select obra;
                obrasocial_combo.DataTextField = "ObraSocialDescripcion";
                obrasocial_combo.DataValueField = "ObraSocialId";
                obrasocial_combo.DataSource = obraSocial;
                obrasocial_combo.DataBind();

                CargarComboPlan(sender, e);
            }
        }

        private void CargarComboPlan(object sender, EventArgs e)
        {
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

        protected void obrasocial_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboPlan(sender, e);
        }

        protected void plan_combo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void volver_btn_Click(object sender, EventArgs e)
        {

        }

        protected void Verificar_link_Click(object sender, EventArgs e)
        {
            if (Dni.Text.Trim() == "" || nroafiliado.Text.Trim() == "")
            {
                Response.Write("<script>alert('Complete los campos faltantes.');</script>");
                return;
            }
            var checkDNIandNroAfil = from afil in db.Afiliado
                                     where afil.nroDNI.ToString() == Dni.Text &&
                                     afil.NroAfiliado == nroafiliado.Text
                                     select afil;

            int IDAfiliado = 0;
            foreach (Afiliado afil in checkDNIandNroAfil)
            {
                IDAfiliado = afil.AfiliadoID;
                strAfiliado = IDAfiliado.ToString();
            }

            if (checkDNIandNroAfil.Count() == 1)
            {

                var checkPlanAndObra = from plan in db.PlanObraSocial
                                       where plan.PlanDescripcion == plan_combo.Text &&
                                       plan.PlanId == checkDNIandNroAfil.First().IDPlan
                                       select plan;

                if (checkPlanAndObra.Count() == 1)
                {

                    Response.Write("<script>alert('La verificacion fue exitosa. Cierre la ventana emergente y proceda a registrarse.');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Los datos ingresados y/o seleccionados no son correctos.');</script>");
                    return;
                }
            }
            else
            {
                Response.Write("<script>alert('Los datos ingresados y/o seleccionados no son correctos.');</script>");
                return;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Dni.Text.Trim() == "" || nroafiliado.Text.Trim() == "")
            {
                Response.Write("<script>alert('Complete los campos faltantes.');</script>");
                return;
            }
            int dni_int = 0;
            int.TryParse(Dni.Text, out dni_int);
            if (dni_int == 0)
            {
                Response.Write("<script>alert('Error.');</script>");
                return;
            }

            var nroAfiliado_txt = nroafiliado.Text;
            var checkDNIandNroAfil = from afil in db.Afiliado
                                     where afil.nroDNI == dni_int
                                     select afil;

            int IDAfiliado = 0;

            var checkafiliado = checkDNIandNroAfil.FirstOrDefault()?.AfiliadoID;

            if (checkafiliado.HasValue)
            {
                IDAfiliado = checkafiliado.Value;
            }

            if (IDAfiliado == 0)
            {
                Response.Write("<script>alert('Probando...');</script>");
            }

            int plan_id = 0;
            int.TryParse(plan_combo.Text, out plan_id);
            if (plan_id == 0)
            {
                Response.Write("<script>alert('Error.');</script>");
                return;
            }


            if (checkDNIandNroAfil.Count() == 1)
            {
                var checkPlanAndObra = from plan in db.PlanObraSocial
                                       where plan.PlanId == plan_id &&
                                       plan.PlanId == checkDNIandNroAfil.First().IDPlan
                                       select plan;

                if (checkPlanAndObra.Count() == 1)
                {
                    Response.Redirect(Constante.BaseUrl.baseurl + "VerificarEmail.aspx?IDAfiliado=" + IDAfiliado);
                }
                else
                {
                    Response.Write("<script>alert('Los datos ingresados y/o seleccionados no son correctos.');</script>");
                    return;
                }
            }
            else
            {
                Response.Write("<script>alert('Los datos ingresados y/o seleccionados no son correctos.');</script>");
                return;
            }

        }
    }
}