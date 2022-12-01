using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using System.Threading;

namespace AppWeb {
    public partial class WebTurnos : System.Web.UI.Page {
        private Usuario whoAmI;
        private Afiliado whoAmIAsAfiliado;
        private TablesDataContext db;

        private List<Turno> myTurnosToDelete;
        private int indexShowed;
        protected void Page_Load(object sender, EventArgs e) {
            //VENDRÍA A SER COMO EL CONSTRUCTOR
            whoAmI = (Usuario)Session["user"]; 
            whoAmIAsAfiliado = (Afiliado)Session["afiliado"];
            if(IsPostBack) {
                db = (TablesDataContext)Session["database"];
                myTurnosToDelete = (List<Turno>)Session["turnos_delete"];
                indexShowed = Convert.ToInt32(Session["myindex"]);
            }
            else {
                db = new TablesDataContext();
                Session["database"] = db;
                indexShowed = -1;
                Session["myindex"] = indexShowed;
            }
            //SACAR ESTO DESP
            if(whoAmI == null)
                whoAmI = (from m in db.Usuario
                          where m.UsuarioID == 6
                          select m).FirstOrDefault();
        }

        protected void addTurnoButton_Click(object sender, EventArgs e) {
            var queryProvincia = from prov in db.Provincia
                                 select prov;
            comboProvincia.ClearSelection();comboLocalidad.ClearSelection();
            comboProvincia.DataSource = queryProvincia;
            comboProvincia.DataTextField = "ProvinciaDescripcion";
            comboProvincia.DataValueField = "ProvinciaId";
            comboProvincia.DataBind();
            changeVisibilityBy(Tools.SACARNUEVOTURNO);
        }

        protected void cancelAddButton_Click(object sender, EventArgs e) {
            changeVisibilityBy(Tools.CANCELARADD);
        }

        protected void comboProvincia_SelectedIndexChanged1(object sender, EventArgs e) {
            int ProvinciaId = Convert.ToInt32(comboProvincia.SelectedValue);
            var queryLocalidad = from loc in db.Localidad
                                 where loc.IDProvincia == ProvinciaId
                                 select loc;
            comboLocalidad.ClearSelection(); comboSucursales.ClearSelection();
            comboLocalidad.DataSource = queryLocalidad;
            comboLocalidad.DataTextField = "LocalidadDescripcion";
            comboLocalidad.DataValueField = "LocalidadId";
            comboLocalidad.DataBind();
            changeVisibilityBy(Tools.PROVINCIASELECTED);
        }
        
        protected void comboLocalidad_SelectedIndexChanged(object sender, EventArgs e) {
            int LocalidadId = Convert.ToInt32(comboLocalidad.SelectedValue);
            var querySucursal = from suc in db.Sucursal
                                where suc.IDLocalidad == LocalidadId
                                select suc;
            comboSucursales.ClearSelection();
            comboSucursales.DataSource = querySucursal;
            comboSucursales.DataTextField = "SucursalDescripcion";
            comboSucursales.DataValueField = "SucursalId";
            comboSucursales.DataBind();
            changeVisibilityBy(Tools.LOCALIDADSELECTED);
        }

        protected void comboSucursales_SelectedIndexChanged(object sender, EventArgs e) {
            int LocalidadId, SucursalId;
            LocalidadId = Convert.ToInt32(comboLocalidad.SelectedValue);
            SucursalId = Convert.ToInt32(comboSucursales.SelectedValue);
            var queryMedicosEspecialidades = from medico in db.Medico
                                             join ms in db.MedicoSucursal
                                                 on medico.MedicoID equals ms.IDMedico
                                             where ms.IDSucursal == SucursalId
                                             select medico.IDEspecialidad;

            List<Especialidad> queryEspecialidades = (from esp in db.Especialidad
                                                      join ID in queryMedicosEspecialidades
                                                          on esp.EspecialidadId equals ID
                                                      select esp)
                                                      .ToList()
                                                      .Distinct()
                                                      .ToList();

            comboEspecialidades.ClearSelection();
            comboEspecialidades.DataSource = queryEspecialidades;
            comboEspecialidades.DataTextField = "EspecialidadDescripcion";
            comboEspecialidades.DataValueField = "EspecialidadId";
            comboEspecialidades.DataBind();
            changeVisibilityBy(Tools.SUCURSALSELECTED);
        }

