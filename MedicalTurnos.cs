using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using Classes;

namespace AppEscritorio {
    public partial class MedicalTurnos : Form {
        private Medico whoAmI;
        private Afiliado whoAmIAsAfiliado;
        private TablesDataContext db;
        private IList<TextBox> boxes = new List<TextBox>();
        private string[,] descripcionTurno;

        private MedicalHome previousState;

        public MedicalTurnos(Medico medico,MedicalHome home) {
            InitializeComponent();
            whoAmI = medico;
            previousState = home;
            db = new TablesDataContext();
            whoAmIAsAfiliado = (from af in db.Afiliado
                                join user in db.Usuario
                                    on af.AfiliadoID equals user.IDAfiliado
                                where user.UsuarioID == whoAmI.IDUsuario
                                select af).First();
        }

        private void makeBoxes(int cant) {
            boxes.Clear();
            const int offsetY = 140,
                      lineLength = 2;
            int deltaY,Xposition,Yposition;
            deltaY = Xposition = Yposition = 0;
            TextBox toAdd;
            switch(cant) {
                case 1:
                    boxes.Add(textBoxTurno1);
                    return;
                case 2:
                    boxes.Add(textBoxTurno1); 
                    boxes.Add(textBoxTurno2);
                    return;
                default:
                    boxes.Add(textBoxTurno1);
                    boxes.Add(textBoxTurno2);
                    for(int i = 2;i < cant; ++i) {
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
                                toAdd.Location = new Point(Xposition, Yposition + deltaY);
                                break;
                            case 1:
                                //pertenece a un box del lado derecho
                                toAdd.Clear();
                                Xposition = boxes[1].Location.X;
                                Yposition = boxes[1].Location.Y;
                                toAdd.Size = boxes[1].Size;
                                toAdd.Multiline = true;
                                toAdd.Location = new Point(Xposition, Yposition + deltaY);
                                break;
                        }
                        boxes.Add(toAdd);
                    }
                    break;
            }
        }

        private void MedicalTurnos_Load(object sender, EventArgs e) {

        }

        private void fecha_ValueChanged(object sender, EventArgs e) {
            string[] lines = new string[5];
            DateTime selected = Convert.ToDateTime(fecha.Value.Date);
            int szQuery;
            Turno tempTurno;
            lines[1] = "";  //para que haya un espacio entre la información
            var queryTurnos = (from turno in db.Turno
                               join ft in db.FechaTurno
                                 on turno.IDFechaTurno equals ft.FechaTurnoID
                               where turno.IDMedico == whoAmI.MedicoID && ft.Fecha == selected
                               select turno).ToList();
            for(int i = 0;i < boxes.Count();++i) {
                if(i < 2) {
                    boxes[i].Visible = false;
                    continue;
                }
                Controls.Remove(boxes[i]);
            }
            szQuery = queryTurnos.Count();
            if(szQuery == 0) {
                generatePDF.Visible = false;
                return;
            }
            makeBoxes(szQuery);
            descripcionTurno = new string[szQuery,3];
            for(int i = 0;i < szQuery;++i){
                tempTurno = queryTurnos[i];
                descripcionTurno[i,0] = "Turno sacado por: ";
                Afiliado afiliado = (from af in db.Afiliado
                                     join user in db.Usuario
                                        on af.AfiliadoID equals user.IDAfiliado
                                     where user.UsuarioID == tempTurno.IDUsuario
                                     select af).FirstOrDefault();
                descripcionTurno[i,0] += afiliado.Nombre.Trim() + " " + afiliado.Apellido.Trim();

                var querySucursal = from suc in db.Sucursal
                                    where suc.SucursalId == tempTurno.IDSucursal
                                    select suc;
                descripcionTurno[i,1] = "En sucursal: " + 
                        querySucursal.FirstOrDefault().SucursalDescripcion;

                var queryProvincia = from prov in db.Provincia
                                     where prov.ProvinciaId == tempTurno.IDProvincia
                                     select prov;
                descripcionTurno[i,1] += ", " + 
                        queryProvincia.FirstOrDefault().ProvinciaDescripcion;

                var queryLocalidad = from loc in db.Localidad
                                     where loc.LocalidadId == tempTurno.IDLocalidad
                                     select loc;
                descripcionTurno[i,1] += ", " + 
                        queryLocalidad.FirstOrDefault().LocalidadDescripcion.Trim();

                descripcionTurno[i, 2] = "Horario: ";
                TimeSpan horario = (from h in db.Horario
                                    join ft in db.FechaTurno
                                        on h.HorarioID equals ft.IDHorario
                                    where tempTurno.IDFechaTurno == ft.FechaTurnoID
                                    select h.Hora).FirstOrDefault();
                descripcionTurno[i, 2] += horario.ToString();
            }
            for(int i = 0;i < szQuery; ++i) {
                lines[0] = descripcionTurno[i, 0];
                lines[2] = descripcionTurno[i, 1];
                lines[4] = descripcionTurno[i, 2];
                boxes[i].Visible = true;
                boxes[i].Enabled = false;
                boxes[i].Lines = lines;
                if(i < 2)
                    continue;
                Controls.Add(boxes[i]);
            }
            generatePDF.Visible = true;
        }

        private void textbox_Paint(object sender, PaintEventArgs e) {
            
        }

        private void BarraTitulo_Paint(object sender, PaintEventArgs e) {

        }

        private void btnCerrar_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void scroller_Scroll(object sender, ScrollEventArgs e) {
            
        }

        private void btnCerrar_Click_1(object sender, EventArgs e) {
            Application.Exit();
        }

        private void back_Click(object sender, EventArgs e) {
            previousState.Show();
            this.Close();
        }

        private void generatePDF_Click(object sender, EventArgs e) {
            PdfDocument document = new PdfDocument();
            PdfPage actualPage = document.AddPage();
            XFont titleFont,headerFont,textFont;
            XGraphics gfx = XGraphics.FromPdfPage(actualPage);
            XTextFormatter formatter = new XTextFormatter(gfx);
            XRect rect;
            int deltaY,cantTurnos;
            const int MAX_TURNOS_PER_PAGE = 8;
            string header = "Turnos para el día " + fecha.Value.ToString("dddd") + " " +
                            fecha.Value.Day.ToString() + " de " +
                            fecha.Value.ToString("MMMM") + " de " +
                            fecha.Value.Year,
                   filepath, filename, caption, msg;

            filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
            filename = "reporte.pdf";
            deltaY = 80; cantTurnos = descripcionTurno.Length / 3;
            rect = new XRect(0, 0, actualPage.Width, actualPage.Height);

            titleFont = new XFont("Calibri", 40, XFontStyle.Bold);
            headerFont = new XFont("Calibri", 16, XFontStyle.Underline);
            textFont = new XFont("Calibri", 12);
            
            //Escribo el título
            gfx.DrawString("Doctor " + whoAmIAsAfiliado.Nombre.Trim() + " "
                                     + whoAmIAsAfiliado.Apellido.Trim(),
                                 titleFont,
                                 XBrushes.DarkBlue,
                                 rect,
                                 XStringFormats.TopCenter
            );

            //Escribo la fecha
            rect.X = 20;
            rect.Y = deltaY;
            formatter.DrawString(header,
                                 headerFont,
                                 XBrushes.DarkRed,
                                 rect,
                                 XStringFormats.TopLeft
            );

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

            MessageBox.Show(msg, caption,MessageBoxButtons.OK);
        }
    }
}
