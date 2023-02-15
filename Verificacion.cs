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

namespace AppEscritorio{
    public partial class Verificacion : Form{
        
        private readonly TablesDataContext db;

        public Verificacion(){
            InitializeComponent();
            db = new TablesDataContext();
        }

        //Se cargan de antemano los datos de la obra social
        private void Form1_Load(object sender, EventArgs e){
            var obraSocial = from obra in db.ObraSocial      
                             select obra;

            comboBox1.DisplayMember = "ObraSocialDescripcion";
            comboBox1.ValueMember = "ObraSocialId";
            comboBox1.DataSource = obraSocial;
        }

        //De acuerdo a la obra social seleccionada, aparecen los planes relacionados a esa obra social
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e){
            var planlist = from plan in db.PlanObraSocial
                           where plan.IDObraSocial == (int)comboBox1.SelectedValue
                           select plan;

            comboBox2.DisplayMember = "PlanDescripcion";
            comboBox2.ValueMember = "PlanId";
            comboBox2.DataSource = planlist;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e){

        }

        //Verifico cada dato ingresado por el usuario con lo que hay almacenado en la bdd
        private void Button1_Click(object sender, EventArgs e){
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == ""){
                MessageBox.Show("Complete los campos faltantes.");
                return;
            }
            int IDAfiliado = 0;
            var checkDNIandNroAfil = from afil in db.Afiliado
                           where afil.nroDNI.ToString() == textBox1.Text &&
                           afil.NroAfiliado == textBox2.Text
                           select afil;

            foreach(Afiliado afil in checkDNIandNroAfil)
                IDAfiliado = afil.AfiliadoID;

            if (checkDNIandNroAfil.Count() == 1) {
                var checkPlanAndObra = from plan in db.PlanObraSocial
                                       where plan.PlanDescripcion == comboBox2.Text &&
                                             plan.PlanId == checkDNIandNroAfil.First().IDPlan
                                       select plan;

                if(checkPlanAndObra.Count() == 1){
                    MessageBox.Show("La verificacion fue exitosa. Cierre la ventana emergente y proceda a registrarse.");
                    
                    //Se redirige al usuario y se continúa con el proceso de registro
                    Hide();
                    VerificarEmail ve = new VerificarEmail(IDAfiliado);
                    ve.Show();
                    Close();
                }
                else{
                    MessageBox.Show("Los datos ingresados no son correctos.");
                    return;
                }
            }
            else{
                MessageBox.Show("Los datos ingresados no son correctos.");
                return;
            }
        }

        //Vuelvo al formulario Ingreso
        private void Button2_Click(object sender, EventArgs e){
            Hide();
            Ingreso ingreso = new Ingreso();
            ingreso.Show();
            Close();
        }
    }
}
