namespace AppEscritorio
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.SolicitarTurno = new System.Windows.Forms.Button();
            this.VerTurnos = new System.Windows.Forms.Button();
            this.OtrasOpciones = new System.Windows.Forms.Button();
            this.BarraTitulo = new System.Windows.Forms.Panel();
            this.PanelMenu = new System.Windows.Forms.Panel();
            this.PanelContenedor = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.logo = new System.Windows.Forms.PictureBox();
            this.BarraTitulo.SuspendLayout();
            this.PanelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // SolicitarTurno
            // 
            this.SolicitarTurno.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SolicitarTurno.Location = new System.Drawing.Point(37, 203);
            this.SolicitarTurno.Name = "SolicitarTurno";
            this.SolicitarTurno.Size = new System.Drawing.Size(107, 23);
            this.SolicitarTurno.TabIndex = 1;
            this.SolicitarTurno.Text = "Solicitar Turnos";
            this.SolicitarTurno.UseVisualStyleBackColor = false;
            this.SolicitarTurno.Click += new System.EventHandler(this.SolicitarTurno_Click);
            // 
            // VerTurnos
            // 
            this.VerTurnos.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.VerTurnos.Location = new System.Drawing.Point(37, 159);
            this.VerTurnos.Name = "VerTurnos";
            this.VerTurnos.Size = new System.Drawing.Size(75, 23);
            this.VerTurnos.TabIndex = 2;
            this.VerTurnos.Text = "Turnos";
            this.VerTurnos.UseVisualStyleBackColor = false;
            this.VerTurnos.Click += new System.EventHandler(this.VerTurnos_Click);
            // 
            // OtrasOpciones
            // 
            this.OtrasOpciones.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.OtrasOpciones.Location = new System.Drawing.Point(35, 243);
            this.OtrasOpciones.Name = "OtrasOpciones";
            this.OtrasOpciones.Size = new System.Drawing.Size(159, 23);
            this.OtrasOpciones.TabIndex = 3;
            this.OtrasOpciones.Text = "Otras opciones de Contacto";
            this.OtrasOpciones.UseVisualStyleBackColor = false;
            this.OtrasOpciones.Click += new System.EventHandler(this.OtrasOpciones_Click);
            // 
            // BarraTitulo
            // 
            this.BarraTitulo.BackColor = System.Drawing.Color.Navy;
            this.BarraTitulo.Controls.Add(this.btnCerrar);
            this.BarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraTitulo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.BarraTitulo.Location = new System.Drawing.Point(0, 0);
            this.BarraTitulo.Name = "BarraTitulo";
            this.BarraTitulo.Size = new System.Drawing.Size(800, 37);
            this.BarraTitulo.TabIndex = 4;
            this.BarraTitulo.Paint += new System.Windows.Forms.PaintEventHandler(this.BarraTitulo_Paint);
            // 
            // PanelMenu
            // 
            this.PanelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.PanelMenu.Controls.Add(this.logo);
            this.PanelMenu.Controls.Add(this.VerTurnos);
            this.PanelMenu.Controls.Add(this.SolicitarTurno);
            this.PanelMenu.Controls.Add(this.OtrasOpciones);
            this.PanelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelMenu.Location = new System.Drawing.Point(0, 37);
            this.PanelMenu.Name = "PanelMenu";
            this.PanelMenu.Size = new System.Drawing.Size(200, 413);
            this.PanelMenu.TabIndex = 5;
            // 
            // PanelContenedor
            // 
            this.PanelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContenedor.Location = new System.Drawing.Point(200, 37);
            this.PanelContenedor.Name = "PanelContenedor";
            this.PanelContenedor.Size = new System.Drawing.Size(600, 413);
            this.PanelContenedor.TabIndex = 6;
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
            this.btnCerrar.Click += new System.EventHandler(this.pictureBox1_Click);
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
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PanelContenedor);
            this.Controls.Add(this.PanelMenu);
            this.Controls.Add(this.BarraTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Home";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            this.BarraTitulo.ResumeLayout(false);
            this.PanelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SolicitarTurno;
        private System.Windows.Forms.Button VerTurnos;
        private System.Windows.Forms.Button OtrasOpciones;
        private System.Windows.Forms.Panel BarraTitulo;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.Panel PanelMenu;
        private System.Windows.Forms.Panel PanelContenedor;
        private System.Windows.Forms.PictureBox logo;
    }
}