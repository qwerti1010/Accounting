namespace Accounting;

partial class LoginForm
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
            this.signUp = new System.Windows.Forms.Button();
            this.passTextBox = new System.Windows.Forms.TextBox();
            this.passLabel = new System.Windows.Forms.Label();
            this.loginTextBox = new System.Windows.Forms.TextBox();
            this.loginLabel = new System.Windows.Forms.Label();
            this.registration = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // signUp
            // 
            this.signUp.Location = new System.Drawing.Point(101, 244);
            this.signUp.Name = "signUp";
            this.signUp.Size = new System.Drawing.Size(125, 43);
            this.signUp.TabIndex = 2;
            this.signUp.Text = "Войти";
            this.signUp.UseVisualStyleBackColor = true;
            this.signUp.Click += new System.EventHandler(this.SignUp_Click);
            // 
            // passTextBox
            // 
            this.passTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.passTextBox.Location = new System.Drawing.Point(54, 176);
            this.passTextBox.MaxLength = 16;
            this.passTextBox.Name = "passTextBox";
            this.passTextBox.PasswordChar = '*';
            this.passTextBox.Size = new System.Drawing.Size(223, 31);
            this.passTextBox.TabIndex = 1;
            // 
            // passLabel
            // 
            this.passLabel.AutoSize = true;
            this.passLabel.Location = new System.Drawing.Point(54, 148);
            this.passLabel.Name = "passLabel";
            this.passLabel.Size = new System.Drawing.Size(78, 25);
            this.passLabel.TabIndex = 2;
            this.passLabel.Text = "Пароль:";
            // 
            // loginTextBox
            // 
            this.loginTextBox.Location = new System.Drawing.Point(54, 98);
            this.loginTextBox.Name = "loginTextBox";
            this.loginTextBox.Size = new System.Drawing.Size(223, 31);
            this.loginTextBox.TabIndex = 0;
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Location = new System.Drawing.Point(54, 70);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(66, 25);
            this.loginLabel.TabIndex = 4;
            this.loginLabel.Text = "Логин:";
            // 
            // registration
            // 
            this.registration.AutoSize = true;
            this.registration.Location = new System.Drawing.Point(73, 303);
            this.registration.Name = "registration";
            this.registration.Size = new System.Drawing.Size(177, 25);
            this.registration.TabIndex = 5;
            this.registration.TabStop = true;
            this.registration.Text = "Зарегестрироваться";
            this.registration.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Registration_LinkClicked);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(343, 350);
            this.Controls.Add(this.registration);
            this.Controls.Add(this.loginLabel);
            this.Controls.Add(this.loginTextBox);
            this.Controls.Add(this.passLabel);
            this.Controls.Add(this.passTextBox);
            this.Controls.Add(this.signUp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();
    }

    #endregion

    private Button signUp;
    private TextBox passTextBox;
    private Label passLabel;
    private TextBox loginTextBox;
    private Label loginLabel;
    private LinkLabel registration;
}
