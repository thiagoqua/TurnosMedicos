using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;

namespace AppWeb {
    public partial class WebTurnos : System.Web.UI.Page {
        private Usuario whoAmI;
        private Afiliado whoAmIAsAfiliado;
        private TablesDataContext db;

        /** ambos campos se utilizan para la eliminación de turnos **/
        //almacena todos los turnos a mi nombre
        private List<Turno> myTurnosToDelete;
        //almacena el índice del último turno mostrado de la lista de arriba 
        private int indexShowed;
        protected void Page_Load(object sender, EventArgs e) {
            whoAmI = (Usuario)Session["user"]; 
            if(IsPostBack) {
                db = (TablesDataContext)Session["database"];
                myTurnosToDelete = (List<Turno>)Session["turnos_delete"];
                indexShowed = Convert.ToInt32(Session["myindex"]);
                whoAmIAsAfiliado = (Afiliado)Session["afiliado"];
            }
            else {
                db = new TablesDataContext();
                /*
                  si whoAmI es null, significa que el Login no guardó en la sesión al usuario, 
                  por lo que tengo que ir a buscarlo a la cookie ya que se trata de un 
                  reinicio del navegador
                */
                if(whoAmI == null) {
                    int UsuarioId = Convert.ToInt32(Request.Cookies["userID"].Value);
                    whoAmI = (from user in db.Usuario
                              where user.UsuarioID == UsuarioId
                              select user).First();


                    whoAmIAsAfiliado = (from af in db.Afiliado
                                        where af.AfiliadoID == whoAmI.IDAfiliado
                                        select af).First();
                    Session["afiliado"] = whoAmIAsAfiliado;
                    Session["user"] = whoAmI;
                }
                /*
                    si un usuario médico cambia la url desde la barra de navegación y quiere
                    acceder a éste componente, se lo impido redirigiéndolo hacia su componente
                    home
                */
                if(whoAmI.isMedico)
                    Response.Redirect("~/WebMedicalHome.aspx");

                Session["myindex"] = indexShowed = -1;
                Session["database"] = db;
            }
        }

        protected void addTurnoButton_Click(object sender, EventArgs e) {
            List<Provincia> queryProvincia = (from prov in db.Provincia
                                              select prov).ToList();
            //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
            queryProvincia.Insert(0,
                createSeleccioneOf(queryProvincia.First().GetType()) as Provincia);
            
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
            changeVisibilityBy(Tools.PROVINCIASELECTED);
            int ProvinciaId = Convert.ToInt32(comboProvincia.SelectedValue);
            //si el item seleccionado es el objeto seleccione aborto
            if(ProvinciaId == -1)
                return;
            List<Localidad> queryLocalidad = (from loc in db.Localidad
                                              where loc.IDProvincia == ProvinciaId
                                              select loc).ToList();
            //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
            if(queryLocalidad.Count > 0)
                queryLocalidad.Insert(0,
                    createSeleccioneOf(queryLocalidad.First().GetType()) as Localidad);

            comboLocalidad.ClearSelection(); comboSucursales.ClearSelection();
            comboLocalidad.DataSource = queryLocalidad;
            comboLocalidad.DataTextField = "LocalidadDescripcion";
            comboLocalidad.DataValueField = "LocalidadId";
            comboLocalidad.DataBind();
            showHayDisponibilidad(queryLocalidad.Count != 0,"localidades");
        }
        
        protected void comboLocalidad_SelectedIndexChanged(object sender, EventArgs e) {
            changeVisibilityBy(Tools.LOCALIDADSELECTED);
            int LocalidadId = Convert.ToInt32(comboLocalidad.SelectedValue);
            //si el item seleccionado es el objeto seleccione aborto
            if(LocalidadId == -1)
                return;
            List<Sucursal> querySucursal = (from suc in db.Sucursal
                                            where suc.IDLocalidad == LocalidadId
                                            select suc).ToList();
            //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
            if(querySucursal.Count > 0)
                querySucursal.Insert(0,
                    createSeleccioneOf(querySucursal.First().GetType()) as Sucursal);

            comboSucursales.ClearSelection();
            comboSucursales.DataSource = querySucursal;
            comboSucursales.DataTextField = "SucursalDescripcion";
            comboSucursales.DataValueField = "SucursalId";
            comboSucursales.DataBind();
            showHayDisponibilidad(querySucursal.Count != 0,"sucursales");
        }

