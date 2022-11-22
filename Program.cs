using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppEscritorio
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MedicalHome(initMedico()));
            //Application.Run(new Home(init()));
            Application.Run(new Ingreso());
        }

        private static Classes.Usuario init() {
            Classes.TablesDataContext db = new Classes.TablesDataContext();
            return (from m in db.Usuario
                    where m.UsuarioID == 4
                    select m).FirstOrDefault();
        }

        private static Classes.Medico initMedico() {
            Classes.TablesDataContext db = new Classes.TablesDataContext();
            return (from m in db.Medico
                    where m.MedicoID == 1
                    select m).FirstOrDefault();
        }
    }
}
