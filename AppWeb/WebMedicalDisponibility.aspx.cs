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
            whoAmI = Session["medico"] as Medico;
            if(IsPostBack) {
                db = Session["database"] as TablesDataContext;
                tempDM = Session["dmodify"] as DisponibilidadMedico;
            }
            else {
                db = new TablesDataContext();
                List<Sucursal> querySucursales = (from suc in db.Sucursal
                                                  join ms in db.MedicoSucursal
                                                      on suc.SucursalId equals ms.IDSucursal
                                                  where ms.IDMedico == whoAmI.MedicoID
                                                  select suc).ToList();

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

        private void showSucursalData(int SucursalId) {
            int LocalidadId;
            string ProvinciaDescripcion;
            Localidad tempLoc;

            LocalidadId = (from suc in db.Sucursal
                           where suc.SucursalId == SucursalId
                           select suc.IDLocalidad).FirstOrDefault();
            tempLoc = (from loc in db.Localidad
                       where loc.LocalidadId == LocalidadId
                       select loc).FirstOrDefault();
            ubicacion.Text = "En " + tempLoc.LocalidadDescripcion.Trim();

            ProvinciaDescripcion = (from prov in db.Provincia
                                    where prov.ProvinciaId == tempLoc.IDProvincia
                                    select prov.ProvinciaDescripcion).FirstOrDefault();
            ubicacion.Text += ", " + ProvinciaDescripcion.Trim() + ".";
        }

        private object createSeleccioneOf(Type clase) {
            /*
                Esta función existe porque los DropDownList no detectan cambios cuando
                se selecciona el primer elemento de la lista, entonces para cada uno
                de ellos ponemos como primer elemento a un objeto (correspondiente) que 
                diga Seleccione, para que puedan ser seleccionadas todas las opciones
                que se listan.
            */
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
            if(SucursalId == -1)
                return;
            showSucursalData(SucursalId);
            var queryDispoM = from dm in db.DisponibilidadMedico
                              where dm.IDMedico == whoAmI.MedicoID &&
                                    dm.IDSucursal == SucursalId
                              select dm;
            List<Dia> queryDias = (from dia in db.Dia
                                   join dm in queryDispoM
                                       on dia.DiaID equals dm.IDDia
                                   select dia).ToList();
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
            if(DiaID == -1)
                return;
            tempDM = (from dm in db.DisponibilidadMedico
                      where dm.IDMedico == whoAmI.MedicoID &&
                            dm.IDSucursal == SucursalId &&
                            dm.IDDia == DiaID
                      select dm).FirstOrDefault();
            
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

        private bool setNewValues() {
            int hora, minutos, segundos;
            string[] stringTime;
            TimeSpan horaInicio, horaFin;

            stringTime = textboxHoraInicio.Text.Split(':');
            hora = Convert.ToInt32(stringTime[0]);
            minutos = Convert.ToInt32(stringTime[1]);
            if(stringTime.Length < 3)
                segundos = 0;
            else
                segundos = Convert.ToInt32(stringTime[2]);
            horaInicio = new TimeSpan(hora, minutos, segundos);

            stringTime = textboxHoraFin.Text.Split(':');
            hora = Convert.ToInt32(stringTime[0]);
            minutos = Convert.ToInt32(stringTime[1]);
            if(stringTime.Length < 3)
                segundos = 0;
            else
                segundos = Convert.ToInt32(stringTime[2]);
            horaFin = new TimeSpan(hora, minutos, segundos);

            if(horaInicio > horaFin) {
                string msg;
                msg = "Usted ha ingresado un rango horario que excede las 23:59 del día en cuestión. " +
                      "Para solucionar ésto, registre una nueva disponibilidad que abarque desde las " +
                      "00:00 del día siguiente, hasta las " + horaFin.ToString() + " del mismo día";
                ClientScript.RegisterStartupScript(this.GetType(),
                            "alert", "alert('" + msg + "');", true);
                return false;
            }

            tempDM.HorarioFin = horaFin;
            tempDM.HorarioInicio = horaInicio;
            tempDM.Consultorio = Convert.ToInt32(textboxConsultorio.Text);
            
            return true;
        }
        
        protected void abmDyS_Click(object sender, EventArgs e) {
            changeVisibilityBy(Tools.MODSUCDIACLICKED);
        }

        protected void rmSuc_Click(object sender, EventArgs e) {
            List<Sucursal> querySucursales = (from suc in db.Sucursal
                                              join ms in db.MedicoSucursal
                                                  on suc.SucursalId equals ms.IDSucursal
                                              where ms.IDMedico == whoAmI.MedicoID
                                              select suc).ToList();
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
            if(Convert.ToInt32(comboSucursalesRemove.SelectedValue) == -1)
                return;
            abmSUC.Visible = true;
        }

        protected void abmSUC_Click(object sender, EventArgs e) {
            string msg;
            int SucursalId = Convert.ToInt32(comboSucursalesRemove.SelectedValue);
            MedicoSucursal msToRemove;
            var queryDM = from dm in db.DisponibilidadMedico
                          where dm.IDMedico == whoAmI.MedicoID &&
                                dm.IDSucursal == SucursalId
                          select dm;
            msToRemove = (from ms in db.MedicoSucursal
                          where ms.IDMedico == whoAmI.MedicoID &&
                                ms.IDSucursal == SucursalId
                          select ms).FirstOrDefault();

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

        protected void addSuc_Click(object sender, EventArgs e) {
            List<Provincia> queryProvincias = (from prov in db.Provincia
                                               select prov).ToList();
            queryProvincias.Insert(0, 
                createSeleccioneOf(queryProvincias.First().GetType()) as Provincia);
            comboProvinciaAdd.DataSource = queryProvincias;
            comboProvinciaAdd.DataTextField = "ProvinciaDescripcion";
            comboProvinciaAdd.DataValueField = "ProvinciaId";
            comboProvinciaAdd.DataBind();
            changeVisibilityBy(Tools.ADDSUCCLICKED);
        }

        protected void comboProvinciaAdd_SelectedIndexChanged(object sender, EventArgs e) {
            int ProvinciaId = Convert.ToInt32(comboProvinciaAdd.SelectedValue);
            if(ProvinciaId == -1)
                return;
            List<Localidad> queryLocalidad = (from loc in db.Localidad
                                              where loc.IDProvincia == ProvinciaId
                                              select loc).ToList();
            if(queryLocalidad.Count > 0)
                queryLocalidad.Insert(0,
                    createSeleccioneOf(queryLocalidad.First().GetType()) as Localidad);
            comboLocalidadAdd.DataSource = queryLocalidad;
            comboLocalidadAdd.DataTextField = "LocalidadDescripcion";
            comboLocalidadAdd.DataValueField = "LocalidadId";
            comboLocalidadAdd.DataBind();
            Label10.Visible = comboLocalidadAdd.Visible = true;
        }

        protected void comboLocalidadAdd_SelectedIndexChanged(object sender, EventArgs e) {
            int LocalidadId = Convert.ToInt32(comboLocalidadAdd.SelectedValue);
            if(LocalidadId == -1)
                return;
            List<Sucursal> querySucursalesEstoy = (from suc in db.Sucursal
                                                   join ms in db.MedicoSucursal
                                                       on suc.SucursalId equals ms.IDSucursal
                                                   where ms.IDMedico == whoAmI.MedicoID
                                                   select suc).ToList();
            List<Sucursal> querySucursalesNoEstoy = (from suc in db.Sucursal
                                                     where suc.IDLocalidad == LocalidadId
                                                     select suc)
                                                     .ToList()
                                                     .Except(querySucursalesEstoy, 
                                                                new SucursalComparer())
                                                     .ToList();

            if(querySucursalesNoEstoy.Count > 0)
                querySucursalesNoEstoy.Insert(0,
                    createSeleccioneOf(querySucursalesNoEstoy.First().GetType()) as Sucursal);
            comboSucursalAdd.DataSource = querySucursalesNoEstoy;
            comboSucursalAdd.DataTextField = "SucursalDescripcion";
            comboSucursalAdd.DataValueField = "SucursalId";
            comboSucursalAdd.DataBind();
            Label11.Visible = comboSucursalAdd.Visible = true;
        }

        protected void comboSucursalAdd_SelectedIndexChanged(object sender, EventArgs e) {
            if(Convert.ToInt32(comboSucursalAdd.SelectedValue) == -1)
                return;
            abmSUC1.Visible = true;
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
            int SucursalId = Convert.ToInt32(comboSucursalesDias.SelectedValue);
            if(SucursalId == -1)
                return;
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

                if(queryDiasEstoy.Count > 0)
                    queryDiasEstoy.Insert(0,
                        createSeleccioneOf(queryDiasEstoy.First().GetType()) as Dia);
                comboDias1.DataSource = queryDiasEstoy;
                comboDias1.DataTextField = "NombreDia";
                comboDias1.DataValueField = "DiaId";
                comboDias1.DataBind();
            }
            Label13.Visible = comboDias1.Visible = true;
        }

        protected void comboDias1_SelectedIndexChanged(object sender, EventArgs e) {
            if(Convert.ToInt32(comboDias1.SelectedValue) == -1)
                return;
            /* Idem punto anterior */
            if(agregarDias.Checked) {
                abmDAY.Text = "Agregar día";
                abmDAY.BackColor = System.Drawing.Color.FromArgb(0, 255, 0);
            }
            else {
                abmDAY.Text = "Eliminar día";
                abmDAY.BackColor = System.Drawing.Color.FromArgb(255, 0, 0);
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
                var dmsToRemove = from dm in db.DisponibilidadMedico
                                  where dm.IDMedico == whoAmI.MedicoID &&
                                        dm.IDDia == DiaId &&
                                        dm.IDSucursal == SucursalId
                                  select dm;
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

        private void changeVisibilityBy(Tools which) {
            switch(which) {
                case Tools.SUCURSALSELECTED:
                    Label3.Visible = comboDias.Visible = true;
                    break;
                case Tools.DIASELECTED:
                    Label4.Visible = Label5.Visible = Label6.Visible = makeABM1.Visible =
                    textboxHoraInicio.Visible = textboxHoraFin.Visible =
                    textboxConsultorio.Visible = true;
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
                    false;

                    cancelRmSuc.Visible = comboSucursalesRemove.Visible = Label8.Visible =
                    true;
                    break;
                case Tools.CANCELRMSUC:
                    Label7.Text = "Agregar/eliminar sucursales y días";
                    abmDyS.Visible = addSuc.Visible = addRmDays.Visible = rmSuc.Visible =
                    true;

                    cancelRmSuc.Visible = comboSucursalesRemove.Visible = Label8.Visible =
                    abmSUC.Visible = false;
                    break;
                case Tools.ADDSUCCLICKED:
                    Label7.Text = addSuc.Text;
                    abmDyS.Visible = rmSuc.Visible = addRmDays.Visible = addSuc.Visible = 
                    false;
                        
                    cancelAddSuc.Visible = comboProvinciaAdd.Visible = Label9.Visible = true;
                    break;
                case Tools.CANCELADDSUC:
                    Label7.Text = "Agregar/eliminar sucursales y días";
                    abmDyS.Visible = rmSuc.Visible = addRmDays.Visible = addSuc.Visible =
                    true;

                    cancelAddSuc.Visible = comboProvinciaAdd.Visible = Label9.Visible =
                    Label11.Visible = Label10.Visible = comboProvinciaAdd.Visible = 
                    comboLocalidadAdd.Visible = comboSucursalAdd.Visible = 
                    abmSUC1.Visible = false;
                    break;
                case Tools.ADDRMDAYSCLICKED:
                    Label7.Text = addRmDays.Text;
                    abmDyS.Visible = rmSuc.Visible = addSuc.Visible = addRmDays.Visible = 
                    false;

                    agregarDias.Checked = eliminarDias.Checked = false;

                    cancelAddRmDays.Visible = agregarDias.Visible = eliminarDias.Visible = true;
                    break;
                case Tools.CANCELADDRMDAYS:
                    Label7.Text = "Agregar/eliminar sucursales y días";
                    abmDyS.Visible = rmSuc.Visible = addSuc.Visible = addRmDays.Visible =
                    true;

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

        private enum Tools {
            SUCURSALSELECTED, DIASELECTED, MODSUCDIACLICKED, RMSUCCLICKED, ADDSUCCLICKED,
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