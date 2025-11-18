namespace Fifa_1
{
    partial class Puntuaciones
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.dgvPuntuaciones = new System.Windows.Forms.DataGridView();
            this.gbDetalle = new System.Windows.Forms.GroupBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblFutbolista = new System.Windows.Forms.Label();
            this.cmbFutbolista = new System.Windows.Forms.ComboBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.numFecha = new System.Windows.Forms.NumericUpDown();
            this.lblPuntaje = new System.Windows.Forms.Label();
            this.txtPuntaje = new System.Windows.Forms.TextBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnVolverMenu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPuntuaciones)).BeginInit();
            this.gbDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFecha)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPuntuaciones
            // 
            this.dgvPuntuaciones.AllowUserToAddRows = false;
            this.dgvPuntuaciones.AllowUserToDeleteRows = false;
            this.dgvPuntuaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPuntuaciones.Location = new System.Drawing.Point(12, 12);
            this.dgvPuntuaciones.MultiSelect = false;
            this.dgvPuntuaciones.Name = "dgvPuntuaciones";
            this.dgvPuntuaciones.ReadOnly = true;
            this.dgvPuntuaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPuntuaciones.Size = new System.Drawing.Size(560, 200);
            this.dgvPuntuaciones.TabIndex = 0;
            this.dgvPuntuaciones.SelectionChanged += new System.EventHandler(this.dgvPuntuaciones_SelectionChanged);
            // 
            // gbDetalle
            // 
            this.gbDetalle.Controls.Add(this.txtPuntaje);
            this.gbDetalle.Controls.Add(this.lblPuntaje);
            this.gbDetalle.Controls.Add(this.numFecha);
            this.gbDetalle.Controls.Add(this.lblFecha);
            this.gbDetalle.Controls.Add(this.cmbFutbolista);
            this.gbDetalle.Controls.Add(this.lblFutbolista);
            this.gbDetalle.Controls.Add(this.btnGuardar);
            this.gbDetalle.Location = new System.Drawing.Point(12, 260);
            this.gbDetalle.Name = "gbDetalle";
            this.gbDetalle.Size = new System.Drawing.Size(560, 130);
            this.gbDetalle.TabIndex = 1;
            this.gbDetalle.TabStop = false;
            this.gbDetalle.Text = "Detalle Puntuación";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(440, 45);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 40);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // lblFutbolista
            // 
            this.lblFutbolista.AutoSize = true;
            this.lblFutbolista.Location = new System.Drawing.Point(20, 30);
            this.lblFutbolista.Name = "lblFutbolista";
            this.lblFutbolista.Size = new System.Drawing.Size(56, 13);
            this.lblFutbolista.TabIndex = 1;
            this.lblFutbolista.Text = "Futbolista:";
            // 
            // cmbFutbolista
            // 
            this.cmbFutbolista.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFutbolista.FormattingEnabled = true;
            this.cmbFutbolista.Location = new System.Drawing.Point(80, 27);
            this.cmbFutbolista.Name = "cmbFutbolista";
            this.cmbFutbolista.Size = new System.Drawing.Size(250, 21);
            this.cmbFutbolista.TabIndex = 2;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(20, 60);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(40, 13);
            this.lblFecha.TabIndex = 3;
            this.lblFecha.Text = "Fecha:";
            // 
            // numFecha
            // 
            this.numFecha.Location = new System.Drawing.Point(80, 58);
            this.numFecha.Maximum = new decimal(new int[] { 49, 0, 0, 0 });
            this.numFecha.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numFecha.Name = "numFecha";
            this.numFecha.Size = new System.Drawing.Size(70, 20);
            this.numFecha.TabIndex = 4;
            this.numFecha.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblPuntaje
            // 
            this.lblPuntaje.AutoSize = true;
            this.lblPuntaje.Location = new System.Drawing.Point(20, 90);
            this.lblPuntaje.Name = "lblPuntaje";
            this.lblPuntaje.Size = new System.Drawing.Size(46, 13);
            this.lblPuntaje.TabIndex = 5;
            this.lblPuntaje.Text = "Puntaje:";
            // 
            // txtPuntaje
            // 
            this.txtPuntaje.Location = new System.Drawing.Point(80, 87);
            this.txtPuntaje.Name = "txtPuntaje";
            this.txtPuntaje.Size = new System.Drawing.Size(70, 20);
            this.txtPuntaje.TabIndex = 6;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(93, 220);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(12, 220);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 23);
            this.btnNuevo.TabIndex = 3;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnVolverMenu
            // 
            this.btnVolverMenu.Location = new System.Drawing.Point(462, 220);
            this.btnVolverMenu.Name = "btnVolverMenu";
            this.btnVolverMenu.Size = new System.Drawing.Size(110, 23);
            this.btnVolverMenu.TabIndex = 4;
            this.btnVolverMenu.Text = "Volver al Menú";
            this.btnVolverMenu.UseVisualStyleBackColor = true;
            this.btnVolverMenu.Click += new System.EventHandler(this.btnVolverMenu_Click);
            // 
            // Puntuaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 401);
            this.Controls.Add(this.btnVolverMenu);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.gbDetalle);
            this.Controls.Add(this.dgvPuntuaciones);
            this.Name = "Puntuaciones";
            this.Text = "Administrar Puntuaciones";
            this.Load += new System.EventHandler(this.Puntuaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPuntuaciones)).EndInit();
            this.gbDetalle.ResumeLayout(false);
            this.gbDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFecha)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.DataGridView dgvPuntuaciones;
        private System.Windows.Forms.GroupBox gbDetalle;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblFutbolista;
        private System.Windows.Forms.ComboBox cmbFutbolista;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.NumericUpDown numFecha;
        private System.Windows.Forms.Label lblPuntaje;
        private System.Windows.Forms.TextBox txtPuntaje;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnVolverMenu;
    }
}