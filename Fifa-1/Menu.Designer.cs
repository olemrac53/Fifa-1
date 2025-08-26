namespace Fifa_1
{
    partial class Menu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            button2 = new Button();
            dataGridView2 = new DataGridView();
            button3 = new Button();
            dataGridView4 = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).BeginInit();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Location = new Point(166, 354);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 13;
            button2.Text = "Equipo";
            button2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(157, 337);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(93, 55);
            dataGridView2.TabIndex = 12;
            dataGridView2.Visible = false;
            // 
            // button3
            // 
            button3.Location = new Point(626, 354);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 15;
            button3.Text = "Jugador";
            button3.UseVisualStyleBackColor = true;
            // 
            // dataGridView4
            // 
            dataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView4.Location = new Point(617, 337);
            dataGridView4.Name = "dataGridView4";
            dataGridView4.Size = new Size(93, 55);
            dataGridView4.TabIndex = 14;
            dataGridView4.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Myanmar Text", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Image = (Image)resources.GetObject("label1.Image");
            label1.ImageAlign = ContentAlignment.TopCenter;
            label1.Location = new Point(324, -2);
            label1.Name = "label1";
            label1.Size = new Size(146, 56);
            label1.TabIndex = 17;
            label1.Text = "Gran D.T";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(539, 425);
            label2.Name = "label2";
            label2.Size = new Size(246, 15);
            label2.TabIndex = 18;
            label2.Text = "Hecho por: Torren Ruben y Carmelo Gonzalez";
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(791, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(dataGridView4);
            Controls.Add(button2);
            Controls.Add(dataGridView2);
            Name = "Menu";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button button2;
        private DataGridView dataGridView2;
        private Button button3;
        private DataGridView dataGridView4;
        private Label label1;
        private Label label2;
    }
}





