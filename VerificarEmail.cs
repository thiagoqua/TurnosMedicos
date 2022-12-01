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
using Microsoft.VisualBasic;

namespace AppEscritorio
{
    public partial class VerificarEmail : Form
    {
        private string emisor;
        private string pass;
        private int IDAfiliado = -1;
        private int nroVerif = 0;
        TablesDataContext db = new TablesDataContext();

        public VerificarEmail(int id)
        {
            InitializeComponent();
            IDAfiliado = id;
        }

        public int getIDAfiliado()
        {
            return IDAfiliado;
        }
        public int getNroVerif()
        {
            return nroVerif;
        }
        public void setNroVerif(int em)
        {
            nroVerif = em;
        }

        public bool isValidEmail(string email)
        {
            try
            {
                var emailValido = new System.Net.Mail.MailAddress(email);
                return emailValido.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool checkEmail(string email)
        {

            var mail = from e in db.Usuario
                       where email == e.UsuarioEmail
                       select e;
            if (mail.Count() >= 1)
            {
                return true;
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)                  //VERIFICAR MAIL
        {

            if (isValidEmail(textBox1.Text) == false)
            {
                MessageBox.Show("Ingrese un mail válido.");
                return;
            }
            else if (checkEmail(textBox1.Text) == true)
            {
                MessageBox.Show("El mail ingresado ya se encuentra registrado. Pruebe con otro mail o proceda a loguearse.");
                return;
            }
            else
            {

                var servidor = from m in db.ServidorMail
                               select m;
                emisor = servidor.FirstOrDefault().Mail;
                pass = servidor.FirstOrDefault().Pass;

                setNroVerif(EnviarMail.Enviar(emisor, pass, textBox1.Text,false));
                MessageBox.Show("Hemos enviado un mensaje a su correo. Siga las instrucciones.");

            }

        }

        public bool Verificacion(TextBox textBox2)
        {
            DialogResult yesno = DialogResult.No;

            do
            {

                if (yesno == DialogResult.Yes)
                {
                    setNroVerif(EnviarMail.Enviar(emisor, pass, textBox1.Text,false));
                    MessageBox.Show("Hemos enviado un nuevo código a su correo.");
                }

                if (getNroVerif() != 0)            //SE ENVIÓ EL CORREO
                {
                    if (nroVerif.ToString() == textBox2.Text)         //SI EL NRO ES IGUAL ESTÁ VERIFICADO
                    {
                        MessageBox.Show("¡Verificacion exitosa!");
                        yesno = DialogResult.No;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Codigo no válido. Reintente.");
                        //return;
                    }
                }
                else
                {
                    yesno = MessageBox.Show("Ocurrió un problema. ¿Desea reenviar el correo?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
            }
            while (yesno == DialogResult.Yes);

            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese el codigo.");
                return;
            }

            bool verificacion = false;

            verificacion = Verificacion(textBox2);

            if (verificacion == true)
            {
                this.Hide();
                Registro reg = new Registro(textBox1.Text, getIDAfiliado());
                reg.Show();
                this.Close();
            }
        }

        public void VerificarEmail_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Verificacion verif = new Verificacion();
            verif.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}