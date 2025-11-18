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
            this.dgvMercado = new System.Windows.Forms.DataGridView();
            this.dgvTitulares = new System.Windows.Forms.DataGridView();
            this.dgvSuplentes = new System.Windows.Forms.DataGridView();
            this.btnFicharTitular = new System.Windows.Forms.Button();
            this.btnFicharSuplente = new System.Windows.Forms.Button();
            this.btnQuitarTitular = new System.Windows.Forms.Button();
            this.btnQuitarSuplente = new System.Windows.Forms.Button();
            this.btnVolverMenu = new System.Windows.Forms.Button();
            this.lblPresupuestoActual = new System.Windows.Forms.Label();
            this.lblMercado = new System.Windows.Forms.Label();
            this.lblTitulares = new System.Windows.Forms.Label();
            this.lblSuplentes = new System.Windows.Forms.Label();
            this.lblPuntaje = new System.Windows.Forms.Label();
            this.gbConfig = new System.Windows.Forms.GroupBox();
            this.btnGuardarConfig = new System.Windows.Forms.Button();
            this.txtCantJugadores = new System.Windows.Forms.TextBox();
            this.lblCantJugadores = new System.Windows.Forms.Label();
            this.txtPresupuesto = new System.Windows.Forms.TextBox();
            this.lblPresupuestoMax = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMercado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTitulares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuplentes)).BeginInit();
            this.gbConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMercado
            // 
            this.dgvMercado.AllowUserToAddRows = false;
            this.dgvMercado.AllowUserToDeleteRows = false;
            this.dgvMercado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMercado.Location = new System.Drawing.Point(12, 38);
            this.dgvMercado.MultiSelect = false;
            this.dgvMercado.Name = "dgvMercado";
            this.dgvMercado.ReadOnly = true;
            this.dgvMercado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMercado.Size = new System.Drawing.Size(430, 200);
            this.dgvMercado.TabIndex = 0;
            this.dgvMercado.SelectionChanged += new System.EventHandler(this.dgvMercado_SelectionChanged);
            // 
            // dgvTitulares
            // 
            this.dgvTitulares.AllowUserToAddRows = false;
            this.dgvTitulares.AllowUserToDeleteRows = false;
            this.dgvTitulares.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTitulares.Location = new System.Drawing.Point(530, 38);
            this.dgvTitulares.MultiSelect = false;
            this.dgvTitulares.Name = "dgvTitulares";
            this.dgvTitulares.ReadOnly = true;
            this.dgvTitulares.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTitulares.Size = new System.Drawing.Size(430, 200);
            this.dgvTitulares.TabIndex = 1;
            this.dgvTitulares.SelectionChanged += new System.EventHandler(this.dgvTitulares_SelectionChanged);
            // 
            // dgvSuplentes
            // 
            this.dgvSuplentes.AllowUserToAddRows = false;
            this.dgvSuplentes.AllowUserToDeleteRows = false;
            this.dgvSuplentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSuplentes.Location = new System.Drawing.Point(530, 281);
            this.dgvSuplentes.MultiSelect = false;
            this.dgvSuplentes.Name = "dgvSuplentes";
            this.dgvSuplentes.ReadOnly = true;
            this.dgvSuplentes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSuplentes.Size = new System.Drawing.Size(430, 150);
            this.dgvSuplentes.TabIndex = 2;
            this.dgvSuplentes.SelectionChanged += new System.EventHandler(this.dgvSuplentes_SelectionChanged);
            // 
            // btnFicharTitular
            // 
            this.btnFicharTitular.Location = new System.Drawing.Point(448, 80);
            this.btnFicharTitular.Name = "btnFicharTitular";
            this.btnFicharTitular.Size = new System.Drawing.Size(75, 40);
            this.btnFicharTitular.TabIndex = 3;
            this.btnFicharTitular.Text = "Fichar >> (Titular)";
            this.btnFicharTitular.UseVisualStyleBackColor = true;
            this.btnFicharTitular.Click += new System.EventHandler(this.btnFicharTitular_Click);
            // 
            // btnFicharSuplente
            // 
            this.btnFicharSuplente.Location = new System.Drawing.Point(448, 126);
            this.btnFicharSuplente.Name = "btnFicharSuplente";
            this.btnFicharSuplente.Size = new System.Drawing.Size(75, 40);
            this.btnFicharSuplente.TabIndex = 4;
            this.btnFicharSuplente.Text = "Fichar > (Suplente)";
            this.btnFicharSuplente.UseVisualStyleBackColor = true;
            this.btnFicharSuplente.Click += new System.EventHandler(this.btnFicharSuplente_Click);
            // 
            // btnQuitarTitular
            // 
            this.btnQuitarTitular.Location = new System.Drawing.Point(966, 80);
            this.btnQuitarTitular.Name = "btnQuitarTitular";
            this.btnQuitarTitular.Size = new System.Drawing.Size(75, 40);
            this.btnQuitarTitular.TabIndex = 5;
            this.btnQuitarTitular.Text = "Quitar <<";
            this.btnQuitarTitular.UseVisualStyleBackColor = true;
            this.btnQuitarTitular.Click += new System.EventHandler(this.btnQuitarTitular_Click);
            // 
            // btnQuitarSuplente
            // 
            this.btnQuitarSuplente.Location = new System.Drawing.Point(966, 321);
            this.btnQuitarSuplente.Name = "btnQuitarSuplente";
            this.btnQuitarSuplente.Size = new System.Drawing.Size(75, 40);
            this.btnQuitarSuplente.TabIndex = 6;
            this.btnQuitarSuplente.Text = "Quitar <<";
            this.btnQuitarSuplente.UseVisualStyleBackColor = true;
            this.btnQuitarSuplente.Click += new System.EventHandler(this.btnQuitarSuplente_Click);
            // 
            // btnVolverMenu
            // 
            this.btnVolverMenu.Location = new System.Drawing.Point(12, 532);
            this.btnVolverMenu.Name = "btnVolverMenu";
            this.btnVolverMenu.Size = new System.Drawing.Size(112, 33);
            this.btnVolverMenu.TabIndex = 7;
            this.btnVolverMenu.Text = "Volver al Menú";
            this.btnVolverMenu.UseVisualStyleBackColor = true;
            this.btnVolverMenu.Click += new System.EventHandler(this.btnVolverMenu_Click);
            // 
            // lblPresupuestoActual
            // 
            this.lblPresupuestoActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresupuestoActual.Location = new System.Drawing.Point(530, 442);
            this.lblPresupuestoActual.Name = "lblPresupuestoActual";
            this.lblPresupuestoActual.Size = new System.Drawing.Size(430, 23);
            this.lblPresupuestoActual.TabIndex = 8;
            this.lblPresupuestoActual.Text = "Presupuesto: $0 / $100,000,000";
            this.lblPresupuestoActual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMercado
            // 
            this.lblMercado.AutoSize = true;
            this.lblMercado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMercado.Location = new System.Drawing.Point(12, 15);
            this.lblMercado.Name = "lblMercado";
            this.lblMercado.Size = new System.Drawing.Size(77, 20);
            this.lblMercado.TabIndex = 9;
            this.lblMercado.Text = "Mercado";
            // 
            // lblTitulares
            // 
            this.lblTitulares.AutoSize = true;
            this.lblTitulares.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulares.Location = new System.Drawing.Point(526, 15);
            this.lblTitulares.Name = "lblTitulares";
            this.lblTitulares.Size = new System.Drawing.Size(78, 20);
            this.lblTitulares.TabIndex = 10;
            this.lblTitulares.Text = "Titulares";
            // 
            // lblSuplentes
            // 
            this.lblSuplentes.AutoSize = true;
            this.lblSuplentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuplentes.Location = new System.Drawing.Point(526, 258);
            this.lblSuplentes.Name = "lblSuplentes";
            this.lblSuplentes.Size = new System.Drawing.Size(90, 20);
            this.lblSuplentes.TabIndex = 11;
            this.lblSuplentes.Text = "Suplentes";
            // 
            // lblPuntaje
            // 
            this.lblPuntaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPuntaje.Location = new System.Drawing.Point(530, 465);
            this.lblPuntaje.Name = "lblPuntaje";
            this.lblPuntaje.Size = new System.Drawing.Size(430, 23);
            this.lblPuntaje.TabIndex = 12;
            this.lblPuntaje.Text = "Puntaje Fecha: 0";
            this.lblPuntaje.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbConfig
            // 
            this.gbConfig.Controls.Add(this.btnGuardarConfig);
            this.gbConfig.Controls.Add(this.txtCantJugadores);
            this.gbConfig.Controls.Add(this.lblCantJugadores);
            this.gbConfig.Controls.Add(this.txtPresupuesto);
            this.gbConfig.Controls.Add(this.lblPresupuestoMax);
            this.gbConfig.Location = new System.Drawing.Point(12, 437);
            this.gbConfig.Name = "gbConfig";
            this.gbConfig.Size = new System.Drawing.Size(430, 89);
            this.gbConfig.TabIndex = 13;
            this.gbConfig.TabStop = false;
            this.gbConfig.Text = "Configuración de Plantilla";
            // 
            // btnGuardarConfig
            // 
            this.btnGuardarConfig.Location = new System.Drawing.Point(300, 35);
            this.btnGuardarConfig.Name = "btnGuardarConfig";
            this.btnGuardarConfig.Size = new System.Drawing.Size(110, 36);
            this.btnGuardarConfig.TabIndex = 4;
            this.btnGuardarConfig.Text = "Guardar Cambios";
            this.btnGuardarConfig.UseVisualStyleBackColor = true;
            this.btnGuardarConfig.Click += new System.EventHandler(this.btnGuardarConfig_Click);
            // 
            // txtCantJugadores
            // 
            this.txtCantJugadores.Location = new System.Drawing.Point(150, 51);
            this.txtCantJugadores.Name = "txtCantJugadores";
            this.txtCantJugadores.Size = new System.Drawing.Size(120, 20);
            this.txtCantJugadores.TabIndex = 3;
            // 
            // lblCantJugadores
            // 
            this.lblCantJugadores.AutoSize = true;
            this.lblCantJugadores.Location = new System.Drawing.Point(10, 54);
            this.lblCantJugadores.Name = "lblCantJugadores";
            this.lblCantJugadores.Size = new System.Drawing.Size(126, 13);
            this.lblCantJugadores.TabIndex = 2;
            this.lblCantJugadores.Text = "Max. Cant. Futbolistas:";
            // 
            // txtPresupuesto
            // 
            this.txtPresupuesto.Location = new System.Drawing.Point(150, 25);
            this.txtPresupuesto.Name = "txtPresupuesto";
            this.txtPresupuesto.Size = new System.Drawing.Size(120, 20);
            this.txtPresupuesto.TabIndex = 1;
            // 
            // lblPresupuestoMax
            // 
            this.lblPresupuestoMax.AutoSize = true;
            this.lblPresupuestoMax.Location = new System.Drawing.Point(10, 28);
            this.lblPresupuestoMax.Name = "lblPresupuestoMax";
            this.lblPresupuestoMax.Size = new System.Drawing.Size(111, 13);
            this.lblPresupuestoMax.TabIndex = 0;
            this.lblPresupuestoMax.Text = "Presupuesto Máximo:";
            // 
            // plantilla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 577);
            this.Controls.Add(this.gbConfig);
            this.Controls.Add(this.lblPuntaje);
            this.Controls.Add(this.lblSuplentes);
            this.Controls.Add(this.lblTitulares);
            this.Controls.Add(this.lblMercado);
            this.Controls.Add(this.lblPresupuestoActual);
            this.Controls.Add(this.btnVolverMenu);
            this.Controls.Add(this.btnQuitarSuplente);
            this.Controls.Add(this.btnQuitarTitular);
            this.Controls.Add(this.btnFicharSuplente);
            this.Controls.Add(this.btnFicharTitular);
            this.Controls.Add(this.dgvSuplentes);
            this.Controls.Add(this.dgvTitulares);
            this.Controls.Add(this.dgvMercado);
            this.Name = "plantilla";
            this.Text = "Gestión de Plantilla";
            this.Load += new System.EventHandler(this.plantilla_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMercado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTitulares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuplentes)).EndInit();
            this.gbConfig.ResumeLayout(false);
            this.gbConfig.PerformLayout();

            this.lblFormacionValida = new System.Windows.Forms.Label();
            // 
            // lblFormacionValida
            // 
            this.lblFormacionValida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormacionValida.Location = new System.Drawing.Point(530, 488); // Debajo de lblPuntaje
            this.lblFormacionValida.Name = "lblFormacionValida";
            this.lblFormacionValida.Size = new System.Drawing.Size(430, 23);
            this.lblFormacionValida.TabIndex = 14;
            this.lblFormacionValida.Text = "Formación: INVÁLIDA";
            this.lblFormacionValida.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFormacionValida.ForeColor = System.Drawing.Color.Red;

            this.Controls.Add(this.lblFormacionValida);


            this.ResumeLayout(false);
            this.PerformLayout();
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