        protected void comboSucursales_SelectedIndexChanged(object sender, EventArgs e) {
            changeVisibilityBy(Tools.SUCURSALSELECTED);
            int LocalidadId, SucursalId;
            List<Especialidad> queryEspecialidades;
            LocalidadId = Convert.ToInt32(comboLocalidad.SelectedValue);
            SucursalId = Convert.ToInt32(comboSucursales.SelectedValue);
            //si el item seleccionado es el objeto seleccione aborto
            if(SucursalId == -1)
                return;
            var queryMedicosEspecialidades = from medico in db.Medico
                                             join ms in db.MedicoSucursal
                                                 on medico.MedicoID equals ms.IDMedico
                                             where ms.IDSucursal == SucursalId
                                             select medico.IDEspecialidad;

            /*
              aplico Distinct() porque al haber varios médicos que trabajan de la misma especialidad,
              habrían especialidades repetidas
            */
            queryEspecialidades = (from esp in db.Especialidad
                                   join ID in queryMedicosEspecialidades
                                       on esp.EspecialidadId equals ID
                                   select esp)
                                   .ToList()
                                   .Distinct()
                                   .ToList();
            //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
            if(queryEspecialidades.Count > 0)
                queryEspecialidades.Insert(0,
                    createSeleccioneOf(queryEspecialidades.First().GetType()) as Especialidad);

            comboEspecialidades.ClearSelection();
            comboEspecialidades.DataSource = queryEspecialidades;
            comboEspecialidades.DataTextField = "EspecialidadDescripcion";
            comboEspecialidades.DataValueField = "EspecialidadId";
            comboEspecialidades.DataBind();
            showHayDisponibilidad(queryEspecialidades.Count != 0,"especialidades");
        }

        protected void comboEspecialidades_SelectedIndexChanged1(object sender, EventArgs e) {
            changeVisibilityBy(Tools.ESPECIALIDADSELECTED);
            int SucursalId, EspecialidadId;
            SucursalId = Convert.ToInt32(comboSucursales.SelectedValue);
            EspecialidadId = Convert.ToInt32(comboEspecialidades.SelectedValue);
            //si el item seleccionado es el objeto seleccione aborto
            if(EspecialidadId == -1)
                return;
            var queryMedicos = from medico in db.Medico
                               join ms in db.MedicoSucursal
                                   on medico.MedicoID equals ms.IDMedico
                               where ms.IDSucursal == SucursalId &&
                                     medico.IDEspecialidad == EspecialidadId
                               select medico;
            List<Afiliado> queryMedicosAfiliados = (from af in db.Afiliado
                                                    join usuario in db.Usuario
                                                       on af.AfiliadoID equals usuario.IDAfiliado
                                                    join medico in queryMedicos
                                                       on usuario.UsuarioID equals medico.IDUsuario
                                                    select af).ToList();
            //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
            if(queryMedicosAfiliados.Count > 0)
                queryMedicosAfiliados.Insert(0,
                    createSeleccioneOf(queryMedicosAfiliados.First().GetType()) as Afiliado);

            comboMedicos.ClearSelection();
            comboMedicos.DataSource = queryMedicosAfiliados;
            comboMedicos.DataTextField = "Apellido";
            comboMedicos.DataValueField = "AfiliadoId";
            comboMedicos.DataBind();
            showHayDisponibilidad(queryMedicosAfiliados.Count != 0,"medicos");
        }

