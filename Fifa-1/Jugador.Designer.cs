namespace Fifa_1
{
    partial class Jugador
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
            this.dgvFutbolistas = new System.Windows.Forms.DataGridView();
            this.gbDetalle = new System.Windows.Forms.GroupBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.cmbEquipo = new System.Windows.Forms.ComboBox();
            this.lblEquipo = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.lblFechaNac = new System.Windows.Forms.Label();
            this.txtCotizacion = new System.Windows.Forms.TextBox();
            this.lblCotizacion = new System.Windows.Forms.Label();
            this.txtNumCamisa = new System.Windows.Forms.TextBox();
            this.lblNumCamisa = new System.Windows.Forms.Label();
            this.txtApodo = new System.Windows.Forms.TextBox();
            this.lblApodo = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFutbolistas)).BeginInit();
            this.gbDetalle.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvFutbolistas
            // 
            this.dgvFutbolistas.AllowUserToAddRows = false;
            this.dgvFutbolistas.AllowUserToDeleteRows = false;
            this.dgvFutbolistas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFutbolistas.Location = new System.Drawing.Point(12, 12);
            this.dgvFutbolistas.MultiSelect = false;
            this.dgvFutbolistas.Name = "dgvFutbolistas";
            this.dgvFutbolistas.ReadOnly = true;
            this.dgvFutbolistas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFutbolistas.Size = new System.Drawing.Size(776, 250);
            this.dgvFutbolistas.TabIndex = 0;
            this.dgvFutbolistas.SelectionChanged += new System.EventHandler(this.dgvFutbolistas_SelectionChanged);
            // 
            // gbDetalle
            // 
            this.gbDetalle.Controls.Add(this.btnGuardar);
            this.gbDetalle.Controls.Add(this.cmbEquipo);
            this.gbDetalle.Controls.Add(this.lblEquipo);
            this.gbDetalle.Controls.Add(this.cmbTipo);
            this.gbDetalle.Controls.Add(this.lblTipo);
            this.gbDetalle.Controls.Add(this.dtpFechaNacimiento);
            this.gbDetalle.Controls.Add(this.lblFechaNac);
            this.gbDetalle.Controls.Add(this.txtCotizacion);
            this.gbDetalle.Controls.Add(this.lblCotizacion);
            this.gbDetalle.Controls.Add(this.txtNumCamisa);
            this.gbDetalle.Controls.Add(this.lblNumCamisa);
            this.gbDetalle.Controls.Add(this.txtApodo);
            this.gbDetalle.Controls.Add(this.lblApodo);
            this.gbDetalle.Controls.Add(this.txtApellido);
            this.gbDetalle.Controls.Add(this.lblApellido);
            this.gbDetalle.Controls.Add(this.txtNombre);
            this.gbDetalle.Controls.Add(this.lblNombre);
            this.gbDetalle.Location = new System.Drawing.Point(12, 310);
            this.gbDetalle.Name = "gbDetalle";
            this.gbDetalle.Size = new System.Drawing.Size(776, 180);
            this.gbDetalle.TabIndex = 1;
            this.gbDetalle.TabStop = false;
            this.gbDetalle.Text = "Detalle de Futbolista";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(660, 130);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 40);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // cmbEquipo
            // 
            this.cmbEquipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEquipo.FormattingEnabled = true;
            this.cmbEquipo.Location = new System.Drawing.Point(400, 130);
            this.cmbEquipo.Name = "cmbEquipo";
            this.cmbEquipo.Size = new System.Drawing.Size(200, 21);
            this.cmbEquipo.TabIndex = 8;
            // 
            // lblEquipo
            // 
            this.lblEquipo.AutoSize = true;
            this.lblEquipo.Location = new System.Drawing.Point(340, 133);
            this.lblEquipo.Name = "lblEquipo";
            this.lblEquipo.Size = new System.Drawing.Size(43, 13);
            this.lblEquipo.TabIndex = 14;
            this.lblEquipo.Text = "Equipo:";
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Location = new System.Drawing.Point(400, 90);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(200, 21);
            this.cmbTipo.TabIndex = 7;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(340, 93);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(31, 13);
            this.lblTipo.TabIndex = 12;
            this.lblTipo.Text = "Tipo:";
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(400, 50);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaNacimiento.TabIndex = 6;
            // 
            // lblFechaNac
            // 
            this.lblFechaNac.AutoSize = true;
            this.lblFechaNac.Location = new System.Drawing.Point(340, 53);
            this.lblFechaNac.Name = "lblFechaNac";
            this.lblFechaNac.Size = new System.Drawing.Size(63, 13);
            this.lblFechaNac.TabIndex = 10;
            this.lblFechaNac.Text = "Nacimiento:";
            // 
            // txtCotizacion
            // 
            this.txtCotizacion.Location = new System.Drawing.Point(400, 20);
            this.txtCotizacion.Name = "txtCotizacion";
            this.txtCotizacion.Size = new System.Drawing.Size(100, 20);
            this.txtCotizacion.TabIndex = 5;
            // 
            // lblCotizacion
            // 
            this.lblCotizacion.AutoSize = true;
            this.lblCotizacion.Location = new System.Drawing.Point(340, 23);
            this.lblCotizacion.Name = "lblCotizacion";
            this.lblCotizacion.Size = new System.Drawing.Size(59, 13);
            this.lblCotizacion.TabIndex = 8;
            this.lblCotizacion.Text = "Cotización:";
            // 
            // txtNumCamisa
            // 
            this.txtNumCamisa.Location = new System.Drawing.Point(80, 140);
            this.txtNumCamisa.Name = "txtNumCamisa";
            this.txtNumCamisa.Size = new System.Drawing.Size(100, 20);
            this.txtNumCamisa.TabIndex = 4;
            // 
            // lblNumCamisa
            // 
            this.lblNumCamisa.AutoSize = true;
            this.lblNumCamisa.Location = new System.Drawing.Point(20, 143);
            this.lblNumCamisa.Name = "lblNumCamisa";
            this.lblNumCamisa.Size = new System.Drawing.Size(46, 13);
            this.lblNumCamisa.TabIndex = 6;
            this.lblNumCamisa.Text = "Camisa:";
            // 
            // txtApodo
            // 
            this.txtApodo.Location = new System.Drawing.Point(80, 100);
            this.txtApodo.Name = "txtApodo";
            this.txtApodo.Size = new System.Drawing.Size(200, 20);
            this.txtApodo.TabIndex = 3;
            // 
            // lblApodo
            // 
            this.lblApodo.AutoSize = true;
            this.lblApodo.Location = new System.Drawing.Point(20, 103);
            this.lblApodo.Name = "lblApodo";
            this.lblApodo.Size = new System.Drawing.Size(41, 13);
            this.lblApodo.TabIndex = 4;
            this.lblApodo.Text = "Apodo:";
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(80, 60);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(200, 20);
            this.txtApellido.TabIndex = 2;
            // 
            // lblApellido
            // 
            this.lblApellido.AutoSize = true;
            this.lblApellido.Location = new System.Drawing.Point(20, 63);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(47, 13);
            this.lblApellido.TabIndex = 2;
            this.lblApellido.Text = "Apellido:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(80, 20);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(200, 20);
            this.txtNombre.TabIndex = 1;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(20, 23);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(47, 13);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre:";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(12, 270);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 23);
            this.btnNuevo.TabIndex = 10;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(93, 270);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 11;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // Jugador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 504);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.gbDetalle);
            this.Controls.Add(this.dgvFutbolistas);
            this.Name = "Jugador";
            this.Text = "Gestión de Futbolistas (Admin)";
            this.Load += new System.EventHandler(this.Jugador_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFutbolistas)).EndInit();
            this.gbDetalle.ResumeLayout(false);
            this.gbDetalle.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFutbolistas;
        private System.Windows.Forms.GroupBox gbDetalle;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ComboBox cmbEquipo;
        private System.Windows.Forms.Label lblEquipo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Label lblFechaNac;
        private System.Windows.Forms.TextBox txtCotizacion;
        private System.Windows.Forms.Label lblCotizacion;
        private System.Windows.Forms.TextBox txtNumCamisa;
        private System.Windows.Forms.Label lblNumCamisa;
        private System.Windows.Forms.TextBox txtApodo;
        private System.Windows.Forms.Label lblApodo;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEliminar;
    }
}