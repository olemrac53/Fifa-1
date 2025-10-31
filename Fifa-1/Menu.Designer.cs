namespace Fifa_1
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            lblBienvenida = new Label();
            lblSeleccionar = new Label();
            cmbPlantillas = new ComboBox();
            btnGestionarPlantilla = new Button();
            btnCerrarSesion = new Button();
            btnCrearPlantilla = new Button();
            btnEliminarPlantilla = new Button();
            btnAdminJugadores = new Button();
            SuspendLayout();
            // 
            // lblBienvenida
            // 
            lblBienvenida.AutoSize = true;
            lblBienvenida.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBienvenida.Location = new Point(35, 35);
            lblBienvenida.Margin = new Padding(4, 0, 4, 0);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(154, 24);
            lblBienvenida.TabIndex = 0;
            lblBienvenida.Text = "Hola, [Usuario]!";
            // 
            // lblSeleccionar
            // 
            lblSeleccionar.AutoSize = true;
            lblSeleccionar.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSeleccionar.Location = new Point(35, 92);
            lblSeleccionar.Margin = new Padding(4, 0, 4, 0);
            lblSeleccionar.Name = "lblSeleccionar";
            lblSeleccionar.Size = new Size(140, 16);
            lblSeleccionar.TabIndex = 1;
            lblSeleccionar.Text = "Selecciona tu plantilla:";
            // 
            // cmbPlantillas
            // 
            cmbPlantillas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlantillas.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbPlantillas.FormattingEnabled = true;
            cmbPlantillas.Location = new Point(38, 115);
            cmbPlantillas.Margin = new Padding(4, 3, 4, 3);
            cmbPlantillas.Name = "cmbPlantillas";
            cmbPlantillas.Size = new Size(373, 24);
            cmbPlantillas.TabIndex = 2;
            // 
            // btnGestionarPlantilla
            // 
            btnGestionarPlantilla.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGestionarPlantilla.Location = new Point(38, 162);
            btnGestionarPlantilla.Margin = new Padding(4, 3, 4, 3);
            btnGestionarPlantilla.Name = "btnGestionarPlantilla";
            btnGestionarPlantilla.Size = new Size(117, 46);
            btnGestionarPlantilla.TabIndex = 3;
            btnGestionarPlantilla.Text = "Gestionar";
            btnGestionarPlantilla.UseVisualStyleBackColor = true;
            btnGestionarPlantilla.Click += btnGestionarPlantilla_Click;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Location = new Point(303, 242);
            btnCerrarSesion.Margin = new Padding(4, 3, 4, 3);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(108, 27);
            btnCerrarSesion.TabIndex = 4;
            btnCerrarSesion.Text = "Cerrar Sesión";
            btnCerrarSesion.UseVisualStyleBackColor = true;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // btnCrearPlantilla
            // 
            btnCrearPlantilla.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCrearPlantilla.Location = new Point(162, 162);
            btnCrearPlantilla.Margin = new Padding(4, 3, 4, 3);
            btnCrearPlantilla.Name = "btnCrearPlantilla";
            btnCrearPlantilla.Size = new Size(117, 46);
            btnCrearPlantilla.TabIndex = 5;
            btnCrearPlantilla.Text = "Crear";
            btnCrearPlantilla.UseVisualStyleBackColor = true;
            btnCrearPlantilla.Click += btnCrearPlantilla_Click;
            // 
            // btnEliminarPlantilla
            // 
            btnEliminarPlantilla.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEliminarPlantilla.Location = new Point(286, 162);
            btnEliminarPlantilla.Margin = new Padding(4, 3, 4, 3);
            btnEliminarPlantilla.Name = "btnEliminarPlantilla";
            btnEliminarPlantilla.Size = new Size(126, 46);
            btnEliminarPlantilla.TabIndex = 6;
            btnEliminarPlantilla.Text = "Eliminar";
            btnEliminarPlantilla.UseVisualStyleBackColor = true;
            btnEliminarPlantilla.Click += btnEliminarPlantilla_Click;
            // 
            // btnAdminJugadores
            // 
            btnAdminJugadores.Location = new Point(38, 242);
            btnAdminJugadores.Margin = new Padding(4, 3, 4, 3);
            btnAdminJugadores.Name = "btnAdminJugadores";
            btnAdminJugadores.Size = new Size(152, 27);
            btnAdminJugadores.TabIndex = 7;
            btnAdminJugadores.Text = "Admin. Jugadores";
            btnAdminJugadores.UseVisualStyleBackColor = true;
            btnAdminJugadores.Click += btnAdminJugadores_Click;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(448, 301);
            Controls.Add(btnAdminJugadores);
            Controls.Add(btnEliminarPlantilla);
            Controls.Add(btnCrearPlantilla);
            Controls.Add(btnCerrarSesion);
            Controls.Add(btnGestionarPlantilla);
            Controls.Add(cmbPlantillas);
            Controls.Add(lblSeleccionar);
            Controls.Add(lblBienvenida);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menú Principal";
            Load += Menu_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Label lblSeleccionar;
        private System.Windows.Forms.ComboBox cmbPlantillas;
        private System.Windows.Forms.Button btnGestionarPlantilla;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Button btnCrearPlantilla;
        private System.Windows.Forms.Button btnEliminarPlantilla;
        private System.Windows.Forms.Button btnAdminJugadores;
    }
}