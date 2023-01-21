using Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace AppEscritorio {
    public partial class Turnos : Form {
        private Usuario whoAmI;
        private Afiliado whoAmIAsAfiliado;
        private TablesDataContext db;
        private Home previousState;
        
        private List<Turno> myTurnosToDelete;
        private int indexShowed;
        public Turnos(Usuario logged,Home home) {
            InitializeComponent();
            db = new TablesDataContext();
            whoAmI = logged;
            whoAmIAsAfiliado = (from af in db.Afiliado
                                where af.AfiliadoID == whoAmI.IDAfiliado
                                select af).First();
            indexShowed = -1;
            previousState = home;
        }

        private void Reset() {
            indexShowed = -1;
            changeVisibilityBy(Tools.RESETALL);
        }

        private void back_Click(object sender, EventArgs e) {
            previousState.Show();
            this.Close();
        }

        private void addTurnoButton_Click(object sender, EventArgs e) {
            var queryProvincia = from prov in db.Provincia
                                 select prov;
            comboProvincia.ResetText(); comboLocalidad.ResetText(); comboSucursales.ResetText();
            comboProvincia.DisplayMember = "ProvinciaDescripcion";
            comboProvincia.ValueMember = "ProvinciaId";
            comboProvincia.DataSource = queryProvincia;
            changeVisibilityBy(Tools.SACARNUEVOTURNO);
        }

        private void comboProvincia_SelectedIndexChanged(object sender, EventArgs e) {
            int ProvinciaId = (int)comboProvincia.SelectedValue;
            var queryLocalidad = from loc in db.Localidad
                                 where loc.IDProvincia == ProvinciaId
                                 select loc;
            comboLocalidad.ResetText(); comboSucursales.ResetText();
            comboLocalidad.DisplayMember = "LocalidadDescripcion";
            comboLocalidad.ValueMember = "LocalidadId";
            comboLocalidad.DataSource = queryLocalidad;
            changeVisibilityBy(Tools.PROVINCIASELECTED);
            showHayDisponibilidad(queryLocalidad.Count() != 0, "localidades");
        }

        private void comboLocalidad_SelectedIndexChanged(object sender, EventArgs e) {
            int LocalidadId = (int)comboLocalidad.SelectedValue;
            var querySucursal = from suc in db.Sucursal
                                where suc.IDLocalidad == LocalidadId
                                select suc;
            comboSucursales.ResetText();
            comboSucursales.DisplayMember = "SucursalDescripcion";
            comboSucursales.ValueMember = "SucursalId";
            comboSucursales.DataSource = querySucursal;
            changeVisibilityBy(Tools.LOCALIDADSELECTED);
            showHayDisponibilidad(querySucursal.Count() != 0, "sucursales");
        }

        private void comboSucursales_SelectedIndexChanged(object sender, EventArgs e) {
            int LocalidadId, SucursalId;
            LocalidadId = (int)comboLocalidad.SelectedValue;
            SucursalId = (int)comboSucursales.SelectedValue;
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
            comboEspecialidades.ResetText();
            comboEspecialidades.DisplayMember = "EspecialidadDescripcion";
            comboEspecialidades.ValueMember = "EspecialidadId";
            comboEspecialidades.DataSource = queryEspecialidades;
            changeVisibilityBy(Tools.SUCURSALSELECTED);
            showHayDisponibilidad(queryEspecialidades.Count != 0, "especialidades");
        }

        private void comboEspecialidades_SelectedIndexChanged(object sender, EventArgs e) {
            int SucursalId, EspecialidadId;
            SucursalId = (int)comboSucursales.SelectedValue;
            EspecialidadId = (int)comboEspecialidades.SelectedValue;
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

            comboMedicos.ResetText();
            comboMedicos.DisplayMember = "Apellido";
            comboMedicos.ValueMember = "AfiliadoId";
            comboMedicos.DataSource = queryAfiliados;
            changeVisibilityBy(Tools.ESPECIALIDADSELECTED);
            showHayDisponibilidad(queryAfiliados.Count() != 0, "medicos");
        }

        private void comboMedicos_SelectedIndexChanged(object sender, EventArgs e) {
            int SucursalId, MedicoIdAsAfiliado, MedicoId, szQuery;
            string adviceDisponibilidad;

            SucursalId = (int)comboSucursales.SelectedValue;
            adviceDisponibilidad = "El médico está disponible ";
            MedicoIdAsAfiliado = (int)comboMedicos.SelectedValue;
            MedicoId = (from med in db.Medico
                        join user in db.Usuario
                            on med.IDUsuario equals user.UsuarioID
                        where user.IDAfiliado == MedicoIdAsAfiliado
                        select med.MedicoID).FirstOrDefault();

            List<Dia> queryDias = (from dia in db.Dia
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
                    adviceDisponibilidad += "únicamente el día " + queryDias.FirstOrDefault().NombreDia.Trim();
                    break;
                default:
                    queryDias.Sort(new DayComparer());
                    adviceDisponibilidad += "los días ";
                    for(int i = 0;i < szQuery - 1; ++i) {
                        adviceDisponibilidad += queryDias[i].NombreDia.Trim();
                        if(i == szQuery - 2) {
                            adviceDisponibilidad += " y " + queryDias[i+1].NombreDia.Trim();
                            continue;
                        }
                        adviceDisponibilidad += ", ";
                    }
                    break;
            }
            adviceDisponibilidad += ".";
            label18.Text = adviceDisponibilidad;
            changeVisibilityBy(Tools.MEDICOSELECTED);
            fechaTurnoPicker.Value = fechaTurnoPicker.MinDate = DateTime.Today;
        }

        private void fechaTurnoPicker_ValueChanged(object sender, EventArgs e) {
            int SucursalId, MedicoIdAsAfiliado, MedicoId, DiaId, DiaSelected;
            bool diaCorrecto = false;
            string msg, caption;
            List<Horario> queryHorariosDisponibles = new List<Horario>();

            SucursalId = (int)comboSucursales.SelectedValue;
            MedicoIdAsAfiliado = (int)comboMedicos.SelectedValue;
            MedicoId = (from med in db.Medico
                        join user in db.Usuario
                            on med.IDUsuario equals user.UsuarioID
                        where user.IDAfiliado == MedicoIdAsAfiliado
                        select med.MedicoID).FirstOrDefault();
            DiaId = -1;

            List<Dia> queryDias = (from dia in db.Dia
                                   join dispom in db.DisponibilidadMedico
                                       on dia.DiaID equals dispom.IDDia
                                   where dispom.IDMedico == MedicoId &&
                                         dispom.IDSucursal == SucursalId
                                   select dia).ToList();

            foreach(Dia dia in queryDias) {
                DiaSelected = (int)fechaTurnoPicker.Value.Date.DayOfWeek;
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
            if(!diaCorrecto && comboMedicos.Visible) {
                caption = "Día seleccionado incorrecto";
                msg = "El médico en cuestión no trabaja en ésta sucursal el día que usted seleccionó";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                changeVisibilityBy(Tools.INCORRECTDAY);
                label18.Visible = true;
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
                              where ft.Fecha.Equals(fechaTurnoPicker.Value.Date) && 
                                    turno.IDSucursal == SucursalId &&
                                    turno.IDMedico == MedicoId
                              select ft;

            var queryHorariosUsados = from hs in db.Horario
                                      join ft in queryTurnos
                                          on hs.HorarioID equals ft.IDHorario
                                      select hs;

            foreach(DisponibilidadMedico dm in queryDm) {
                var queryTemp = (from hs in db.Horario
                                 where hs.Hora.CompareTo(dm.HorarioInicio) >= 0 &&
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
            comboHorarios.ResetText();
            comboHorarios.DisplayMember = "Hora";
            comboHorarios.ValueMember = "HorarioId";
            comboHorarios.DataSource = queryHorariosDisponibles;
            changeVisibilityBy(Tools.FECHASELECTED);
            showHayDisponibilidad(queryHorariosDisponibles.Count() != 0, "horarios");
        }

        private void comboHorarios_SelectedIndexChanged(object sender, EventArgs e) {
            changeVisibilityBy(Tools.HORASELECTED);
        }

        private void sacarTurnoButton_Click(object sender, EventArgs e) {
            int DiaId, HorarioId;
            DateTime fecha;
            string msg, caption;
            Turno tToAdd = new Turno();
            FechaTurno ftToAdd = new FechaTurno();

            DiaId = (int)fechaTurnoPicker.Value.Date.DayOfWeek;
            if(DiaId == 0)
                DiaId = 7;  //el por qué de esto está exiplicado en la línea 176
            HorarioId = (int)comboHorarios.SelectedValue;
            fecha = fechaTurnoPicker.Value;

            tToAdd.IDMedico = (from med in db.Medico
                               join user in db.Usuario
                                   on med.IDUsuario equals user.UsuarioID
                               where user.IDAfiliado == (int)comboMedicos.SelectedValue
                               select med.MedicoID).FirstOrDefault();
            tToAdd.IDProvincia = (int)comboProvincia.SelectedValue;
            tToAdd.IDLocalidad = (int)comboLocalidad.SelectedValue;
            tToAdd.IDSucursal = (int)comboSucursales.SelectedValue;
            tToAdd.IDEspecialidad = (int)comboEspecialidades.SelectedValue;
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
                    caption = "Operación realizada con éxito";
                    msg = "El turno ha sido generado exitosamente.";
                    MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                    this.Reset();
                }
                catch(Exception) {
                    caption = "Operación fallida";
                    msg = "El turno no se ha podido registrar. Comuníquese con los desarrolladores.";
                    MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                    Application.Exit();
                }
            }
            catch(Exception) {
                caption = "Operación fallida";
                msg = "No se ha podido registrar la fecha del turno. Comuníquese con los desarrolladores.";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        private void rmTurnoButton_Click(object sender, EventArgs e) {
            string caption, msg;

            myTurnosToDelete = (from turno in db.Turno
                                where turno.IDUsuario == whoAmI.UsuarioID
                                select turno).ToList();

            switch(myTurnosToDelete.Count) {
                case 0:
                    caption = "Atención";
                    msg = "Usted no tiene turnos registrados a su nombre.";
                    MessageBox.Show(msg, caption, MessageBoxButtons.OK);
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
        }

        private void nextTurnoButton_Click(object sender, EventArgs e) {
            showTurnoValues(myTurnosToDelete[++indexShowed]);
            if(!backTurnoButton.Visible)
                changeVisibilityBy(Tools.SIGUIENTETURNO);
            if(indexShowed == myTurnosToDelete.Count - 1)
                changeVisibilityBy(Tools.LASTTURNOTODELETE);
        }

        private void backTurnoButton_Click(object sender, EventArgs e) {
            showTurnoValues(myTurnosToDelete[--indexShowed]);
            if(!nextTurnoButton.Visible)
                changeVisibilityBy(Tools.ANTERIORTURNO);
            if(indexShowed == 0)
                changeVisibilityBy(Tools.FIRSTTURNOTODELETE);
        }

        private void eliminarTurnoButton_Click(object sender, EventArgs e) {
            string caption, msg;
            db.Turno.DeleteOnSubmit(myTurnosToDelete[indexShowed]);
            try {
                db.SubmitChanges();
                caption = "Operación realizada con éxito";
                msg = "El turno ha sido eliminado exitosamente.";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                this.Reset();
            }
            catch(Exception) {
                caption = "Operación fallida";
                msg = "El turno no se ha podido eliminar. Comuníquese con los desarrolladores.";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                Application.Exit();
            }
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
            textBox1.Text = medicoAsAfiliado.Nombre.Trim() + " " +
                            medicoAsAfiliado.Apellido.Trim();

            temp = (from esp in db.Especialidad
                    join m in db.Medico
                        on esp.EspecialidadId equals m.IDEspecialidad
                    where m.MedicoID == medico.MedicoID
                    select esp.EspecialidadDescripcion).First();
            textBox7.Text = temp.Trim();

            temp = (from prov in db.Provincia
                    where prov.ProvinciaId == turno.IDProvincia
                    select prov.ProvinciaDescripcion).First();
            textBox3.Text = temp.Trim();

            temp = (from loc in db.Localidad
                    where loc.LocalidadId == turno.IDLocalidad
                    select loc.LocalidadDescripcion).First();
            textBox4.Text = temp.Trim();

            temp = (from suc in db.Sucursal
                    where suc.SucursalId == turno.IDSucursal
                    select suc.SucursalDescripcion).First();
            textBox5.Text = temp.Trim();

            ft = (from ftt in db.FechaTurno
                  where ftt.FechaTurnoID == turno.IDFechaTurno
                  select ftt).First();
            temp = ft.Fecha.ToString("dd/MM/yyyy");
            textBox2.Text = temp;

            temp = (from hs in db.Horario
                    where hs.HorarioID == ft.IDHorario
                    select hs.Hora).First().ToString();
            textBox6.Text = temp;
        }

        private void showHayDisponibilidad(bool hay, string category) {
            if(!hay) {
                label18.Text = "Actualmente, no tenemos " + category +
                               " según los apartados seleccionados.";
                label18.Visible = true;
            }
        }

        private void cancelAddButton_Click(object sender, EventArgs e) {
            changeVisibilityBy(Tools.CANCELARADD);
        }

        private void cancelRmButton_Click(object sender, EventArgs e) {
            changeVisibilityBy(Tools.CANCELARRM);
        }

        private void changeVisibilityBy(Tools which) {
            switch(which) {
                case Tools.ELIMINARTURNO:
                    label11.Visible = label12.Visible = label13.Visible = label14.Visible =
                    label15.Visible = label16.Visible = label17.Visible = textBox1.Visible =
                    textBox2.Visible = textBox3.Visible = textBox4.Visible = textBox5.Visible =
                    textBox6.Visible = textBox7.Visible = label5.Visible = label9.Visible =
                    nextTurnoButton.Visible = eliminarTurnoButton.Visible =
                    cancelRmButton.Visible = true;
                    rmTurnoButton.Enabled = false;
                    break;
                case Tools.LASTTURNOTODELETE:
                    label5.Visible = nextTurnoButton.Visible = false;
                    break;
                case Tools.ANTERIORTURNO:
                    label5.Visible = nextTurnoButton.Visible = true;
                    break;
                case Tools.FIRSTTURNOTODELETE:
                    backTurnoButton.Visible = label10.Visible = false;
                    break;
                case Tools.SACARNUEVOTURNO:
                    rmTurnoButton.Visible = addTurnoButton.Enabled = false;
                    label1.Visible = comboProvincia.Visible = cancelAddButton.Visible = true;
                    break;
                case Tools.PROVINCIASELECTED:
                    label2.Visible = comboLocalidad.Visible = true;
                    label3.Visible = label7.Visible = label4.Visible = label8.Visible = 
                    label6.Visible = label18.Visible = comboSucursales.Visible = 
                    comboEspecialidades.Visible = comboMedicos.Visible = 
                    fechaTurnoPicker.Visible = comboHorarios.Visible =
                    sacarTurnoButton.Visible = false;
                    break;
                case Tools.LOCALIDADSELECTED:
                    label3.Visible = comboSucursales.Visible = true;
                    label7.Visible = label4.Visible = label8.Visible = label6.Visible =
                    label18.Visible = comboEspecialidades.Visible =
                    comboMedicos.Visible = fechaTurnoPicker.Visible = comboHorarios.Visible =
                    sacarTurnoButton.Visible = false;
                    break;
                case Tools.SUCURSALSELECTED:
                    label7.Visible = comboEspecialidades.Visible = true;
                    label4.Visible = label8.Visible = label6.Visible = label18.Visible = 
                    comboMedicos.Visible = fechaTurnoPicker.Visible = comboHorarios.Visible =
                    sacarTurnoButton.Visible = false;
                    break;
                case Tools.ESPECIALIDADSELECTED:
                    label4.Visible = comboMedicos.Visible = true;
                    label8.Visible = label6.Visible = label18.Visible =
                    fechaTurnoPicker.Visible = comboHorarios.Visible =
                    sacarTurnoButton.Visible = false;
                    break;
                case Tools.MEDICOSELECTED:
                    label8.Visible = fechaTurnoPicker.Visible = label18.Visible = true;
                    label6.Visible = comboHorarios.Visible =
                    sacarTurnoButton.Visible = false;
                    break;
                case Tools.FECHASELECTED:
                    label6.Visible = comboHorarios.Visible = true;
                    sacarTurnoButton.Visible = false;
                    break;
                case Tools.HORASELECTED:
                    sacarTurnoButton.Visible = true;
                    break;
                case Tools.CANCELARADD:
                    addTurnoButton.Enabled = rmTurnoButton.Visible = true;
                    cancelAddButton.Visible = label1.Visible = label2.Visible =
                    label3.Visible = label4.Visible = label5.Visible = label6.Visible =
                    label7.Visible = label8.Visible = comboProvincia.Visible =
                    comboLocalidad.Visible = comboSucursales.Visible = comboMedicos.Visible =
                    fechaTurnoPicker.Visible = comboHorarios.Visible = comboEspecialidades.Visible = 
                    label18.Visible = false;
                    break;
                case Tools.CANCELARRM:
                    rmTurnoButton.Enabled = addTurnoButton.Visible = true;
                    cancelRmButton.Visible = label9.Visible = label11.Visible = label12.Visible =
                    label13.Visible = label14.Visible = label15.Visible = label16.Visible = 
                    label17.Visible = textBox1.Visible = textBox2.Visible = textBox3.Visible =
                    textBox4.Visible = textBox5.Visible = textBox6.Visible = textBox7.Visible =
                    eliminarTurnoButton.Visible = label5.Visible = nextTurnoButton.Visible =
                    backTurnoButton.Visible = label10.Visible = 
                    false;
                    indexShowed = -1;
                    break;
                case Tools.SIGUIENTETURNO:
                    backTurnoButton.Visible = label10.Visible = true;
                    break;
                case Tools.INCORRECTDAY:
                    label6.Visible = comboHorarios.Visible = sacarTurnoButton.Visible = false;
                    break;
                case Tools.RESETALL:
                    addTurnoButton.Visible = addTurnoButton.Enabled =
                    rmTurnoButton.Visible = rmTurnoButton.Enabled = true;

                    cancelAddButton.Visible = cancelRmButton.Visible = label1.Visible =
                    label2.Visible = label3.Visible = label4.Visible = label5.Visible =
                    label6.Visible = label7.Visible = label8.Visible = label9.Visible =
                    label10.Visible = label11.Visible = label12.Visible = label13.Visible =
                    label14.Visible = label15.Visible = label16.Visible = label17.Visible =
                    label18.Visible = comboProvincia.Visible = comboLocalidad.Visible =
                    comboSucursales.Visible = comboEspecialidades.Visible =
                    comboMedicos.Visible = fechaTurnoPicker.Visible = comboHorarios.Visible =
                    textBox1.Visible = textBox2.Visible = textBox3.Visible = textBox4.Visible =
                    textBox5.Visible = textBox6.Visible = textBox7.Visible =
                    sacarTurnoButton.Visible = eliminarTurnoButton.Visible =
                    nextTurnoButton.Visible = backTurnoButton.Visible = false;
                    break;
            }
            /*
                si la label18 está visible y no la hizo visible la selección del médico,
                es porque la hizo visible la falta de disponibilidad de alguna categoría,
                por lo que la ocultamos 
            */
            if(label18.Visible && (which != Tools.MEDICOSELECTED))
                label18.Visible = false;
        }

        private class HorarioComparer : IEqualityComparer<Horario> {
            public bool Equals(Horario x, Horario y) {
                return x.Hora == y.Hora;
            }

            public int GetHashCode(Horario obj) {
                return base.GetHashCode();
            }
        }

        private class DayComparer : IComparer<Dia> {
            public int Compare(Dia x, Dia y) {
                return x.DiaID.CompareTo(y.DiaID);
            }
        }

        private enum Tools {
            SACARNUEVOTURNO, SACARTURNO, ELIMINARTURNO, ELIMINARESTETURNO, CANCELARADD,
            CANCELARRM, SIGUIENTETURNO, ANTERIORTURNO,

            PROVINCIASELECTED, LOCALIDADSELECTED, SUCURSALSELECTED, ESPECIALIDADSELECTED,
            MEDICOSELECTED,FECHASELECTED,HORASELECTED,
            
            ONLYTURNOTODELETE, LASTTURNOTODELETE, FIRSTTURNOTODELETE, INCORRECTDAY, RESETALL
        }

        private void btnCerrar_Click(object sender, EventArgs e) {
            Application.Exit();
        }
    }
}
