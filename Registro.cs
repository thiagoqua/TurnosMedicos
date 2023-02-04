using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Classes;

namespace AppEscritorio
{
    public partial class Registro : Form{
        private TablesDataContext db;
        private string email;
        private static bool isMedico;
        private static int IDPerfil;
        private int IDAfiliado;

        public Registro(string e,int id){
            InitializeComponent();
            db = new TablesDataContext();
            email = e;
            IDAfiliado = id;
            IDPerfil = 1;
            isMedico = false;
        }

        public string getEmail(){
            return email;
        }

        public int getIDAfiliado(){
            return IDAfiliado;
        }

        private void button1_Click(object sender, EventArgs e){
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == ""){
                MessageBox.Show("Complete los campos faltantes.");
                return;
            }
            else if (textBox1.Text == textBox2.Text){
                MessageBox.Show("¡Su registro ha sido completado! Proceda a ingresar a su cuenta.");

                //ACTUALIZAR BDD INGRESANDO EL NVO USUARIO
                insertarUsuario(email, textBox1.Text);

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

        private void insertarUsuario(string email, string pass){
            Usuario user = new Usuario();
            user.IDAfiliado = getIDAfiliado();
            user.IDPerfil = IDPerfil;
            user.isMedico = isMedico;
            user.UsuarioEmail = email;
            user.UsuarioContraseña = Encriptar.GetSHA256(pass);

            db.Usuario.InsertOnSubmit(user);
            try{
                db.SubmitChanges();
            }
            catch(Exception){
            }
        }

        private void Registro_Load(object sender, EventArgs e){
            textBox1.PasswordChar = '*';
            textBox2.PasswordChar = '*';
        }

        private void pictureBox1_Click(object sender, EventArgs e){
            pictureBox2.BringToFront();
            textBox1.PasswordChar = '*';
        }

        private void pictureBox2_Click(object sender, EventArgs e){
            pictureBox1.BringToFront();
            textBox1.PasswordChar = '\0';
        }

        private void pictureBox3_Click(object sender, EventArgs e){
            pictureBox4.BringToFront();
            textBox2.PasswordChar = '*';
        }

        private void pictureBox4_Click(object sender, EventArgs e){
            pictureBox3.BringToFront();
            textBox2.PasswordChar = '\0';
        }
    }
}