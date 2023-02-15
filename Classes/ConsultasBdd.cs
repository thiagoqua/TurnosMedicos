using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes{
    public class ConsultasBdd{
        
        //Funcion para modificar usuarios ya ingresados previamente en la bdd
        //El dato que se modifica es la contraseña
        public static void ActualizarUsuario(string mailUser, string newPass){
            TablesDataContext db = new TablesDataContext();
            Usuario user;

            user = (from usuario in db.Usuario
                    where mailUser == usuario.UsuarioEmail
                    select usuario).First();

            user.UsuarioContraseña = Encriptar.GetSHA256(newPass);
            try{
                db.SubmitChanges();
            }
            catch (Exception ex){
                throw (ex);
            }
        }
    }
}