        protected void comboEspecialidades_SelectedIndexChanged1(object sender, EventArgs e) {
            int SucursalId, EspecialidadId;
            SucursalId = Convert.ToInt32(comboSucursales.SelectedValue);
            EspecialidadId = Convert.ToInt32(comboEspecialidades.SelectedValue);
            var queryMedicos = from medico in db.Medico
                               join ms in db.MedicoSucursal
                                   on medico.MedicoID equals ms.IDMedico
                               where ms.IDSucursal == SucursalId &&
                                     medico.IDEspecialidad == EspecialidadId
                               select medico;
            var queryAfiliados = from af in db.Afiliado
                                 join usuario in db.Usuario
                                    on af.AfiliadoID equals usuario.IDAfiliado
                                 join medico in queryMedicos
                                    on usuario.UsuarioID equals medico.IDUsuario
                                 select af;
            comboMedicos.ClearSelection();
            comboMedicos.DataSource = queryAfiliados;
            comboMedicos.DataTextField = "Apellido";
            comboMedicos.DataValueField = "AfiliadoId";
            comboMedicos.DataBind();
            changeVisibilityBy(Tools.ESPECIALIDADSELECTED);
        }

        protected void comboMedicos_SelectedIndexChanged(object sender, EventArgs e) {
            int SucursalId, MedicoId, szQuery;
            string adviceDisponibilidad;

            SucursalId = Convert.ToInt32(comboSucursales.SelectedValue);
            MedicoId = Convert.ToInt32(comboMedicos.SelectedValue);
            adviceDisponibilidad = "El médico está disponible ";

            List<Dia> queryDias = (from dia in db.Dia
                                   join dm in db.DisponibilidadMedico
                                       on dia.DiaID equals dm.IDDia
                                   where dm.IDMedico == MedicoId &&
                                         dm.IDSucursal == SucursalId
                                   select dia).ToList().Distinct().ToList();

            szQuery = queryDias.Count;
            switch(szQuery) {
                case 0:
                    return;
                case 1:
                    adviceDisponibilidad += "únicamente el día " + queryDias.FirstOrDefault().NombreDia.Trim();
                    break;
                default:
                    adviceDisponibilidad += "los días ";
                    for(int i = 0; i < szQuery - 1; ++i) {
                        adviceDisponibilidad += queryDias[i].NombreDia.Trim();
                        if(i == szQuery - 2) {
                            adviceDisponibilidad += " y " + queryDias[i + 1].NombreDia.Trim();
                            continue;
                        }
                        adviceDisponibilidad += ", ";
                    }
                    break;
            }
            adviceDisponibilidad += ".";
            Label18.Text = adviceDisponibilidad;
            changeVisibilityBy(Tools.MEDICOSELECTED);
            fechaTurnoPicker.VisibleDate = DateTime.Now;
        }

        protected void fechaTurnoPicker_SelectionChanged(object sender, EventArgs e) {
            string msg;
            //SOLUCION TEMPORAL, VER DE HACER DE OTRA FORMA SINO
            if(fechaTurnoPicker.SelectedDate < DateTime.Now) {
                msg = "ATENCIÓN! La fecha seleccionada es incorrecta ya que es una fecha anterior " +
                      "al día de hoy. No podemos sacarle un turno para un día que ya pasó. " + 
                      "Por favor, seleccione una fecha válida.";
                ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "')", true);
                return;
            }
            int SucursalId, MedicoId, DiaId, DiaSelected;
            bool diaCorrecto = false;
            List<Horario> queryHorariosDisponibles = new List<Horario>();

