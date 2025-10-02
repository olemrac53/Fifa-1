namespace Fifa_1
{
    partial class Inicio_sesion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio_sesion));
            label1 = new Label();
            button1 = new Button();
            label3 = new Label();
            label2 = new Label();
            listBox1 = new ListBox();
            listBox2 = new ListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(262, 22);
            label1.Name = "label1";
            label1.Size = new Size(278, 37);
            label1.TabIndex = 0;
            label1.Text = "Login de Gran Dt";
            // 
            // button1
            // 
            button1.Location = new Point(330, 357);
            button1.Name = "button1";
            button1.Size = new Size(146, 39);
            button1.TabIndex = 2;
            button1.Text = "Continuar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Emoji", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(262, 77);
            label3.Name = "label3";
            label3.Size = new Size(89, 26);
            label3.TabIndex = 3;
            label3.Text = "Usuario:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Emoji", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(262, 253);
            label2.Name = "label2";
            label2.Size = new Size(89, 26);
            label2.TabIndex = 4;
            label2.Text = "Usuario:";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(262, 116);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(232, 34);
            listBox1.TabIndex = 2;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(262, 291);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(232, 34);
            listBox2.TabIndex = 5;
            // 
            // Inicio_sesion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(listBox1);
            Controls.Add(label2);
            Controls.Add(listBox2);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(label1);
            Name = "Inicio_sesion";
            Text = "Inicio_sesion";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private Label label3;
        private Label label2;
        private ListBox listBox1;
        private ListBox listBox2;
    }
}