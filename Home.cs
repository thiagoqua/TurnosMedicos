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
    public partial class Home : Form
    {
        public Home(Usuario logged){
            InitializeComponent();
        }

        private void SolicitarTurno_Click(object sender, EventArgs e)
        {

        }

        private void VerTurnos_Click(object sender, EventArgs e)
        {
            Turnos open = new Turnos();
            this.Hide();
            open.Show();
        }

        private void OtrasOpciones_Click(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void BarraTitulo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
