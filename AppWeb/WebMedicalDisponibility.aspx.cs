using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb {
    public partial class WebMedicalDisponibility : System.Web.UI.Page {
        private Medico whoAmI;
        private TablesDataContext db;
        private DisponibilidadMedico tempDM;

        protected void Page_Load(object sender, EventArgs e) {
            whoAmI = (Medico)Session["medico"];
            if(IsPostBack) {
                db = Session["database"] as TablesDataContext;
                tempDM = Session["dmodify"] as DisponibilidadMedico;
            }
            else {
                db = new TablesDataContext();

                /*
                  si un usuario paciente cambia la url desde la barra de navegación y quiere
                  acceder a éste componente, se lo impido redirigiéndolo hacia su componente
                  home
                */
                if(!((Usuario)Session["user"]).isMedico)
                    Response.Redirect("~/WebHome.aspx");

                /*
                  si whoAmI es null y no fue PostBack, significa que el WebMedicalHome no guardó 
                  en la sesión al usuario, por lo que tengo que ir a buscarlo a la cookie 
                  ya que se trata de un reinicio del navegador
                */
                if(whoAmI == null) {
                    int UsuarioId = Convert.ToInt32(Request.Cookies["userID"].Value);
                    whoAmI = (from medico in db.Medico
                              where medico.IDUsuario == UsuarioId
                              select medico).First();
                    Session["medico"] = whoAmI;
                }
                List<Sucursal> querySucursales = (from suc in db.Sucursal
                                                  join ms in db.MedicoSucursal
                                                      on suc.SucursalId equals ms.IDSucursal
                                                  where ms.IDMedico == whoAmI.MedicoID
                                                  select suc).ToList();
                //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
                if(querySucursales.Count > 0)
                    querySucursales.Insert(0,
                        createSeleccioneOf(querySucursales.First().GetType()) as Sucursal);
                comboSucursales.DataSource = querySucursales;
                comboSucursales.DataTextField = "SucursalDescripcion";
                comboSucursales.DataValueField = "SucursalId";
                comboSucursales.DataBind();
                Session["database"] = db;
            }
        }

        /// <summary>
        ///     Muestra en la interfaz la ubicación de la sucursal seleccionada.
        /// </summary>
        /// <param name="SucursalId">ID de la sucursal en cuestión</param>
        private void showSucursalData(int SucursalId) {
            int LocalidadId;
            string ProvinciaDescripcion;
            Localidad tempLoc;

            LocalidadId = (from suc in db.Sucursal
                           where suc.SucursalId == SucursalId
                           select suc.IDLocalidad).First();
            tempLoc = (from loc in db.Localidad
                       where loc.LocalidadId == LocalidadId
                       select loc).First();
            ubicacion.Text = "En " + tempLoc.LocalidadDescripcion.Trim();

            ProvinciaDescripcion = (from prov in db.Provincia
                                    where prov.ProvinciaId == tempLoc.IDProvincia
                                    select prov.ProvinciaDescripcion).First();
            ubicacion.Text += ", " + ProvinciaDescripcion.Trim() + ".";
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
                case "Sucursal":
                    Sucursal aux_suc = new Sucursal();
                    aux_suc.SucursalId = defId;
                    aux_suc.SucursalDescripcion = defDescripcion;
                    ret = aux_suc;
                    break;
                case "Dia":
                    Dia aux_dia = new Dia();
                    aux_dia.DiaID = defId;
                    aux_dia.NombreDia = defDescripcion;
                    ret = aux_dia;
                    break;
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
                default:
                    ret = null;
                    break;
            }
            return ret;
        }

        protected void comboSucursales_SelectedIndexChanged(object sender, EventArgs e) {
            int SucursalId = Convert.ToInt32(comboSucursales.SelectedValue);
            //si el item seleccionado es el objeto seleccione aborto
            if(SucursalId == -1) {
                changeVisibilityBy(Tools.INCORRECTSUCSELECTED);
                return;
            }
            showSucursalData(SucursalId);
            var queryDispoM = from dm in db.DisponibilidadMedico
                              where dm.IDMedico == whoAmI.MedicoID &&
                                    dm.IDSucursal == SucursalId
                              select dm;
            List<Dia> queryDias = (from dia in db.Dia
                                   join dm in queryDispoM
                                       on dia.DiaID equals dm.IDDia
                                   select dia).ToList();
            //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
            if(queryDias.Count > 0)
                queryDias.Insert(0,
                    createSeleccioneOf(queryDias.First().GetType()) as Dia);
            comboDias.ClearSelection();
            comboDias.DataSource = queryDias;
            comboDias.DataTextField = "NombreDia";
            comboDias.DataValueField = "DiaId";
            comboDias.DataBind();
            changeVisibilityBy(Tools.SUCURSALSELECTED);
        }

        protected void comboDias_SelectedIndexChanged(object sender, EventArgs e) {
            int SucursalId, DiaID;

            SucursalId = Convert.ToInt32(comboSucursales.SelectedValue);
            DiaID = Convert.ToInt32(comboDias.SelectedValue);

            //si el item seleccionado es el objeto seleccione aborto
            if(DiaID == -1) {
                changeVisibilityBy(Tools.INCORRECTDSELECTED);
                return;
            }

            tempDM = (from dm in db.DisponibilidadMedico
                      where dm.IDMedico == whoAmI.MedicoID &&
                            dm.IDSucursal == SucursalId &&
                            dm.IDDia == DiaID
                      select dm).First();
            
            textboxHoraInicio.Text = tempDM.HorarioInicio.ToString();
            textboxHoraFin.Text = tempDM.HorarioFin.ToString();
            textboxConsultorio.Text = tempDM.Consultorio.ToString();

            Session["dmodify"] = tempDM;
            changeVisibilityBy(Tools.DIASELECTED);
        }

        protected void makeABM1_Click(object sender, EventArgs e) {
            string msg;
            if(!setNewValues())
                return;
            try {
                db.SubmitChanges();
                msg = "La relación entre usted y la sucursal ha sido modificada correctamente.";
                ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "');" +
                            "window.location='WebMedicalDisponibility.aspx';", true);
            }
            catch(Exception) {
                msg = "La relación entre usted y la sucursal no se ha podido modificar. Comuníquese con los desarrolladores.";
                ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "');" +
                            "window.location='WebMedicalDisponibility.aspx';", true);
            }
        }

        /// <summary>
        ///     Setea los valores nuevos/modificados a la variable tempDM, para así poder
        ///     actualizar la disponibilidad del médico en la base de datos
        /// </summary>
        /// <returns>
        ///     true si se puedieron modificar los valores correctamente.
        ///     false en caso contario.
        /// </returns>
        private bool setNewValues() {
            int hora, minutos, segundos, consultorio;
            string[] stringTime;
            TimeSpan horaInicio, horaFin;
            string msg;

            consultorio = 0; horaInicio = horaFin = new TimeSpan();  

            try {
                stringTime = textboxHoraInicio.Text.Split(':'); 
                if(stringTime.Length < 2) {
                    msg = "Los datos ingresados son incorrectos. Al ingresar los horarios, ingreselos " +
                          "con el formato HORA:MINUTOS u HORA:MINUTOS:SEGUNDOS. Al ingresar el consultorio, " +
                          "verifique que haya únicamente valores numéricos. Por favor, intente nuevamente."; 
                    ClientScript.RegisterStartupScript(this.GetType(),
                                "alert", "alert('" + msg + "');", true);
                    return false;
                }
                hora = Convert.ToInt32(stringTime[0]);
                minutos = Convert.ToInt32(stringTime[1]);
                if(stringTime.Length < 3)
                    segundos = 0;
                else
                    segundos = Convert.ToInt32(stringTime[2]);
                horaInicio = new TimeSpan(hora, minutos, segundos);

                stringTime = textboxHoraFin.Text.Split(':');
                if(stringTime.Length < 2) {
                    msg = "Los datos ingresados son incorrectos. Al ingresar los horarios, ingreselos " +
                          "con el formato HORA:MINUTOS u HORA:MINUTOS:SEGUNDOS. Al ingresar el consultorio, " +
                          "verifique que haya únicamente valores numéricos. Por favor, intente nuevamente.";
                    ClientScript.RegisterStartupScript(this.GetType(),
                                "alert", "alert('" + msg + "');", true);
                    return false;
                }
                hora = Convert.ToInt32(stringTime[0]);
                minutos = Convert.ToInt32(stringTime[1]);
                if(stringTime.Length < 3)
                    segundos = 0;
                else
                    segundos = Convert.ToInt32(stringTime[2]);
                horaFin = new TimeSpan(hora, minutos, segundos);

                consultorio = Convert.ToInt32(textboxConsultorio.Text);
            }
            catch(FormatException) {
                msg = "Los datos ingresados son incorrectos. Al ingresar los horarios, ingreselos " +
                      "con el formato HORA:MINUTOS u HORA:MINUTOS:SEGUNDOS. Al ingresar el consultorio, " +
                      "verifique que haya únicamente valores numéricos. Por favor, intente nuevamente.";
                ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "');", true);
                return false;
            }

            if(horaInicio > horaFin) {
                msg = "Usted ha ingresado un rango horario que excede las 23:59 del día en cuestión. " +
                      "Para solucionar ésto, registre una nueva disponibilidad que abarque desde las " +
                      "00:00 del día siguiente, hasta las " + horaFin.ToString() + " del mismo día";
                ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "');", true);
                return false;
            }

            deleteAndAdvice_Times(horaInicio, tempDM.HorarioInicio, horaFin, tempDM.HorarioFin);

            tempDM.HorarioFin = horaFin;
            tempDM.HorarioInicio = horaInicio;
            tempDM.Consultorio = consultorio;

            return true;
        }

        /// <summary>
        ///     Elimina todos los turnos que los pacientes tengan con el médico en cuestión
        ///     que queden fuera de su nuevo/actualizado rango horario.
        /// </summary>
        /// <param name="newInicio">hora de inicio nueva/actualizada</param>
        /// <param name="oldInicio">hora de inicio que el médico tenía antes de actualizarla</param>
        /// <param name="newFin">hora de finalización nueva/actualizada</param>
        /// <param name="oldFin">hora de finalización que el médico tenía antes de actualizarla</param>
        private void deleteAndAdvice_Times(TimeSpan newInicio, TimeSpan oldInicio,
                                           TimeSpan newFin, TimeSpan oldFin) {
            IQueryable<Turno> queryTurnos = null;
            IQueryable<FechaTurno> queryFT = null;
            bool HIChanged = false;
            /*
              cuando la hora de inicio actualizada es POSTERIOR a la hora de inicio antes 
              de actualizar, se eliminan todos los turnos de los pacientes que estén entre 
              dicha franja horaria
            */
            if(newInicio > oldInicio) {
                HIChanged = true;
                queryTurnos = from turno in db.Turno
                              join ft in db.FechaTurno
                                 on turno.IDFechaTurno equals ft.FechaTurnoID
                              join hs in db.Horario
                                 on ft.IDHorario equals hs.HorarioID
                              where hs.Hora >= oldInicio &&
                                    hs.Hora <= newInicio
                              select turno;
                //caso en el que no haya turnos en la franja horaria mencionada más arriba
                if(queryTurnos.Count() == 0)
                    return;
                queryFT = from ft in db.FechaTurno
                          join turno in queryTurnos
                             on ft.FechaTurnoID equals turno.IDFechaTurno
                          select ft;

                db.Turno.DeleteAllOnSubmit(queryTurnos);
                db.FechaTurno.DeleteAllOnSubmit(queryFT);
            }
            /*
              cuando la hora de finalización actualizada es PREVIA a la hora de finalización 
              antes de actualizar, se eliminan todos los turnos de los pacientes que estén 
              entre dicha franja horaria
            */
            if(newFin < oldFin) {
                HIChanged = false;
                queryTurnos = from turno in db.Turno
                              join ft in db.FechaTurno
                                 on turno.IDFechaTurno equals ft.FechaTurnoID
                              join hs in db.Horario
                                 on ft.IDHorario equals hs.HorarioID
                              where hs.Hora <= oldFin &&
                                    hs.Hora >= newFin
                              select turno;
                //caso en el que no haya turnos en la franja horaria mencionada más arriba
                if(queryTurnos.Count() == 0)
                    return;
                queryFT = from ft in db.FechaTurno
                          join turno in queryTurnos
                             on ft.FechaTurnoID equals turno.IDFechaTurno
                          select ft;

                db.Turno.DeleteAllOnSubmit(queryTurnos);
                db.FechaTurno.DeleteAllOnSubmit(queryFT);
            }
            if(queryTurnos != null) {
                EnviarMail sender = new EnviarMail(whoAmI.IDUsuario);
                Usuario afectado;
                FechaTurno ft;
                TimeSpan hs;

                foreach(Turno turno in queryTurnos) {
                    afectado = (from user in db.Usuario
                                where user.UsuarioID == turno.IDUsuario
                                select user).First();
                    ft = (from fecha in db.FechaTurno
                          where fecha.FechaTurnoID == turno.IDFechaTurno
                          select fecha).First();
                    hs = (from h in db.Horario
                          where h.HorarioID == ft.IDHorario
                          select h.Hora).First();

                    sender.advicePatient(afectado, ft.Fecha, hs, HIChanged ?
                                                                EnviarMail.Motivo.HICHANGED :
                                                                EnviarMail.Motivo.HFCHANGED);
                }
            }
        }

        protected void abmDyS_Click(object sender, EventArgs e) {
            changeVisibilityBy(Tools.MODSUCDIACLICKED);
            if(Label2.Visible)
                abmDyS.Text = "Modificar sucursales y días";
            else
                abmDyS.Text = "Modificar disponibilidad";
        }

        protected void rmSuc_Click(object sender, EventArgs e) {
            List<Sucursal> querySucursales = (from suc in db.Sucursal
                                              join ms in db.MedicoSucursal
                                                  on suc.SucursalId equals ms.IDSucursal
                                              where ms.IDMedico == whoAmI.MedicoID
                                              select suc).ToList();
            //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
            if(querySucursales.Count > 0)
                querySucursales.Insert(0,
                    createSeleccioneOf(querySucursales.First().GetType()) as Sucursal);
            comboSucursalesRemove.DataSource = querySucursales;
            comboSucursalesRemove.DataTextField = "SucursalDescripcion";
            comboSucursalesRemove.DataValueField = "SucursalId";
            comboSucursalesRemove.DataBind();
            changeVisibilityBy(Tools.RMSUCCLICKED);
        }

        protected void comboSucursalesRemove_SelectedIndexChanged(object sender, EventArgs e) {
            //si el item seleccionado es el objeto seleccione aborto
            if(Convert.ToInt32(comboSucursalesRemove.SelectedValue) == -1)
                return;
            abmSUC.Visible = true;
        }

        protected void abmSUC_Click(object sender, EventArgs e) {
            string msg;
            int SucursalId = Convert.ToInt32(comboSucursalesRemove.SelectedValue);
            MedicoSucursal msToRemove;

            //elimino toda la disponiblidad (días y horarios) del medico en dicha sucursal
            var queryDM = from dm in db.DisponibilidadMedico
                          where dm.IDMedico == whoAmI.MedicoID &&
                                dm.IDSucursal == SucursalId
                          select dm;
            //elimino la relación entre el médico y la sucursal en cuestión
            msToRemove = (from ms in db.MedicoSucursal
                          where ms.IDMedico == whoAmI.MedicoID &&
                                ms.IDSucursal == SucursalId
                          select ms).First();

            deleteAndAdvice_Sucursal(SucursalId);

            db.DisponibilidadMedico.DeleteAllOnSubmit(queryDM);
            db.MedicoSucursal.DeleteOnSubmit(msToRemove);
            try {
                db.SubmitChanges();
                msg = "La relación entre usted y la sucursal ha sido removida correctamente.";
                ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "');" +
                            "window.location='WebMedicalDisponibility.aspx';", true);
            }
            catch(Exception) {
                msg = "La relación entre usted y la sucursal no se ha podido remover. Comuníquese con los desarrolladores.";
                ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "');", true);
            }
        }

        /// <summary>
        ///     Elimina todos los turnos que los pacientes tengan con el médico en cuestión en
        ///     la sucursal en la cual el mismo ya no asiste.
        /// </summary>
        /// <param name="SucursalId">ID de la sucural en la que el médico deja de trabajar</param>
        private void deleteAndAdvice_Sucursal(int SucursalId) {
            var queryTurnos = from turno in db.Turno
                              where turno.IDMedico == whoAmI.MedicoID &&
                                    turno.IDSucursal == SucursalId
                              select turno;
            //caso en el que no haya turnos en la sucursal eliminada
            if(queryTurnos.Count() == 0)
                return;
            var queryFT = from ft in db.FechaTurno
                          join turno in queryTurnos
                             on ft.FechaTurnoID equals turno.IDFechaTurno
                          select ft;

            if(queryTurnos != null) {
                EnviarMail sender = new EnviarMail(whoAmI.IDUsuario);
                Usuario afectado;
                FechaTurno ft;
                TimeSpan hs;

                foreach(Turno turno in queryTurnos) {
                    afectado = (from user in db.Usuario
                                where user.UsuarioID == turno.IDUsuario
                                select user).First();
                    ft = (from fecha in db.FechaTurno
                          where fecha.FechaTurnoID == turno.IDFechaTurno
                          select fecha).First();
                    hs = (from h in db.Horario
                          where h.HorarioID == ft.IDHorario
                          select h.Hora).First();

                    sender.advicePatient(afectado, ft.Fecha, hs, EnviarMail.Motivo.SUCURSAL);
                }
                db.Turno.DeleteAllOnSubmit(queryTurnos);
                db.FechaTurno.DeleteAllOnSubmit(queryFT);
            }
        }

        protected void addSuc_Click(object sender, EventArgs e) {
            List<Provincia> queryProvincias = (from prov in db.Provincia
                                               select prov).ToList();

            //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
            queryProvincias.Insert(0, 
                createSeleccioneOf(queryProvincias.First().GetType()) as Provincia);
            comboProvinciaAdd.DataSource = queryProvincias;
            comboProvinciaAdd.DataTextField = "ProvinciaDescripcion";
            comboProvinciaAdd.DataValueField = "ProvinciaId";
            comboProvinciaAdd.DataBind();
            changeVisibilityBy(Tools.ADDSUCCLICKED);
        }

        protected void comboProvinciaAdd_SelectedIndexChanged(object sender, EventArgs e) {
            Label10.Visible = comboLocalidadAdd.Visible = true;
            Label11.Visible = comboSucursalAdd.Visible = abmSUC1.Visible = false;

            int ProvinciaId = Convert.ToInt32(comboProvinciaAdd.SelectedValue);
            //si el item seleccionado es el objeto seleccione aborto
            if(ProvinciaId == -1) {
                comboLocalidadAdd.Items.Clear();
                return;
            }
            List<Localidad> queryLocalidad = (from loc in db.Localidad
                                              where loc.IDProvincia == ProvinciaId
                                              select loc).ToList();
            //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
            if(queryLocalidad.Count > 0)
                queryLocalidad.Insert(0,
                    createSeleccioneOf(queryLocalidad.First().GetType()) as Localidad);
            comboLocalidadAdd.DataSource = queryLocalidad;
            comboLocalidadAdd.DataTextField = "LocalidadDescripcion";
            comboLocalidadAdd.DataValueField = "LocalidadId";
            comboLocalidadAdd.DataBind();
        }

        protected void comboLocalidadAdd_SelectedIndexChanged(object sender, EventArgs e) {
            Label11.Visible = comboSucursalAdd.Visible = true;
            abmSUC1.Visible = false;

            int LocalidadId = Convert.ToInt32(comboLocalidadAdd.SelectedValue);
            List<Sucursal> querySucursalesEstoy, querySucursalesNoEstoy;
            //si el item seleccionado es el objeto seleccione aborto
            if(LocalidadId == -1) {
                comboSucursalAdd.Items.Clear();
                return;
            }
            querySucursalesEstoy = (from suc in db.Sucursal
                                    join ms in db.MedicoSucursal
                                        on suc.SucursalId equals ms.IDSucursal
                                    where ms.IDMedico == whoAmI.MedicoID
                                    select suc).ToList();
            querySucursalesNoEstoy = (from suc in db.Sucursal
                                      where suc.IDLocalidad == LocalidadId
                                      select suc)
                                      .ToList()
                                      .Except(querySucursalesEstoy,new SucursalComparer())
                                      .ToList();
            //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
            if(querySucursalesNoEstoy.Count > 0)
                querySucursalesNoEstoy.Insert(0,
                    createSeleccioneOf(querySucursalesNoEstoy.First().GetType()) as Sucursal);
            comboSucursalAdd.DataSource = querySucursalesNoEstoy;
            comboSucursalAdd.DataTextField = "SucursalDescripcion";
            comboSucursalAdd.DataValueField = "SucursalId";
            comboSucursalAdd.DataBind();
        }

        protected void comboSucursalAdd_SelectedIndexChanged(object sender, EventArgs e) {
            bool enable = true;
            //si el item seleccionado es el objeto seleccione aborto
            if(Convert.ToInt32(comboSucursalAdd.SelectedValue) == -1)
                enable = false;
            abmSUC1.Visible = enable;
        }

        protected void abmSUC1_Click(object sender, EventArgs e) {
            MedicoSucursal ms = new MedicoSucursal();
            string msg;
            if(comboProvinciaAdd.SelectedItem == null ||
               comboLocalidadAdd.SelectedItem == null ||
               comboSucursalAdd.SelectedItem == null) {
                msg = "Hay campos incompletos.\nPor favor, complete los campos.";
                ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "');", true);
                return;
            }
            ms.IDMedico = whoAmI.MedicoID;
            ms.IDSucursal = Convert.ToInt32(comboSucursalAdd.SelectedValue);
            db.MedicoSucursal.InsertOnSubmit(ms);
            try {
                db.SubmitChanges();
                msg = "La relación entre usted y la sucursal ha sido registrada correctamente.";
                ClientScript.RegisterStartupScript(this.GetType(),
                             "alert", "alert('" + msg + "');" +
                             "window.location='WebMedicalDisponibility.aspx';", true);
            }
            catch(Exception) {
                msg = "La relación entre usted y la sucursal no se ha podido registrar. Comuníquese con los desarrolladores.";
                ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "');" +
                            "window.location='WebMedicalDisponibility.aspx';", true);
            }
        }

        protected void addRmDays_Click(object sender, EventArgs e) {
            changeVisibilityBy(Tools.ADDRMDAYSCLICKED);
        }

        protected void cancelRmSuc_Click(object sender, EventArgs e) {
            changeVisibilityBy(Tools.CANCELRMSUC);
        }

        protected void cancelAddSuc_Click(object sender, EventArgs e) {
            changeVisibilityBy(Tools.CANCELADDSUC);
        }

        protected void cancelAddRmSuc_Click(object sender, EventArgs e) {
            changeVisibilityBy(Tools.CANCELADDRMDAYS);
        }

        protected void agregarDias_CheckedChanged(object sender, EventArgs e) {
            if(agregarDias.Checked) {
                List<Sucursal> querySucursales = (from suc in db.Sucursal
                                                  join ms in db.MedicoSucursal
                                                      on suc.SucursalId equals ms.IDSucursal
                                                  where ms.IDMedico == whoAmI.MedicoID
                                                  select suc).ToList();
                //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
                if(querySucursales.Count > 0)
                    querySucursales.Insert(0,
                        createSeleccioneOf(querySucursales.First().GetType()) as Sucursal);
                comboSucursalesDias.DataSource = querySucursales;
                comboSucursalesDias.DataTextField = "SucursalDescripcion";
                comboSucursalesDias.DataValueField = "SucursalId";
                comboSucursalesDias.DataBind();
            }
            changeVisibilityBy(Tools.ADDDAYCHECKED);
        }

        protected void eliminarDias_CheckedChanged(object sender, EventArgs e) {
            if(eliminarDias.Checked) {
                List<Sucursal> querySucursales = (from suc in db.Sucursal
                                                  join ms in db.MedicoSucursal
                                                      on suc.SucursalId equals ms.IDSucursal
                                                  where ms.IDMedico == whoAmI.MedicoID
                                                  select suc).ToList();
                //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
                if(querySucursales.Count > 0)
                    querySucursales.Insert(0,
                        createSeleccioneOf(querySucursales.First().GetType()) as Sucursal);
                comboSucursalesDias.DataSource = querySucursales;
                comboSucursalesDias.DataTextField = "SucursalDescripcion";
                comboSucursalesDias.DataValueField = "SucursalId";
                comboSucursalesDias.DataBind();
            }
            changeVisibilityBy(Tools.RMDAYCHECKED);
        }

        protected void comboSucursalesDias_SelectedIndexChanged(object sender, EventArgs e) {
            Label13.Visible = comboDias1.Visible = true;
            abmDAY.Visible = false;

            int SucursalId = Convert.ToInt32(comboSucursalesDias.SelectedValue);
            //si el item seleccionado es el objeto seleccione aborto
            if(SucursalId == -1) {
                comboDias1.Items.Clear();
                return;
            }
            /*
              Esto es porque uso un solo DropDownList para ambos casos, entonces
              me fijo que Checkbox está checkeado para saber si el DropDownList
              se está usando para agregar días o para borrarlos
             */
            if(agregarDias.Checked) {
                var queryDiasEstoy = from dia in db.Dia
                                     join dm in db.DisponibilidadMedico on
                                          dia.DiaID equals dm.IDDia
                                     where dm.IDMedico == whoAmI.MedicoID &&
                                           dm.IDSucursal == SucursalId
                                     select dia;
                List<Dia> queryDiasNoEstoy = (from dia in db.Dia
                                              select dia)
                                              .ToList()
                                              .Except(queryDiasEstoy, new DayComparer())
                                              .ToList();
                //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
                if(queryDiasNoEstoy.Count > 0)
                    queryDiasNoEstoy.Insert(0,
                        createSeleccioneOf(queryDiasNoEstoy.First().GetType()) as Dia);
                comboDias1.DataSource = queryDiasNoEstoy;
                comboDias1.DataTextField = "NombreDia";
                comboDias1.DataValueField = "DiaId";
                comboDias1.DataBind();
            }
            else {
                List<Dia> queryDiasEstoy = (from dia in db.Dia
                                            join dm in db.DisponibilidadMedico on
                                                dia.DiaID equals dm.IDDia
                                            where dm.IDMedico == whoAmI.MedicoID &&
                                                  dm.IDSucursal == SucursalId
                                            select dia).ToList();
                //inserto al objeto seleccione como primer elemento de la lista que recibe el DropDownList
                if(queryDiasEstoy.Count > 0)
                    queryDiasEstoy.Insert(0,
                        createSeleccioneOf(queryDiasEstoy.First().GetType()) as Dia);
                comboDias1.DataSource = queryDiasEstoy;
                comboDias1.DataTextField = "NombreDia";
                comboDias1.DataValueField = "DiaId";
                comboDias1.DataBind();
            }
        }

        protected void comboDias1_SelectedIndexChanged(object sender, EventArgs e) {
            //si el item seleccionado es el objeto seleccione aborto
            if(Convert.ToInt32(comboDias1.SelectedValue) == -1) {
                abmDAY.Visible = false;
                return;
            }
            /* Idem función anterior */
            if(agregarDias.Checked) {
                abmDAY.Text = "Agregar día";
                abmDAY.CssClass = "nuevo-turno";
            }
            else {
                abmDAY.Text = "Eliminar día";
                abmDAY.CssClass = "borrar-turno";
            }
            abmDAY.Visible = true;
        }

        protected void abmDAY_Click(object sender, EventArgs e) {
            string msg;
            int SucursalId, DiaId;
        
            SucursalId = Convert.ToInt32(comboSucursalesDias.SelectedValue);
            DiaId = Convert.ToInt32(comboDias1.SelectedValue);

            if(agregarDias.Checked) {
                DisponibilidadMedico dm = new DisponibilidadMedico();

                dm.IDMedico = whoAmI.MedicoID;
                dm.IDDia = DiaId;
                dm.Consultorio = 0;
                dm.HorarioInicio = new TimeSpan(0, 0, 0);
                dm.HorarioFin = new TimeSpan(0, 0, 0);
                dm.IDSucursal = SucursalId;

                db.DisponibilidadMedico.InsertOnSubmit(dm);
                try {
                    db.SubmitChanges();
                    msg = "El nuevo día ha sido registrado exitosamente. Ahora proceda a editar los horarios y el consultorio en el apartado ~Modificar disponibilidad~. " +
                          "Dichos valores están en 0 de manera predeterminada hasta que usted los modifique.";
                    ClientScript.RegisterStartupScript(this.GetType(),
                                "alert", "alert('" + msg + "');" +
                                "window.location='WebMedicalDisponibility.aspx';", true);
                }
                catch(Exception) {
                    msg = "El nuevo día no se ha podido registrar. Comuníquese con los desarrolladores.";
                    ClientScript.RegisterStartupScript(this.GetType(),
                                "alert", "alert('" + msg + "');", true);
                }
            }
            else {
                //elimino todos los días que el médico trabaja en dicha sucursal
                var dmsToRemove = from dm in db.DisponibilidadMedico
                                  where dm.IDMedico == whoAmI.MedicoID &&
                                        dm.IDDia == DiaId &&
                                        dm.IDSucursal == SucursalId
                                  select dm;

                deleteAndAdvice_Day(SucursalId, DiaId);

                db.DisponibilidadMedico.DeleteAllOnSubmit(dmsToRemove);
                try {
                    db.SubmitChanges();
                    msg = "El día ha sido eliminado exitosamente.";
                    ClientScript.RegisterStartupScript(this.GetType(),
                                "alert", "alert('" + msg + "');" +
                                "window.location='WebMedicalDisponibility.aspx';", true);
                }
                catch(Exception) {
                    msg = "El día no se ha podido eliminar. Comuníquese con los desarrolladores.";
                    ClientScript.RegisterStartupScript(this.GetType(),
                                "alert", "alert('" + msg + "');", true);
                }
            }
        }
        /// <summary>
        ///     Elimina todos los turnos que los pacientes tengan con el médico en cuestión para
        ///     el día que el médico que ya no asiste en una determinada sucursal.
        /// </summary>
        /// <param name="SucursalId">ID de la sucural en cuestión</param>
        /// <param name="DiaId">ID del día que el médico dejará de asistir en la sucursal</param>
        private void deleteAndAdvice_Day(int SucursalId, int DiaId) {
            var queryTurnos = from turno in db.Turno
                              join ft in db.FechaTurno
                                 on turno.IDFechaTurno equals ft.FechaTurnoID
                              where turno.IDMedico == whoAmI.MedicoID &&
                                    turno.IDSucursal == SucursalId &&
                                    ft.IDDia == DiaId
                              select turno;
            //caso en el que no haya turnos en la sucursal eliminada
            if(queryTurnos.Count() == 0)
                return;
            var queryFT = from ft in db.FechaTurno
                          join turno in queryTurnos
                             on ft.FechaTurnoID equals turno.IDFechaTurno
                          select ft;
            if(queryTurnos != null) {
                EnviarMail sender = new EnviarMail(whoAmI.IDUsuario);
                Usuario afectado;
                FechaTurno ft;
                TimeSpan hs;

                foreach(Turno turno in queryTurnos) {
                    afectado = (from user in db.Usuario
                                where user.UsuarioID == turno.IDUsuario
                                select user).First();
                    ft = (from fecha in db.FechaTurno
                          where fecha.FechaTurnoID == turno.IDFechaTurno
                          select fecha).First();
                    hs = (from h in db.Horario
                          where h.HorarioID == ft.IDHorario
                          select h.Hora).First();

                    sender.advicePatient(afectado, ft.Fecha, hs, EnviarMail.Motivo.DAY);
                }
                db.Turno.DeleteAllOnSubmit(queryTurnos);
                db.FechaTurno.DeleteAllOnSubmit(queryFT);
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
                case Tools.SUCURSALSELECTED:
                    Label3.Visible = comboDias.Visible = true;
                    Label4.Visible = Label5.Visible = Label6.Visible = textboxHoraInicio.Visible =
                    textboxHoraFin.Visible = textboxConsultorio.Visible = makeABM1.Visible =
                    false;
                    break;
                case Tools.INCORRECTSUCSELECTED:
                    Label4.Visible = Label5.Visible = Label6.Visible = makeABM1.Visible =
                    textboxHoraInicio.Visible = textboxHoraFin.Visible =
                    textboxConsultorio.Visible = false;
                    ubicacion.Text = "";
                    comboDias.Items.Clear();
                    break;
                case Tools.DIASELECTED:
                    Label4.Visible = Label5.Visible = Label6.Visible = makeABM1.Visible =
                    textboxHoraInicio.Visible = textboxHoraFin.Visible =
                    textboxConsultorio.Visible = true;
                    break;
                case Tools.INCORRECTDSELECTED:
                    Label4.Visible = Label5.Visible = Label6.Visible = makeABM1.Visible =
                    textboxHoraInicio.Visible = textboxHoraFin.Visible =
                    textboxConsultorio.Visible = false;
                    break;
                case Tools.MODSUCDIACLICKED:
                    if(comboSucursales.Visible) {
                        Label1.Visible = Label2.Visible = Label3.Visible = Label4.Visible =
                        Label5.Visible = Label6.Visible = ubicacion.Visible =
                        comboSucursales.Visible = comboDias.Visible = makeABM1.Visible =
                        textboxHoraInicio.Visible = textboxHoraFin.Visible = 
                        textboxConsultorio.Visible = false;

                        Label7.Visible = rmSuc.Visible = addSuc.Visible = addRmDays.Visible =
                        true;
                    }
                    else {
                        Label1.Visible = Label2.Visible = comboSucursales.Visible = ubicacion.Visible = 
                        true;
                        ubicacion.Text = "";
                        comboSucursales.SelectedValue = (-1).ToString();

                        Label7.Visible = rmSuc.Visible = addSuc.Visible = addRmDays.Visible =
                        false;
                    }
                    break;
                case Tools.RMSUCCLICKED:
                    Label7.Text = rmSuc.Text;
                    abmDyS.Visible = addSuc.Visible = addRmDays.Visible = rmSuc.Visible =
                    Label14.Visible = false;

                    cancelRmSuc.Visible = comboSucursalesRemove.Visible = Label8.Visible =
                    true;
                    break;
                case Tools.CANCELRMSUC:
                    Label7.Text = "Agregar/eliminar sucursales y días";
                    abmDyS.Visible = addSuc.Visible = addRmDays.Visible = rmSuc.Visible =
                    Label14.Visible = true;

                    cancelRmSuc.Visible = comboSucursalesRemove.Visible = Label8.Visible =
                    abmSUC.Visible = false;
                    break;
                case Tools.ADDSUCCLICKED:
                    Label7.Text = addSuc.Text;
                    abmDyS.Visible = rmSuc.Visible = addRmDays.Visible = addSuc.Visible = 
                    Label14.Visible = false;

                    cancelAddSuc.Visible = comboProvinciaAdd.Visible = Label9.Visible =
                    true;
                    break;
                case Tools.CANCELADDSUC:
                    Label7.Text = "Agregar/eliminar sucursales y días";
                    abmDyS.Visible = rmSuc.Visible = addRmDays.Visible = addSuc.Visible =
                    Label14.Visible = true;

                    cancelAddSuc.Visible = comboProvinciaAdd.Visible = Label9.Visible =
                    Label11.Visible = Label10.Visible = comboProvinciaAdd.Visible = 
                    comboLocalidadAdd.Visible = comboSucursalAdd.Visible = 
                    abmSUC1.Visible = false;
                    break;
                case Tools.ADDRMDAYSCLICKED:
                    Label7.Text = addRmDays.Text;
                    abmDyS.Visible = rmSuc.Visible = addSuc.Visible = addRmDays.Visible =
                    Label14.Visible = false;

                    agregarDias.Checked = eliminarDias.Checked = false;

                    cancelAddRmDays.Visible = agregarDias.Visible = eliminarDias.Visible = true;
                    break;
                case Tools.CANCELADDRMDAYS:
                    Label7.Text = "Agregar/eliminar sucursales y días";
                    abmDyS.Visible = rmSuc.Visible = addSuc.Visible = addRmDays.Visible =
                    Label14.Visible = true;

                    cancelAddRmDays.Visible = agregarDias.Visible = eliminarDias.Visible = 
                    comboSucursalesDias.Visible = Label12.Visible = Label13.Visible = 
                    comboDias1.Visible = abmDAY.Visible = false;
                    break;
                case Tools.ADDDAYCHECKED:
                    if(agregarDias.Checked) {
                        Label12.Visible = comboSucursalesDias.Visible = true;
                        
                        eliminarDias.Visible = false;
                    }
                    else {
                        Label12.Visible = comboSucursalesDias.Visible = Label13.Visible =
                        comboDias1.Visible = abmDAY.Visible = false;
                        eliminarDias.Visible = true;
                    }
                    break;
                case Tools.RMDAYCHECKED:
                    if(eliminarDias.Checked) {
                        Label12.Visible = comboSucursalesDias.Visible = true;
                        
                        agregarDias.Visible = false;
                    }
                    else {
                        Label12.Visible = comboSucursalesDias.Visible = Label13.Visible =
                        comboDias1.Visible = abmDAY.Visible = false;

                        agregarDias.Visible = true;
                    }
                    break;
            }
        }

        /// <summary>
        ///     Constantes utilizadas para describir un evento del programa, como un click
        ///     o alguna notificación al usuario.
        /// </summary>
        private enum Tools {
            SUCURSALSELECTED, INCORRECTSUCSELECTED ,DIASELECTED, INCORRECTDSELECTED,
            MODSUCDIACLICKED, RMSUCCLICKED, ADDSUCCLICKED,
            ADDRMDAYSCLICKED, CANCELRMSUC, CANCELADDSUC, CANCELADDRMDAYS, ADDDAYCHECKED,
            RMDAYCHECKED,
        }
    }

    class SucursalComparer : IEqualityComparer<Sucursal> {
        public bool Equals(Sucursal x, Sucursal y) {
            return x.SucursalId == y.SucursalId;
        }
        public int GetHashCode(Sucursal x) {
            return x.SucursalId.GetHashCode();
        }
    }

    class DayComparer : IEqualityComparer<Dia> {
        public bool Equals(Dia x, Dia y) {
            return x.DiaID == y.DiaID;
        }
        public int GetHashCode(Dia x) {
            return x.DiaID.GetHashCode();
        }
    }
}