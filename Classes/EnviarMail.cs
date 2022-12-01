using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace Classes
{
    public class EnviarMail
    {
        public static int Enviar(string emisor, string password, string receptor, bool bit)  //'PASSWORD' SERÁ LA CONTRASEÑA DE APLICACION GENERADA PARA LAS APPS MENOS SEGURAS
        {
            int nro = 0;

            if(bit == false)
            {
                Random r = new Random();
                nro = r.Next(100000, 1000000);      //GENERO UN NRO ALEATORIO PARA LA VERIFICACION
            }

            MailMessage msg = new MailMessage();    //CREO EL OBJETO PARA GENERAR EL CORREO
            msg.To.Add(receptor);                   //AGREGO EL MAIL DEL RECEPTOR
            msg.Subject = "Correo de verificacion"; //AGREGO UN ASUNTO (TE LO PIDE A LA FUERZA)
            msg.SubjectEncoding = Encoding.UTF8;    //AGREGO UNA CODIFICACION MEDIANTE UTF8

            if (bit)
            {
                msg.Body = "Ha solicitado un cambio de contraseña. Por favor, haga click en el siguiente link para crear una nueva contraseña: https://localhost:44332/ChangePass.aspx";
            }
            else
            {
                msg.Body = "Su codigo de verificacion es '" + nro + "'.\nPor favor, ingrese este numero en la casilla designada en la aplicacion.";
            }
            
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;                  //PARA QUE PUEDA SER ENTENDIDO USAMOS EL FORMATO HTML
            msg.From = new MailAddress(emisor);     //EL QUE ENVIA EL MSJ ES EL EMISOR
            
            SmtpClient cliente = new SmtpClient();  //PROTOCOLO PARA EL ENVIO DEL MSJ
            cliente.Credentials = new NetworkCredential(emisor, password);  //C# USARÁ NUESTRO CORREO PARA EL ENVIO, POR LO QUE LE PASAMOS NUESTRO MAIL Y CONTRASEÑA
            cliente.Port = 587;                     //DEFINIMOS EL PUERTO DONDE SE ESTABLECERÁ LA CONEXION Y EL ENVÍO
            cliente.EnableSsl = true;               //CIFRA LA CONEXION
            cliente.Host = "smtp.gmail.com";

            try
            {
                cliente.Send(msg);                  //ENVIO EL MSJ
            }
            catch (Exception)                       //SI EL CORREO FALLA ENTONCES...
            {

                MessageBox.Show("No se pudo enviar el correo. Por favor, reintente.");
                nro = 0;
            }
            finally                 //LIBERAMOS LOS RECURSOS
            {
                msg.Dispose();
                cliente.Dispose();
            }
            return nro;
        }
    }
}
