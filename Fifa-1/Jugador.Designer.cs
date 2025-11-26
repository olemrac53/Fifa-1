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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Jugador));
            dgvFutbolistas = new DataGridView();
            gbDetalle = new GroupBox();
            btnGuardar = new Button();
            cmbEquipo = new ComboBox();
            lblEquipo = new Label();
            cmbTipo = new ComboBox();
            lblTipo = new Label();
            dtpFechaNacimiento = new DateTimePicker();
            lblFechaNac = new Label();
            txtCotizacion = new TextBox();
            lblCotizacion = new Label();
            txtNumCamisa = new TextBox();
            lblNumCamisa = new Label();
            txtApodo = new TextBox();
            lblApodo = new Label();
            txtApellido = new TextBox();
            lblApellido = new Label();
            txtNombre = new TextBox();
            lblNombre = new Label();
            btnNuevo = new Button();
            btnEliminar = new Button();
            gbNuevoEquipo = new GroupBox();
            btnCrearEquipo = new Button();
            txtNuevoEquipoNombre = new TextBox();
            lblNuevoEquipo = new Label();
            btnVolverMenu = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvFutbolistas).BeginInit();
            gbDetalle.SuspendLayout();
            gbNuevoEquipo.SuspendLayout();
            SuspendLayout();
            // 
            // dgvFutbolistas
            // 
            dgvFutbolistas.AllowUserToAddRows = false;
            dgvFutbolistas.AllowUserToDeleteRows = false;
            dgvFutbolistas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFutbolistas.Location = new Point(14, 14);
            dgvFutbolistas.Margin = new Padding(4, 3, 4, 3);
            dgvFutbolistas.MultiSelect = false;
            dgvFutbolistas.Name = "dgvFutbolistas";
            dgvFutbolistas.ReadOnly = true;
            dgvFutbolistas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFutbolistas.Size = new Size(905, 288);
            dgvFutbolistas.TabIndex = 0;
            dgvFutbolistas.SelectionChanged += dgvFutbolistas_SelectionChanged;
            // 
            // gbDetalle
            // 
            gbDetalle.Controls.Add(btnGuardar);
            gbDetalle.Controls.Add(cmbEquipo);
            gbDetalle.Controls.Add(lblEquipo);
            gbDetalle.Controls.Add(cmbTipo);
            gbDetalle.Controls.Add(lblTipo);
            gbDetalle.Controls.Add(dtpFechaNacimiento);
            gbDetalle.Controls.Add(lblFechaNac);
            gbDetalle.Controls.Add(txtCotizacion);
            gbDetalle.Controls.Add(lblCotizacion);
            gbDetalle.Controls.Add(txtNumCamisa);
            gbDetalle.Controls.Add(lblNumCamisa);
            gbDetalle.Controls.Add(txtApodo);
            gbDetalle.Controls.Add(lblApodo);
            gbDetalle.Controls.Add(txtApellido);
            gbDetalle.Controls.Add(lblApellido);
            gbDetalle.Controls.Add(txtNombre);
            gbDetalle.Controls.Add(lblNombre);
            gbDetalle.Location = new Point(14, 358);
            gbDetalle.Margin = new Padding(4, 3, 4, 3);
            gbDetalle.Name = "gbDetalle";
            gbDetalle.Padding = new Padding(4, 3, 4, 3);
            gbDetalle.Size = new Size(905, 208);
            gbDetalle.TabIndex = 1;
            gbDetalle.TabStop = false;
            gbDetalle.Text = "Detalle de Futbolista";
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.Location = new Point(770, 150);
            btnGuardar.Margin = new Padding(4, 3, 4, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(128, 46);
            btnGuardar.TabIndex = 9;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // cmbEquipo
            // 
            cmbEquipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEquipo.FormattingEnabled = true;
            cmbEquipo.Location = new Point(467, 150);
            cmbEquipo.Margin = new Padding(4, 3, 4, 3);
            cmbEquipo.Name = "cmbEquipo";
            cmbEquipo.Size = new Size(233, 23);
            cmbEquipo.TabIndex = 8;
            // 
            // lblEquipo
            // 
            lblEquipo.AutoSize = true;
            lblEquipo.Location = new Point(397, 153);
            lblEquipo.Margin = new Padding(4, 0, 4, 0);
            lblEquipo.Name = "lblEquipo";
            lblEquipo.Size = new Size(47, 15);
            lblEquipo.TabIndex = 14;
            lblEquipo.Text = "Equipo:";
            // 
            // cmbTipo
            // 
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.FormattingEnabled = true;
            cmbTipo.Location = new Point(467, 104);
            cmbTipo.Margin = new Padding(4, 3, 4, 3);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(233, 23);
            cmbTipo.TabIndex = 7;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(397, 107);
            lblTipo.Margin = new Padding(4, 0, 4, 0);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(33, 15);
            lblTipo.TabIndex = 12;
            lblTipo.Text = "Tipo:";
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Location = new Point(467, 58);
            dtpFechaNacimiento.Margin = new Padding(4, 3, 4, 3);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(233, 23);
            dtpFechaNacimiento.TabIndex = 6;
            // 
            // lblFechaNac
            // 
            lblFechaNac.AutoSize = true;
            lblFechaNac.Location = new Point(397, 61);
            lblFechaNac.Margin = new Padding(4, 0, 4, 0);
            lblFechaNac.Name = "lblFechaNac";
            lblFechaNac.Size = new Size(72, 15);
            lblFechaNac.TabIndex = 10;
            lblFechaNac.Text = "Nacimiento:";
            // 
            // txtCotizacion
            // 
            txtCotizacion.Location = new Point(467, 23);
            txtCotizacion.Margin = new Padding(4, 3, 4, 3);
            txtCotizacion.Name = "txtCotizacion";
            txtCotizacion.Size = new Size(116, 23);
            txtCotizacion.TabIndex = 5;
            // 
            // lblCotizacion
            // 
            lblCotizacion.AutoSize = true;
            lblCotizacion.Location = new Point(397, 27);
            lblCotizacion.Margin = new Padding(4, 0, 4, 0);
            lblCotizacion.Name = "lblCotizacion";
            lblCotizacion.Size = new Size(66, 15);
            lblCotizacion.TabIndex = 8;
            lblCotizacion.Text = "Cotización:";
            // 
            // txtNumCamisa
            // 
            txtNumCamisa.Location = new Point(93, 162);
            txtNumCamisa.Margin = new Padding(4, 3, 4, 3);
            txtNumCamisa.Name = "txtNumCamisa";
            txtNumCamisa.Size = new Size(116, 23);
            txtNumCamisa.TabIndex = 4;
            // 
            // lblNumCamisa
            // 
            lblNumCamisa.AutoSize = true;
            lblNumCamisa.Location = new Point(23, 165);
            lblNumCamisa.Margin = new Padding(4, 0, 4, 0);
            lblNumCamisa.Name = "lblNumCamisa";
            lblNumCamisa.Size = new Size(49, 15);
            lblNumCamisa.TabIndex = 6;
            lblNumCamisa.Text = "Camisa:";
            // 
            // txtApodo
            // 
            txtApodo.Location = new Point(93, 115);
            txtApodo.Margin = new Padding(4, 3, 4, 3);
            txtApodo.Name = "txtApodo";
            txtApodo.Size = new Size(233, 23);
            txtApodo.TabIndex = 3;
            // 
            // lblApodo
            // 
            lblApodo.AutoSize = true;
            lblApodo.Location = new Point(23, 119);
            lblApodo.Margin = new Padding(4, 0, 4, 0);
            lblApodo.Name = "lblApodo";
            lblApodo.Size = new Size(46, 15);
            lblApodo.TabIndex = 4;
            lblApodo.Text = "Apodo:";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(93, 69);
            txtApellido.Margin = new Padding(4, 3, 4, 3);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(233, 23);
            txtApellido.TabIndex = 2;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(23, 73);
            lblApellido.Margin = new Padding(4, 0, 4, 0);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(54, 15);
            lblApellido.TabIndex = 2;
            lblApellido.Text = "Apellido:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(93, 23);
            txtNombre.Margin = new Padding(4, 3, 4, 3);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(233, 23);
            txtNombre.TabIndex = 1;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(23, 27);
            lblNombre.Margin = new Padding(4, 0, 4, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(54, 15);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(14, 312);
            btnNuevo.Margin = new Padding(4, 3, 4, 3);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(88, 27);
            btnNuevo.TabIndex = 10;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(108, 312);
            btnEliminar.Margin = new Padding(4, 3, 4, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(88, 27);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // gbNuevoEquipo
            // 
            gbNuevoEquipo.Controls.Add(btnCrearEquipo);
            gbNuevoEquipo.Controls.Add(txtNuevoEquipoNombre);
            gbNuevoEquipo.Controls.Add(lblNuevoEquipo);
            gbNuevoEquipo.Location = new Point(14, 572);
            gbNuevoEquipo.Margin = new Padding(4, 3, 4, 3);
            gbNuevoEquipo.Name = "gbNuevoEquipo";
            gbNuevoEquipo.Padding = new Padding(4, 3, 4, 3);
            gbNuevoEquipo.Size = new Size(905, 63);
            gbNuevoEquipo.TabIndex = 12;
            gbNuevoEquipo.TabStop = false;
            gbNuevoEquipo.Text = "Crear Nuevo Equipo";
            // 
            // btnCrearEquipo
            // 
            btnCrearEquipo.Location = new Point(400, 20);
            btnCrearEquipo.Margin = new Padding(4, 3, 4, 3);
            btnCrearEquipo.Name = "btnCrearEquipo";
            btnCrearEquipo.Size = new Size(117, 27);
            btnCrearEquipo.TabIndex = 2;
            btnCrearEquipo.Text = "Crear Equipo";
            btnCrearEquipo.UseVisualStyleBackColor = true;
            btnCrearEquipo.Click += btnCrearEquipo_Click;
            // 
            // txtNuevoEquipoNombre
            // 
            txtNuevoEquipoNombre.Location = new Point(140, 22);
            txtNuevoEquipoNombre.Margin = new Padding(4, 3, 4, 3);
            txtNuevoEquipoNombre.Name = "txtNuevoEquipoNombre";
            txtNuevoEquipoNombre.Size = new Size(233, 23);
            txtNuevoEquipoNombre.TabIndex = 1;
            // 
            // lblNuevoEquipo
            // 
            lblNuevoEquipo.AutoSize = true;
            lblNuevoEquipo.Location = new Point(23, 25);
            lblNuevoEquipo.Margin = new Padding(4, 0, 4, 0);
            lblNuevoEquipo.Name = "lblNuevoEquipo";
            lblNuevoEquipo.Size = new Size(110, 15);
            lblNuevoEquipo.TabIndex = 0;
            lblNuevoEquipo.Text = "Nombre de Equipo:";
            // 
            // btnVolverMenu
            // 
            btnVolverMenu.Location = new Point(791, 312);
            btnVolverMenu.Margin = new Padding(4, 3, 4, 3);
            btnVolverMenu.Name = "btnVolverMenu";
            btnVolverMenu.Size = new Size(128, 27);
            btnVolverMenu.TabIndex = 13;
            btnVolverMenu.Text = "Volver al Menú";
            btnVolverMenu.UseVisualStyleBackColor = true;
            btnVolverMenu.Click += btnVolverMenu_Click;
            // 
            // Jugador
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(933, 650);
            Controls.Add(btnVolverMenu);
            Controls.Add(gbNuevoEquipo);
            Controls.Add(btnEliminar);
            Controls.Add(btnNuevo);
            Controls.Add(gbDetalle);
            Controls.Add(dgvFutbolistas);
            Cursor = Cursors.Hand;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Jugador";
            Text = "Gestión de Futbolistas (Admin)";
            Load += Jugador_Load;
            ((System.ComponentModel.ISupportInitialize)dgvFutbolistas).EndInit();
            gbDetalle.ResumeLayout(false);
            gbDetalle.PerformLayout();
            gbNuevoEquipo.ResumeLayout(false);
            gbNuevoEquipo.PerformLayout();
            ResumeLayout(false);

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
        private System.Windows.Forms.GroupBox gbNuevoEquipo;
        private System.Windows.Forms.Button btnCrearEquipo;
        private System.Windows.Forms.TextBox txtNuevoEquipoNombre;
        private System.Windows.Forms.Label lblNuevoEquipo;
        private System.Windows.Forms.Button btnVolverMenu;
    }
}