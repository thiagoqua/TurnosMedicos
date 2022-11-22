using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Classes;

namespace AppEscritorio
{
    public partial class Registro : Form
    {
        TablesDataContext db = new TablesDataContext(); //Tables contiene las tablas generadas con linq to SQL
        string email = "";
        static bool isMedico = false;
        static int IDPerfil = 1;
        int IDAfiliado = -1;
        //bool usado = false;

        public Registro(string e,int id)
        {
            InitializeComponent();
            email = e;
            IDAfiliado = id;
        }

        public string getEmail()
        {
            return email;
        }
        public int getIDAfiliado()
        {
            return IDAfiliado;
        }

        private void button1_Click(object sender, EventArgs e)      //REGISTRARSE
        {

            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Complete los campos faltantes.");
                return;
            }
            else if (textBox1.Text == textBox2.Text)
            {
                MessageBox.Show("¡Su registro ha sido completado! Proceda a ingresar a su cuenta.");

                //ACTUALIZAR BDD INGRESANDO EL NVO USUARIO
                insertarUsuario(email, textBox1.Text);

                this.Hide();
                Login login = new Login();
                login.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden. Vuelva a intentarlo.");
                return;
            }
            

        }

        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        //DESENCRIPTAR CONTRASEÑA PARA LOGUEARSE (PARA TIKI)
        public void login(string user, string pass) //el usuario ingresa su usuario y contraseña
        {
            /*
                la contraseña que ingresa el usuario para loguearse se encripta
                 y la cadena que genera es la misma que la cadena de la contraseña encriptada
                  que se generó cuando el usuario hizo el registro
             */
            string ePass = GetSHA256(pass);

            /*
             CONSULTA SUPUESTA A LA BASE DE DATOS PARA CORROBORAR USUARIO Y CONTRASEÑA (SE PUEDE ADAPTAR EN BASE A TU CODIGO)
             */
            var query = from u in db.Usuario
                       where u.UsuarioEmail == user && u.UsuarioContraseña == ePass
                       select u;
            if(query != null)
            {
                //la contraseña existe
            }
            else
            {
                //los datos son incorrectos (no ingresó bien la contraseña)
            }
        }

        private void insertarUsuario(string email, string pass)
        {
            Usuario user = new Usuario();
            user.IDAfiliado = getIDAfiliado();
            user.IDPerfil = IDPerfil;
            user.isMedico = isMedico;
            user.UsuarioEmail = email;
            user.UsuarioContraseña = GetSHA256(pass);

            /*
            do
            {
                usado = false;
                
                Random r = new Random();
                user.UsuarioID = r.Next(0, 1000);

                var id = from us in db.Usuario
                         where user.UsuarioID == us.UsuarioID
                         select user;
                
                if(id.Count() >= 1)
                {
                    usado = true;
                }

            } while (usado == true);
            */

            db.Usuario.InsertOnSubmit(user);
            try
            {
                db.SubmitChanges();
            }
            catch(Exception ex)
            {
                //throw (ex);
            }
        }

        private void Registro_Load(object sender, EventArgs e)      //NO BORRAR O SE ROMPE TODO
        {
            textBox1.PasswordChar = '*';
            textBox2.PasswordChar = '*';
        }

        private void pictureBox1_Click(object sender, EventArgs e)  //ocultar
        {
            pictureBox2.BringToFront();
            textBox1.PasswordChar = '*';
        }

        private void pictureBox2_Click(object sender, EventArgs e)  //mostrar
        {
            pictureBox1.BringToFront();
            textBox1.PasswordChar = '\0';
        }

        private void pictureBox3_Click(object sender, EventArgs e)  //ocultar repetir
        {
            pictureBox4.BringToFront();
            textBox2.PasswordChar = '*';
        }

        private void pictureBox4_Click(object sender, EventArgs e)  //mostrar repetir
        {
            pictureBox3.BringToFront();
            textBox2.PasswordChar = '\0';
        }
    }
}
