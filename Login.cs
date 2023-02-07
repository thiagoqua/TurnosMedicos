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
    public partial class Login : Form{
        public Login(){
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e){
        }

        private void Button1_Click(object sender, EventArgs e) {
            
            TablesDataContext db = new TablesDataContext();
            
            //Se encripta la contraseña ingresada y se verifica que coincida con la contraseña encriptada que se guardó en la bdd
            string ePass = Encriptar.GetSHA256(ContraseñaTxt.Text);
            var checkUser = from user in db.Usuario
                            where user.UsuarioEmail == UsuarioTxt.Text &&
                                  user.UsuarioContraseña == ePass
                            select user;

            //Si coinciden se muestra un mensaje de bienvenida y se redirige al usuario a Home
            if(checkUser.Count() == 1){
                Usuario logged = checkUser.First();
                Afiliado aLogged;
                var queryAfiliado = from af in db.Afiliado
                                    where af.AfiliadoID == logged.IDAfiliado
                                    select af;
                aLogged = queryAfiliado.First();
                MessageBox.Show("Bienvenido al sistema " + aLogged.Nombre.Trim() + " " + 
                                                           aLogged.Apellido.Trim());
                
                //Si el usuario es médico o paciente, se redigirán al Home correspondiente
                if(logged.isMedico) {
                    Medico mLogged = new Medico();
                    var getMLogged = from medico in db.Medico
                                     where medico.IDUsuario == logged.UsuarioID
                                     select medico;
                    mLogged = getMLogged.First();
                    MedicalHome mhome = new MedicalHome(mLogged);
                    Hide();
                    mhome.Show();
                }
                else {
                    Home home = new Home(logged);
                    home.Show();
                    Hide();
                }
                Close();
            }
            else{   
                MessageBox.Show("Usuario o contraseña incorrectos, por favor intente de nuevo");
                return;
            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void BtnMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void BtnMax_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

        }

        //Si el usuario clickea la X, se cierra la aplicación
        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UsuarioTxt_TextChanged(object sender, EventArgs e) {

        }

        private void DniLbl_Click(object sender, EventArgs e) {

        }

        //Si el usuario olvidó la contraseña, se lo redirige al formulario para que modifique la contraseña
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            RecuperarContraseña rc = new RecuperarContraseña();
            rc.Show();
            Close();
        }
    }
}
