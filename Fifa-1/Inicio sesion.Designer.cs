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
            label1 = new Label();
            groupBox1 = new GroupBox();
            listBox2 = new ListBox();
            listBox1 = new ListBox();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Leelawadee", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(262, 42);
            label1.Name = "label1";
            label1.Size = new Size(274, 39);
            label1.TabIndex = 0;
            label1.Text = "Login de Gran Dt";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ControlDarkDark;
            groupBox1.Controls.Add(listBox2);
            groupBox1.Controls.Add(listBox1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(247, 101);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(292, 230);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(15, 146);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(232, 34);
            listBox2.TabIndex = 5;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(15, 57);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(232, 34);
            listBox1.TabIndex = 2;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Emoji", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Gold;
            label2.Location = new Point(15, 117);
            label2.Name = "label2";
            label2.Size = new Size(89, 26);
            label2.TabIndex = 4;
            label2.Text = "Usuario:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Emoji", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Gold;
            label3.Location = new Point(15, 28);
            label3.Name = "label3";
            label3.Size = new Size(89, 26);
            label3.TabIndex = 3;
            label3.Text = "Usuario:";
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
            // Inicio_sesion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Name = "Inicio_sesion";
            Text = "Inicio_sesion";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private Label label2;
        private Label label3;
        private ListBox listBox1;
        private ListBox listBox2;
        private Button button1;
    }
}