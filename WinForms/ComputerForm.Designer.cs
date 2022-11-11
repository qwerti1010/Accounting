namespace Accounting;

partial class ComputerForm
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
            this.name = new System.Windows.Forms.TextBox();
            this.price = new System.Windows.Forms.TextBox();
            this.location = new System.Windows.Forms.ComboBox();
            this.status = new System.Windows.Forms.ComboBox();
            this.employee = new System.Windows.Forms.ComboBox();
            this.regDate = new System.Windows.Forms.DateTimePicker();
            this.explStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.enableRedact = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ram = new System.Windows.Forms.ComboBox();
            this.graphicsCard = new System.Windows.Forms.ComboBox();
            this.memory = new System.Windows.Forms.ComboBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.cpu = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(268, 22);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(250, 31);
            this.name.TabIndex = 0;
            // 
            // price
            // 
            this.price.Location = new System.Drawing.Point(268, 350);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(250, 31);
            this.price.TabIndex = 6;
            // 
            // location
            // 
            this.location.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.location.FormattingEnabled = true;
            this.location.Location = new System.Drawing.Point(268, 75);
            this.location.Name = "location";
            this.location.Size = new System.Drawing.Size(250, 33);
            this.location.TabIndex = 16;
            // 
            // status
            // 
            this.status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.status.FormattingEnabled = true;
            this.status.Location = new System.Drawing.Point(268, 131);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(250, 33);
            this.status.TabIndex = 17;
            // 
            // employee
            // 
            this.employee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.employee.FormattingEnabled = true;
            this.employee.Location = new System.Drawing.Point(268, 185);
            this.employee.Name = "employee";
            this.employee.Size = new System.Drawing.Size(250, 33);
            this.employee.TabIndex = 18;
            // 
            // regDate
            // 
            this.regDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.regDate.Location = new System.Drawing.Point(268, 296);
            this.regDate.Name = "regDate";
            this.regDate.Size = new System.Drawing.Size(250, 31);
            this.regDate.TabIndex = 19;
            // 
            // explStart
            // 
            this.explStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.explStart.Location = new System.Drawing.Point(268, 690);
            this.explStart.Name = "explStart";
            this.explStart.Size = new System.Drawing.Size(250, 31);
            this.explStart.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 25);
            this.label1.TabIndex = 21;
            this.label1.Text = "Название";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 25);
            this.label2.TabIndex = 22;
            this.label2.Text = "Материнская плата";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 25);
            this.label3.TabIndex = 23;
            this.label3.Text = "Статус";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 25);
            this.label4.TabIndex = 24;
            this.label4.Text = "Сотрудник";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 248);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 25);
            this.label5.TabIndex = 25;
            this.label5.Text = "Блок питания";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 302);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(213, 25);
            this.label6.TabIndex = 26;
            this.label6.Text = "Дата постановки на учет";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 356);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 25);
            this.label7.TabIndex = 27;
            this.label7.Text = "Цена (руб.)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 412);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 25);
            this.label9.TabIndex = 29;
            this.label9.Text = "Процессор";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(26, 469);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(182, 25);
            this.label11.TabIndex = 31;
            this.label11.Text = "Оперативная память";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(26, 528);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(107, 25);
            this.label12.TabIndex = 32;
            this.label12.Text = "Видеокарта";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(26, 585);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(156, 25);
            this.label13.TabIndex = 33;
            this.label13.Text = "Хранение данных";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(26, 645);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 25);
            this.label14.TabIndex = 34;
            this.label14.Text = "Корпус";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(26, 696);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(224, 25);
            this.label15.TabIndex = 35;
            this.label15.Text = "Дата начала эксплуатации";
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(426, 776);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(143, 66);
            this.close.TabIndex = 37;
            this.close.Text = "Выход";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.Close_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(214, 776);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(143, 66);
            this.update.TabIndex = 38;
            this.update.Text = "Изменить";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.Update_Click);
            // 
            // enableRedact
            // 
            this.enableRedact.AutoSize = true;
            this.enableRedact.Location = new System.Drawing.Point(524, 287);
            this.enableRedact.Name = "enableRedact";
            this.enableRedact.Size = new System.Drawing.Size(173, 54);
            this.enableRedact.TabIndex = 40;
            this.enableRedact.Text = "Включить\r\nредактирование";
            this.enableRedact.UseVisualStyleBackColor = true;
            this.enableRedact.CheckStateChanged += new System.EventHandler(this.EnableRedact_CheckStateChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(268, 240);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(250, 33);
            this.comboBox1.TabIndex = 41;
            // 
            // ram
            // 
            this.ram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ram.FormattingEnabled = true;
            this.ram.Location = new System.Drawing.Point(268, 461);
            this.ram.Name = "ram";
            this.ram.Size = new System.Drawing.Size(250, 33);
            this.ram.TabIndex = 43;
            // 
            // graphicsCard
            // 
            this.graphicsCard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.graphicsCard.FormattingEnabled = true;
            this.graphicsCard.Location = new System.Drawing.Point(268, 520);
            this.graphicsCard.Name = "graphicsCard";
            this.graphicsCard.Size = new System.Drawing.Size(250, 33);
            this.graphicsCard.TabIndex = 44;
            // 
            // memory
            // 
            this.memory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.memory.FormattingEnabled = true;
            this.memory.Location = new System.Drawing.Point(268, 577);
            this.memory.Name = "memory";
            this.memory.Size = new System.Drawing.Size(250, 33);
            this.memory.TabIndex = 45;
            // 
            // comboBox6
            // 
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new System.Drawing.Point(268, 637);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(250, 33);
            this.comboBox6.TabIndex = 46;
            // 
            // cpu
            // 
            this.cpu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cpu.FormattingEnabled = true;
            this.cpu.Location = new System.Drawing.Point(268, 409);
            this.cpu.Name = "cpu";
            this.cpu.Size = new System.Drawing.Size(250, 33);
            this.cpu.TabIndex = 47;
            // 
            // ComputerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 858);
            this.Controls.Add(this.cpu);
            this.Controls.Add(this.comboBox6);
            this.Controls.Add(this.memory);
            this.Controls.Add(this.graphicsCard);
            this.Controls.Add(this.ram);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.enableRedact);
            this.Controls.Add(this.update);
            this.Controls.Add(this.close);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.explStart);
            this.Controls.Add(this.regDate);
            this.Controls.Add(this.employee);
            this.Controls.Add(this.status);
            this.Controls.Add(this.location);
            this.Controls.Add(this.price);
            this.Controls.Add(this.name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ComputerForm";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.ComputerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private TextBox name;
    private TextBox price;
    private ComboBox location;
    private ComboBox status;
    private ComboBox employee;
    private DateTimePicker regDate;
    private DateTimePicker explStart;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private Label label7;
    private Label label9;
    private Label label11;
    private Label label12;
    private Label label13;
    private Label label14;
    private Label label15;
    private Button close;
    private Button update;
    private CheckBox enableRedact;
    private ComboBox comboBox1;
    private ComboBox ram;
    private ComboBox graphicsCard;
    private ComboBox memory;
    private ComboBox comboBox6;
    private ComboBox cpu;
}
