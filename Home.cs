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
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace AppEscritorio {
    public partial class Home : Form {
        private Usuario whoAmI;
        private Afiliado whoAmIAsAfiliado;
        private TablesDataContext db;

        private List<TextBox> boxes;
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

        private void makeBoxes(int cant) {
            boxes.Clear();
            const int offsetY = 180,
                      offsetX = 200,
                      lineLength = 2;
            int deltaY, Xposition, Yposition;
            deltaY = Xposition = Yposition = 0;
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
                        querySucursal.FirstOrDefault().SucursalDescripcion.Trim();

                var queryProvincia = from prov in db.Provincia
                                     where prov.ProvinciaId == tempTurno.IDProvincia
                                     select prov;
                descripcionTurno[i, 2] += ", " +
                        queryProvincia.FirstOrDefault().ProvinciaDescripcion.Trim();

                var queryLocalidad = from loc in db.Localidad
                                     where loc.LocalidadId == tempTurno.IDLocalidad
                                     select loc;
                descripcionTurno[i, 2] += ", " +
                        queryLocalidad.FirstOrDefault().LocalidadDescripcion.Trim();

                descripcionTurno[i, 3] = "Horario: ";
                TimeSpan horario = (from h in db.Horario
                                    join ft in db.FechaTurno
                                        on h.HorarioID equals ft.IDHorario
                                    where tempTurno.IDFechaTurno == ft.FechaTurnoID
                                    select h.Hora).FirstOrDefault();
                descripcionTurno[i, 3] += horario.ToString();
            }

            for(i = 0; i < szQuery; ++i) {
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
            PdfDocument document = new PdfDocument();
            PdfPage actualPage = document.AddPage();
            XFont titleFont, textFont;
            XGraphics gfx = XGraphics.FromPdfPage(actualPage);
            XTextFormatter formatter = new XTextFormatter(gfx);
            XRect rect;
            int deltaY, cantTurnos;
            const int MAX_TURNOS_PER_PAGE = 7;
            string filepath, filename, caption, msg;

            filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
            filename = "reporte.pdf";
            deltaY = 80; cantTurnos = descripcionTurno.Length / 4;
            rect = new XRect(0, 0, actualPage.Width, actualPage.Height);

            titleFont = new XFont("Calibri", 40, XFontStyle.Bold);
            textFont = new XFont("Calibri", 12);

            //Escribo el título
            gfx.DrawString("Turnos para " + whoAmIAsAfiliado.Nombre.Trim() + " "
                                          + whoAmIAsAfiliado.Apellido.Trim(),
                                 titleFont,
                                 XBrushes.DarkBlue,
                                 rect,
                                 XStringFormats.TopCenter
            );
            
            rect.X = 20;
            rect.Y = deltaY;
            for(int i = 0; i < cantTurnos; ++i) {
                deltaY += 80;
                rect.Y = deltaY;
                gfx.DrawString(descripcionTurno[i, 0],
                               textFont,
                               XBrushes.Black,
                               rect,
                               XStringFormats.TopLeft
                );

                rect.Y = deltaY + 15;
                gfx.DrawString(descripcionTurno[i, 1],
                               textFont,
                               XBrushes.Black,
                               rect,
                               XStringFormats.TopLeft
                );

                rect.Y = deltaY + 30;
                gfx.DrawString(descripcionTurno[i, 2],
                               textFont,
                               XBrushes.Black,
                               rect,
                               XStringFormats.TopLeft
                );

                rect.Y = deltaY + 45;
                gfx.DrawString(descripcionTurno[i, 3],
                               textFont,
                               XBrushes.Black,
                               rect,
                               XStringFormats.TopLeft
                );

                if(i % MAX_TURNOS_PER_PAGE == MAX_TURNOS_PER_PAGE - 1 &&
                   i != cantTurnos - 1) {
                    deltaY = 0;
                    actualPage = document.AddPage();
                    gfx = XGraphics.FromPdfPage(actualPage);
                }
            }

            document.Save(filepath + filename);

            caption = "Reporte generado de manera exitosa";
            msg = "El reporte ha sido creado y almacenado en " + filepath +
                  " con el nombre de " + filename;

            MessageBox.Show(msg, caption, MessageBoxButtons.OK);
        }

        private void SolicitarTurno_Click(object sender, EventArgs e) {
            Turnos turno = new Turnos(whoAmI,this);
            turno.Show();
            this.Hide();
        }
    }
}
