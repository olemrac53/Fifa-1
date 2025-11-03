namespace Fifa_1
{
    partial class Jugador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Jugador));
            dataGridView1 = new DataGridView();
            Salir = new Button();
            button2 = new Button();
            button3 = new Button();
            Posicion = new DataGridViewTextBoxColumn();
            POS = new DataGridViewTextBoxColumn();
            Nombre = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            rwsf = new DataGridViewTextBoxColumn();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Posicion, POS, Nombre, Column1, Column2, rwsf });
            dataGridView1.Location = new Point(88, 21);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(642, 150);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // Salir
            // 
            Salir.Location = new Point(32, 390);
            Salir.Name = "Salir";
            Salir.Size = new Size(75, 23);
            Salir.TabIndex = 1;
            Salir.Text = "Salir";
            Salir.UseVisualStyleBackColor = true;
            Salir.Click += Salir_Click;
            // 
            // button2
            // 
            button2.Location = new Point(146, 251);
            button2.Name = "button2";
            button2.Size = new Size(126, 23);
            button2.TabIndex = 2;
            button2.Text = "Agregar Jugador";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(346, 251);
            button3.Name = "button3";
            button3.Size = new Size(127, 23);
            button3.TabIndex = 3;
            button3.Text = "Editar Jugador";
            button3.UseVisualStyleBackColor = true;
            // 
            // Posicion
            // 
            Posicion.HeaderText = "Posicion";
            Posicion.Name = "Posicion";
            Posicion.ReadOnly = true;
            // 
            // POS
            // 
            POS.HeaderText = "Nombre";
            POS.Name = "POS";
            POS.ReadOnly = true;
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Apellido";
            Nombre.Name = "Nombre";
            Nombre.ReadOnly = true;
            // 
            // Column1
            // 
            Column1.HeaderText = "Apodo";
            Column1.Name = "Column1";
            // 
            // Column2
            // 
            Column2.HeaderText = "Numero de Camiseta";
            Column2.Name = "Column2";
            // 
            // rwsf
            // 
            rwsf.HeaderText = "Equipo";
            rwsf.Name = "rwsf";
            // 
            // button1
            // 
            button1.Location = new Point(552, 251);
            button1.Name = "button1";
            button1.Size = new Size(134, 23);
            button1.TabIndex = 4;
            button1.Text = "Eliminar Jugador";
            button1.UseVisualStyleBackColor = true;
            // 
            // Jugador
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(Salir);
            Controls.Add(dataGridView1);
            Name = "Jugador";
            Text = "Jugador";
            Load += Jugador_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button Salir;
        private Button button2;
        private Button button3;
        private DataGridViewTextBoxColumn Posicion;
        private DataGridViewTextBoxColumn POS;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn rwsf;
        private Button button1;
    }
}