namespace AppEscritorio
{
    partial class Turnos
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Turnos));
            this.PanelContenedor = new System.Windows.Forms.Panel();
            this.turnoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.aplicacionDataSet = new AppEscritorio.AplicacionDataSet();
            this.PanelMenu = new System.Windows.Forms.Panel();
            this.logo = new System.Windows.Forms.PictureBox();
            this.VerTurnos = new System.Windows.Forms.Button();
            this.SolicitarTurno = new System.Windows.Forms.Button();
            this.OtrasOpciones = new System.Windows.Forms.Button();
            this.BarraTitulo = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.turnoTableAdapter = new AppEscritorio.AplicacionDataSetTableAdapters.TurnoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.turnoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aplicacionDataSet)).BeginInit();
            this.PanelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.BarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelContenedor
            // 
            this.PanelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContenedor.Location = new System.Drawing.Point(200, 37);
            this.PanelContenedor.Name = "PanelContenedor";
            this.PanelContenedor.Size = new System.Drawing.Size(600, 302);
            this.PanelContenedor.TabIndex = 9;
            // 
            // turnoBindingSource
            // 
            this.turnoBindingSource.DataMember = "Turno";
            this.turnoBindingSource.DataSource = this.aplicacionDataSet;
            // 
            // aplicacionDataSet
            // 
            this.aplicacionDataSet.DataSetName = "AplicacionDataSet";
            this.aplicacionDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.PanelMenu.Size = new System.Drawing.Size(200, 302);
            this.PanelMenu.TabIndex = 8;
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
            // VerTurnos
            // 
            this.VerTurnos.BackColor = System.Drawing.Color.DarkGray;
            this.VerTurnos.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.VerTurnos.Location = new System.Drawing.Point(37, 159);
            this.VerTurnos.Name = "VerTurnos";
            this.VerTurnos.Size = new System.Drawing.Size(75, 23);
            this.VerTurnos.TabIndex = 2;
            this.VerTurnos.Text = "Turnos";
            this.VerTurnos.UseVisualStyleBackColor = false;
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
            this.BarraTitulo.TabIndex = 7;
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
            // turnoTableAdapter
            // 
            this.turnoTableAdapter.ClearBeforeFill = true;
            // 
            // Turnos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelContenedor);
            this.Controls.Add(this.PanelMenu);
            this.Controls.Add(this.BarraTitulo);
            this.Name = "Turnos";
            this.Size = new System.Drawing.Size(800, 339);
            ((System.ComponentModel.ISupportInitialize)(this.turnoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aplicacionDataSet)).EndInit();
            this.PanelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.BarraTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelContenedor;
        private System.Windows.Forms.Panel PanelMenu;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Button VerTurnos;
        private System.Windows.Forms.Button SolicitarTurno;
        private System.Windows.Forms.Button OtrasOpciones;
        private System.Windows.Forms.Panel BarraTitulo;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.BindingSource turnoBindingSource;
        private AplicacionDataSet aplicacionDataSet;
        private AplicacionDataSetTableAdapters.TurnoTableAdapter turnoTableAdapter;
    }
}
