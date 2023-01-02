using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb {
    public partial class WebMedicalTurnos : System.Web.UI.Page {
        private Usuario whoAmI;
        private Afiliado whoAmIAsAfiliado;
        private Medico whoAmIAsMedico;
        private TablesDataContext db;

        private List<TextBox> boxes;
        private string[,] descripcionTurno;

        protected void Page_Load(object sender, EventArgs e) {
            //al usuario lo tomo del componente WebHome
            whoAmI = (Usuario)Session["user"];
            signOutButton.ServerClick += new EventHandler(signOutButton_Click);
            boxes = new List<TextBox>();

            if(IsPostBack) {
                db = (TablesDataContext)Session["database"];
                descripcionTurno = (string[,])Session["turnos"];
                whoAmIAsAfiliado = (Afiliado)Session["afiliado"];
                whoAmIAsMedico = (Medico)Session["medico"];
            }
            else {
                //ESTA PARTE VENDRÍA A REEMPLAZAR EL CONSTRUCTOR
                fecha.Attributes["min"] = DateTime.Now.ToString("dd-MM-yyyy");
                fecha.Attributes["max"] = DateTime.Now.AddYears(1).ToString("dd-MM-yyyy");
                db = new TablesDataContext();
                whoAmIAsAfiliado = (from af in db.Afiliado
                                    where af.AfiliadoID == whoAmI.IDAfiliado
                                    select af).FirstOrDefault();
                whoAmIAsMedico = (from me in db.Medico
                                  where me.IDUsuario == whoAmI.UsuarioID
                                  select me).FirstOrDefault();

                Session["database"] = db;
                Session["afiliado"] = whoAmIAsAfiliado;
                Session["medico"] = whoAmIAsMedico;
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
                                toAdd.BackColor = boxes[0].BackColor;
                                break;
                            case 1:
                                //pertenece a un box del lado derecho
                                toAdd.Text = "";
                                toAdd.Height = boxes[1].Height;
                                toAdd.Width = boxes[1].Width;
                                toAdd.BackColor = boxes[1].BackColor;
                                break;
                        }
                        boxes.Add(toAdd);
                    }
                    break;
            }
        }

        protected void generatePDF_Click(object sender, EventArgs e) {
            PDFDrawer drawer;
            string filepath, filename, msg;

            filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
            filename = "reporte.pdf";
            drawer = new PDFDrawer(filepath, filename);

            drawer.drawFromMedico(whoAmIAsAfiliado, descripcionTurno, Convert.ToDateTime(fecha.Text));

            msg = "El reporte ha sido creado y almacenado en " + filepath +
                  " con el nombre de " + filename;

            ClientScript.RegisterStartupScript(this.GetType(),
                "alert", "alert('" + msg + "');" +
                         "window.location='WebMedicalTurnos.aspx';", true);
        }

        protected void fecha_TextChanged(object sender, EventArgs e) {
            DateTime selected = Convert.ToDateTime(fecha.Text);
            int szQuery,i;
            Turno tempTurno;
            const string newline = "\r\n";

            List<Turno> queryTurnos = (from turno in db.Turno
                                       join ft in db.FechaTurno
                                           on turno.IDFechaTurno equals ft.FechaTurnoID
                                       where turno.IDMedico == whoAmIAsMedico.MedicoID && 
                                             ft.Fecha == selected
                                       select turno).ToList();
            
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
            descripcionTurno = new string[szQuery, 3];
            for(i = 0; i < szQuery; ++i) {
                tempTurno = queryTurnos[i];
                descripcionTurno[i, 0] = "Turno sacado por: ";
                Afiliado afiliado = (from af in db.Afiliado
                                     join user in db.Usuario
                                        on af.AfiliadoID equals user.IDAfiliado
                                     where user.UsuarioID == tempTurno.IDUsuario
                                     select af).FirstOrDefault();
                descripcionTurno[i, 0] += afiliado.Nombre.Trim() + " " + afiliado.Apellido.Trim();

                var querySucursal = from suc in db.Sucursal
                                    where suc.SucursalId == tempTurno.IDSucursal
                                    select suc;
                descripcionTurno[i, 1] = "En sucursal: " +
                        querySucursal.FirstOrDefault().SucursalDescripcion;

                var queryProvincia = from prov in db.Provincia
                                     where prov.ProvinciaId == tempTurno.IDProvincia
                                     select prov;
                descripcionTurno[i, 1] += ", " +
                        queryProvincia.FirstOrDefault().ProvinciaDescripcion;

                var queryLocalidad = from loc in db.Localidad
                                     where loc.LocalidadId == tempTurno.IDLocalidad
                                     select loc;
                descripcionTurno[i, 1] += ", " +
                        queryLocalidad.FirstOrDefault().LocalidadDescripcion.Trim();

                descripcionTurno[i, 2] = "Horario: ";
                TimeSpan horario = (from h in db.Horario
                                    join ft in db.FechaTurno
                                        on h.HorarioID equals ft.IDHorario
                                    where tempTurno.IDFechaTurno == ft.FechaTurnoID
                                    select h.Hora).FirstOrDefault();
                descripcionTurno[i, 2] += horario.ToString();
            }

            for(i = 0; i < szQuery; ++i) {
                boxes[i].Visible = true;
                boxes[i].Enabled = false;
                boxes[i].TextMode = TextBoxMode.MultiLine;
                boxes[i].ReadOnly = true;
                boxes[i].CssClass = "text-boxes";
                boxes[i].Text = descripcionTurno[i, 0] + newline +
                                descripcionTurno[i, 1] + newline +
                                descripcionTurno[i, 2] + newline;
                if(i < 2)
                    continue;
                addInfo.Controls.Add(boxes[i]);
            }
            generatePDF.Visible = true;
            Session["turnos"] = descripcionTurno;
        }

        protected void signOutButton_Click(object sender, EventArgs e) {
            FormsAuthentication.SignOut();
            Response.Redirect("login.aspx", true);
        }
    }
}