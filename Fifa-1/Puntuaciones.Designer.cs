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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Puntuaciones));
            dgvPuntuaciones = new DataGridView();
            gbDetalle = new GroupBox();
            txtPuntaje = new TextBox();
            lblPuntaje = new Label();
            numFecha = new NumericUpDown();
            lblFecha = new Label();
            cmbFutbolista = new ComboBox();
            lblFutbolista = new Label();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnNuevo = new Button();
            btnVolverMenu = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPuntuaciones).BeginInit();
            gbDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numFecha).BeginInit();
            SuspendLayout();
            // 
            // dgvPuntuaciones
            // 
            dgvPuntuaciones.AllowUserToAddRows = false;
            dgvPuntuaciones.AllowUserToDeleteRows = false;
            dgvPuntuaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPuntuaciones.Location = new Point(14, 14);
            dgvPuntuaciones.Margin = new Padding(4, 3, 4, 3);
            dgvPuntuaciones.MultiSelect = false;
            dgvPuntuaciones.Name = "dgvPuntuaciones";
            dgvPuntuaciones.ReadOnly = true;
            dgvPuntuaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPuntuaciones.Size = new Size(653, 231);
            dgvPuntuaciones.TabIndex = 0;
            dgvPuntuaciones.SelectionChanged += dgvPuntuaciones_SelectionChanged;
            // 
            // gbDetalle
            // 
            gbDetalle.Controls.Add(txtPuntaje);
            gbDetalle.Controls.Add(lblPuntaje);
            gbDetalle.Controls.Add(numFecha);
            gbDetalle.Controls.Add(lblFecha);
            gbDetalle.Controls.Add(cmbFutbolista);
            gbDetalle.Controls.Add(lblFutbolista);
            gbDetalle.Controls.Add(btnGuardar);
            gbDetalle.Location = new Point(14, 300);
            gbDetalle.Margin = new Padding(4, 3, 4, 3);
            gbDetalle.Name = "gbDetalle";
            gbDetalle.Padding = new Padding(4, 3, 4, 3);
            gbDetalle.Size = new Size(653, 150);
            gbDetalle.TabIndex = 1;
            gbDetalle.TabStop = false;
            gbDetalle.Text = "Detalle Puntuación";
            // 
            // txtPuntaje
            // 
            txtPuntaje.Location = new Point(93, 100);
            txtPuntaje.Margin = new Padding(4, 3, 4, 3);
            txtPuntaje.Name = "txtPuntaje";
            txtPuntaje.Size = new Size(81, 23);
            txtPuntaje.TabIndex = 6;
            // 
            // lblPuntaje
            // 
            lblPuntaje.AutoSize = true;
            lblPuntaje.Location = new Point(23, 104);
            lblPuntaje.Margin = new Padding(4, 0, 4, 0);
            lblPuntaje.Name = "lblPuntaje";
            lblPuntaje.Size = new Size(50, 15);
            lblPuntaje.TabIndex = 5;
            lblPuntaje.Text = "Puntaje:";
            // 
            // numFecha
            // 
            numFecha.Location = new Point(93, 67);
            numFecha.Margin = new Padding(4, 3, 4, 3);
            numFecha.Maximum = new decimal(new int[] { 49, 0, 0, 0 });
            numFecha.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numFecha.Name = "numFecha";
            numFecha.Size = new Size(82, 23);
            numFecha.TabIndex = 4;
            numFecha.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(23, 69);
            lblFecha.Margin = new Padding(4, 0, 4, 0);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(41, 15);
            lblFecha.TabIndex = 3;
            lblFecha.Text = "Fecha:";
            // 
            // cmbFutbolista
            // 
            cmbFutbolista.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFutbolista.FormattingEnabled = true;
            cmbFutbolista.Location = new Point(93, 31);
            cmbFutbolista.Margin = new Padding(4, 3, 4, 3);
            cmbFutbolista.Name = "cmbFutbolista";
            cmbFutbolista.Size = new Size(291, 23);
            cmbFutbolista.TabIndex = 2;
            // 
            // lblFutbolista
            // 
            lblFutbolista.AutoSize = true;
            lblFutbolista.Location = new Point(23, 35);
            lblFutbolista.Margin = new Padding(4, 0, 4, 0);
            lblFutbolista.Name = "lblFutbolista";
            lblFutbolista.Size = new Size(62, 15);
            lblFutbolista.TabIndex = 1;
            lblFutbolista.Text = "Futbolista:";
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(513, 52);
            btnGuardar.Margin = new Padding(4, 3, 4, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(117, 46);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(108, 254);
            btnEliminar.Margin = new Padding(4, 3, 4, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(88, 27);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(14, 254);
            btnNuevo.Margin = new Padding(4, 3, 4, 3);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(88, 27);
            btnNuevo.TabIndex = 3;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnVolverMenu
            // 
            btnVolverMenu.Location = new Point(539, 254);
            btnVolverMenu.Margin = new Padding(4, 3, 4, 3);
            btnVolverMenu.Name = "btnVolverMenu";
            btnVolverMenu.Size = new Size(128, 27);
            btnVolverMenu.TabIndex = 4;
            btnVolverMenu.Text = "Volver al Menú";
            btnVolverMenu.UseVisualStyleBackColor = true;
            btnVolverMenu.Click += btnVolverMenu_Click;
            // 
            // Puntuaciones
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(681, 463);
            Controls.Add(btnVolverMenu);
            Controls.Add(btnNuevo);
            Controls.Add(btnEliminar);
            Controls.Add(gbDetalle);
            Controls.Add(dgvPuntuaciones);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Puntuaciones";
            Text = "Administrar Puntuaciones";
            Load += Puntuaciones_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPuntuaciones).EndInit();
            gbDetalle.ResumeLayout(false);
            gbDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numFecha).EndInit();
            ResumeLayout(false);

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