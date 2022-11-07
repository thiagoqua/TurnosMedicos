using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classes;

namespace AppEscritorio
{
    public partial class Verificacion : Form
    {

        TablesDataContext db = new TablesDataContext(); //'TABLES' CONTIENE LAS TABLAS GENERADAS CON LINQ-TO-SQL

        public Verificacion()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var obraSocial = from obra in db.ObraSocial            //CARGO DATOS DE OBRA SOCIAL
                             select obra;
            comboBox1.DisplayMember = "ObraSocialDescripcion";
            comboBox1.ValueMember = "ObraSocialId";
            comboBox1.DataSource = obraSocial;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //OBRA SOCIAL
        {
            var planlist = from plan in db.PlanObraSocial
                           where plan.IDObraSocial == (int)comboBox1.SelectedValue
                           select plan;
            comboBox2.DisplayMember = "PlanDescripcion";
            comboBox2.ValueMember = "PlanId";
            comboBox2.DataSource = planlist;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) //PLAN
        {

        }

        private void button1_Click(object sender, EventArgs e)  //BOTON VERIFICAR
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Complete los campos faltantes.");
                return;
            }
            var checkDNIandNroAfil = from afil in db.Afiliado
                           where afil.nroDNI.ToString() == textBox1.Text &&
                           afil.NroAfiliado == textBox2.Text
                           select afil;

            int IDAfiliado = 0;
            foreach(Afiliado afil in checkDNIandNroAfil)
            {
                IDAfiliado = afil.AfiliadoID;
            }

            if (checkDNIandNroAfil.Count() == 1)
            {
                var checkPlanAndObra = from plan in db.PlanObraSocial
                                       where plan.PlanDescripcion == comboBox2.Text &&
                                       plan.PlanId == checkDNIandNroAfil.First().IDPlan
                                       select plan;

                if(checkPlanAndObra.Count() == 1)
                {
                    MessageBox.Show("La verificacion fue exitosa. Cierre la ventana emergente y proceda a registrarse.");
                    this.Hide();
                    VerificarEmail ve = new VerificarEmail(IDAfiliado);
                    ve.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Los datos ingresados y/o seleccionados no son correctos.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Los datos ingresados y/o seleccionados no son correctos.");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ingreso ingreso = new Ingreso();
            ingreso.Show();
            this.Close();
        }
    }
}
