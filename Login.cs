using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Classes;

namespace AppEscritorio{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e) {
            TablesDataContext db = new TablesDataContext();
            var checkUser = from user in db.Usuario
                            where user.UsuarioEmail == UsuarioTxt.Text &&
                                  user.UsuarioContraseña == ContraseñaTxt.Text
                            select user;
            if(checkUser.Count() == 1){
                Usuario logged = checkUser.First();
                IQueryable<Afiliado> queryAfiliado = from af in db.Afiliado
                                                     where af.AfiliadoID == logged.IDAfiliado
                                                     select af;
                Afiliado aLogged = queryAfiliado.First();
                MessageBox.Show("Bienvenido al sistema " + aLogged.Nombre.Trim() + " " + aLogged.Apellido.Trim());
                if(logged.isMedico) {
                    Medico mLogged = new Medico();
                    var getMLogged = from medico in db.Medico
                                     where medico.MedicoID == logged.UsuarioID
                                     select medico;
                    mLogged = getMLogged.First();
                    MedicalHome mhome = new MedicalHome(mLogged);
                    mhome.Show();
                    this.Hide();
                }
                else {
                    Home home = new Home(logged);
                    home.Show();
                    this.Hide();
                }
            }
            else{   
                MessageBox.Show("Usuario o contraseña incorrectos, por favor intente de nuevo");
                return;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UsuarioTxt_TextChanged(object sender, EventArgs e) {

        }

        private void DniLbl_Click(object sender, EventArgs e) {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            RecuperarContraseña rc = new RecuperarContraseña();
            rc.Show();
            this.Close();
        }
    }
}
