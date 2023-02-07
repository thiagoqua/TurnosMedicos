using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Classes{
    public class Encriptar{
        /// <summary>
        ///     Genera la encriptación de una contraseña usando el algoritmo de encriptación SHA256.
        /// </summary>
        /// <param name="str">Contraseña en texto plano</param>
        /// <returns>Contraseña encriptada.</returns>
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
