﻿using Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppEscritorio
{
    public partial class RecuperarContraseña : Form{
        private VerificarEmail ve;
        private TablesDataContext db;
        private string emisor;
        private string mailUsuario;
        private string pass;

        public RecuperarContraseña(){
            InitializeComponent();
            db = new TablesDataContext();
            ve = new VerificarEmail(0);
        }

        public string getMailUsuario(){
            return mailUsuario;
        }

        private void button1_Click(object sender, EventArgs e){
            if (!ve.isValidEmail(textBox1.Text)){
                MessageBox.Show("Ingrese un mail válido.");
                return;
            }
            else if (!ve.checkEmail(textBox1.Text)){
                MessageBox.Show("El mail no se encuentra en la base de datos. Pruebe con otro.");
                return;
            }
            else{
                mailUsuario = textBox1.Text;
                var servidor = from m in db.ServidorMail
                               select m;
                emisor = servidor.First().Mail;
                pass = servidor.First().Pass;

                ve.setNroVerif(EnviarMail.Enviar(emisor, pass, textBox1.Text, false));
                MessageBox.Show("Hemos enviado un mensaje a su correo. Siga las instrucciones.");

                label2.Visible = button2.Visible = textBox2.Visible = true;

                button1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e){
            bool verificacion;
            if (textBox2.Text.Trim() == ""){
                MessageBox.Show("Ingrese el codigo.");
                return;
            }

            verificacion = ve.Verificacion(textBox2);

            if(verificacion){
                label3.Visible = label4.Visible = textBox3.Visible = textBox4.Visible = button3.Visible = true;
                
                button2.Enabled = false;

                pictureBox2.Visible = pictureBox3.Visible = pictureBox4.Visible = pictureBox5.Visible = true;

                textBox3.PasswordChar = '*';
                textBox4.PasswordChar = '*';

            }

        }

        private void label2_Click(object sender, EventArgs e){

        }

        private void textBox2_TextChanged(object sender, EventArgs e){

        }

        private void RecuperarContraseña_Load(object sender, EventArgs e){
            label2.Visible = label3.Visible = label4.Visible = false;
            
            textBox2.Visible = textBox3.Visible = textBox4.Visible = false;
            
            button2.Visible = button3.Visible = false;

            pictureBox2.Visible = pictureBox3.Visible = pictureBox4.Visible = pictureBox5.Visible = false;

        }

        private void pictureBox1_Click(object sender, EventArgs e){
            this.Hide();
            Login login = new Login();
            login.Show();
            this.Close();
        }

        public void actualizarUsuario(string mailUser, string newPass){
            Usuario user;

            var getUsuario = from usuario in db.Usuario
                             where mailUser == usuario.UsuarioEmail
                             select usuario;
            user = getUsuario.First();
            user.UsuarioContraseña = Encriptar.GetSHA256(newPass);

            try{
                db.SubmitChanges();
            }
            catch(Exception ex){
                throw (ex);
            }
        }

        private void button3_Click(object sender, EventArgs e){
            if (textBox3.Text.Trim() == "" || textBox4.Text.Trim() == ""){
                MessageBox.Show("Complete los campos faltantes.");
                return;
            }
            else if (textBox3.Text == textBox4.Text){
                MessageBox.Show("Su contraseña ha sido cambiada con éxito. Intente loguearse.");
                actualizarUsuario(getMailUsuario(),textBox4.Text);

                this.Hide();
                Login login = new Login();
                login.Show();
                this.Close();
            }
            else{
                MessageBox.Show("Las contraseñas no coinciden. Vuelva a intentarlo.");
                return;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e){
            pictureBox2.BringToFront();
            textBox3.PasswordChar = '\0';
        }

        private void pictureBox2_Click(object sender, EventArgs e){
            pictureBox3.BringToFront();
            textBox3.PasswordChar = '*';
        }

        private void pictureBox4_Click(object sender, EventArgs e){
            pictureBox5.BringToFront();
            textBox4.PasswordChar = '*';
        }

        private void pictureBox5_Click(object sender, EventArgs e){
            pictureBox4.BringToFront();
            textBox4.PasswordChar = '\0';
        }

        private void textBox3_TextChanged(object sender, EventArgs e){
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e){
            
        }
    }
}
