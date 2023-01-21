
namespace AppEscritorio {
    partial class MedicalTurnos {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MedicalTurnos));
            this.BarraTitulo = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.logo = new System.Windows.Forms.PictureBox();
            this.PanelMenu = new System.Windows.Forms.Panel();
            this.back = new System.Windows.Forms.Button();
            this.fecha = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTurno1 = new System.Windows.Forms.TextBox();
            this.textBoxTurno2 = new System.Windows.Forms.TextBox();
            this.generatePDF = new System.Windows.Forms.Button();
            this.BarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.PanelMenu.SuspendLayout();
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
            this.BarraTitulo.Size = new System.Drawing.Size(819, 37);
            this.BarraTitulo.TabIndex = 9;
            this.BarraTitulo.Paint += new System.Windows.Forms.PaintEventHandler(this.BarraTitulo_Paint);
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
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click_1);
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
            // PanelMenu
            // 
            this.PanelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.PanelMenu.Controls.Add(this.back);
            this.PanelMenu.Controls.Add(this.logo);
            this.PanelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelMenu.Location = new System.Drawing.Point(0, 0);
            this.PanelMenu.Name = "PanelMenu";
            this.PanelMenu.Size = new System.Drawing.Size(200, 491);
            this.PanelMenu.TabIndex = 10;
            // 
            // back
            // 
            this.back.Font = new System.Drawing.Font("Microsoft Tai Le", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.back.Location = new System.Drawing.Point(24, 218);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(157, 84);
            this.back.TabIndex = 6;
            this.back.Text = "Volver";
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // fecha
            // 
            this.fecha.CalendarForeColor = System.Drawing.Color.Crimson;
            this.fecha.CalendarMonthBackground = System.Drawing.SystemColors.HotTrack;
            this.fecha.CalendarTitleForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.fecha.Location = new System.Drawing.Point(434, 56);
            this.fecha.MinDate = new System.DateTime(2022, 10, 13, 0, 0, 0, 0);
            this.fecha.Name = "fecha";
            this.fecha.Size = new System.Drawing.Size(285, 20);
            this.fecha.TabIndex = 11;
            this.fecha.ValueChanged += new System.EventHandler(this.fecha_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(248, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 24);
            this.label2.TabIndex = 14;
            this.label2.Text = "Seleccione Fecha";
            // 
            // textBoxTurno1
            // 
            this.textBoxTurno1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBoxTurno1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTurno1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTurno1.Location = new System.Drawing.Point(236, 107);
            this.textBoxTurno1.Multiline = true;
            this.textBoxTurno1.Name = "textBoxTurno1";
            this.textBoxTurno1.Size = new System.Drawing.Size(291, 82);
            this.textBoxTurno1.TabIndex = 15;
            this.textBoxTurno1.Visible = false;
            // 
            // textBoxTurno2
            // 
            this.textBoxTurno2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBoxTurno2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTurno2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTurno2.Location = new System.Drawing.Point(658, 107);
            this.textBoxTurno2.Multiline = true;
            this.textBoxTurno2.Name = "textBoxTurno2";
            this.textBoxTurno2.Size = new System.Drawing.Size(291, 82);
            this.textBoxTurno2.TabIndex = 16;
            this.textBoxTurno2.Visible = false;
            // 
            // generatePDF
            // 
            this.generatePDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(171)))), ((int)(((byte)(237)))));
            this.generatePDF.FlatAppearance.BorderSize = 0;
            this.generatePDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generatePDF.Font = new System.Drawing.Font("Calibri", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generatePDF.Image = global::AppEscritorio.Properties.Resources.PDF_IMAGE_L;
            this.generatePDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.generatePDF.Location = new System.Drawing.Point(745, 42);
            this.generatePDF.Name = "generatePDF";
            this.generatePDF.Size = new System.Drawing.Size(243, 53);
            this.generatePDF.TabIndex = 18;
            this.generatePDF.Text = "Generar Reporte";
            this.generatePDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.generatePDF.UseVisualStyleBackColor = false;
            this.generatePDF.Visible = false;
            this.generatePDF.Click += new System.EventHandler(this.generatePDF_Click);
            // 
            // MedicalTurnos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1019, 491);
            this.Controls.Add(this.generatePDF);
            this.Controls.Add(this.textBoxTurno2);
            this.Controls.Add(this.textBoxTurno1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fecha);
            this.Controls.Add(this.BarraTitulo);
            this.Controls.Add(this.PanelMenu);
            this.Name = "MedicalTurnos";
            this.Text = "MedicalTurnos";
            this.Load += new System.EventHandler(this.MedicalTurnos_Load);
            this.BarraTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.PanelMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel BarraTitulo;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Panel PanelMenu;
        private System.Windows.Forms.DateTimePicker fecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTurno1;
        private System.Windows.Forms.TextBox textBoxTurno2;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button generatePDF;
    }
}