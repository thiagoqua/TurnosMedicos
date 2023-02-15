using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes{
    public class Validar{
        
        //Se valida si el email tiene el formato correcto
        public static bool IsValidEmail(string email){
            try{
                var emailValido = new System.Net.Mail.MailAddress(email);
                return emailValido.Address == email;
            }
            catch{
                return false;
            }
        }

        //Se valida si el email existe en la bdd
        public static bool ExistingMail(string email) {
            TablesDataContext db = new TablesDataContext();

            var mail = from e in db.Usuario
                        where email == e.UsuarioEmail
                        select e;

            return mail.Count() > 0;
        }

        //Validacion del email y contraseña del usuario para el login
        public static Usuario Validate(string email, string pass){
            TablesDataContext db = new TablesDataContext();
            Usuario logging;
            string lookupPassword = null, ePass;

            if(!IsValidEmail(email))
                return null;

            if((pass == null) || (pass.Length == 0))
                return null;

            var mail = from user in db.Usuario
                       where email == user.UsuarioEmail
                       select user;
            
            if(mail.Count() > 0){
                logging = mail.First();
                lookupPassword = logging.UsuarioContraseña;
            }
            else
                return null;

            ePass = Encriptar.GetSHA256(pass);

            if (ePass.Equals(lookupPassword))
                return logging;
            
            return null;
        }

    }
}
