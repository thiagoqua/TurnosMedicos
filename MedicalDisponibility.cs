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
        private List<Sucursal> tempSucursales;
        private DisponibilidadMedico tempDM;

        private MedicalHome previousState;
        public MedicalDisponibility(Medico medico,MedicalHome home) {
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
        }

        private void initSucursales() {
            int szSucursales;
            comboSucursales.Items.Clear();
            tempSucursales = (from suc in db.Sucursal
                              join ms in db.MedicoSucursal
                                  on suc.SucursalId equals ms.IDSucursal
                              where ms.IDMedico == whoAmI.MedicoID
                              select suc).ToList();

            szSucursales = tempSucursales.Count();
            string[] localidesText = getLocalidadesComplex(szSucursales);
            for(int i = 0; i < szSucursales; ++i) {
                comboSucursales.Items.Add(tempSucursales[i].SucursalDescripcion.Trim() +
                                          " en " + localidesText[i]);
            }
        }

        private void comboSucursales_SelectedIndexChanged(object sender, EventArgs e) {
            comboDias.ResetText(); abmInicio.ResetText(); abmFin.ResetText();
            abmConsultorio.ResetText();
            int index = comboSucursales.SelectedIndex;
            var queryDispoM = from dm in db.DisponibilidadMedico
                              where dm.IDMedico == whoAmI.MedicoID &&
                                    dm.IDSucursal == tempSucursales[index].SucursalId
                              select dm;
            var queryDias = from dia in db.Dia
                            join dm in queryDispoM 
                                on dia.DiaID equals dm.IDDia
                            select dia;

            comboDias.DisplayMember = "NombreDia";
            comboDias.ValueMember = "DiaId";
            comboDias.DataSource = queryDias;
            label3.Visible = comboDias.Visible = label3.Enabled = comboDias.Enabled = true;
        }
        private void comboDias_SelectedIndexChanged(object sender, EventArgs e) {
            tempDM = getDM();
            abmInicio.Text = tempDM.HorarioInicio.ToString();
            abmFin.Text = tempDM.HorarioFin.ToString();
            abmConsultorio.Text = tempDM.Consultorio.ToString();

            abmInicio.Visible = abmFin.Visible = abmConsultorio.Visible = label1.Visible =
            label4.Visible = label5.Visible = true;
        }

        private string[] getLocalidadesComplex(int size) {
            string[] localidadesComplex = new string[size];
            Localidad temp = new Localidad();
            for(int i = 0;i < size; ++i) {
                temp = (from loc in db.Localidad
                                         where loc.LocalidadId == tempSucursales[i].IDLocalidad
                                         select loc).FirstOrDefault();
                localidadesComplex[i] = temp.LocalidadDescripcion.Trim();
                var queryProvincias = from prov in db.Provincia
                                      where prov.ProvinciaId == temp.IDProvincia
                                      select prov.ProvinciaDescripcion;
                localidadesComplex[i] += ", " + queryProvincias.FirstOrDefault().Trim();
            }
            return localidadesComplex;
        }

        private DisponibilidadMedico getDM() {
            int selectedSucIndex;
            selectedSucIndex = comboSucursales.SelectedIndex;
            var queryDM = from dm in db.DisponibilidadMedico
                          where dm.IDMedico == whoAmI.MedicoID &&
                                dm.IDSucursal == tempSucursales[selectedSucIndex].SucursalId &&
                                dm.IDDia == (int) comboDias.SelectedValue
                          select dm;
            return queryDM.FirstOrDefault();
        }

        private void abmInicio_TextChanged(object sender, EventArgs e) {
            if(!makeABM1.Visible)
                makeABM1.Visible = true;
        }

        private void abmFin_TextChanged(object sender, EventArgs e) {
            if(!makeABM1.Visible)
                makeABM1.Visible = true;
        }

        private void abmConsultorio_TextChanged(object sender, EventArgs e) {
            if(!makeABM1.Visible)
                makeABM1.Visible = true;
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

        private bool setNewValues() {
            int hora, minutos, segundos;
            string[] stringTime;
            TimeSpan horaInicio, horaFin;

            stringTime = abmInicio.Text.Split(':');
            hora = Convert.ToInt32(stringTime[0]);
            minutos = Convert.ToInt32(stringTime[1]);
            if(stringTime.Length < 3)
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

            if(horaInicio > horaFin) {
                string msg, caption;
                caption = "La hora de finalización es inválida";
                msg = "Usted ha ingresado un rango horario que excede las 23:59 del día en cuestión. " +
                      "Para solucionar ésto, registre una nueva disponibilidad que abarque desde las " +
                      "00:00 del día siguiente, hasta las " + horaFin.ToString() + " del mismo día";
                MessageBox.Show(msg, caption, MessageBoxButtons.OK);
                return false;
            }
            tempDM.HorarioFin = horaFin;
            tempDM.HorarioInicio = horaInicio;
            tempDM.Consultorio = Convert.ToInt32(abmConsultorio.Text);

            return true;
        }

        private void abmDyS_Click_1(object sender, EventArgs e) {
            if(AddSuc.Visible){
                label2.Enabled = comboSucursales.Enabled = true;
                RmSuc.Visible = AddSuc.Visible = AddRmDays.Visible = false;
            }
            else {
                abmInicio.Visible = abmFin.Visible = abmConsultorio.Visible = label1.Visible =
                label4.Visible = label5.Visible = makeABM1.Visible = label2.Enabled =
                label3.Enabled = comboDias.Enabled = label3.Visible = comboDias.Visible = 
                comboSucursales.Enabled = false;

                RmSuc.Visible = AddSuc.Visible = AddRmDays.Visible = true;
                string[] localidesText = getLocalidadesComplex(tempSucursales.Count());
                for(int i = 0; i < localidesText.Length; ++i) {
                    comboSucursalesRemove.Items.Add(tempSucursales[i].SucursalDescripcion +
                                              " en " + localidesText[i]);
                }
            }
        }

        private void RmSuc_CheckedChanged(object sender, EventArgs e) {
            if(RmSuc.Checked) {
                label6.Visible = comboSucursalesRemove.Visible = true;
                AddSuc.Visible = abmDyS.Enabled = AddRmDays.Visible = false;
            }
            else {
                label6.Visible = comboSucursalesRemove.Visible = abmSuc.Visible = false;
                AddSuc.Visible = abmDyS.Enabled = AddRmDays.Visible = true;
            }
        }

        private void AddSuc_CheckedChanged(object sender, EventArgs e) {
            if(AddSuc.Checked) {
                label7.Visible = comboProvincia.Visible = true;
                RmSuc.Visible = abmDyS.Enabled = AddRmDays.Visible = false;
                var queryProvincia = from prov in db.Provincia
                                     select prov;
                comboProvincia.DisplayMember = "ProvinciaDescripcion";
                comboProvincia.ValueMember = "ProvinciaId";
                comboProvincia.DataSource = queryProvincia;
            }
            else {
                label7.Visible = label8.Visible = label9.Visible = comboProvincia.Visible =
                comboLocalidad.Visible = comboSucursalesAñadir.Visible = abmSuc1.Visible = false;
                RmSuc.Visible = abmDyS.Enabled = AddRmDays.Visible = true;
            }
        }

        private void abmSuc_Click(object sender, EventArgs e) {
            string msg, caption;
            int index = comboSucursalesRemove.SelectedIndex;
            var queryDM = from dm in db.DisponibilidadMedico
                          where dm.IDMedico == whoAmI.MedicoID &&
                                dm.IDSucursal == tempSucursales[index].SucursalId
                          select dm;
            var queryMS = from ms in db.MedicoSucursal
                          where ms.IDMedico == whoAmI.MedicoID &&
                                ms.IDSucursal == tempSucursales[index].SucursalId
                          select ms;
            db.DisponibilidadMedico.DeleteAllOnSubmit(queryDM);
            db.MedicoSucursal.DeleteOnSubmit(queryMS.First());
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
            int ProvinciaId = (int) comboProvincia.SelectedValue;
            comboLocalidad.ResetText(); comboSucursalesAñadir.ResetText();
            var queryLocalidad = from loc in db.Localidad
                                 where loc.IDProvincia == ProvinciaId
                                 select loc;
            comboLocalidad.DisplayMember = "LocalidadDescripcion";
            comboLocalidad.ValueMember = "LocalidadId";
            comboLocalidad.DataSource = queryLocalidad;
            comboLocalidad.Visible = label9.Visible =  true;
        }

        private void comboLocalidad_SelectedIndexChanged(object sender, EventArgs e) {
            int LocalidadId = (int) comboLocalidad.SelectedValue;
            comboSucursalesAñadir.ResetText();
            var querySucursal = (from suc in db.Sucursal
                                 where suc.IDLocalidad == LocalidadId
                                 select suc)
                                 .ToList()
                                 .Except(tempSucursales, new SucursalComparer())
                                 .ToList();
            comboSucursalesAñadir.DisplayMember = "SucursalDescripcion";
            comboSucursalesAñadir.ValueMember = "SucursalId";
            comboSucursalesAñadir.DataSource = (IList<Sucursal>) querySucursal;
            comboSucursalesAñadir.Visible = label8.Visible = true;
        }
        private void comboSucursalesAñadir_SelectedIndexChanged(object sender, EventArgs e) {
            abmSuc1.Visible = true;
        }

        private void AddRmDays_CheckedChanged(object sender, EventArgs e) {
            if(AddRmDays.Checked) {
                label10.Visible = comboSucModDias.Visible = true;
                RmSuc.Visible = abmDyS.Enabled = AddSuc.Visible = false;
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
                label10.Visible = comboSucModDias.Visible = addDay.Visible =
                rmDay.Visible = addDay.Checked = rmDay.Checked = label11.Visible = 
                comboDayToAdd.Visible = false;
                RmSuc.Visible = abmDyS.Enabled = AddSuc.Visible = true;
            }
        }

        private void comboSucModDias_SelectedIndexChanged(object sender, EventArgs e) {
            addDay.Visible = rmDay.Visible = true;
            addDay.Checked = rmDay.Checked = false;
        }

        private void addDay_CheckedChanged(object sender, EventArgs e) {
            if(addDay.Checked) {
                int SucursalId = (int) comboSucModDias.SelectedValue;
                rmDay.Visible = false;
                var queryExceptionalDays = from dia in db.Dia
                                           join dm in db.DisponibilidadMedico on
                                                dia.DiaID equals dm.IDDia
                                           where dm.IDMedico == whoAmI.MedicoID &&
                                                 dm.IDSucursal == SucursalId
                                           select dia;
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
                rmDay.Visible = true;
                label11.Visible = comboDayToAdd.Visible = abmDay.Visible = false;
            }
        }

        private void rmDay_CheckedChanged(object sender, EventArgs e) {
            if(rmDay.Checked) {
                addDay.Visible = abmDay.Visible = label11.Visible = comboDayToAdd.Visible =
                false;
                
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
                label13.Visible = comboDayToRm.Visible = true;
            }
            else {
                addDay.Visible = true;
                label13.Visible = comboDayToRm.Visible = abmDay1.Visible = false;
            }
        }

        private void dayToAdd_SelectedIndexChanged(object sender, EventArgs e) {
            abmDay.Visible = true;
        }

        private void abmDay_Click(object sender, EventArgs e) {
            string msg, caption;
            int SucursalId, DiaId;
            SucursalId = (int)comboSucModDias.SelectedValue;
            DiaId = (int)comboDayToAdd.SelectedValue;
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
            abmDay1.Visible = true;
        }

        private void abmDay1_Click(object sender, EventArgs e) {
            string msg, caption;
            int SucursalId, DiaId;
            SucursalId = (int)comboSucModDias.SelectedValue;
            DiaId = (int)comboDayToRm.SelectedValue;
            var dmsToRemove = from dm in db.DisponibilidadMedico
                              where dm.IDMedico == whoAmI.MedicoID &&
                                    dm.IDDia == DiaId &&
                                    dm.IDSucursal == SucursalId
                              select dm;
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

        private void resetEverything() {
            comboSucursales.ResetText();
            abmConsultorio.ResetText();
            abmInicio.ResetText();
            abmFin.ResetText();
            label1.Visible = label3.Visible = label4.Visible = label5.Visible =
            comboDias.Visible = abmInicio.Visible = abmFin.Visible = abmConsultorio.Visible = 
            makeABM1.Visible = 
            AddSuc.Visible = RmSuc.Visible = AddRmDays.Visible = 
            AddSuc.Checked = RmSuc.Checked = AddRmDays.Checked            
            = false;
            initSucursales();
        }

        private void back_Click(object sender, EventArgs e) {
            previousState.Show();
            this.Close();
        }
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