        protected void comboMedicos_SelectedIndexChanged(object sender, EventArgs e) {
            changeVisibilityBy(Tools.MEDICOSELECTED);
            int SucursalId, MedicoIdAsAfiliado, MedicoId, szQuery;
            string adviceDisponibilidad;
            List<Dia> queryDias;

            SucursalId = Convert.ToInt32(comboSucursales.SelectedValue);
            adviceDisponibilidad = "El médico está disponible ";
            MedicoIdAsAfiliado= Convert.ToInt32(comboMedicos.SelectedValue);
            //si el item seleccionado es el objeto seleccione aborto
            if(MedicoIdAsAfiliado == -1)
                return;
            MedicoId = (from med in db.Medico
                        join user in db.Usuario
                            on med.IDUsuario equals user.UsuarioID
                        where user.IDAfiliado == MedicoIdAsAfiliado
                        select med.MedicoID).First();

            queryDias = (from dia in db.Dia
                         join dm in db.DisponibilidadMedico
                             on dia.DiaID equals dm.IDDia
                         where dm.IDMedico == MedicoId &&
                               dm.IDSucursal == SucursalId
                         select dia).ToList().Distinct().ToList();

            szQuery = queryDias.Count;
            switch(szQuery) {
                case 0:
                    showHayDisponibilidad(false, "dias disponibles");
                    return;
                case 1:
                    adviceDisponibilidad += "únicamente el día " + queryDias.First().NombreDia.Trim();
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
            fechaTurnoPicker.VisibleDate = DateTime.Now;
        }

        protected void fechaTurnoPicker_SelectionChanged(object sender, EventArgs e) {
            string msg;
            if(fechaTurnoPicker.SelectedDate < DateTime.Now) {
                msg = "ATENCIÓN! La fecha seleccionada es incorrecta ya que es una fecha anterior " +
                      "al día de hoy. No podemos sacarle un turno para un día que ya pasó. " + 
                      "Por favor, seleccione una fecha válida.";
                ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "')", true);
                return;
            }

            int SucursalId, MedicoIdAsAfiliado, MedicoId, DiaId, DiaSelected;
            bool diaCorrecto = false;
            List<Horario> queryHorariosDisponibles = new List<Horario>();

            SucursalId = Convert.ToInt32(comboSucursales.SelectedValue);
            MedicoIdAsAfiliado = Convert.ToInt32(comboMedicos.SelectedValue);
            MedicoId = (from med in db.Medico
                        join user in db.Usuario
                            on med.IDUsuario equals user.UsuarioID
                        where user.IDAfiliado == MedicoIdAsAfiliado
                        select med.MedicoID).First();
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

            /*
              esto se dá en el caso de que el médico trabaje en dos (o más) franjas horarias distintas
              el mismo día en una misma sucursal, por lo que en tal caso habrán dos 
              (o la cantidad que sea) registros de tipo DisponibilidadMedico con el mismo 
              MedicoId, DiaId y SucursalId, pero con distinto horario de inicio, horario de finalización 
              o consultorio.
            */
            foreach(DisponibilidadMedico dm in queryDm) {
                var queryTemp = (from hs in db.Horario
                                 where hs.Hora.CompareTo(dm.HorarioInicio) >= 0 &&
                                       hs.Hora.CompareTo(dm.HorarioFin) < 0
                                 select hs).ToList();
                queryHorariosDisponibles = queryHorariosDisponibles
                                           .Concat(queryTemp)
                                           .ToList();
            }

            //todos los turnos sacados para el día seleccionado
            var queryTurnos = from ft in db.FechaTurno
                              join turno in db.Turno
                                  on ft.FechaTurnoID equals turno.IDFechaTurno
                              where ft.Fecha.Equals(fechaTurnoPicker.SelectedDate) &&
                                    turno.IDSucursal == SucursalId &&
                                    turno.IDMedico == MedicoId
                              select ft;

            //los horarios de los turnos sacados para el día seleccionado
            var queryHorariosUsados = from hs in db.Horario
                                      join ft in queryTurnos
                                          on hs.HorarioID equals ft.IDHorario
                                      select hs;

            /*
              en la lista de horarios a elegir, muestro todos los
              disponibles EXCEPTO aquellos que no están disponibles debido a que corresponden
              al horario de otro turno
            */
            if(queryHorariosUsados.Count() > 0)
                queryHorariosDisponibles = queryHorariosDisponibles
                                           .Except(queryHorariosUsados, new HorarioComparer())
                                           .ToList();
            //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
            if(queryHorariosDisponibles.Count > 0)
                queryHorariosDisponibles.Insert(0,
                    createSeleccioneOf(queryHorariosDisponibles.First().GetType()) as Horario);

            comboHorarios.ClearSelection();
            comboHorarios.DataSource = queryHorariosDisponibles;
            comboHorarios.DataTextField = "Hora";
            comboHorarios.DataValueField = "HorarioId";
            comboHorarios.DataBind();
            showHayDisponibilidad(queryHorariosDisponibles.Count != 0,"horarios");
            changeVisibilityBy(Tools.FECHASELECTED);
        }

        protected void comboHorarios_SelectedIndexChanged(object sender, EventArgs e) {
            Tools pasado = comboHorarios.SelectedValue == "-1" ?
                           Tools.INCORRECTIME : Tools.HORASELECTED;
            changeVisibilityBy(pasado);
        }

        protected void sacarTurnoButton_Click(object sender, EventArgs e) {
            int DiaId, HorarioId;
            DateTime fecha;
            string msg;
            Turno tToAdd = new Turno();
            FechaTurno ftToAdd = new FechaTurno();

            DiaId = (int)fechaTurnoPicker.SelectedDate.DayOfWeek;
            //el por qué de esto está exiplicado en la línea 269
            if(DiaId == 0)
                DiaId = 7;  
            HorarioId = Convert.ToInt32(comboHorarios.SelectedValue);
            fecha = fechaTurnoPicker.SelectedDate;

            tToAdd.IDMedico = (from med in db.Medico
                               join user in db.Usuario
                                   on med.IDUsuario equals user.UsuarioID
                               where user.IDAfiliado == Convert.ToInt32(comboMedicos.SelectedValue)
                               select med.MedicoID).First();
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

        /// <summary>
        ///     Muestra en la interfaz los detalles del turno que recibe.
        /// </summary>
        /// <param name="turno">turno que se quiere mostrar</param>
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
            TextBox2.Text = temp.Trim();

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
            TextBox6.Text = temp;

            temp = (from hs in db.Horario
                    where hs.HorarioID == ft.IDHorario
                    select hs.Hora).First().ToString();
            TextBox7.Text = temp;
        }

