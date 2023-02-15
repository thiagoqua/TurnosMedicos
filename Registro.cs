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
        
        private readonly TablesDataContext db;
        private readonly string email;
        private static bool isMedico;
        private static int IDPerfil;
        private readonly int IDAfiliado;

        //Se inicializan algunos datos que servirán para la modificacion del usuario en la bdd
        public Registro(string e,int id){
            InitializeComponent();
            db = new TablesDataContext();
            email = e;
            IDAfiliado = id;
            IDPerfil = 1;
            isMedico = false;
        }

        public string GetEmail(){
            return email;
        }

        public int GetIDAfiliado(){
            return IDAfiliado;
        }

        //Se chequea que los campos para la contraseña no estén vacíos y que ambos coincidan en el contenido
        private void Button1_Click(object sender, EventArgs e){
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == ""){
                MessageBox.Show("Complete los campos faltantes.");
                return;
            }
            else if (textBox1.Text == textBox2.Text){
                MessageBox.Show("¡Su registro ha sido completado! Proceda a ingresar a su cuenta.");

                //Se actualiza la contraseña del usuario en la bdd
                InsertarUsuario(email, textBox1.Text);

                //Se redirige al usuario a Login
                Hide();
                Login login = new Login();
                login.Show();
                Close();
            }
            else{
                MessageBox.Show("Las contraseñas no coinciden. Vuelva a intentarlo.");
                return;
            }
        }

        //Se modifica al usuario en la bdd
        private void InsertarUsuario(string email, string pass){
            Usuario user = new Usuario
            {
                IDAfiliado = GetIDAfiliado(),
                IDPerfil = IDPerfil,
                isMedico = isMedico,
                UsuarioEmail = email,
                UsuarioContraseña = Encriptar.GetSHA256(pass)
            };

            db.Usuario.InsertOnSubmit(user);
            try{
                db.SubmitChanges();
            }
            catch(Exception){
            }
        }

        //Las siguientes funciones son para los iconos que visibilizan o no la contraseña
        private void Registro_Load(object sender, EventArgs e){
            textBox1.PasswordChar = '*';
            textBox2.PasswordChar = '*';
        }

        private void PictureBox1_Click(object sender, EventArgs e){
            pictureBox2.BringToFront();
            textBox1.PasswordChar = '*';
        }

        private void PictureBox2_Click(object sender, EventArgs e){
            pictureBox1.BringToFront();
            textBox1.PasswordChar = '\0';
        }

        private void PictureBox3_Click(object sender, EventArgs e){
            pictureBox4.BringToFront();
            textBox2.PasswordChar = '*';
        }

        private void PictureBox4_Click(object sender, EventArgs e){
            pictureBox3.BringToFront();
            textBox2.PasswordChar = '\0';
        }
    }
}