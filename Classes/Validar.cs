using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Validar
    {

        public static bool IsValidEmail(string email)
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

        public static Usuario Validate(string email, string pass)
        {
            TablesDataContext db = new TablesDataContext();
            Usuario logging;
            string lookupPassword = null;

            // Check for invalid email.
            if (IsValidEmail(email) == false)
            {
                return null;
            }

            // Check for invalid passWord.
            // passWord must not be null and must be between 1 and 25 characters.
            if ((null == pass) || (0 == pass.Length))
            {
                return null;
            }

            var mail = (from user in db.Usuario
                        where email == user.UsuarioEmail
                        select user);

            // Execute command and fetch pwd field into lookupPassword string.
            if(mail.Count() > 0){
                logging = mail.FirstOrDefault();
                lookupPassword = logging.UsuarioContraseña;
            }
            else{
                return null;
            }

            string ePass = Encriptar.GetSHA256(pass);

            if (ePass.Equals(lookupPassword))
            {
                return logging;
            }
            
            return null;

        }

    }
}
