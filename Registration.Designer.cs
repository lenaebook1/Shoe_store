namespace WindowsFormsApp1
{
    partial class Registration
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Chartreuse;
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(557, 742);
            this.panel1.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(222, 529);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(124, 26);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.Text = "Я не робот";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.button3.Location = new System.Drawing.Point(98, 431);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(197, 38);
            this.button3.TabIndex = 15;
            this.button3.Text = "Подтвердить пароль";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(94, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 22);
            this.label5.TabIndex = 14;
            this.label5.Text = "ФИО";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(98, 95);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(352, 30);
            this.textBox4.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.button1.Location = new System.Drawing.Point(190, 669);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(197, 57);
            this.button1.TabIndex = 12;
            this.button1.Text = "Есть аккаунт? Войти...";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(94, 370);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(229, 22);
            this.label4.TabIndex = 11;
            this.label4.Text = "Введите пароль повторно";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(98, 395);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(352, 30);
            this.textBox3.TabIndex = 10;
            this.textBox3.UseSystemPasswordChar = true;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.button2.Location = new System.Drawing.Point(190, 606);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(197, 57);
            this.button2.TabIndex = 9;
            this.button2.Text = "Зарегестрироваться";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 274);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "Пароль";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(98, 299);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(352, 30);
            this.textBox2.TabIndex = 4;
            this.textBox2.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Логин";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(98, 198);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(352, 30);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Регистрация";
            // 
            // Registration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 766);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Registration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ООО \"Обувь\"";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}