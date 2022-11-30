namespace Accounting
{
    partial class RegistrationForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.phone = new System.Windows.Forms.TextBox();
            this.login = new System.Windows.Forms.TextBox();
            this.send = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.position = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Номер телефона";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Должность";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Логин";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(187, 33);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(281, 31);
            this.name.TabIndex = 7;
            // 
            // phone
            // 
            this.phone.Location = new System.Drawing.Point(187, 98);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(281, 31);
            this.phone.TabIndex = 8;
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(187, 235);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(281, 31);
            this.login.TabIndex = 10;
            // 
            // send
            // 
            this.send.Location = new System.Drawing.Point(104, 379);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(124, 34);
            this.send.TabIndex = 13;
            this.send.Text = "Регистрация";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.Send_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(308, 379);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(124, 34);
            this.close.TabIndex = 14;
            this.close.Text = "Выйти";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.Close_Click);
            // 
            // position
            // 
            this.position.FormattingEnabled = true;
            this.position.Location = new System.Drawing.Point(187, 165);
            this.position.Name = "position";
            this.position.Size = new System.Drawing.Size(281, 33);
            this.position.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 25);
            this.label2.TabIndex = 16;
            this.label2.Text = "Пароль";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(187, 306);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(281, 31);
            this.password.TabIndex = 17;
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 435);
            this.Controls.Add(this.password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.position);
            this.Controls.Add(this.close);
            this.Controls.Add(this.send);
            this.Controls.Add(this.login);
            this.Controls.Add(this.phone);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "RegistrationForm";
            this.Text = "Регистрация";
            this.Load += new System.EventHandler(this.RegistrationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox name;
        private TextBox phone;
        private TextBox login;
        private Button send;
        private Button close;
        private ComboBox position;
        private Label label2;
        private TextBox password;
    }
}