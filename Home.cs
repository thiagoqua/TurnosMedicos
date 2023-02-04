using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classes;

namespace AppEscritorio {
    public partial class Home : Form {
        private Usuario whoAmI;
        private Afiliado whoAmIAsAfiliado;
        private TablesDataContext db;

        //almacena los TextBoxes que hay en la interfaz
        private List<TextBox> boxes;
        //almacena los turnos que los pacientes tienen con el médico
        private string[,] descripcionTurno;

        public Home(Usuario logged) {
            InitializeComponent();
            db = new TablesDataContext();
            whoAmI = logged;
            whoAmIAsAfiliado = (from af in db.Afiliado
                                where af.AfiliadoID == whoAmI.IDAfiliado
                                select af).First();
            boxes = new List<TextBox>();
        }

        /// <summary>
        ///     Crea una cierta cantidad de TextBoxes con los parámetros necesarios, invisibles 
        ///     y los agrega a la lista boxes.
        /// </summary>
        /// <param name="cant">cantidad de TextBoxes que hay que crear</param>
        private void makeBoxes(int cant) {
            /*
              tanto textBoxTurno0 (lado izquierdo) como textBoxTurno1 (lado derecho) 
              son los dos TextBoxes que ya se encuentran puestos en la interfaz, 
              pero de manera invisible. Existen ya que se usan como "molde" para crear 
              los próximos TextBoxes en caso de que sean necesarios.
            */
            boxes.Clear();
            const int offsetY = 180,
                      offsetX = 200,
                      lineLength = 2;
            int deltaY, deltaX,Xposition, Yposition;
            deltaY = deltaX = Xposition = Yposition = 0;
            TextBox toAdd;
            switch(cant) {
                case 1:
                    boxes.Add(textBoxTurno0);
                    return;
                case 2:
                    boxes.Add(textBoxTurno0);
                    boxes.Add(textBoxTurno1);
                    return;
                default:
                    boxes.Add(textBoxTurno0);
                    boxes.Add(textBoxTurno1);
                    for(int i = 2; i < cant; ++i) {
                        toAdd = new TextBox();
                        switch(i % lineLength) {
                            case 0:
                                //pertenece a un box del lado izquierdo
                                toAdd.Clear();
                                deltaY += offsetY;
                                Xposition = boxes[0].Location.X;
                                Yposition = boxes[0].Location.Y;
                                toAdd.Size = boxes[0].Size;
                                toAdd.Multiline = true;
                                if(i == 3)
                                toAdd.Location = new Point(Xposition + offsetX,
                                                           Yposition + deltaY);
                                break;
                            case 1:
                                //pertenece a un box del lado derecho
                                toAdd.Clear();
                                Xposition = boxes[1].Location.X;
                                Yposition = boxes[1].Location.Y;
                                toAdd.Size = boxes[1].Size;
                                toAdd.Multiline = true;
                                toAdd.Location = new Point(Xposition + offsetX,
                                                           Yposition + deltaY);
                                break;
                        }
                        toAdd.BorderStyle = boxes[0].BorderStyle;
                        toAdd.BackColor = boxes[0].BackColor;
                        toAdd.Font = boxes[0].Font;
                        boxes.Add(toAdd);
                    }
                    break;
            }
        }

