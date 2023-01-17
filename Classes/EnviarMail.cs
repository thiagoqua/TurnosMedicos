using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using Classes;

namespace Classes {
    public class EnviarMail {
        TablesDataContext db;
        Afiliado medicoInCuestion;
        MailMessage msg;
        SmtpClient cliente;
        string sender, psw;
        string[] bodyParts, motivos;

        //recibe el UsuarioId del médico que cambia la disponibilidad
        public EnviarMail(int MedicoUsuarioId) {
            db = new TablesDataContext();

            var datos = from d in db.ServidorMail
                        select d;
            sender = datos.First().Mail;
            psw = datos.First().Pass;
            medicoInCuestion = (from af in db.Afiliado
                                join user in db.Usuario
                                    on af.AfiliadoID equals user.IDAfiliado
                                where user.UsuarioID == MedicoUsuarioId
                                select af).First();
            cliente = new SmtpClient();
            cliente.Credentials = new NetworkCredential(sender, psw);
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.Host = "smtp.gmail.com";

            bodyParts = new string[4];
            bodyParts[0] = "le informamos que su turno para el doctor ";
            bodyParts[1] = " con fecha ";
            bodyParts[2] = " y hora ";
            bodyParts[3] = " ha sido cancelado debido a que el médico ya no estrá disponible " +
                           "en dicho momento. Por favor, le pedimos que saque un nuevo turno " +
                           "a través de nuestra página web para poder conservar la asistencia. " + "<br />";

            motivos = new string[4];
            motivos[0] = "El motivo de la baja de su turno se da ya que el médico ha atrasado " +
                         "su horario de llegada a la sucursal, por lo que el mismo lamentablemente " +
                         "pasó a quedar fuera de su rango horario de trabajo.";
            motivos[1] = "El motivo de la baja de su turno se da ya que el médico ha adelantado " +
                         "su horario de salida de la sucursal, por lo que el mismo lamentablemente " +
                         "pasó a quedar fuera de su rango horario de trabajo.";
            motivos[2] = "El motivo de la baja de su turno se da ya que el médico no trabaja " + 
                         "más ese día en la sucursal.";
            motivos[3] = "El motivo de la baja de su turno se da ya que el médico no trabaja " +
                         "más en esa sucursal";
        }

        public static int Enviar(string emisor, string password, string receptor, bool bit)  //'PASSWORD' SERÁ LA CONTRASEÑA DE APLICACION GENERADA PARA LAS APPS MENOS SEGURAS
        {
            int nro = 0;
            TablesDataContext db = new TablesDataContext();
            //int id = -1;

            if(bit == false) {
                Random r = new Random();
                nro = r.Next(100000, 1000000);      //GENERO UN NRO ALEATORIO PARA LA VERIFICACION
            }
            else {
                //obtengo el id del usuario que realizó la peticion y lo hago en base al mail ingresado
                var account = from a in db.Usuario
                              where receptor == a.UsuarioEmail
                              select a.UsuarioID;

                //genero el registro en la tabla NuevaContraseña

                NuevaContraseña nc = new NuevaContraseña {
                    IDUsuario = account.FirstOrDefault(),
                    Creacion = DateTime.Now
                };

                db.NuevaContraseña.InsertOnSubmit(nc);

                try {
                    db.SubmitChanges();
                }
                catch(Exception ex) {
                    throw (ex);
                }

            }

            MailMessage msg = new MailMessage();    //CREO EL OBJETO PARA GENERAR EL CORREO
            msg.To.Add(receptor);                   //AGREGO EL MAIL DEL RECEPTOR
            msg.Subject = "Correo de verificacion"; //AGREGO UN ASUNTO (TE LO PIDE A LA FUERZA)
            msg.SubjectEncoding = Encoding.UTF8;    //AGREGO UNA CODIFICACION MEDIANTE UTF8

            if(bit) {
                //agrego el id del nuevo registro de la tabla NuevaContraseña para crear links temporales unicos
                string newUrl = makeUrl();
                msg.Body = "Ha solicitado un cambio de contraseña. Por favor, haga click en el siguiente link para crear una nueva contraseña: " + newUrl + " - El link se vencerá pasada una hora.";
            }
            else {
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

            try {
                cliente.Send(msg);                  //ENVIO EL MSJ
            }
            catch(Exception)                       //SI EL CORREO FALLA ENTONCES...
            {
                /*
                if(bit == false)
                {
                    MessageBox.Show("No se pudo enviar el correo. Por favor, reintente.");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No se ha podido enviar el correo. Por favor, reintente.')", true);
                }
                */

                nro = 0;
            }
            finally                 //LIBERAMOS LOS RECURSOS
            {
                msg.Dispose();
                cliente.Dispose();
            }
            return nro;
        }

        public static string makeUrl() {
            TablesDataContext db = new TablesDataContext();

            var getId = from g in db.NuevaContraseña
                        orderby g.ID descending
                        select g.ID;

            string url = "https://localhost:44332/ChangePass.aspx" + "?var=" + getId.FirstOrDefault();

            return url;
        }

        public void advicePatient(Usuario patient, DateTime fecha, TimeSpan hora, Motivo motivo) {
            Afiliado patientAsAfiliado = (from af in db.Afiliado
                                          where af.AfiliadoID == patient.IDAfiliado
                                          select af).First();
            msg = new MailMessage();
            msg.To.Add(patient.UsuarioEmail);          
            msg.Subject = "Baja de turno"; 
            msg.SubjectEncoding = Encoding.UTF8;
            msg.Body = "Estimado " + patientAsAfiliado.Nombre.Trim() + ", " +
                       bodyParts[0] + medicoInCuestion.Nombre.Trim() + " " 
                                    + medicoInCuestion.Apellido.Trim() + 
                       bodyParts[1] + fecha.ToString("dd/MM/yyyy") +
                       bodyParts[2] + hora.ToString() + bodyParts[3] + "<br />";
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;                  
            msg.From = new MailAddress(sender);     

            switch(motivo) {
                case Motivo.HICHANGED:
                    msg.Body += motivos[0];
                    break;
                case Motivo.HFCHANGED:
                    msg.Body += motivos[1];
                    break;
                case Motivo.DAY:
                    msg.Body += motivos[2];
                    break;
                case Motivo.SUCURSAL:
                    msg.Body += motivos[3];
                    break;
            }
            msg.Body += "<br /><br />Disculpas cordiales.";

            try {
                cliente.Send(msg);                  
            }
            catch(Exception){}
            finally{
                msg.Dispose();
                cliente.Dispose();
            }
        }

        public enum Motivo {
            HICHANGED,HFCHANGED,DAY,SUCURSAL
        }
    }
}
