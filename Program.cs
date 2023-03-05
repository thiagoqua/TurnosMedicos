﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppEscritorio{
    internal static class Program{
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(){
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Ingreso());
            Application.Run(new MedicalHome((from m in new Classes.TablesDataContext().Medico
                                             where m.MedicoID == 15
                                             select m).First()));
        }
    }
}
