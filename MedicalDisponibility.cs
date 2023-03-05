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
    public partial class MedicalDisponibility : Form {
        private Medico whoAmI;
        private TablesDataContext db;
        private List<Sucursal> workingSucursals;
        private DisponibilidadMedico tempDM;

        //utilizado para volver al componente anterior
        private MedicalHome previousState;
        public MedicalDisponibility(Medico medico, MedicalHome home) {
            InitializeComponent();
            whoAmI = medico;
            previousState = home;
            db = new TablesDataContext();
            tempDM = new DisponibilidadMedico();
            initSucursales();
        }

        private void Reset() {
            tempDM = new DisponibilidadMedico();
            initSucursales();
            changeVisibilityBy(Tools.RESETALL);
        }

        /// <summary>
        ///     Carga todas las sucursales en las que trabaja el médico en cuestión en 
        ///     tempSucursales y las inserta en el ComboBox de Sucursales.
        /// </summary>
        private void initSucursales() {
            int szSucursales;
            comboSucursales.Items.Clear();
            workingSucursals = (from suc in db.Sucursal
                              join ms in db.MedicoSucursal
                                  on suc.SucursalId equals ms.IDSucursal
                              where ms.IDMedico == whoAmI.MedicoID
                              select suc).ToList();

            szSucursales = workingSucursals.Count();
            string[] localidesText = getLocalidadesComplex(szSucursales);
            for(int i = 0; i < szSucursales; ++i) {
                comboSucursales.Items.Add(workingSucursals[i].SucursalDescripcion.Trim() +
                                          " en " + localidesText[i]);
            }
        }

        private void comboSucursales_SelectedIndexChanged(object sender, EventArgs e) {
            comboDias.ResetText(); abmInicio.ResetText(); abmFin.ResetText();
            abmConsultorio.ResetText();
            int index = comboSucursales.SelectedIndex;
            var queryDispoM = from dm in db.DisponibilidadMedico
                              where dm.IDMedico == whoAmI.MedicoID &&
                                    dm.IDSucursal == workingSucursals[index].SucursalId
                              select dm;
            var queryDias = from dia in db.Dia
                            join dm in queryDispoM
                                on dia.DiaID equals dm.IDDia
                            select dia;

            comboDias.DisplayMember = "NombreDia";
            comboDias.ValueMember = "DiaId";
            comboDias.DataSource = queryDias;
            changeVisibilityBy(Tools.SUCSELECTED);
        }
        private void comboDias_SelectedIndexChanged(object sender, EventArgs e) {
            tempDM = getDM();
            abmInicio.Text = tempDM.HorarioInicio.ToString();
            abmFin.Text = tempDM.HorarioFin.ToString();
            abmConsultorio.Text = tempDM.Consultorio.ToString();
            changeVisibilityBy(Tools.DIASELECTED);
        }

        /// <param name="size">
        ///     tamaño de la lista de sucursales en donde trabaja el médico
        /// </param>
        /// <returns>
        ///     Un arreglo de strings con el nombre de la localidad concatenado al nombre
        ///     de su respectiva provincia.
        /// </returns>
        private string[] getLocalidadesComplex(int size) {
            string[] localidadesComplex = new string[size];
            Localidad temp;

            for(int i = 0; i < size; ++i) {
                temp = (from loc in db.Localidad
                        where loc.LocalidadId == workingSucursals[i].IDLocalidad
                        select loc).First();
                localidadesComplex[i] = temp.LocalidadDescripcion.Trim();
                var queryProvincias = from prov in db.Provincia
                                      where prov.ProvinciaId == temp.IDProvincia
                                      select prov.ProvinciaDescripcion;
                localidadesComplex[i] += ", " + queryProvincias.First().Trim();
            }
            return localidadesComplex;
        }

        /// <returns>
        ///     El objeto DisponibilidadMedico correspondiente a la sucursal y día seleccionado.
        /// </returns>
        private DisponibilidadMedico getDM() {
            int selectedSucIndex;
            selectedSucIndex = comboSucursales.SelectedIndex;
            /*
              esto funciona porque, dada la forma en que ingresamos los datos en el ComboBox,
              el index del item seleccionado del ComboBox corresponde al mismo item de la
              lista tempSucursales
            */
            var queryDM = from dm in db.DisponibilidadMedico
                          where dm.IDMedico == whoAmI.MedicoID &&
                                dm.IDSucursal == workingSucursals[selectedSucIndex].SucursalId &&
                                dm.IDDia == (int)comboDias.SelectedValue
                          select dm;
            return queryDM.First();
        }

        private void abmInicio_TextChanged(object sender, EventArgs e) {
            changeVisibilityBy(Tools.ABMsTXTCHANGED);
        }

        private void abmFin_TextChanged(object sender, EventArgs e) {
            changeVisibilityBy(Tools.ABMsTXTCHANGED);
        }

        private void abmConsultorio_TextChanged(object sender, EventArgs e) {
            changeVisibilityBy(Tools.ABMsTXTCHANGED);
        }

        private void makeABM1_Click(object sender, EventArgs e) {
            string msg, caption;
            if(!setNewValues())
                return;
            try {
                db.SubmitChanges();
                caption = "Operación realizada con éxito";
                msg = "La relación entre usted y la sucursal ha sido modificada correctamente.";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                this.Reset();
            }
            catch(Exception) {
                caption = "Operación fallida";
                msg = "La relación entre usted y la sucursal no se ha podido modificar. Comuníquese con los desarrolladores.";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                Application.Exit();
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

            consultorio = 0; horaInicio = horaFin = new TimeSpan();

            try {
                stringTime = abmInicio.Text.Split(':');
                if(stringTime.Length < 2)
                    throw new FormatException();
                hora = Convert.ToInt32(stringTime[0]);
                minutos = Convert.ToInt32(stringTime[1]);
                if(stringTime.Length == 2)
                    segundos = 0;
                else
                    segundos = Convert.ToInt32(stringTime[2]);
                horaInicio = new TimeSpan(hora, minutos, segundos);

                stringTime = abmFin.Text.Split(':');
                hora = Convert.ToInt32(stringTime[0]);
                minutos = Convert.ToInt32(stringTime[1]);
                if(stringTime.Length < 3)
                    segundos = 0;
                else
                    segundos = Convert.ToInt32(stringTime[2]);
                horaFin = new TimeSpan(hora, minutos, segundos);

                consultorio = Convert.ToInt32(abmConsultorio.Text);
            }
            catch(FormatException) {
                string msg, caption;
                caption = "Datos incorrectos";
                msg = "Los datos ingresados son incorrectos. Al ingresar los horarios, ingreselos " +
                      "con el formato HORA:MINUTOS u HORA:MINUTOS:SEGUNDOS. Al ingresar el consultorio, " +
                      "verifique que haya únicamente valores numéricos. Por favor, intente nuevamente.";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                return false;
            }

            if(horaInicio > horaFin) {
                string msg, caption;
                caption = "La hora de finalización es inválida";
                msg = "Usted ha ingresado un rango horario que excede las 23:59 del día en cuestión. " +
                      "Para solucionar ésto, registre una nueva disponibilidad que abarque desde las " +
                      "00:00 del día siguiente, hasta las " + horaFin.ToString() + " del mismo día";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
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
        private void deleteAndAdvice_Times(TimeSpan newInicio,TimeSpan oldInicio,
                                           TimeSpan newFin,TimeSpan oldFin) {
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

                Cursor.Current = Cursors.WaitCursor;
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

                    sender.advicePatient(afectado,ft.Fecha,hs,HIChanged ? 
                                                                EnviarMail.Motivo.HICHANGED :
                                                                EnviarMail.Motivo.HFCHANGED);
                }
                Cursor.Current = Cursors.Default;
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

                Cursor.Current = Cursors.WaitCursor;
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
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        ///     Elimina todos los turnos que los pacientes tengan con el médico en cuestión para
        ///     el día que el médico que ya no asiste en determinada sucursal.
        /// </summary>
        /// <param name="SucursalId">ID de la sucural en cuestión</param>
        /// <param name="DiaId">ID del día que el médico dejará de asistir en la sucursal</param>
        private void deleteAndAdvice_Day(int SucursalId, int DiaId) {
            var queryTurnos= from turno in db.Turno
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

                Cursor.Current = Cursors.WaitCursor;
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
                Cursor.Current = Cursors.Default;
            }
        }

        private void abmDyS_Click_1(object sender, EventArgs e) {
            if(AddSuc.Visible){
                changeVisibilityBy(Tools.ABMDYS_SUCVISIBLE);
            }
            else {
                changeVisibilityBy(Tools.ABMDYS_SUCNOVISIBLE);
                string[] localidesText;
                localidesText = getLocalidadesComplex(workingSucursals.Count());
                for(int i = 0; i < localidesText.Length; ++i) {
                    comboSucursalesRemove.Items.Add(workingSucursals[i].SucursalDescripcion +
                                              " en " + localidesText[i]);
                }
            }
        }

        private void RmSuc_CheckedChanged(object sender, EventArgs e) {
            if(RmSuc.Checked) {
                changeVisibilityBy(Tools.RMSUCCHK);
            }
            else {
                changeVisibilityBy(Tools.RMSUCNOCHK);
            }
        }

        private void AddSuc_CheckedChanged(object sender, EventArgs e) {
            if(AddSuc.Checked) {
                changeVisibilityBy(Tools.ADDSUCCHK);
                var queryProvincia = from prov in db.Provincia
                                     select prov;
                comboProvincia.DisplayMember = "ProvinciaDescripcion";
                comboProvincia.ValueMember = "ProvinciaId";
                comboProvincia.DataSource = queryProvincia;
            }
            else {
                changeVisibilityBy(Tools.ADDSUCNOCHK);
            }
        }

        private void abmSuc_Click(object sender, EventArgs e) {
            string msg, caption;
            int index,SucursalId;
            MedicoSucursal msToRemove;

            index = comboSucursalesRemove.SelectedIndex;
            SucursalId = workingSucursals[index].SucursalId;

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
                caption = "Operación realizada con éxito";
                msg = "La relación entre usted y la sucursal ha sido removida correctamente.";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                this.Reset();
            }
            catch(Exception) {
                caption = "Operación fallida";
                msg = "La relación entre usted y la sucursal no se ha podido remover. Comuníquese con los desarrolladores.";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        private void comboSucursalesRemove_SelectedIndexChanged(object sender, EventArgs e) {
            abmSuc.Visible = true;
        }

        private void abmSuc1_Click(object sender, EventArgs e) {
            MedicoSucursal ms = new MedicoSucursal();
            string msg,caption;
            if(comboProvincia.SelectedItem == null ||
               comboLocalidad.SelectedItem == null ||
               comboSucursalesAñadir.SelectedItem == null) {
                MessageBox.Show("Hay campos incompletos.\nPor favor, complete los campos.");
                return;
            }
            ms.IDMedico = whoAmI.MedicoID;
            ms.IDSucursal = (int) comboSucursalesAñadir.SelectedValue;
            db.MedicoSucursal.InsertOnSubmit(ms);
            try {
                db.SubmitChanges();
                caption = "Operación realizada con éxito";
                msg = "La relación entre usted y la sucursal ha sido registrada correctamente.";
                MessageBox.Show(msg, caption,MessageBoxButtons.OK);
                this.Reset();
            }
            catch(Exception) {
                caption = "Operación fallida";
                msg = "La relación entre usted y la sucursal no se ha podido registrar. Comuníquese con los desarrolladores.";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        private void comboProvincia_SelectedIndexChanged(object sender, EventArgs e) {
            comboLocalidad.ResetText(); comboSucursalesAñadir.ResetText();
            int ProvinciaId = (int) comboProvincia.SelectedValue;
            var queryLocalidad = from loc in db.Localidad
                                 where loc.IDProvincia == ProvinciaId
                                 select loc;
            comboLocalidad.DisplayMember = "LocalidadDescripcion";
            comboLocalidad.ValueMember = "LocalidadId";
            comboLocalidad.DataSource = queryLocalidad;
            changeVisibilityBy(Tools.PROVINCIASELECTED);
        }

        private void comboLocalidad_SelectedIndexChanged(object sender, EventArgs e) {
            int LocalidadId = (int) comboLocalidad.SelectedValue;
            comboSucursalesAñadir.ResetText();
            /*
              en la lista de sucursales a añadir a la disponibilidad del médico, muestro todas las
              disponibles EXCEPTO aquellas en las que el médico ya trabaja, que son las que 
              están presentes en la lista tempSucursales
            */  
            var querySucursal = (from suc in db.Sucursal
                                 where suc.IDLocalidad == LocalidadId
                                 select suc)
                                 .ToList()
                                 .Except(workingSucursals, new SucursalComparer())
                                 .ToList();
            comboSucursalesAñadir.DisplayMember = "SucursalDescripcion";
            comboSucursalesAñadir.ValueMember = "SucursalId";
            comboSucursalesAñadir.DataSource = querySucursal;
            changeVisibilityBy(Tools.LOCALIDADSELECTED);
        }
        private void comboSucursalesAñadir_SelectedIndexChanged(object sender, EventArgs e) {
            changeVisibilityBy(Tools.ADDSUCSELECTED);
        }

        private void AddRmDays_CheckedChanged(object sender, EventArgs e) {
            if(AddRmDays.Checked) {
                changeVisibilityBy(Tools.ADDRMDAYSCHK);
                var querySucursales = from suc in db.Sucursal
                                      join ms in db.MedicoSucursal on
                                        suc.SucursalId equals ms.IDSucursal
                                      where ms.IDMedico == whoAmI.MedicoID
                                      select suc;
                comboSucModDias.DisplayMember = "SucursalDescripcion";
                comboSucModDias.ValueMember = "SucursalId";
                comboSucModDias.DataSource = querySucursales;
            }
            else {
                changeVisibilityBy(Tools.ADDRMDAYSNOCHK);
            }
        }

        private void comboSucModDias_SelectedIndexChanged(object sender, EventArgs e) {
            changeVisibilityBy(Tools.SUCMODDIASELECTED);
        }

        private void addDay_CheckedChanged(object sender, EventArgs e) {
            if(addDay.Checked) {
                int SucursalId = (int) comboSucModDias.SelectedValue;
                changeVisibilityBy(Tools.ADDDAYCHK);
                var queryExceptionalDays = from dia in db.Dia
                                           join dm in db.DisponibilidadMedico on
                                                dia.DiaID equals dm.IDDia
                                           where dm.IDMedico == whoAmI.MedicoID &&
                                                 dm.IDSucursal == SucursalId
                                           select dia;
                /*
                  en la lista de días a añadir a la disponibilidad del médico, muestro todos los
                  disponibles EXCEPTO los que el médico ya trabaja
                */
                var queryDias = (from dia in db.Dia
                                 select dia).ToList()
                                 .Except(queryExceptionalDays, new DayComparer())
                                 .ToList();
                comboDayToAdd.DisplayMember = "NombreDia";
                comboDayToAdd.ValueMember = "DiaID";
                comboDayToAdd.DataSource = queryDias;
                label11.Visible = comboDayToAdd.Visible = true;
            }
            else {
                changeVisibilityBy(Tools.ADDDAYNOCHK);
            }
        }

        private void rmDay_CheckedChanged(object sender, EventArgs e) {
            if(rmDay.Checked) {
                changeVisibilityBy(Tools.RMDAYCHK);
                int SucursalId = (int)comboSucModDias.SelectedValue;
                var queryDias = from dia in db.Dia
                                join dm in db.DisponibilidadMedico on
                                    dia.DiaID equals dm.IDDia
                                where dm.IDMedico == whoAmI.MedicoID &&
                                      dm.IDSucursal == SucursalId
                                select dia;
                comboDayToRm.DisplayMember = "NombreDia";
                comboDayToRm.ValueMember = "DiaID";
                comboDayToRm.DataSource = queryDias;
            }
            else {
                changeVisibilityBy(Tools.RMDAYNOCHK);
            }
        }

        private void dayToAdd_SelectedIndexChanged(object sender, EventArgs e) {
            changeVisibilityBy(Tools.DAYADDSELECTED);
        }

        private void abmDay_Click(object sender, EventArgs e) {
            string msg, caption;
            int SucursalId, DiaId;
            DisponibilidadMedico dm = new DisponibilidadMedico();

            SucursalId = (int)comboSucModDias.SelectedValue;
            DiaId = (int)comboDayToAdd.SelectedValue;
            dm.IDMedico = whoAmI.MedicoID;
            dm.IDDia = DiaId;
            dm.Consultorio = 0;
            dm.HorarioInicio = new TimeSpan(0, 0, 0);
            dm.HorarioFin = new TimeSpan(0, 0, 0);
            dm.IDSucursal = SucursalId;

            db.DisponibilidadMedico.InsertOnSubmit(dm);
            try {
                db.SubmitChanges();
                caption = "Operación realizada con éxito";
                msg = "El nuevo día ha sido registrado exitosamente. Ahora proceda a editar los horarios y el consultorio en el apartado 'Modificar disponibilidad'. " + 
                      "Dichos valores están en 0 de manera predeterminada hasta que usted los modifique.";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                this.Reset();
            }
            catch(Exception) {
                caption = "Operación fallida";
                msg = "El nuevo día no se ha podido registrar. Comuníquese con los desarrolladores.";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        private void comboDayToRm_SelectedIndexChanged(object sender, EventArgs e) {
            changeVisibilityBy(Tools.DAYTORMSELECTED);
        }

        private void abmDay1_Click(object sender, EventArgs e) {
            string msg, caption;
            int SucursalId, DiaId;

            SucursalId = (int)comboSucModDias.SelectedValue;
            DiaId = (int)comboDayToRm.SelectedValue;

            //elimino todos los días que el médico trabaja en dicha sucursal
            var dmsToRemove = from dm in db.DisponibilidadMedico
                              where dm.IDMedico == whoAmI.MedicoID &&
                                    dm.IDDia == DiaId &&
                                    dm.IDSucursal == SucursalId
                              select dm;

            deleteAndAdvice_Day(SucursalId,DiaId);

            db.DisponibilidadMedico.DeleteAllOnSubmit(dmsToRemove);
            try {
                db.SubmitChanges();
                caption = "Operación realizada con éxito";
                msg = "El día ha sido eliminado exitosamente.";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                this.Reset();
            }
            catch(Exception) {
                caption = "Operación fallida";
                msg = "El día no se ha podido eliminar. Comuníquese con los desarrolladores.";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        private void back_Click(object sender, EventArgs e) {
            previousState.Show();
            this.Close();
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
                case Tools.RESETALL:
                    abmDyS.Enabled = label12.Enabled = label2.Enabled = comboSucursales.Enabled = true;

                    label1.Visible = label3.Visible = label4.Visible = label5.Visible =
                    label6.Visible = label7.Visible = label8.Visible = label9.Visible =
                    label10.Visible = label11.Visible = label13.Visible = comboDias.Visible =
                    comboSucursalesRemove.Visible = comboProvincia.Visible = comboLocalidad.Visible =
                    comboSucursalesAñadir.Visible = comboSucModDias.Visible = comboDayToAdd.Visible =
                    comboDayToRm.Visible = abmInicio.Visible = abmFin.Visible = abmConsultorio.Visible =
                    makeABM1.Visible = abmInicio.Visible = abmSuc.Visible = abmSuc1.Visible =
                    abmDay.Visible = abmDay1.Visible = RmSuc.Visible = AddSuc.Visible = AddRmDays.Visible =
                    addDay.Visible = rmDay.Visible = addDay.Checked = rmDay.Checked = RmSuc.Checked =
                    AddSuc.Checked = AddRmDays.Checked = false;

                    comboSucursalesRemove.Items.Clear();
                    break;
                case Tools.SUCSELECTED:
                    label3.Visible = comboDias.Visible = label3.Enabled = comboDias.Enabled = true;
                    label1.Visible = label4.Visible = label5.Visible = abmInicio.Visible =
                    abmFin.Visible = abmConsultorio.Visible = makeABM1.Visible = false;
                    break;
                case Tools.DIASELECTED:
                    abmInicio.Visible = abmFin.Visible = abmConsultorio.Visible = label1.Visible =
                    label4.Visible = label5.Visible = true;
                    break;
                case Tools.ABMsTXTCHANGED:
                    if(!makeABM1.Visible)
                        makeABM1.Visible = true;
                    break;
                case Tools.ABMDYS_SUCVISIBLE:
                    label2.Enabled = comboSucursales.Enabled = true;
                    RmSuc.Visible = AddSuc.Visible = AddRmDays.Visible = false;
                    break;
                case Tools.ABMDYS_SUCNOVISIBLE:
                    abmInicio.Visible = abmFin.Visible = abmConsultorio.Visible = label1.Visible =
                    label4.Visible = label5.Visible = makeABM1.Visible = label2.Enabled =
                    label3.Enabled = comboDias.Enabled = label3.Visible = comboDias.Visible =
                    comboSucursales.Enabled = false;
                    RmSuc.Visible = AddSuc.Visible = AddRmDays.Visible = true;
                    break;
                case Tools.RMSUCCHK:
                    label6.Visible = comboSucursalesRemove.Visible = true;
                    AddSuc.Visible = abmDyS.Enabled = AddRmDays.Visible = false;
                    break;
                case Tools.RMSUCNOCHK:
                    label6.Visible = comboSucursalesRemove.Visible = abmSuc.Visible = false;
                    AddSuc.Visible = abmDyS.Enabled = AddRmDays.Visible = true;
                    break;
                case Tools.ADDSUCCHK:
                    label7.Visible = comboProvincia.Visible = true;
                    RmSuc.Visible = abmDyS.Enabled = AddRmDays.Visible = false;
                    break;
                case Tools.ADDSUCNOCHK:
                    label7.Visible = label8.Visible = label9.Visible = comboProvincia.Visible =
                    comboLocalidad.Visible = comboSucursalesAñadir.Visible = abmSuc1.Visible = false;
                    RmSuc.Visible = abmDyS.Enabled = AddRmDays.Visible = true;
                    break;
                case Tools.PROVINCIASELECTED:
                    comboLocalidad.Visible = label9.Visible = true;
                    label8.Visible = comboSucursalesAñadir.Visible = abmSuc1.Visible = false;
                    break;
                case Tools.LOCALIDADSELECTED:
                    comboSucursalesAñadir.Visible = label8.Visible = true;
                    abmSuc1.Visible = false;
                    break;
                case Tools.ADDSUCSELECTED:
                    abmSuc1.Visible = true;
                    break;
                case Tools.ADDRMDAYSCHK:
                    label10.Visible = comboSucModDias.Visible = true;
                    RmSuc.Visible = abmDyS.Enabled = AddSuc.Visible = false;
                    break;
                case Tools.ADDRMDAYSNOCHK:
                    label10.Visible = comboSucModDias.Visible = addDay.Visible =
                    rmDay.Visible = addDay.Checked = rmDay.Checked = label11.Visible =
                    comboDayToAdd.Visible = false;
                    RmSuc.Visible = abmDyS.Enabled = AddSuc.Visible = true;
                    break;
                case Tools.SUCMODDIASELECTED:
                    addDay.Visible = rmDay.Visible = true;
                    addDay.Checked = rmDay.Checked = false;
                    break;
                case Tools.ADDDAYCHK:
                    rmDay.Visible = false;
                    break;
                case Tools.ADDDAYNOCHK:
                    rmDay.Visible = true;
                    label11.Visible = comboDayToAdd.Visible = abmDay.Visible = false;
                    break;
                case Tools.RMDAYCHK:
                    addDay.Visible = abmDay.Visible = label11.Visible = comboDayToAdd.Visible = false;
                    label13.Visible = comboDayToRm.Visible = true;
                    break;
                case Tools.RMDAYNOCHK:
                    addDay.Visible = true;
                    label13.Visible = comboDayToRm.Visible = abmDay1.Visible = false;
                    break;
                case Tools.DAYADDSELECTED:
                    abmDay.Visible = true;
                    break;
                case Tools.DAYTORMSELECTED:
                    abmDay1.Visible = true;
                    break;
            }
        }
    }


    /// <summary>
    ///     Constantes utilizadas para describir un evento del programa, como un click
    ///     o alguna notificación al usuario.
    /// </summary>
    enum Tools {
        RESETALL,
        SUCSELECTED,
        DIASELECTED,
        ABMsTXTCHANGED,
        ABMDYS_SUCVISIBLE,
        ABMDYS_SUCNOVISIBLE,
        RMSUCCHK,
        RMSUCNOCHK,
        ADDSUCCHK,
        ADDSUCNOCHK,
        PROVINCIASELECTED,
        LOCALIDADSELECTED,
        ADDSUCSELECTED,
        ADDRMDAYSCHK,
        ADDRMDAYSNOCHK,
        SUCMODDIASELECTED,
        ADDDAYCHK,
        ADDDAYNOCHK,
        RMDAYCHK,
        RMDAYNOCHK,
        DAYADDSELECTED,
        DAYTORMSELECTED
    }

    class SucursalComparer : IEqualityComparer<Sucursal> {
        public bool Equals(Sucursal x,Sucursal y) {
            return x.SucursalId == y.SucursalId;
        }
        public int GetHashCode(Sucursal x) {
            return x.SucursalId.GetHashCode();
        }
    }

    class DayComparer : IEqualityComparer<Dia> {
        public bool Equals(Dia x, Dia y) {
            return x.DiaID == y.DiaID ;
        }
        public int GetHashCode(Dia x) {
            return x.DiaID.GetHashCode();
        }
    }
}
