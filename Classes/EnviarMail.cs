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
        private TablesDataContext db;
        //medico que alteró su disponibilidad
        private Afiliado medicoInCuestion;
        private MailMessage msg;
        private SmtpClient cliente;
        private string sender, psw;
        private string[] bodyParts, motivos;

        /// <param name="MedicoUsuarioId">
        ///     el UsuarioId del médico que cambia la disponibilidad
        /// </param>
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

        /// <summary>
        ///     Genera y envía un mail al nuevo usuario del sistema con el fin de verificarle
        ///     su dirección de email.
        /// </summary>
        /// <param name="emisor">dirección email del emisor</param>
        /// <param name="password">contraseña de aplicación generada para aplicaciones menos seguras</param>
        /// <param name="receptor">dirección email del receptor</param>
        /// <returns>El número aleatorio generado para la posterior verificación.</returns>
        public static int Enviar(string emisor, string password, string receptor, bool bit){
            int nro = 0;
            TablesDataContext db = new TablesDataContext();

            if(!bit) {
                Random r = new Random();
                nro = r.Next(100000, 1000000);
            }
            else {
                //obtengo el id del usuario que realizó la peticion en base al mail ingresado
                var account = from a in db.Usuario
                              where receptor == a.UsuarioEmail
                              select a.UsuarioID;

                //genero el registro en la tabla NuevaContraseña
                NuevaContraseña nc = new NuevaContraseña {
                    IDUsuario = account.First(),
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

            //Construyo un correo con los datos necesarios
            MailMessage msg = new MailMessage();    
            msg.To.Add(receptor);                   
            msg.Subject = "Correo de verificacion"; 
            msg.SubjectEncoding = Encoding.UTF8;    

            if(bit) {
                //agrego el id del nuevo registro de la tabla NuevaContraseña para crear links temporales unicos
                string newUrl = makeUrl();
                msg.Body = "Ha solicitado un cambio de contraseña. Por favor, " + 
                           "haga click en el siguiente link para crear una nueva contraseña: " + 
                           newUrl + " - El link se vencerá pasada una hora.";
            }
            else {
                msg.Body = "Su codigo de verificacion es '" + nro + 
                           "'.\nPor favor, ingrese este numero en la casilla designada en " + 
                           "la aplicacion.";
            }

            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.From = new MailAddress(emisor);

            SmtpClient cliente = new SmtpClient();
            //usaremos nuestro correo como emisor del correo
            cliente.Credentials = new NetworkCredential(emisor, password); 
            cliente.Port = 587;               
            cliente.EnableSsl = true;
            cliente.Host = "smtp.gmail.com";

            try {
                cliente.Send(msg);                  
            }
            catch(Exception){
                nro = 0;
            }
            finally{
                //liberamos los recursos
                msg.Dispose();
                cliente.Dispose();
            }
            return nro;
        }

        /// <returns>La URL que va a permitir realizar el cambio de contraseña.</returns>
        public static string makeUrl() {
            TablesDataContext db = new TablesDataContext();
            var getId = from g in db.NuevaContraseña
                        orderby g.ID descending
                        select g.ID;

            return "https://localhost:44332/ChangePass.aspx" + "?var=" + getId.First();
        }

        /// <summary>
        ///     Avisa al paciente que un turno suyo con un médico en específico fue dado de baja
        ///     por motivos de cambios de disponibilidad del mismo en la sucursal del turno.
        /// </summary>
        /// <param name="patient">paciente en cuestión</param>
        /// <param name="fecha">fecha del turno dado de baja</param>
        /// <param name="hora">hora del turno dado de baja</param>
        /// <param name="motivo">motivo del turno dado de baja</param>
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
                //liberamos los recursos
                msg.Dispose();
                cliente.Dispose();
            }
        }

        /// <summary>
        ///     Constantes utilizadas para describir el motivo de la baja del turno del 
        ///     paciente por parte del médico.
        /// </summary>
        public enum Motivo {
            HICHANGED,HFCHANGED,DAY,SUCURSAL
        }
    }
}
