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

        public static bool Validate(string email, string pass)
        {
            TablesDataContext db = new TablesDataContext();
            string lookupPassword = null;

            // Check for invalid email.
            if (IsValidEmail(email) == false)
            {
                return false;
            }

            // Check for invalid passWord.
            // passWord must not be null and must be between 1 and 25 characters.
            if ((null == pass) || (0 == pass.Length))
            {
                return false;
            }

            var mail = from e in db.Usuario
                        where email == e.UsuarioEmail
                        select e.UsuarioContraseña;

            // Execute command and fetch pwd field into lookupPassword string.
            if(mail.Count() > 0)
            {
                lookupPassword = mail.FirstOrDefault().ToString();
            }
            else
            {
                return false;
            }

            string ePass = Encriptar.GetSHA256(pass);

            if (ePass.Equals(lookupPassword))
            {
                return true;
            }
            
            return false;

        }

    }
}
