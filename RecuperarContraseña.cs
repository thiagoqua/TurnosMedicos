using Classes;
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
        
        private readonly VerificarEmail ve;
        private readonly TablesDataContext db;
        private string emisor;
        private string mailUsuario;
        private string pass;

        public RecuperarContraseña(){
            InitializeComponent();
            db = new TablesDataContext();
            ve = new VerificarEmail(0);
        }

        public string GetMailUsuario(){
            return mailUsuario;
        }

        //Verificación del mail
        private void Button1_Click(object sender, EventArgs e){
            if (!Validar.IsValidEmail(textBox1.Text)){
                MessageBox.Show("Ingrese un mail válido.");
                return;
            }
            else if (!Validar.ExistingMail(textBox1.Text)){
                MessageBox.Show("El mail no se encuentra en la base de datos. Pruebe con otro.");
                return;
            }
            else{
                mailUsuario = textBox1.Text;
                var servidor = from m in db.ServidorMail
                               select m;
                emisor = servidor.First().Mail;
                pass = servidor.First().Pass;

                ve.SetNroVerif(EnviarMail.Enviar(emisor, pass, textBox1.Text, false));
                MessageBox.Show("Hemos enviado un mensaje a su correo. Siga las instrucciones.");

                label2.Visible = button2.Visible = textBox2.Visible = true;

                button1.Enabled = false;
            }
        }

        //Verificacion del código
        //Llamo a una clase y utilizo un método de esa clase para realizar la verificación (MALA IMPLEMENTACIÓN)
        private void Button2_Click(object sender, EventArgs e){
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

        private void Label2_Click(object sender, EventArgs e){

        }

        private void TextBox2_TextChanged(object sender, EventArgs e){

        }

        //Cuando el formulario cargue algunos botones estarán desactivados para evitar validaciones innecesarias
        private void RecuperarContraseña_Load(object sender, EventArgs e){
            label2.Visible = label3.Visible = label4.Visible = false;
            textBox2.Visible = textBox3.Visible = textBox4.Visible = false;
            button2.Visible = button3.Visible = false;
            pictureBox2.Visible = pictureBox3.Visible = pictureBox4.Visible = pictureBox5.Visible = false;

        }

        //El ícono de 'flecha hacia atrás' indica que se puede volver a Login y evitar cambiar la contraseña si el usuario se arrepintió
        private void PictureBox1_Click(object sender, EventArgs e){
            Hide();
            Login login = new Login();
            login.Show();
            Close();
        }

        //Se modifica el usuario en la bdd con la nueva contraseña encriptada
        public void ActualizarUsuario(string mailUser, string newPass){
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

        //Se chequea que los campos para la contraseña no estén vacíos y que ambos coincidan en el contenido
        private void Button3_Click(object sender, EventArgs e){
            if (textBox3.Text.Trim() == "" || textBox4.Text.Trim() == ""){
                MessageBox.Show("Complete los campos faltantes.");
                return;
            }
            else if (textBox3.Text == textBox4.Text){
                MessageBox.Show("Su contraseña ha sido cambiada con éxito. Intente loguearse.");
                
                //Se actualiza la contraseña del usuario en la bdd y se redirige al usuario a Login
                ActualizarUsuario(GetMailUsuario(),textBox4.Text);

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

        //Las 4 funciones debajo alternan el icono para visibilizar o no la contraseña
        private void PictureBox3_Click(object sender, EventArgs e){
            pictureBox2.BringToFront();
            textBox3.PasswordChar = '\0';
        }

        private void PictureBox2_Click(object sender, EventArgs e){
            pictureBox3.BringToFront();
            textBox3.PasswordChar = '*';
        }

        private void PictureBox4_Click(object sender, EventArgs e){
            pictureBox5.BringToFront();
            textBox4.PasswordChar = '*';
        }

        private void PictureBox5_Click(object sender, EventArgs e){
            pictureBox4.BringToFront();
            textBox4.PasswordChar = '\0';
        }

        private void TextBox3_TextChanged(object sender, EventArgs e){
            
        }

        private void TextBox4_TextChanged(object sender, EventArgs e){
            
        }
    }
}
