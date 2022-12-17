using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI;

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
            //al usuario lo tomo del componente Login
            whoAmI = (Usuario) Session["user"];
            boxes = new List<TextBox>();
            signOutButton.ServerClick += new EventHandler(signOutButton_Click);
            if(IsPostBack) {
                db = (TablesDataContext) Session["database"];
                descripcionTurno = (string[,]) Session["turnos"];
                whoAmIAsAfiliado = (Afiliado) Session["afiliado"];
            }
            else {
                //ESTA PARTE VENDRÍA A REEMPLAZAR EL CONSTRUCTOR
                db = new TablesDataContext();
                whoAmIAsAfiliado = (from af in db.Afiliado
                                    where af.AfiliadoID == whoAmI.IDAfiliado
                                    select af).FirstOrDefault();

                Session["database"] = db;
                Session["afiliado"] = whoAmIAsAfiliado;
            }
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
                switch(i) {
                    case 0:
                        textBoxTurno0.Visible = false;
                        break;
                    case 1:
                        textBoxTurno1.Visible = false;
                        break;
                    default:
                        addInfo.Controls.Remove(boxes[i]);
                        break;
                }
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

        protected void signOutButton_Click(object sender, EventArgs e) {
            FormsAuthentication.SignOut();
            Response.Redirect("login.aspx", true);
        }
    }
}