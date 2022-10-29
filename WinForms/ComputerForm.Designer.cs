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
            this.producer = new System.Windows.Forms.TextBox();
            this.price = new System.Windows.Forms.TextBox();
            this.regNumber = new System.Windows.Forms.TextBox();
            this.amortPeriod = new System.Windows.Forms.TextBox();
            this.bodySize = new System.Windows.Forms.TextBox();
            this.memory = new System.Windows.Forms.TextBox();
            this.graphicsCard = new System.Windows.Forms.TextBox();
            this.ram = new System.Windows.Forms.TextBox();
            this.coresCount = new System.Windows.Forms.TextBox();
            this.cpu = new System.Windows.Forms.TextBox();
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
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.enableRedact = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(268, 22);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(250, 31);
            this.name.TabIndex = 0;
            // 
            // producer
            // 
            this.producer.Location = new System.Drawing.Point(268, 406);
            this.producer.Name = "producer";
            this.producer.Size = new System.Drawing.Size(250, 31);
            this.producer.TabIndex = 7;
            // 
            // price
            // 
            this.price.Location = new System.Drawing.Point(268, 350);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(250, 31);
            this.price.TabIndex = 6;
            // 
            // regNumber
            // 
            this.regNumber.Location = new System.Drawing.Point(268, 242);
            this.regNumber.Name = "regNumber";
            this.regNumber.Size = new System.Drawing.Size(250, 31);
            this.regNumber.TabIndex = 4;
            // 
            // amortPeriod
            // 
            this.amortPeriod.Location = new System.Drawing.Point(268, 884);
            this.amortPeriod.Name = "amortPeriod";
            this.amortPeriod.Size = new System.Drawing.Size(250, 31);
            this.amortPeriod.TabIndex = 15;
            // 
            // bodySize
            // 
            this.bodySize.Location = new System.Drawing.Point(268, 770);
            this.bodySize.Name = "bodySize";
            this.bodySize.Size = new System.Drawing.Size(250, 31);
            this.bodySize.TabIndex = 13;
            // 
            // memory
            // 
            this.memory.Location = new System.Drawing.Point(268, 710);
            this.memory.Name = "memory";
            this.memory.Size = new System.Drawing.Size(250, 31);
            this.memory.TabIndex = 12;
            // 
            // graphicsCard
            // 
            this.graphicsCard.Location = new System.Drawing.Point(268, 650);
            this.graphicsCard.Name = "graphicsCard";
            this.graphicsCard.Size = new System.Drawing.Size(250, 31);
            this.graphicsCard.TabIndex = 11;
            // 
            // ram
            // 
            this.ram.Location = new System.Drawing.Point(268, 594);
            this.ram.Name = "ram";
            this.ram.Size = new System.Drawing.Size(250, 31);
            this.ram.TabIndex = 10;
            // 
            // coresCount
            // 
            this.coresCount.Location = new System.Drawing.Point(268, 527);
            this.coresCount.Name = "coresCount";
            this.coresCount.Size = new System.Drawing.Size(250, 31);
            this.coresCount.TabIndex = 9;
            // 
            // cpu
            // 
            this.cpu.Location = new System.Drawing.Point(268, 465);
            this.cpu.Name = "cpu";
            this.cpu.Size = new System.Drawing.Size(250, 31);
            this.cpu.TabIndex = 8;
            // 
            // location
            // 
            this.location.FormattingEnabled = true;
            this.location.Location = new System.Drawing.Point(268, 75);
            this.location.Name = "location";
            this.location.Size = new System.Drawing.Size(250, 33);
            this.location.TabIndex = 16;
            // 
            // status
            // 
            this.status.FormattingEnabled = true;
            this.status.Location = new System.Drawing.Point(268, 131);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(250, 33);
            this.status.TabIndex = 17;
            // 
            // employee
            // 
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
            this.explStart.Location = new System.Drawing.Point(268, 818);
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
            this.label2.Size = new System.Drawing.Size(63, 25);
            this.label2.TabIndex = 22;
            this.label2.Text = "Место";
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
            this.label5.Size = new System.Drawing.Size(183, 25);
            this.label5.TabIndex = 25;
            this.label5.Text = "Инвентарный номер";
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 412);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 25);
            this.label8.TabIndex = 28;
            this.label8.Text = "Производитель";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 471);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 25);
            this.label9.TabIndex = 29;
            this.label9.Text = "Процессор";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(26, 518);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(151, 50);
            this.label10.TabIndex = 30;
            this.label10.Text = "Количество ядер\r\nпроцессора\r\n";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(26, 585);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(182, 50);
            this.label11.TabIndex = 31;
            this.label11.Text = "Объем оперативной\r\nпамяти (гб.)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(26, 656);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(194, 25);
            this.label12.TabIndex = 32;
            this.label12.Text = "Название видеокарты";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(26, 700);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(171, 50);
            this.label13.TabIndex = 33;
            this.label13.Text = "Объем постоянной\r\nпамяти (тб.)\r\n";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(26, 776);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(200, 25);
            this.label14.TabIndex = 34;
            this.label14.Text = "Размеры корпуса (см²)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(26, 824);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(224, 25);
            this.label15.TabIndex = 35;
            this.label15.Text = "Дата начала эксплуатации";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(26, 874);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(168, 50);
            this.label16.TabIndex = 36;
            this.label16.Text = "Срок амортизации\r\n(кол-во месяцев)";
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(406, 935);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(112, 54);
            this.close.TabIndex = 37;
            this.close.Text = "Выход";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.Close_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(268, 935);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(112, 54);
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
            // ComputerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 1001);
            this.Controls.Add(this.enableRedact);
            this.Controls.Add(this.update);
            this.Controls.Add(this.close);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
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
            this.Controls.Add(this.amortPeriod);
            this.Controls.Add(this.bodySize);
            this.Controls.Add(this.memory);
            this.Controls.Add(this.graphicsCard);
            this.Controls.Add(this.ram);
            this.Controls.Add(this.coresCount);
            this.Controls.Add(this.cpu);
            this.Controls.Add(this.producer);
            this.Controls.Add(this.price);
            this.Controls.Add(this.regNumber);
            this.Controls.Add(this.name);
            this.Name = "ComputerForm";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.ComputerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private TextBox name;
    private TextBox producer;
    private TextBox price;
    private TextBox regNumber;
    private TextBox amortPeriod;
    private TextBox bodySize;
    private TextBox memory;
    private TextBox graphicsCard;
    private TextBox ram;
    private TextBox coresCount;
    private TextBox cpu;
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
    private Label label8;
    private Label label9;
    private Label label10;
    private Label label11;
    private Label label12;
    private Label label13;
    private Label label14;
    private Label label15;
    private Label label16;
    private Button close;
    private Button update;
    private CheckBox enableRedact;
}
