using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class ConsultasBdd
    {
        public static void ActualizarUsuario(string mailUser, string newPass)
        {
            TablesDataContext db = new TablesDataContext();

            var getUsuario = from usuario in db.Usuario
                             where mailUser == usuario.UsuarioEmail
                             select usuario;

            Usuario user = getUsuario.First();
            user.UsuarioContraseña = Encriptar.GetSHA256(newPass);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