        private void VerTurnos_Click(object sender, EventArgs e) {
        int szQuery, i;
        string[] lines = new string[7];
        Turno tempTurno;
        Afiliado tempMedico;
        List<Turno> queryTurnos = (from turno in db.Turno
                                    where turno.IDUsuario == whoAmI.UsuarioID
                                    select turno).ToList();
            
        szQuery = queryTurnos.Count();
        //elimino de la interfaz los TextBoxes si es que hay visibles
        for(i = 0; i < boxes.Count(); ++i) {
            if(i < 2) {
                boxes[i].Visible = false;
                continue;
            }
            Controls.Remove(boxes[i]);
        }
        if(szQuery == 0) {
            generatePDF.Visible = false;
            return;
        }

        makeBoxes(szQuery);
        descripcionTurno = new string[szQuery, 4];
        //se arma el texto donde se detalla cada turno
        for(i = 0; i < szQuery; ++i) {
            tempTurno = queryTurnos[i];
            descripcionTurno[i, 0] = "Fecha del turno: ";
            DateTime queryFecha = (from ft in db.FechaTurno
                                    where ft.FechaTurnoID == tempTurno.IDFechaTurno
                                    select ft.Fecha).First();
            descripcionTurno[i, 0] += queryFecha.ToString("d/MM/yyyy");

            descripcionTurno[i, 1] = "Para médico: ";
            tempMedico = (from medico in db.Medico
                            join medicoUsuario in db.Usuario
                                on medico.IDUsuario equals medicoUsuario.UsuarioID
                            join medicoAfiliado in db.Afiliado
                                on medicoUsuario.IDAfiliado equals medicoAfiliado.AfiliadoID
                            where tempTurno.IDMedico == medico.MedicoID
                            select medicoAfiliado).First();
            descripcionTurno[i, 1] += tempMedico.Nombre.Trim() + " " + tempMedico.Apellido.Trim();

            var querySucursal = from suc in db.Sucursal
                                where suc.SucursalId == tempTurno.IDSucursal
                                select suc;
            descripcionTurno[i, 2] = "En sucursal: " +
                    querySucursal.First().SucursalDescripcion.Trim();

            var queryProvincia = from prov in db.Provincia
                                    where prov.ProvinciaId == tempTurno.IDProvincia
                                    select prov;
            descripcionTurno[i, 2] += ", " +
                    queryProvincia.First().ProvinciaDescripcion.Trim();

            var queryLocalidad = from loc in db.Localidad
                                    where loc.LocalidadId == tempTurno.IDLocalidad
                                    select loc;
            descripcionTurno[i, 2] += ", " +
                    queryLocalidad.First().LocalidadDescripcion.Trim();

            descripcionTurno[i, 3] = "Horario: ";
            TimeSpan horario = (from h in db.Horario
                                join ft in db.FechaTurno
                                    on h.HorarioID equals ft.IDHorario
                                where tempTurno.IDFechaTurno == ft.FechaTurnoID
                                select h.Hora).First();
            descripcionTurno[i, 3] += horario.ToString();
        }

        for(i = 0; i < szQuery; ++i) {
            //salteo posiciones para crear un espacio entre cada línea del TextBox
            lines[0] = descripcionTurno[i, 0];
            lines[2] = descripcionTurno[i, 1];
            lines[4] = descripcionTurno[i, 2];
            lines[6] = descripcionTurno[i, 3];
            boxes[i].Visible = true;
            boxes[i].Enabled = false;
            boxes[i].Lines = lines;
            if(i < 2)
                continue;
            Controls.Add(boxes[i]);
            boxes[i].BringToFront();
        }
        generatePDF.Visible = true;
        }

        private void generatePDF_Click(object sender, EventArgs e) {
            PDFDrawer drawer;
            string filepath, filename, caption, msg;
            
            filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
            filename = "reporte.pdf";
            drawer = new PDFDrawer(filepath, filename);

            drawer.drawFromUser(whoAmIAsAfiliado,descripcionTurno);

            caption = "Reporte generado de manera exitosa";
            msg = "El reporte ha sido creado y almacenado en " + filepath +
                  " con el nombre de " + filename;

            MessageBox.Show(msg, caption, MessageBoxButtons.OK);
        }

        private void SolicitarTurno_Click(object sender, EventArgs e) {
            Turnos turno = new Turnos(whoAmI,this);
            this.Hide();
            turno.Show();

        }

        private void btnCerrar_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void logo_Click(object sender, EventArgs e) {

        }

        private void OtrasOpciones_Click(object sender, EventArgs e) {
            Contacto contacto = new Contacto(this);
            this.Hide();
            contacto.Show();
        }
    }
}
