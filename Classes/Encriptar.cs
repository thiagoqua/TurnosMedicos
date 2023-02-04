using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Classes{
    public class Encriptar{
        /// <summary>
        ///     Genera la encriptación de una contraseña usango el algoritmo de encriptación
        ///     SHA256.
        /// </summary>
        /// <param name="str">contraseña en texto plano</param>
        /// <returns>La contraseña que se pasa como argumento encriptada.</returns>
        public static string GetSHA256(string str){
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

    }
}
