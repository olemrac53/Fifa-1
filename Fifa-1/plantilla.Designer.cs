namespace Fifa_1
{
    partial class plantilla
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(plantilla));
            dgvMercado = new DataGridView();
            dgvTitulares = new DataGridView();
            dgvSuplentes = new DataGridView();
            btnFicharTitular = new Button();
            btnFicharSuplente = new Button();
            btnQuitarTitular = new Button();
            btnQuitarSuplente = new Button();
            btnVolverMenu = new Button();
            lblPresupuestoActual = new Label();
            lblMercado = new Label();
            lblTitulares = new Label();
            lblSuplentes = new Label();
            lblPuntaje = new Label();
            gbConfig = new GroupBox();
            btnGuardarConfig = new Button();
            txtCantJugadores = new TextBox();
            lblCantJugadores = new Label();
            txtPresupuesto = new TextBox();
            lblPresupuestoMax = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvMercado).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTitulares).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSuplentes).BeginInit();
            gbConfig.SuspendLayout();
            SuspendLayout();
            // 
            // dgvMercado
            // 
            dgvMercado.AllowUserToAddRows = false;
            dgvMercado.AllowUserToDeleteRows = false;
            dgvMercado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMercado.Location = new Point(14, 44);
            dgvMercado.Margin = new Padding(4, 3, 4, 3);
            dgvMercado.MultiSelect = false;
            dgvMercado.Name = "dgvMercado";
            dgvMercado.ReadOnly = true;
            dgvMercado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMercado.Size = new Size(448, 179);
            dgvMercado.TabIndex = 0;
            dgvMercado.SelectionChanged += dgvMercado_SelectionChanged;
            // 
            // dgvTitulares
            // 
            dgvTitulares.AllowUserToAddRows = false;
            dgvTitulares.AllowUserToDeleteRows = false;
            dgvTitulares.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTitulares.Location = new Point(618, 44);
            dgvTitulares.Margin = new Padding(4, 3, 4, 3);
            dgvTitulares.MultiSelect = false;
            dgvTitulares.Name = "dgvTitulares";
            dgvTitulares.ReadOnly = true;
            dgvTitulares.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTitulares.Size = new Size(502, 179);
            dgvTitulares.TabIndex = 1;
            dgvTitulares.SelectionChanged += dgvTitulares_SelectionChanged;
            // 
            // dgvSuplentes
            // 
            dgvSuplentes.AllowUserToAddRows = false;
            dgvSuplentes.AllowUserToDeleteRows = false;
            dgvSuplentes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSuplentes.Location = new Point(618, 324);
            dgvSuplentes.Margin = new Padding(4, 3, 4, 3);
            dgvSuplentes.MultiSelect = false;
            dgvSuplentes.Name = "dgvSuplentes";
            dgvSuplentes.ReadOnly = true;
            dgvSuplentes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSuplentes.Size = new Size(502, 173);
            dgvSuplentes.TabIndex = 2;
            dgvSuplentes.SelectionChanged += dgvSuplentes_SelectionChanged;
            // 
            // btnFicharTitular
            // 
            btnFicharTitular.Location = new Point(523, 92);
            btnFicharTitular.Margin = new Padding(4, 3, 4, 3);
            btnFicharTitular.Name = "btnFicharTitular";
            btnFicharTitular.Size = new Size(88, 46);
            btnFicharTitular.TabIndex = 3;
            btnFicharTitular.Text = "Fichar >> (Titular)";
            btnFicharTitular.UseVisualStyleBackColor = true;
            btnFicharTitular.Click += btnFicharTitular_Click;
            // 
            // btnFicharSuplente
            // 
            btnFicharSuplente.Location = new Point(523, 145);
            btnFicharSuplente.Margin = new Padding(4, 3, 4, 3);
            btnFicharSuplente.Name = "btnFicharSuplente";
            btnFicharSuplente.Size = new Size(88, 46);
            btnFicharSuplente.TabIndex = 4;
            btnFicharSuplente.Text = "Fichar > (Suplente)";
            btnFicharSuplente.UseVisualStyleBackColor = true;
            btnFicharSuplente.Click += btnFicharSuplente_Click;
            // 
            // btnQuitarTitular
            // 
            btnQuitarTitular.Location = new Point(1127, 92);
            btnQuitarTitular.Margin = new Padding(4, 3, 4, 3);
            btnQuitarTitular.Name = "btnQuitarTitular";
            btnQuitarTitular.Size = new Size(88, 46);
            btnQuitarTitular.TabIndex = 5;
            btnQuitarTitular.Text = "Quitar <<";
            btnQuitarTitular.UseVisualStyleBackColor = true;
            btnQuitarTitular.Click += btnQuitarTitular_Click;
            // 
            // btnQuitarSuplente
            // 
            btnQuitarSuplente.Location = new Point(1127, 370);
            btnQuitarSuplente.Margin = new Padding(4, 3, 4, 3);
            btnQuitarSuplente.Name = "btnQuitarSuplente";
            btnQuitarSuplente.Size = new Size(88, 46);
            btnQuitarSuplente.TabIndex = 6;
            btnQuitarSuplente.Text = "Quitar <<";
            btnQuitarSuplente.UseVisualStyleBackColor = true;
            btnQuitarSuplente.Click += btnQuitarSuplente_Click;
            // 
            // btnVolverMenu
            // 
            btnVolverMenu.Location = new Point(14, 614);
            btnVolverMenu.Margin = new Padding(4, 3, 4, 3);
            btnVolverMenu.Name = "btnVolverMenu";
            btnVolverMenu.Size = new Size(131, 38);
            btnVolverMenu.TabIndex = 7;
            btnVolverMenu.Text = "Volver al Menú";
            btnVolverMenu.UseVisualStyleBackColor = true;
            btnVolverMenu.Click += btnVolverMenu_Click;
            // 
            // lblPresupuestoActual
            // 
            lblPresupuestoActual.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPresupuestoActual.Location = new Point(618, 510);
            lblPresupuestoActual.Margin = new Padding(4, 0, 4, 0);
            lblPresupuestoActual.Name = "lblPresupuestoActual";
            lblPresupuestoActual.Size = new Size(502, 27);
            lblPresupuestoActual.TabIndex = 8;
            lblPresupuestoActual.Text = "Presupuesto: $0 / $100,000,000";
            lblPresupuestoActual.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblMercado
            // 
            lblMercado.AutoSize = true;
            lblMercado.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMercado.Location = new Point(14, 17);
            lblMercado.Margin = new Padding(4, 0, 4, 0);
            lblMercado.Name = "lblMercado";
            lblMercado.Size = new Size(78, 20);
            lblMercado.TabIndex = 9;
            lblMercado.Text = "Mercado";
            // 
            // lblTitulares
            // 
            lblTitulares.AutoSize = true;
            lblTitulares.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulares.Location = new Point(614, 17);
            lblTitulares.Margin = new Padding(4, 0, 4, 0);
            lblTitulares.Name = "lblTitulares";
            lblTitulares.Size = new Size(78, 20);
            lblTitulares.TabIndex = 10;
            lblTitulares.Text = "Titulares";
            // 
            // lblSuplentes
            // 
            lblSuplentes.AutoSize = true;
            lblSuplentes.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSuplentes.Location = new Point(614, 298);
            lblSuplentes.Margin = new Padding(4, 0, 4, 0);
            lblSuplentes.Name = "lblSuplentes";
            lblSuplentes.Size = new Size(90, 20);
            lblSuplentes.TabIndex = 11;
            lblSuplentes.Text = "Suplentes";
            // 
            // lblPuntaje
            // 
            lblPuntaje.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPuntaje.Location = new Point(618, 537);
            lblPuntaje.Margin = new Padding(4, 0, 4, 0);
            lblPuntaje.Name = "lblPuntaje";
            lblPuntaje.Size = new Size(502, 27);
            lblPuntaje.TabIndex = 12;
            lblPuntaje.Text = "Puntaje Fecha: 0";
            lblPuntaje.TextAlign = ContentAlignment.MiddleRight;
            // 
            // gbConfig
            // 
            gbConfig.BackColor = Color.Transparent;
            gbConfig.BackgroundImageLayout = ImageLayout.None;
            gbConfig.Controls.Add(btnGuardarConfig);
            gbConfig.Controls.Add(txtCantJugadores);
            gbConfig.Controls.Add(lblCantJugadores);
            gbConfig.Controls.Add(txtPresupuesto);
            gbConfig.Controls.Add(lblPresupuestoMax);
            gbConfig.ForeColor = Color.FromArgb(128, 255, 255);
            gbConfig.Location = new Point(14, 504);
            gbConfig.Margin = new Padding(4, 3, 4, 3);
            gbConfig.Name = "gbConfig";
            gbConfig.Padding = new Padding(4, 3, 4, 3);
            gbConfig.Size = new Size(502, 103);
            gbConfig.TabIndex = 13;
            gbConfig.TabStop = false;
            gbConfig.Text = "Configuración de Plantilla";
            // 
            // btnGuardarConfig
            // 
            btnGuardarConfig.ForeColor = Color.Black;
            btnGuardarConfig.Location = new Point(350, 40);
            btnGuardarConfig.Margin = new Padding(4, 3, 4, 3);
            btnGuardarConfig.Name = "btnGuardarConfig";
            btnGuardarConfig.Size = new Size(128, 42);
            btnGuardarConfig.TabIndex = 4;
            btnGuardarConfig.Text = "Guardar Cambios";
            btnGuardarConfig.UseVisualStyleBackColor = true;
            btnGuardarConfig.Click += btnGuardarConfig_Click;
            // 
            // txtCantJugadores
            // 
            txtCantJugadores.Location = new Point(175, 59);
            txtCantJugadores.Margin = new Padding(4, 3, 4, 3);
            txtCantJugadores.Name = "txtCantJugadores";
            txtCantJugadores.Size = new Size(139, 23);
            txtCantJugadores.TabIndex = 3;
            // 
            // lblCantJugadores
            // 
            lblCantJugadores.AutoSize = true;
            lblCantJugadores.ForeColor = Color.FromArgb(128, 255, 255);
            lblCantJugadores.Location = new Point(12, 62);
            lblCantJugadores.Margin = new Padding(4, 0, 4, 0);
            lblCantJugadores.Name = "lblCantJugadores";
            lblCantJugadores.Size = new Size(127, 15);
            lblCantJugadores.TabIndex = 2;
            lblCantJugadores.Text = "Max. Cant. Futbolistas:";
            // 
            // txtPresupuesto
            // 
            txtPresupuesto.Location = new Point(175, 29);
            txtPresupuesto.Margin = new Padding(4, 3, 4, 3);
            txtPresupuesto.Name = "txtPresupuesto";
            txtPresupuesto.Size = new Size(139, 23);
            txtPresupuesto.TabIndex = 1;
            // 
            // lblPresupuestoMax
            // 
            lblPresupuestoMax.AutoSize = true;
            lblPresupuestoMax.ForeColor = Color.Cyan;
            lblPresupuestoMax.Location = new Point(12, 32);
            lblPresupuestoMax.Margin = new Padding(4, 0, 4, 0);
            lblPresupuestoMax.Name = "lblPresupuestoMax";
            lblPresupuestoMax.Size = new Size(122, 15);
            lblPresupuestoMax.TabIndex = 0;
            lblPresupuestoMax.Text = "Presupuesto Máximo:";
            // 
            // plantilla
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1256, 679);
            Controls.Add(gbConfig);
            Controls.Add(lblPuntaje);
            Controls.Add(lblSuplentes);
            Controls.Add(lblTitulares);
            Controls.Add(lblMercado);
            Controls.Add(lblPresupuestoActual);
            Controls.Add(btnVolverMenu);
            Controls.Add(btnQuitarSuplente);
            Controls.Add(btnQuitarTitular);
            Controls.Add(btnFicharSuplente);
            Controls.Add(btnFicharTitular);
            Controls.Add(dgvSuplentes);
            Controls.Add(dgvTitulares);
            Controls.Add(dgvMercado);
            Margin = new Padding(4, 3, 4, 3);
            Name = "plantilla";
            Text = "Gestión de Plantilla";
            Load += plantilla_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMercado).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTitulares).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSuplentes).EndInit();
            gbConfig.ResumeLayout(false);
            gbConfig.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMercado;
        private System.Windows.Forms.DataGridView dgvTitulares;
        private System.Windows.Forms.DataGridView dgvSuplentes;
        private System.Windows.Forms.Button btnFicharTitular;
        private System.Windows.Forms.Button btnFicharSuplente;
        private System.Windows.Forms.Button btnQuitarTitular;
        private System.Windows.Forms.Button btnQuitarSuplente;
        private System.Windows.Forms.Button btnVolverMenu;
        private System.Windows.Forms.Label lblPresupuestoActual;
        private System.Windows.Forms.Label lblMercado;
        private System.Windows.Forms.Label lblTitulares;
        private System.Windows.Forms.Label lblSuplentes;
        private System.Windows.Forms.Label lblPuntaje;
        private System.Windows.Forms.GroupBox gbConfig;
        private System.Windows.Forms.Button btnGuardarConfig;
        private System.Windows.Forms.TextBox txtCantJugadores;
        private System.Windows.Forms.Label lblCantJugadores;
        private System.Windows.Forms.TextBox txtPresupuesto;
        private System.Windows.Forms.Label lblPresupuestoMax;
    }
}