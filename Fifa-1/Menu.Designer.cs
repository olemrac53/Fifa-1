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
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.lblSeleccionar = new System.Windows.Forms.Label();
            this.cmbPlantillas = new System.Windows.Forms.ComboBox();
            this.btnGestionarPlantilla = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienvenida.Location = new System.Drawing.Point(30, 30);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(158, 24);
            this.lblBienvenida.TabIndex = 0;
            this.lblBienvenida.Text = "Hola, [Usuario]!";
            // 
            // lblSeleccionar
            // 
            this.lblSeleccionar.AutoSize = true;
            this.lblSeleccionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeleccionar.Location = new System.Drawing.Point(30, 80);
            this.lblSeleccionar.Name = "lblSeleccionar";
            this.lblSeleccionar.Size = new System.Drawing.Size(147, 16);
            this.lblSeleccionar.TabIndex = 1;
            this.lblSeleccionar.Text = "Selecciona tu plantilla:";
            // 
            // cmbPlantillas
            // 
            this.cmbPlantillas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlantillas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPlantillas.FormattingEnabled = true;
            this.cmbPlantillas.Location = new System.Drawing.Point(33, 100);
            this.cmbPlantillas.Name = "cmbPlantillas";
            this.cmbPlantillas.Size = new System.Drawing.Size(320, 24);
            this.cmbPlantillas.TabIndex = 2;
            // 
            // btnGestionarPlantilla
            // 
            this.btnGestionarPlantilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionarPlantilla.Location = new System.Drawing.Point(33, 140);
            this.btnGestionarPlantilla.Name = "btnGestionarPlantilla";
            this.btnGestionarPlantilla.Size = new System.Drawing.Size(160, 40);
            this.btnGestionarPlantilla.TabIndex = 3;
            this.btnGestionarPlantilla.Text = "Gestionar Plantilla";
            this.btnGestionarPlantilla.UseVisualStyleBackColor = true;
            this.btnGestionarPlantilla.Click += new System.EventHandler(this.btnGestionarPlantilla_Click);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Location = new System.Drawing.Point(260, 210);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(93, 23);
            this.btnCerrarSesion.TabIndex = 4;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.btnGestionarPlantilla);
            this.Controls.Add(this.cmbPlantillas);
            this.Controls.Add(this.lblSeleccionar);
            this.Controls.Add(this.lblBienvenida);
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menú Principal";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Label lblSeleccionar;
        private System.Windows.Forms.ComboBox cmbPlantillas;
        private System.Windows.Forms.Button btnGestionarPlantilla;
        private System.Windows.Forms.Button btnCerrarSesion;
    }
}