            SucursalId = Convert.ToInt32(comboSucursales.SelectedValue);
            MedicoId = Convert.ToInt32(comboMedicos.SelectedValue);
            DiaId = -1;

            var queryDias = (from dia in db.Dia
                             join dispom in db.DisponibilidadMedico
                                 on dia.DiaID equals dispom.IDDia
                             where dispom.IDMedico == MedicoId &&
                                   dispom.IDSucursal == SucursalId
                             select dia).ToList();

            foreach(Dia dia in queryDias) {
                DiaSelected = (int)fechaTurnoPicker.SelectedDate.DayOfWeek;
                /* 
                   esto de abajo es porque el picker toma al día domingo como el día 0
                   de la semana, mientras que para la base de datos, el día domingo es
                   el día 7
                 */
                if(DiaSelected == 0)
                    DiaSelected = 7;
                diaCorrecto = DiaSelected == dia.DiaID;
                if(diaCorrecto) {
                    DiaId = dia.DiaID;
                    break;
                }
            }
            if(!diaCorrecto) {
                msg = "El médico en cuestión no trabaja en ésta sucursal el día que usted seleccionó";
                ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "')", true);
                changeVisibilityBy(Tools.INCORRECTDAY);
                return;
            }

            var queryDm = from dispom in db.DisponibilidadMedico
                          where dispom.IDMedico == MedicoId &&
                                dispom.IDDia == DiaId &&
                                dispom.IDSucursal == SucursalId
                          select dispom;

            var queryTurnos = from ft in db.FechaTurno
                              join turno in db.Turno
                                  on ft.FechaTurnoID equals turno.IDFechaTurno
                              where ft.Fecha.Equals(fechaTurnoPicker.SelectedDate) &&
                                    turno.IDSucursal == SucursalId &&
                                    turno.IDMedico == MedicoId
                              select ft;

            var queryHorariosUsados = from hs in db.Horario
                                      join ft in queryTurnos
                                          on hs.HorarioID equals ft.IDHorario
                                      select hs;

            foreach(DisponibilidadMedico dm in queryDm) {
                var queryTemp = (from hs in db.Horario
                                 where hs.Hora.CompareTo(dm.HorarioInicio) > 0 &&
                                       hs.Hora.CompareTo(dm.HorarioFin) < 0
                                 select hs).ToList();
                queryHorariosDisponibles = queryHorariosDisponibles
                                           .Concat(queryTemp)
                                           .ToList();
            }

