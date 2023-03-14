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

namespace AppEscritorio{
    public partial class VerificarEmail : Form{
        
        private string emisor;
        private string pass;
        private readonly int IDAfiliado;
        private int nroVerif;
        private readonly TablesDataContext db;

        //se inicializan algunos datos que servirán para la verificación
        public VerificarEmail(int id){
            InitializeComponent();
            IDAfiliado = id;
            nroVerif = 0;
            db = new TablesDataContext();
        }

        public int GetIDAfiliado(){
            return IDAfiliado;
        }

        public int GetNroVerif(){
            return nroVerif;
        }

        public void SetNroVerif(int em){
            nroVerif = em;
        }

        //Se validan los datos ingresados para que el codigo pueda ser enviado al email correspondiente
        private void Button1_Click(object sender, EventArgs e){
            if (Validar.IsValidEmail(textBox1.Text) == false){
                MessageBox.Show("Ingrese un mail válido.");
                return;
            }
            else if (Validar.ExistingMail(textBox1.Text) == true){
                MessageBox.Show("El mail ingresado ya se encuentra registrado. Pruebe con otro mail o proceda a loguearse.");
                return;
            }
            else{
                var servidor = from m in db.ServidorMail
                               select m;

                emisor = servidor.First().Mail;
                pass = servidor.First().Pass;

                SetNroVerif(EnviarMail.Enviar(emisor, pass, textBox1.Text,false));
                MessageBox.Show("Hemos enviado un mensaje a su correo. Siga las instrucciones.");
            }
        }

        //Funcion auxiliar para la verificación del código ingresado
        public bool Verificacion(TextBox textBox2){
            DialogResult yesno = DialogResult.No;
            do{
                if (yesno == DialogResult.Yes){
                    SetNroVerif(EnviarMail.Enviar(emisor, pass, textBox1.Text,false));
                    MessageBox.Show("Hemos enviado un nuevo código a su correo.");
                }

                //se envió el correo
                if(GetNroVerif() != 0){
                    //si el número es igual al verificado
                    if(nroVerif.ToString() == textBox2.Text){
                        MessageBox.Show("¡Verificacion exitosa!");
                        return true;
                    }
                    else{
                        MessageBox.Show("Codigo no válido. Reintente.");
                    }
                }
                else{
                    yesno = MessageBox.Show("Ocurrió un problema. ¿Desea reenviar el correo?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
            }
            while (yesno == DialogResult.Yes);
            return false;
        }

        //Verificación del código ingresado
        private void Button2_Click(object sender, EventArgs e){
            if (textBox2.Text.Trim() == ""){
                MessageBox.Show("Ingrese el codigo.");
                return;
            }

            bool verificacion = Verificacion(textBox2);

            if (verificacion == true){
                Hide();
                Registro reg = new Registro(textBox1.Text, GetIDAfiliado());
                reg.Show();
                Close();
            }
        }

        public void VerificarEmail_Load(object sender, EventArgs e){

        }

        private void TextBox2_TextChanged(object sender, EventArgs e){

        }

        //Funcion para retornar al formulario previo
        private void Button3_Click(object sender, EventArgs e){
            Hide();
            Verificacion verif = new Verificacion();
            verif.Show();
            Close();
        }

        private void Label2_Click(object sender, EventArgs e){

        }
    }
}