        /// <summary>
        ///     Crea un objeto cuyo fin es ser siempre el primero en los DropDownLists.
        ///     Ya que los DropDownList no detectan cambios cuando se selecciona el primer 
        ///     elemento de la lista, para cada uno de ellos ponemos como primer 
        ///     elemento a un objeto (que corresponda al tipo de dato que almacena el 
        ///     DropDownList) con el único fin de que en la interfaz diga Seleccione, 
        ///     para que puedan ser seleccionadas todas las opciones que se listan.
        /// </summary>
        /// <param name="clase">tipo de dato del objeto a crear</param>
        /// <returns>
        ///     Un objeto cuyo tipo es el que recibe como argumento y cuyo campo descipción es 
        ///     "--Seleccione--" o valor 0 en caso de Horario.
        /// </returns>
        private object createSeleccioneOf(Type clase) {
            object ret;
            const int defId = -1;
            const string defDescripcion = "--Seleccione--";

            switch(clase.Name.Trim()) {
                case "Provincia":
                    Provincia aux_prv = new Provincia();
                    aux_prv.ProvinciaId = defId;
                    aux_prv.ProvinciaDescripcion = defDescripcion;
                    ret = aux_prv;
                    break;
                case "Localidad":
                    Localidad aux_loc = new Localidad();
                    aux_loc.LocalidadId = defId;
                    aux_loc.LocalidadDescripcion = defDescripcion;
                    ret = aux_loc;
                    break;
                case "Sucursal":
                    Sucursal aux_suc = new Sucursal();
                    aux_suc.SucursalId = defId;
                    aux_suc.SucursalDescripcion = defDescripcion;
                    ret = aux_suc;
                    break;
                case "Especialidad":
                    Especialidad aux_esp = new Especialidad();
                    aux_esp.EspecialidadId = defId;
                    aux_esp.EspecialidadDescripcion = defDescripcion;
                    ret = aux_esp;
                    break;
                case "Afiliado":
                    Afiliado aux_af = new Afiliado();
                    aux_af.AfiliadoID = defId;
                    aux_af.Apellido = defDescripcion;
                    ret = aux_af;
                    break;
                case "Horario":
                    Horario aux_hs = new Horario();
                    aux_hs.HorarioID = defId;
                    aux_hs.Hora = new TimeSpan(0, 0, 0);
                    ret = aux_hs;
                    break;
                default:
                    ret = null;
                    break;
            }
            return ret;
        }

        /// <summary>
        ///     Muestra en la interfaz un cartel detallando si no hay disponibilidad de algún
        ///     campo a la hora de sacar el turno.
        /// </summary>
        /// <param name="hay">existencia o no de la disponibilidad</param>
        /// <param name="category">
        ///     campo por el cual se tiene que notificar la no disponibilidad en caso de que así sea.
        /// </param>
        private void showHayDisponibilidad(bool hay,string category) {
            if(!hay) {
                Label18.Text = "Actualmente, no tenemos " + category +
                               " según los apartados seleccionados.";
                Label18.Visible = true;
            }
        }

        /// <summary>
        ///     Cambia la visibilidad de todos los componentes dependiendo de que ocurra
        ///     en el programa
        /// </summary>
        /// <param name="which">
        ///     cuál es el evento que produjo el cambio en la visibilidad
        /// </param>
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
                    rmTurnoButton.Enabled = addTurnoButton.Visible = false;
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
                case Tools.INCORRECTIME:
                    sacarTurnoButton.Visible = false;
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

        /// <summary>
        ///     Constantes utilizadas para describir un evento del programa, como un click
        ///     o alguna notificación al usuario.
        /// </summary>
        private enum Tools {
            SACARNUEVOTURNO, SACARTURNO, ELIMINARTURNO, ELIMINARESTETURNO, CANCELARADD,
            CANCELARRM, SIGUIENTETURNO, ANTERIORTURNO,

            PROVINCIASELECTED, LOCALIDADSELECTED, SUCURSALSELECTED, ESPECIALIDADSELECTED,
            MEDICOSELECTED, FECHASELECTED, HORASELECTED,

            ONLYTURNOTODELETE, LASTTURNOTODELETE, FIRSTTURNOTODELETE, INCORRECTDAY, INCORRECTIME
        }
    }

}