            if(queryHorariosUsados.Count() > 0)
                queryHorariosDisponibles = queryHorariosDisponibles
                                           .Except(queryHorariosUsados, new HorarioComparer())
                                           .ToList();
            comboHorarios.ClearSelection();
            comboHorarios.DataSource = queryHorariosDisponibles;
            comboHorarios.DataTextField = "Hora";
            comboHorarios.DataValueField = "HorarioId";
            comboHorarios.DataBind();
            changeVisibilityBy(Tools.FECHASELECTED);
        }

        protected void comboHorarios_SelectedIndexChanged(object sender, EventArgs e) {
            changeVisibilityBy(Tools.HORASELECTED);
        }

        protected void sacarTurnoButton_Click(object sender, EventArgs e) {
            int DiaId, HorarioId;
            DateTime fecha;
            string msg;
            Turno tToAdd = new Turno();
            FechaTurno ftToAdd = new FechaTurno();

            DiaId = (int)fechaTurnoPicker.SelectedDate.DayOfWeek;
            if(DiaId == 0)
                DiaId = 7;  //el por qué de esto está exiplicado en la línea 178
            HorarioId = Convert.ToInt32(comboHorarios.SelectedValue);
            fecha = fechaTurnoPicker.SelectedDate;

            tToAdd.IDMedico = Convert.ToInt32(comboMedicos.SelectedValue);
            tToAdd.IDProvincia = Convert.ToInt32(comboProvincia.SelectedValue);
            tToAdd.IDLocalidad = Convert.ToInt32(comboLocalidad.SelectedValue);
            tToAdd.IDSucursal = Convert.ToInt32(comboSucursales.SelectedValue);
            tToAdd.IDEspecialidad = Convert.ToInt32(comboEspecialidades.SelectedValue);
            tToAdd.IDUsuario = whoAmI.UsuarioID;

            ftToAdd.IDDia = DiaId;
            ftToAdd.IDHorario = HorarioId;
            ftToAdd.Fecha = fecha;

            db.FechaTurno.InsertOnSubmit(ftToAdd);
            try {
                db.SubmitChanges();
                tToAdd.IDFechaTurno = ftToAdd.FechaTurnoID;
                db.Turno.InsertOnSubmit(tToAdd);
                try {
                    db.SubmitChanges();
                    msg = "El turno ha sido generado exitosamente.";
                    ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "');" +
                            "window.location='WebTurnos.aspx';", true);
                }
                catch(Exception) {
                    msg = "El turno no se ha podido registrar. Comuníquese con los desarrolladores.";
                    ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "');" +
                            "window.location='WebTurnos.aspx';", true);
                }
            }
            catch(Exception) {
                msg = "No se ha podido registrar la fecha del turno. Comuníquese con los desarrolladores.";
                ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "');" +
                            "window.location='WebTurnos.aspx';", true);
            }
        }

        protected void rmTurnoButton_Click(object sender, EventArgs e) {
            string msg;

            myTurnosToDelete = (from turno in db.Turno
                                where turno.IDUsuario == whoAmI.UsuarioID
                                select turno).ToList();
            Session["turnos_delete"] = myTurnosToDelete;

            switch(myTurnosToDelete.Count) {
                case 0:
                    msg = "Usted no tiene turnos registrados a su nombre.";
                    ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "');" +
                            "window.location='WebTurnos.aspx';", true);
                    break;
                case 1:
                    changeVisibilityBy(Tools.ELIMINARTURNO);
                    changeVisibilityBy(Tools.LASTTURNOTODELETE);
                    showTurnoValues(myTurnosToDelete[++indexShowed]);
                    break;
                default:
                    changeVisibilityBy(Tools.ELIMINARTURNO);
                    showTurnoValues(myTurnosToDelete[++indexShowed]);
                    break;
            }
            Session["myindex"] = indexShowed;
        }

        protected void backTurnoButton_Click(object sender, ImageClickEventArgs e) {
            showTurnoValues(myTurnosToDelete[--indexShowed]);
            if(!nextTurnoButton.Visible)
                changeVisibilityBy(Tools.ANTERIORTURNO);
            if(indexShowed == 0)
                changeVisibilityBy(Tools.FIRSTTURNOTODELETE);
            Session["myindex"] = indexShowed;
        }

        protected void nextTurnoButton_Click(object sender, ImageClickEventArgs e) {
            showTurnoValues(myTurnosToDelete[++indexShowed]);
            if(!backTurnoButton.Visible)
                changeVisibilityBy(Tools.SIGUIENTETURNO);
            if(indexShowed == myTurnosToDelete.Count - 1)
                changeVisibilityBy(Tools.LASTTURNOTODELETE);
            Session["myindex"] = indexShowed;
        }

        protected void eliminarTurnoButton_Click(object sender, EventArgs e) {
            string msg;
            db.Turno.DeleteOnSubmit(myTurnosToDelete[indexShowed]);
            try {
                db.SubmitChanges();
                msg = "El turno ha sido eliminado exitosamente.";
                Session["myindex"] = -1;
                ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "');" +
                            "window.location='WebTurnos.aspx';", true);
            }
            catch(Exception) {
                msg = "El turno no se ha podido eliminar. Comuníquese con los desarrolladores.";
                ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "');" +
                            "window.location='WebTurnos.aspx';", true);
            }
        }

        protected void cancelRmButton_Click(object sender, EventArgs e) {
            changeVisibilityBy(Tools.CANCELARRM);
        }

        private void showTurnoValues(Turno turno) {
            Afiliado medicoAsAfiliado;
            Medico medico;
            FechaTurno ft;
            string temp;

            medico = (from m in db.Medico
                      where m.MedicoID == turno.IDMedico
                      select m).First();
            medicoAsAfiliado = (from af in db.Afiliado
                                join user in db.Usuario
                                    on af.AfiliadoID equals user.IDAfiliado
                                join m in db.Medico
                                    on user.UsuarioID equals m.IDUsuario
                                where m.MedicoID == turno.IDMedico
                                select af).First();
            TextBox1.Text = medicoAsAfiliado.Nombre.Trim() + " " +
                            medicoAsAfiliado.Apellido.Trim();

            temp = (from esp in db.Especialidad
                    join m in db.Medico
                        on esp.EspecialidadId equals m.IDEspecialidad
                    where m.MedicoID == medico.MedicoID
                    select esp.EspecialidadDescripcion).First();
            TextBox7.Text = temp.Trim();

            temp = (from prov in db.Provincia
                    where prov.ProvinciaId == turno.IDProvincia
                    select prov.ProvinciaDescripcion).First();
            TextBox3.Text = temp.Trim();

            temp = (from loc in db.Localidad
                    where loc.LocalidadId == turno.IDLocalidad
                    select loc.LocalidadDescripcion).First();
            TextBox4.Text = temp.Trim();

            temp = (from suc in db.Sucursal
                    where suc.SucursalId == turno.IDSucursal
                    select suc.SucursalDescripcion).First();
            TextBox5.Text = temp.Trim();

            ft = (from ftt in db.FechaTurno
                  where ftt.FechaTurnoID == turno.IDFechaTurno
                  select ftt).First();
            temp = ft.Fecha.ToString("dd/MM/yyyy");
            TextBox2.Text = temp;

            temp = (from hs in db.Horario
                    where hs.HorarioID == ft.IDHorario
                    select hs.Hora).First().ToString();
            TextBox6.Text = temp;
        }

        private void changeVisibilityBy(Tools which) {
            switch(which) {
                case Tools.ELIMINARTURNO:
                    Label9.Visible = Label10.Visible = Label11.Visible = Label12.Visible =
                    Label15.Visible = Label14.Visible = Label13.Visible = TextBox1.Visible =
                    TextBox2.Visible = TextBox3.Visible = TextBox4.Visible = TextBox5.Visible =
                    TextBox6.Visible = TextBox7.Visible = true;
                    Label17.Visible = Label8.Visible =
                    nextTurnoButton.Visible = eliminarTurnoButton.Visible =
                    cancelRmButton.Visible = true;
                    rmTurnoButton.Enabled = false;
                    break;
                case Tools.LASTTURNOTODELETE:
                    Label17.Visible = nextTurnoButton.Visible = false;
                    break;
                case Tools.ANTERIORTURNO:
                    Label17.Visible = nextTurnoButton.Visible = true;
                    break;
                case Tools.FIRSTTURNOTODELETE:
                    backTurnoButton.Visible = Label16.Visible = false;
                    break;
                case Tools.SACARNUEVOTURNO:
                    rmTurnoButton.Visible = addTurnoButton.Enabled = false;
                    Label2.Visible = comboProvincia.Visible = cancelAddButton.Visible = true;
                    break;
                case Tools.PROVINCIASELECTED:
                    Label3.Visible = comboLocalidad.Visible = true;
                    Label4.Visible = Label5.Visible = Label6.Visible = Label7.Visible = 
                    Label1.Visible = comboSucursales.Visible = comboEspecialidades.Visible =
                    comboMedicos.Visible = comboHorarios.Visible = fechaTurnoPicker.Visible =
                    Label18.Visible = sacarTurnoButton.Visible = false;
                    break;
                case Tools.LOCALIDADSELECTED:
                    Label4.Visible = comboSucursales.Visible = true;
                    Label5.Visible = Label6.Visible = Label7.Visible = Label1.Visible =
                    comboEspecialidades.Visible = comboMedicos.Visible = comboHorarios.Visible = 
                    fechaTurnoPicker.Visible = Label18.Visible = sacarTurnoButton.Visible = false;
                    break;
                case Tools.SUCURSALSELECTED:
                    Label1.Visible = comboEspecialidades.Visible = true;
                    Label5.Visible = Label6.Visible = Label7.Visible = comboMedicos.Visible = 
                    comboHorarios.Visible = fechaTurnoPicker.Visible = Label18.Visible = 
                    sacarTurnoButton.Visible = false;
                    break;
                case Tools.ESPECIALIDADSELECTED:
                    Label5.Visible = comboMedicos.Visible = true;
                    Label6.Visible = Label7.Visible = comboHorarios.Visible =
                    fechaTurnoPicker.Visible = Label18.Visible = sacarTurnoButton.Visible = false;
                    break;
                case Tools.MEDICOSELECTED:
                    Label6.Visible = fechaTurnoPicker.Visible = Label18.Visible = true;
                    Label7.Visible = comboHorarios.Visible = sacarTurnoButton.Visible = false;
                    break;
                case Tools.FECHASELECTED:
                    Label7.Visible = comboHorarios.Visible = true;
                    sacarTurnoButton.Visible = false;
                    break;
                case Tools.HORASELECTED:
                    sacarTurnoButton.Visible = true;
                    break;
                case Tools.CANCELARADD:
                    addTurnoButton.Enabled = rmTurnoButton.Visible = true;
                    cancelAddButton.Visible = Label1.Visible = Label2.Visible =
                    Label3.Visible = Label4.Visible = Label5.Visible = Label6.Visible =
                    Label7.Visible = comboProvincia.Visible =
                    comboLocalidad.Visible = comboSucursales.Visible = comboMedicos.Visible =
                    fechaTurnoPicker.Visible = comboHorarios.Visible = comboEspecialidades.Visible =
                    Label18.Visible = sacarTurnoButton.Visible = false;
                    break;
                case Tools.CANCELARRM:
                    rmTurnoButton.Enabled = addTurnoButton.Visible = true;
                    cancelRmButton.Visible = Label8.Visible = Label9.Visible = Label10.Visible =
                    Label11.Visible = Label12.Visible = Label13.Visible = Label14.Visible =
                    Label15.Visible = Label16.Visible = Label17.Visible = TextBox1.Visible = 
                    TextBox2.Visible = TextBox3.Visible = TextBox4.Visible = TextBox5.Visible = 
                    TextBox6.Visible = TextBox7.Visible = eliminarTurnoButton.Visible = 
                    nextTurnoButton.Visible = backTurnoButton.Visible = false;
                    Session["myindex"] = -1;
                    break;
                case Tools.SIGUIENTETURNO:
                    backTurnoButton.Visible = Label16.Visible = true;
                    break;
                case Tools.INCORRECTDAY:
                    Label7.Visible = comboHorarios.Visible = sacarTurnoButton.Visible = false;
                    break;
            }
        }

        private class HorarioComparer : IEqualityComparer<Horario> {
            public bool Equals(Horario x, Horario y) {
                return x.Hora == y.Hora;
            }

            public int GetHashCode(Horario obj) {
                return base.GetHashCode();
            }
        }

        private enum Tools {
            SACARNUEVOTURNO, SACARTURNO, ELIMINARTURNO, ELIMINARESTETURNO, CANCELARADD,
            CANCELARRM, SIGUIENTETURNO, ANTERIORTURNO,

            PROVINCIASELECTED, LOCALIDADSELECTED, SUCURSALSELECTED, ESPECIALIDADSELECTED,
            MEDICOSELECTED, FECHASELECTED, HORASELECTED,

            ONLYTURNOTODELETE, LASTTURNOTODELETE, FIRSTTURNOTODELETE, INCORRECTDAY,
        }
    }

}