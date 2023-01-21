
namespace AppEscritorio {
    partial class MedicalDisponibility {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MedicalDisponibility));
            this.BarraTitulo = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.PanelMenu = new System.Windows.Forms.Panel();
            this.back = new System.Windows.Forms.Button();
            this.logo = new System.Windows.Forms.PictureBox();
            this.comboSucursales = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboDias = new System.Windows.Forms.ComboBox();
            this.abmDyS = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.abmInicio = new System.Windows.Forms.TextBox();
            this.abmFin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.abmConsultorio = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.makeABM1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboSucursalesRemove = new System.Windows.Forms.ComboBox();
            this.abmSuc = new System.Windows.Forms.Button();
            this.RmSuc = new System.Windows.Forms.CheckBox();
            this.AddSuc = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.abmSuc1 = new System.Windows.Forms.Button();
            this.comboProvincia = new System.Windows.Forms.ComboBox();
            this.comboSucursalesAñadir = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboLocalidad = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.AddRmDays = new System.Windows.Forms.CheckBox();
            this.comboSucModDias = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.addDay = new System.Windows.Forms.CheckBox();
            this.rmDay = new System.Windows.Forms.CheckBox();
            this.comboDayToAdd = new System.Windows.Forms.ComboBox();
            this.abmDay = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.comboDayToRm = new System.Windows.Forms.ComboBox();
            this.abmDay1 = new System.Windows.Forms.Button();
            this.BarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.PanelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // BarraTitulo
            // 
            this.BarraTitulo.BackColor = System.Drawing.Color.Navy;
            this.BarraTitulo.Controls.Add(this.btnCerrar);
            this.BarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraTitulo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.BarraTitulo.Location = new System.Drawing.Point(200, 0);
            this.BarraTitulo.Name = "BarraTitulo";
            this.BarraTitulo.Size = new System.Drawing.Size(801, 37);
            this.BarraTitulo.TabIndex = 9;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCerrar.BackgroundImage")));
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCerrar.Location = new System.Drawing.Point(763, 6);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 25);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.TabStop = false;
            // 
            // PanelMenu
            // 
            this.PanelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.PanelMenu.Controls.Add(this.back);
            this.PanelMenu.Controls.Add(this.logo);
            this.PanelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelMenu.Location = new System.Drawing.Point(0, 0);
            this.PanelMenu.Name = "PanelMenu";
            this.PanelMenu.Size = new System.Drawing.Size(200, 463);
            this.PanelMenu.TabIndex = 10;
            // 
            // back
            // 
            this.back.Font = new System.Drawing.Font("Microsoft Tai Le", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.back.Location = new System.Drawing.Point(22, 209);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(157, 84);
            this.back.TabIndex = 7;
            this.back.Text = "Volver";
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // logo
            // 
            this.logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("logo.BackgroundImage")));
            this.logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.logo.Location = new System.Drawing.Point(35, 28);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(157, 101);
            this.logo.TabIndex = 4;
            this.logo.TabStop = false;
            // 
            // comboSucursales
            // 
            this.comboSucursales.FormattingEnabled = true;
            this.comboSucursales.Location = new System.Drawing.Point(675, 76);
            this.comboSucursales.Name = "comboSucursales";
            this.comboSucursales.Size = new System.Drawing.Size(290, 21);
            this.comboSucursales.TabIndex = 11;
            this.comboSucursales.SelectedIndexChanged += new System.EventHandler(this.comboSucursales_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(557, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Seleccione Sucursal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(557, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Seleccione Día";
            this.label3.Visible = false;
            // 
            // comboDias
            // 
            this.comboDias.FormattingEnabled = true;
            this.comboDias.Location = new System.Drawing.Point(675, 127);
            this.comboDias.Name = "comboDias";
            this.comboDias.Size = new System.Drawing.Size(290, 21);
            this.comboDias.TabIndex = 14;
            this.comboDias.Visible = false;
            this.comboDias.SelectedIndexChanged += new System.EventHandler(this.comboDias_SelectedIndexChanged);
            // 
            // abmDyS
            // 
            this.abmDyS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(171)))), ((int)(((byte)(237)))));
            this.abmDyS.FlatAppearance.BorderSize = 0;
            this.abmDyS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.abmDyS.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.abmDyS.Location = new System.Drawing.Point(220, 76);
            this.abmDyS.Name = "abmDyS";
            this.abmDyS.Size = new System.Drawing.Size(309, 69);
            this.abmDyS.TabIndex = 17;
            this.abmDyS.Text = "Modificar sucursales y días";
            this.abmDyS.UseVisualStyleBackColor = false;
            this.abmDyS.Click += new System.EventHandler(this.abmDyS_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(254, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Horario inicio";
            this.label1.Visible = false;
            // 
            // abmInicio
            // 
            this.abmInicio.Location = new System.Drawing.Point(391, 171);
            this.abmInicio.Name = "abmInicio";
            this.abmInicio.Size = new System.Drawing.Size(100, 20);
            this.abmInicio.TabIndex = 19;
            this.abmInicio.Visible = false;
            this.abmInicio.TextChanged += new System.EventHandler(this.abmInicio_TextChanged);
            // 
            // abmFin
            // 
            this.abmFin.Location = new System.Drawing.Point(391, 216);
            this.abmFin.Name = "abmFin";
            this.abmFin.Size = new System.Drawing.Size(100, 20);
            this.abmFin.TabIndex = 21;
            this.abmFin.Visible = false;
            this.abmFin.TextChanged += new System.EventHandler(this.abmFin_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(254, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Horario finalización";
            this.label4.Visible = false;
            // 
            // abmConsultorio
            // 
            this.abmConsultorio.Location = new System.Drawing.Point(798, 174);
            this.abmConsultorio.Name = "abmConsultorio";
            this.abmConsultorio.Size = new System.Drawing.Size(100, 20);
            this.abmConsultorio.TabIndex = 23;
            this.abmConsultorio.Visible = false;
            this.abmConsultorio.TextChanged += new System.EventHandler(this.abmConsultorio_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(713, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Consultorio";
            this.label5.Visible = false;
            // 
            // makeABM1
            // 
            this.makeABM1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.makeABM1.FlatAppearance.BorderSize = 0;
            this.makeABM1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.makeABM1.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.makeABM1.Location = new System.Drawing.Point(716, 209);
            this.makeABM1.Name = "makeABM1";
            this.makeABM1.Size = new System.Drawing.Size(182, 38);
            this.makeABM1.TabIndex = 24;
            this.makeABM1.Text = "Guardar cambios";
            this.makeABM1.UseVisualStyleBackColor = false;
            this.makeABM1.Visible = false;
            this.makeABM1.Click += new System.EventHandler(this.makeABM1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(206, 311);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Seleccione";
            this.label6.Visible = false;
            // 
            // comboSucursalesRemove
            // 
            this.comboSucursalesRemove.FormattingEnabled = true;
            this.comboSucursalesRemove.Location = new System.Drawing.Point(272, 308);
            this.comboSucursalesRemove.Name = "comboSucursalesRemove";
            this.comboSucursalesRemove.Size = new System.Drawing.Size(117, 21);
            this.comboSucursalesRemove.TabIndex = 26;
            this.comboSucursalesRemove.Visible = false;
            this.comboSucursalesRemove.SelectedIndexChanged += new System.EventHandler(this.comboSucursalesRemove_SelectedIndexChanged);
            // 
            // abmSuc
            // 
            this.abmSuc.BackColor = System.Drawing.Color.Red;
            this.abmSuc.FlatAppearance.BorderSize = 0;
            this.abmSuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.abmSuc.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.abmSuc.Location = new System.Drawing.Point(209, 345);
            this.abmSuc.Name = "abmSuc";
            this.abmSuc.Size = new System.Drawing.Size(180, 45);
            this.abmSuc.TabIndex = 28;
            this.abmSuc.Text = "Eliminar";
            this.abmSuc.UseVisualStyleBackColor = false;
            this.abmSuc.Visible = false;
            this.abmSuc.Click += new System.EventHandler(this.abmSuc_Click);
            // 
            // RmSuc
            // 
            this.RmSuc.AutoSize = true;
            this.RmSuc.Location = new System.Drawing.Point(256, 280);
            this.RmSuc.Name = "RmSuc";
            this.RmSuc.Size = new System.Drawing.Size(106, 17);
            this.RmSuc.TabIndex = 29;
            this.RmSuc.Text = "Eliminar Sucursal";
            this.RmSuc.UseVisualStyleBackColor = true;
            this.RmSuc.Visible = false;
            this.RmSuc.CheckedChanged += new System.EventHandler(this.RmSuc_CheckedChanged);
            // 
            // AddSuc
            // 
            this.AddSuc.AutoSize = true;
            this.AddSuc.Location = new System.Drawing.Point(491, 280);
            this.AddSuc.Name = "AddSuc";
            this.AddSuc.Size = new System.Drawing.Size(107, 17);
            this.AddSuc.TabIndex = 30;
            this.AddSuc.Text = "Agregar Sucursal";
            this.AddSuc.UseVisualStyleBackColor = true;
            this.AddSuc.Visible = false;
            this.AddSuc.CheckedChanged += new System.EventHandler(this.AddSuc_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(440, 309);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Seleccione provincia";
            this.label7.Visible = false;
            // 
            // abmSuc1
            // 
            this.abmSuc1.BackColor = System.Drawing.Color.Lime;
            this.abmSuc1.FlatAppearance.BorderSize = 0;
            this.abmSuc1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.abmSuc1.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.abmSuc1.Location = new System.Drawing.Point(443, 393);
            this.abmSuc1.Name = "abmSuc1";
            this.abmSuc1.Size = new System.Drawing.Size(232, 45);
            this.abmSuc1.TabIndex = 37;
            this.abmSuc1.Text = "Agregar";
            this.abmSuc1.UseVisualStyleBackColor = false;
            this.abmSuc1.Visible = false;
            this.abmSuc1.Click += new System.EventHandler(this.abmSuc1_Click);
            // 
            // comboProvincia
            // 
            this.comboProvincia.FormattingEnabled = true;
            this.comboProvincia.Location = new System.Drawing.Point(558, 306);
            this.comboProvincia.Name = "comboProvincia";
            this.comboProvincia.Size = new System.Drawing.Size(117, 21);
            this.comboProvincia.TabIndex = 38;
            this.comboProvincia.Visible = false;
            this.comboProvincia.SelectedIndexChanged += new System.EventHandler(this.comboProvincia_SelectedIndexChanged);
            // 
            // comboSucursalesAñadir
            // 
            this.comboSucursalesAñadir.FormattingEnabled = true;
            this.comboSucursalesAñadir.Location = new System.Drawing.Point(558, 362);
            this.comboSucursalesAñadir.Name = "comboSucursalesAñadir";
            this.comboSucursalesAñadir.Size = new System.Drawing.Size(117, 21);
            this.comboSucursalesAñadir.TabIndex = 40;
            this.comboSucursalesAñadir.Visible = false;
            this.comboSucursalesAñadir.SelectedIndexChanged += new System.EventHandler(this.comboSucursalesAñadir_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(440, 365);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Seleccione sucursal";
            this.label8.Visible = false;
            // 
            // comboLocalidad
            // 
            this.comboLocalidad.FormattingEnabled = true;
            this.comboLocalidad.Location = new System.Drawing.Point(558, 333);
            this.comboLocalidad.Name = "comboLocalidad";
            this.comboLocalidad.Size = new System.Drawing.Size(117, 21);
            this.comboLocalidad.TabIndex = 42;
            this.comboLocalidad.Visible = false;
            this.comboLocalidad.SelectedIndexChanged += new System.EventHandler(this.comboLocalidad_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(440, 336);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 13);
            this.label9.TabIndex = 41;
            this.label9.Text = "Seleccione localidad";
            this.label9.Visible = false;
            // 
            // AddRmDays
            // 
            this.AddRmDays.AutoSize = true;
            this.AddRmDays.Location = new System.Drawing.Point(798, 280);
            this.AddRmDays.Name = "AddRmDays";
            this.AddRmDays.Size = new System.Drawing.Size(130, 17);
            this.AddRmDays.TabIndex = 43;
            this.AddRmDays.Text = "Agregar/ eliminar días";
            this.AddRmDays.UseVisualStyleBackColor = true;
            this.AddRmDays.Visible = false;
            this.AddRmDays.CheckedChanged += new System.EventHandler(this.AddRmDays_CheckedChanged);
            // 
            // comboSucModDias
            // 
            this.comboSucModDias.FormattingEnabled = true;
            this.comboSucModDias.Location = new System.Drawing.Point(869, 306);
            this.comboSucModDias.Name = "comboSucModDias";
            this.comboSucModDias.Size = new System.Drawing.Size(117, 21);
            this.comboSucModDias.TabIndex = 45;
            this.comboSucModDias.Visible = false;
            this.comboSucModDias.SelectedIndexChanged += new System.EventHandler(this.comboSucModDias_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(735, 308);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 13);
            this.label10.TabIndex = 44;
            this.label10.Text = "Seleccione sucursal";
            this.label10.Visible = false;
            // 
            // addDay
            // 
            this.addDay.AutoSize = true;
            this.addDay.Location = new System.Drawing.Point(738, 336);
            this.addDay.Name = "addDay";
            this.addDay.Size = new System.Drawing.Size(87, 17);
            this.addDay.TabIndex = 46;
            this.addDay.Text = "Agregar días";
            this.addDay.UseVisualStyleBackColor = true;
            this.addDay.Visible = false;
            this.addDay.CheckedChanged += new System.EventHandler(this.addDay_CheckedChanged);
            // 
            // rmDay
            // 
            this.rmDay.AutoSize = true;
            this.rmDay.Location = new System.Drawing.Point(889, 335);
            this.rmDay.Name = "rmDay";
            this.rmDay.Size = new System.Drawing.Size(86, 17);
            this.rmDay.TabIndex = 47;
            this.rmDay.Text = "Eliminar días";
            this.rmDay.UseVisualStyleBackColor = true;
            this.rmDay.Visible = false;
            this.rmDay.CheckedChanged += new System.EventHandler(this.rmDay_CheckedChanged);
            // 
            // comboDayToAdd
            // 
            this.comboDayToAdd.FormattingEnabled = true;
            this.comboDayToAdd.Location = new System.Drawing.Point(738, 381);
            this.comboDayToAdd.Name = "comboDayToAdd";
            this.comboDayToAdd.Size = new System.Drawing.Size(81, 21);
            this.comboDayToAdd.TabIndex = 48;
            this.comboDayToAdd.Visible = false;
            this.comboDayToAdd.SelectedIndexChanged += new System.EventHandler(this.dayToAdd_SelectedIndexChanged);
            // 
            // abmDay
            // 
            this.abmDay.BackColor = System.Drawing.Color.Lime;
            this.abmDay.FlatAppearance.BorderSize = 0;
            this.abmDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.abmDay.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.abmDay.Location = new System.Drawing.Point(718, 410);
            this.abmDay.Name = "abmDay";
            this.abmDay.Size = new System.Drawing.Size(132, 41);
            this.abmDay.TabIndex = 49;
            this.abmDay.Text = "Agregar";
            this.abmDay.UseVisualStyleBackColor = false;
            this.abmDay.Visible = false;
            this.abmDay.Click += new System.EventHandler(this.abmDay_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(715, 361);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(127, 13);
            this.label11.TabIndex = 50;
            this.label11.Text = "Seleccione día a agregar";
            this.label11.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Calibri", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label12.Location = new System.Drawing.Point(619, 38);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(319, 37);
            this.label12.TabIndex = 51;
            this.label12.Text = "Modificar disponibilidad";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(865, 361);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(117, 13);
            this.label13.TabIndex = 53;
            this.label13.Text = "Seleccione día a quitar";
            this.label13.Visible = false;
            // 
            // comboDayToRm
            // 
            this.comboDayToRm.FormattingEnabled = true;
            this.comboDayToRm.Location = new System.Drawing.Point(887, 382);
            this.comboDayToRm.Name = "comboDayToRm";
            this.comboDayToRm.Size = new System.Drawing.Size(81, 21);
            this.comboDayToRm.TabIndex = 52;
            this.comboDayToRm.Visible = false;
            this.comboDayToRm.SelectedIndexChanged += new System.EventHandler(this.comboDayToRm_SelectedIndexChanged);
            // 
            // abmDay1
            // 
            this.abmDay1.BackColor = System.Drawing.Color.Red;
            this.abmDay1.FlatAppearance.BorderSize = 0;
            this.abmDay1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.abmDay1.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            this.abmDay1.Location = new System.Drawing.Point(868, 410);
            this.abmDay1.Name = "abmDay1";
            this.abmDay1.Size = new System.Drawing.Size(132, 41);
            this.abmDay1.TabIndex = 54;
            this.abmDay1.Text = "Eliminar";
            this.abmDay1.UseVisualStyleBackColor = false;
            this.abmDay1.Visible = false;
            this.abmDay1.Click += new System.EventHandler(this.abmDay1_Click);
            // 
            // MedicalDisponibility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 463);
            this.Controls.Add(this.abmDay1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.comboDayToRm);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.abmDay);
            this.Controls.Add(this.comboDayToAdd);
            this.Controls.Add(this.rmDay);
            this.Controls.Add(this.addDay);
            this.Controls.Add(this.comboSucModDias);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.AddRmDays);
            this.Controls.Add(this.comboLocalidad);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboSucursalesAñadir);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboProvincia);
            this.Controls.Add(this.abmSuc1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.AddSuc);
            this.Controls.Add(this.RmSuc);
            this.Controls.Add(this.abmSuc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboSucursalesRemove);
            this.Controls.Add(this.makeABM1);
            this.Controls.Add(this.abmConsultorio);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.abmFin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.abmInicio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.abmDyS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboDias);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboSucursales);
            this.Controls.Add(this.BarraTitulo);
            this.Controls.Add(this.PanelMenu);
            this.Name = "MedicalDisponibility";
            this.Text = "MedicalDisponibility";
            this.BarraTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.PanelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel BarraTitulo;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.Panel PanelMenu;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.ComboBox comboSucursales;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboDias;
        private System.Windows.Forms.Button abmDyS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox abmInicio;
        private System.Windows.Forms.TextBox abmFin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox abmConsultorio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button makeABM1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboSucursalesRemove;
        private System.Windows.Forms.Button abmSuc;
        private System.Windows.Forms.CheckBox RmSuc;
        private System.Windows.Forms.CheckBox AddSuc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button abmSuc1;
        private System.Windows.Forms.ComboBox comboProvincia;
        private System.Windows.Forms.ComboBox comboSucursalesAñadir;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboLocalidad;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox AddRmDays;
        private System.Windows.Forms.ComboBox comboSucModDias;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox addDay;
        private System.Windows.Forms.CheckBox rmDay;
        private System.Windows.Forms.ComboBox comboDayToAdd;
        private System.Windows.Forms.Button abmDay;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboDayToRm;
        private System.Windows.Forms.Button abmDay1;
        private System.Windows.Forms.Button back;
    }
}