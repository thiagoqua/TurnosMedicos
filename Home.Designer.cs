
namespace AppEscritorio {
    partial class Home {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.PanelMenu = new System.Windows.Forms.Panel();
            this.logo = new System.Windows.Forms.PictureBox();
            this.VerTurnos = new System.Windows.Forms.Button();
            this.SolicitarTurno = new System.Windows.Forms.Button();
            this.OtrasOpciones = new System.Windows.Forms.Button();
            this.PanelContenedor = new System.Windows.Forms.Panel();
            this.textBoxTurno1 = new System.Windows.Forms.TextBox();
            this.textBoxTurno0 = new System.Windows.Forms.TextBox();
            this.generatePDF = new System.Windows.Forms.Button();
            this.BarraTitulo = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.PanelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.PanelContenedor.SuspendLayout();
            this.BarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelMenu
            // 
            this.PanelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.PanelMenu.Controls.Add(this.logo);
            this.PanelMenu.Controls.Add(this.VerTurnos);
            this.PanelMenu.Controls.Add(this.SolicitarTurno);
            this.PanelMenu.Controls.Add(this.OtrasOpciones);
            this.PanelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelMenu.Location = new System.Drawing.Point(0, 0);
            this.PanelMenu.Name = "PanelMenu";
            this.PanelMenu.Size = new System.Drawing.Size(200, 511);
            this.PanelMenu.TabIndex = 8;
            // 
            // logo
            // 
            this.logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("logo.BackgroundImage")));
            this.logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.logo.Location = new System.Drawing.Point(25, 27);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(157, 101);
            this.logo.TabIndex = 4;
            this.logo.TabStop = false;
            this.logo.Click += new System.EventHandler(this.logo_Click);
            // 
            // VerTurnos
            // 
            this.VerTurnos.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.VerTurnos.Location = new System.Drawing.Point(25, 171);
            this.VerTurnos.Name = "VerTurnos";
            this.VerTurnos.Size = new System.Drawing.Size(95, 23);
            this.VerTurnos.TabIndex = 2;
            this.VerTurnos.Text = "Ver mis turnos";
            this.VerTurnos.UseVisualStyleBackColor = false;
            this.VerTurnos.Click += new System.EventHandler(this.VerTurnos_Click);
            // 
            // SolicitarTurno
            // 
            this.SolicitarTurno.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SolicitarTurno.Location = new System.Drawing.Point(25, 248);
            this.SolicitarTurno.Name = "SolicitarTurno";
            this.SolicitarTurno.Size = new System.Drawing.Size(156, 23);
            this.SolicitarTurno.TabIndex = 1;
            this.SolicitarTurno.Text = "Solicitar/Eliminar Turnos";
            this.SolicitarTurno.UseVisualStyleBackColor = false;
            this.SolicitarTurno.Click += new System.EventHandler(this.SolicitarTurno_Click);
            // 
            // OtrasOpciones
            // 
            this.OtrasOpciones.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.OtrasOpciones.Location = new System.Drawing.Point(22, 328);
            this.OtrasOpciones.Name = "OtrasOpciones";
            this.OtrasOpciones.Size = new System.Drawing.Size(159, 23);
            this.OtrasOpciones.TabIndex = 3;
            this.OtrasOpciones.Text = "Otras opciones de Contacto";
            this.OtrasOpciones.UseVisualStyleBackColor = false;
            this.OtrasOpciones.Click += new System.EventHandler(this.OtrasOpciones_Click);
            // 
            // PanelContenedor
            // 
            this.PanelContenedor.Controls.Add(this.textBoxTurno1);
            this.PanelContenedor.Controls.Add(this.textBoxTurno0);
            this.PanelContenedor.Controls.Add(this.generatePDF);
            this.PanelContenedor.Location = new System.Drawing.Point(200, 37);
            this.PanelContenedor.Name = "PanelContenedor";
            this.PanelContenedor.Size = new System.Drawing.Size(764, 474);
            this.PanelContenedor.TabIndex = 9;
            // 
            // textBoxTurno1
            // 
            this.textBoxTurno1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBoxTurno1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTurno1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTurno1.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.textBoxTurno1.Location = new System.Drawing.Point(447, 99);
            this.textBoxTurno1.Multiline = true;
            this.textBoxTurno1.Name = "textBoxTurno1";
            this.textBoxTurno1.Size = new System.Drawing.Size(291, 108);
            this.textBoxTurno1.TabIndex = 21;
            this.textBoxTurno1.Visible = false;
            // 
            // textBoxTurno0
            // 
            this.textBoxTurno0.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBoxTurno0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTurno0.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTurno0.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBoxTurno0.Location = new System.Drawing.Point(46, 99);
            this.textBoxTurno0.Multiline = true;
            this.textBoxTurno0.Name = "textBoxTurno0";
            this.textBoxTurno0.Size = new System.Drawing.Size(291, 108);
            this.textBoxTurno0.TabIndex = 20;
            this.textBoxTurno0.Visible = false;
            // 
            // generatePDF
            // 
            this.generatePDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(171)))), ((int)(((byte)(237)))));
            this.generatePDF.FlatAppearance.BorderSize = 0;
            this.generatePDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generatePDF.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generatePDF.Image = global::AppEscritorio.Properties.Resources.PDF_IMAGE_L;
            this.generatePDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.generatePDF.Location = new System.Drawing.Point(268, 20);
            this.generatePDF.Name = "generatePDF";
            this.generatePDF.Size = new System.Drawing.Size(269, 53);
            this.generatePDF.TabIndex = 19;
            this.generatePDF.Text = "Generar Reporte";
            this.generatePDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.generatePDF.UseVisualStyleBackColor = false;
            this.generatePDF.Visible = false;
            this.generatePDF.Click += new System.EventHandler(this.generatePDF_Click);
            // 
            // BarraTitulo
            // 
            this.BarraTitulo.BackColor = System.Drawing.Color.Navy;
            this.BarraTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BarraTitulo.Controls.Add(this.btnCerrar);
            this.BarraTitulo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.BarraTitulo.Location = new System.Drawing.Point(0, 0);
            this.BarraTitulo.Name = "BarraTitulo";
            this.BarraTitulo.Size = new System.Drawing.Size(984, 37);
            this.BarraTitulo.TabIndex = 7;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCerrar.BackgroundImage")));
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCerrar.Location = new System.Drawing.Point(936, 7);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 25);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(984, 511);
            this.Controls.Add(this.PanelMenu);
            this.Controls.Add(this.PanelContenedor);
            this.Controls.Add(this.BarraTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.PanelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.PanelContenedor.ResumeLayout(false);
            this.PanelContenedor.PerformLayout();
            this.BarraTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Panel PanelMenu;
        private System.Windows.Forms.Button VerTurnos;
        private System.Windows.Forms.Button SolicitarTurno;
        private System.Windows.Forms.Button OtrasOpciones;
        private System.Windows.Forms.Panel PanelContenedor;
        private System.Windows.Forms.Button generatePDF;
        private System.Windows.Forms.Panel BarraTitulo;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.TextBox textBoxTurno1;
        private System.Windows.Forms.TextBox textBoxTurno0;
    }
}