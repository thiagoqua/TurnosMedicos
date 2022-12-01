using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb {
    public partial class WebHome : System.Web.UI.Page {
        private Usuario whoAmI;
        private Afiliado whoAmIAsAfiliado;
        private TablesDataContext db;

        private List<TextBox> boxes;
        private string[,] descripcionTurno;

        public WebHome() {
            
        }

        protected void Page_Load(object sender, EventArgs e) {
            if(IsPostBack) {
                db = (TablesDataContext) Session["database"];
                descripcionTurno = (string[,]) Session["turnos"];
                whoAmI = (Usuario) Session["user"];
                whoAmIAsAfiliado = (Afiliado) Session["afiliado"];
                boxes = new List<TextBox>();
            }
            else {
                //ESTA PARTE VENDRÍA A REEMPLAZAR EL CONSTRUCTOR
                db = new TablesDataContext();
                whoAmI = init();
                whoAmIAsAfiliado = (from af in db.Afiliado
                                    where af.AfiliadoID == whoAmI.IDAfiliado
                                    select af).First();
                boxes = new List<TextBox>();

                Session["database"] = db;
                Session["user"] = whoAmI;
                Session["afiliado"] = whoAmIAsAfiliado;
            }
        }

        private static Usuario init() {
            Classes.TablesDataContext db = new Classes.TablesDataContext();
            return (from m in db.Usuario
                    where m.UsuarioID == 6
                    select m).FirstOrDefault();
        }

        private void makeBoxes(int cant) {
            boxes.Clear();
            const int lineLength = 2;
            TextBox toAdd;
            switch(cant) {
                case 1:
                    boxes.Add((TextBox)FindControl("textboxTurno0"));
                    return;
                case 2:
                    boxes.Add((TextBox)FindControl("textboxTurno0"));
                    boxes.Add((TextBox)FindControl("textboxTurno1"));
                    return;
                default:
                    boxes.Add((TextBox)FindControl("textboxTurno0"));
                    boxes.Add((TextBox)FindControl("textboxTurno1"));
                    for(int i = 2; i < cant; ++i) {
                        toAdd = new TextBox();
                        switch(i % lineLength) {
                            case 0:
                                //pertenece a un box del lado izquierdo
                                toAdd.Text = "";
                                toAdd.Height = boxes[0].Height;
                                toAdd.Width = boxes[0].Width;
                                break;
                            case 1:
                                //pertenece a un box del lado derecho
                                toAdd.Text = "";
                                toAdd.Height = boxes[1].Height;
                                toAdd.Width = boxes[1].Width;
                                break;
                        }
                        boxes.Add(toAdd);
                    }
                    break;
            }
        }

        protected void verTurnosButton_Click(object sender, EventArgs e) {
            label1.Visible = label2.Visible = label3.Visible = false;
            int szQuery, i;
            Turno tempTurno;
            Afiliado tempMedico;
            List<Turno> queryTurnos = (from turno in db.Turno
                                       where turno.IDUsuario == whoAmI.UsuarioID
                                       select turno).ToList();
            const string newline = "\r\n";

            szQuery = queryTurnos.Count();
            for(i = 0; i < boxes.Count(); ++i) {
                if(i < 2) {
                    boxes[i].Visible = false;
                    continue;
                }
                addInfo.Controls.Remove(boxes[i]);
            }
            if(szQuery == 0) {
                generatePDF.Visible = false;
                return;
            }
            makeBoxes(szQuery);
            descripcionTurno = new string[szQuery,4];
            for(i = 0; i < szQuery; ++i) {
                tempTurno = queryTurnos[i];
                descripcionTurno[i,0] = "Fecha del turno: ";
                DateTime queryFecha = (from ft in db.FechaTurno
                                       where ft.FechaTurnoID == tempTurno.IDFechaTurno
                                       select ft.Fecha).First();
                descripcionTurno[i, 0] += queryFecha.ToString("d/MM/yyyy");

                descripcionTurno[i, 1] += "Para médico: ";
                tempMedico = (from medico in db.Medico
                              join medicoUsuario in db.Usuario
                                 on medico.IDUsuario equals medicoUsuario.UsuarioID
                              join medicoAfiliado in db.Afiliado
                                 on medicoUsuario.IDAfiliado equals medicoAfiliado.AfiliadoID
                              where tempTurno.IDMedico == medico.MedicoID
                              select medicoAfiliado).First();
                descripcionTurno[i,1] += tempMedico.Nombre.Trim() + " " + tempMedico.Apellido.Trim();

                var querySucursal = from suc in db.Sucursal
                                    where suc.SucursalId == tempTurno.IDSucursal
                                    select suc;
                descripcionTurno[i,2] += "En sucursal: " +
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

                descripcionTurno[i, 3] += "Horario: ";
                TimeSpan horario = (from h in db.Horario
                                    join ft in db.FechaTurno
                                        on h.HorarioID equals ft.IDHorario
                                    where tempTurno.IDFechaTurno == ft.FechaTurnoID
                                    select h.Hora).FirstOrDefault();
                descripcionTurno[i, 3] += horario.ToString();
            }

            for(i = 0; i < szQuery; ++i) {
                boxes[i].Visible = true;
                boxes[i].Enabled = false;
                boxes[i].TextMode = TextBoxMode.MultiLine;
                boxes[i].ReadOnly = true;
                boxes[i].CssClass = "text-boxes";
                boxes[i].Text = descripcionTurno[i, 0] + newline +
                                descripcionTurno[i, 1] + newline +
                                descripcionTurno[i, 2] + newline +
                                descripcionTurno[i, 3] + newline;
                if(i < 2)
                    continue;
                addInfo.Controls.Add(boxes[i]);
            }
            generatePDF.Visible = true;
            Session["turnos"] = descripcionTurno;
            Session["boxes"] = boxes;
        }

        protected void generatePDF_Click(object sender, EventArgs e) {
            PDFDrawer drawer;
            string filepath, filename, msg;

            filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
            filename = "reporte.pdf";
            drawer = new PDFDrawer(filepath, filename);

            drawer.drawFromUser(whoAmIAsAfiliado, descripcionTurno);

            msg = "El reporte ha sido creado y almacenado en " + filepath +
                  " con el nombre de " + filename;

            ClientScript.RegisterStartupScript(this.GetType(), 
                "alert", "alert('" + msg + "')", true);
        }

        protected void OtrasOpc_Click(object sender, EventArgs e) {
            label1.Visible = label2.Visible = label3.Visible = true;
            generatePDF.Visible = false;
            for(int i = 0; i < boxes.Count(); ++i) {
                if(i < 2) {
                    boxes[i].Visible = false;
                    continue;
                }
                addInfo.Visible = false;
            }
        }
    }
}