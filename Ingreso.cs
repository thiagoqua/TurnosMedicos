using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppEscritorio{
    public partial class Ingreso : Form{
        public Ingreso(){
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e){

        }

        //Botón Registro
        private void Button1_Click(object sender, EventArgs e){
            Hide();
            new Verificacion().Show();
        }

        //Botón Ingreso
        private void Button2_Click(object sender, EventArgs e){
            Hide();
            new Login().Show();
        }
    }
}
