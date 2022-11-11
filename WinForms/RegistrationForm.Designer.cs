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
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.phone = new System.Windows.Forms.TextBox();
            this.login = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.confirmation = new System.Windows.Forms.TextBox();
            this.send = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.position = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Номер телефона";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Должность";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Логин";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 25);
            this.label6.TabIndex = 5;
            this.label6.Text = "Пароль";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 281);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 50);
            this.label7.TabIndex = 6;
            this.label7.Text = "Подтверждение\r\nпароля\r\n";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(187, 12);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(281, 31);
            this.name.TabIndex = 7;
            // 
            // phone
            // 
            this.phone.Location = new System.Drawing.Point(187, 68);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(281, 31);
            this.phone.TabIndex = 8;
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(187, 174);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(281, 31);
            this.login.TabIndex = 10;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(187, 227);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(281, 31);
            this.password.TabIndex = 11;
            // 
            // confirmation
            // 
            this.confirmation.Location = new System.Drawing.Point(187, 291);
            this.confirmation.Name = "confirmation";
            this.confirmation.PasswordChar = '*';
            this.confirmation.Size = new System.Drawing.Size(281, 31);
            this.confirmation.TabIndex = 12;
            // 
            // send
            // 
            this.send.Location = new System.Drawing.Point(187, 347);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(124, 34);
            this.send.TabIndex = 13;
            this.send.Text = "Регистрация";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.Send_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(344, 347);
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
            this.position.Location = new System.Drawing.Point(187, 122);
            this.position.Name = "position";
            this.position.Size = new System.Drawing.Size(281, 33);
            this.position.TabIndex = 15;
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 393);
            this.Controls.Add(this.position);
            this.Controls.Add(this.close);
            this.Controls.Add(this.send);
            this.Controls.Add(this.confirmation);
            this.Controls.Add(this.password);
            this.Controls.Add(this.login);
            this.Controls.Add(this.phone);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
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
        private Label label6;
        private Label label7;
        private TextBox name;
        private TextBox phone;
        private TextBox login;
        private TextBox password;
        private TextBox confirmation;
        private Button send;
        private Button close;
        private ComboBox position;
    }
}