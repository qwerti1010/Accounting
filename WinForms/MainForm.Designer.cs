namespace Accounting;

partial class MainForm
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
            this.mainFormTabPage = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.deleteComputer = new System.Windows.Forms.Button();
            this.getComputer = new System.Windows.Forms.Button();
            this.chengeOperation = new System.Windows.Forms.Button();
            this.addComputer = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.deleteEmployee = new System.Windows.Forms.Button();
            this.addEmployee = new System.Windows.Forms.Button();
            this.updateEmployee = new System.Windows.Forms.Button();
            this.phoneTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.status = new System.Windows.Forms.ComboBox();
            this.regCheckBox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.regDatePicker = new System.Windows.Forms.DateTimePicker();
            this.applyFilters = new System.Windows.Forms.Button();
            this.price = new System.Windows.Forms.TextBox();
            this.computerName = new System.Windows.Forms.TextBox();
            this.Статус = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.position = new System.Windows.Forms.ComboBox();
            this.mainFormTabPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // mainFormTabPage
            // 
            this.mainFormTabPage.Controls.Add(this.tabPage1);
            this.mainFormTabPage.Controls.Add(this.tabPage2);
            this.mainFormTabPage.Controls.Add(this.tabPage3);
            this.mainFormTabPage.Controls.Add(this.tabPage4);
            this.mainFormTabPage.Location = new System.Drawing.Point(-3, -3);
            this.mainFormTabPage.Name = "mainFormTabPage";
            this.mainFormTabPage.SelectedIndex = 0;
            this.mainFormTabPage.Size = new System.Drawing.Size(989, 325);
            this.mainFormTabPage.TabIndex = 0;
            this.mainFormTabPage.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.TabPage_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.deleteComputer);
            this.tabPage1.Controls.Add(this.getComputer);
            this.tabPage1.Controls.Add(this.chengeOperation);
            this.tabPage1.Controls.Add(this.addComputer);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(981, 287);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Компьютеры";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // deleteComputer
            // 
            this.deleteComputer.Enabled = false;
            this.deleteComputer.Location = new System.Drawing.Point(580, 148);
            this.deleteComputer.Name = "deleteComputer";
            this.deleteComputer.Size = new System.Drawing.Size(220, 111);
            this.deleteComputer.TabIndex = 5;
            this.deleteComputer.Text = "Удалить устройство";
            this.deleteComputer.UseVisualStyleBackColor = true;
            this.deleteComputer.Click += new System.EventHandler(this.DeleteComputer_Click);
            // 
            // getComputer
            // 
            this.getComputer.Enabled = false;
            this.getComputer.Location = new System.Drawing.Point(156, 148);
            this.getComputer.Name = "getComputer";
            this.getComputer.Size = new System.Drawing.Size(220, 111);
            this.getComputer.TabIndex = 3;
            this.getComputer.Text = "Посмотреть устройство";
            this.getComputer.UseVisualStyleBackColor = true;
            this.getComputer.Click += new System.EventHandler(this.GetComputer_Click);
            // 
            // chengeOperation
            // 
            this.chengeOperation.Location = new System.Drawing.Point(580, 22);
            this.chengeOperation.Name = "chengeOperation";
            this.chengeOperation.Size = new System.Drawing.Size(220, 111);
            this.chengeOperation.TabIndex = 1;
            this.chengeOperation.Text = "Обновить состояние базы ";
            this.chengeOperation.UseVisualStyleBackColor = true;
            this.chengeOperation.Click += new System.EventHandler(this.RefreshDbState_Click);
            // 
            // addComputer
            // 
            this.addComputer.Location = new System.Drawing.Point(156, 22);
            this.addComputer.Name = "addComputer";
            this.addComputer.Size = new System.Drawing.Size(220, 111);
            this.addComputer.TabIndex = 0;
            this.addComputer.Text = "Добавить новое устройство";
            this.addComputer.UseVisualStyleBackColor = true;
            this.addComputer.Click += new System.EventHandler(this.CreateComputer_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.position);
            this.tabPage2.Controls.Add(this.deleteEmployee);
            this.tabPage2.Controls.Add(this.addEmployee);
            this.tabPage2.Controls.Add(this.updateEmployee);
            this.tabPage2.Controls.Add(this.phoneTextBox);
            this.tabPage2.Controls.Add(this.nameTextBox);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(981, 287);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Сотрудники";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // deleteEmployee
            // 
            this.deleteEmployee.Enabled = false;
            this.deleteEmployee.Location = new System.Drawing.Point(645, 176);
            this.deleteEmployee.Name = "deleteEmployee";
            this.deleteEmployee.Size = new System.Drawing.Size(154, 54);
            this.deleteEmployee.TabIndex = 8;
            this.deleteEmployee.Text = "Удалить";
            this.deleteEmployee.UseVisualStyleBackColor = true;
            this.deleteEmployee.Click += new System.EventHandler(this.DeleteEmployee_Click);
            // 
            // addEmployee
            // 
            this.addEmployee.Location = new System.Drawing.Point(645, 23);
            this.addEmployee.Name = "addEmployee";
            this.addEmployee.Size = new System.Drawing.Size(154, 54);
            this.addEmployee.TabIndex = 7;
            this.addEmployee.Text = "Добавить";
            this.addEmployee.UseVisualStyleBackColor = true;
            this.addEmployee.Click += new System.EventHandler(this.CreateEmployee_Click);
            // 
            // updateEmployee
            // 
            this.updateEmployee.Enabled = false;
            this.updateEmployee.Location = new System.Drawing.Point(645, 101);
            this.updateEmployee.Name = "updateEmployee";
            this.updateEmployee.Size = new System.Drawing.Size(154, 54);
            this.updateEmployee.TabIndex = 6;
            this.updateEmployee.Text = "Изменить";
            this.updateEmployee.UseVisualStyleBackColor = true;
            this.updateEmployee.Click += new System.EventHandler(this.UpdateEmployee_Click);
            // 
            // phoneTextBox
            // 
            this.phoneTextBox.Location = new System.Drawing.Point(214, 191);
            this.phoneTextBox.Name = "phoneTextBox";
            this.phoneTextBox.Size = new System.Drawing.Size(218, 31);
            this.phoneTextBox.TabIndex = 5;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(214, 38);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(218, 31);
            this.nameTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Телефон";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Должность";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Имя сотрудника";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.status);
            this.tabPage3.Controls.Add(this.regCheckBox);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.regDatePicker);
            this.tabPage3.Controls.Add(this.applyFilters);
            this.tabPage3.Controls.Add(this.price);
            this.tabPage3.Controls.Add(this.computerName);
            this.tabPage3.Controls.Add(this.Статус);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Location = new System.Drawing.Point(4, 34);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(981, 287);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Фильтры";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // status
            // 
            this.status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.status.FormattingEnabled = true;
            this.status.Location = new System.Drawing.Point(186, 179);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(267, 33);
            this.status.TabIndex = 13;
            // 
            // regCheckBox
            // 
            this.regCheckBox.AutoSize = true;
            this.regCheckBox.Location = new System.Drawing.Point(474, 247);
            this.regCheckBox.Name = "regCheckBox";
            this.regCheckBox.Size = new System.Drawing.Size(22, 21);
            this.regCheckBox.TabIndex = 12;
            this.regCheckBox.UseVisualStyleBackColor = true;
            this.regCheckBox.CheckedChanged += new System.EventHandler(this.RegCheckBox_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 249);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 25);
            this.label7.TabIndex = 11;
            this.label7.Text = "Дата регистрации";
            // 
            // regDatePicker
            // 
            this.regDatePicker.Enabled = false;
            this.regDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.regDatePicker.Location = new System.Drawing.Point(186, 243);
            this.regDatePicker.Name = "regDatePicker";
            this.regDatePicker.Size = new System.Drawing.Size(267, 31);
            this.regDatePicker.TabIndex = 10;
            // 
            // applyFilters
            // 
            this.applyFilters.Location = new System.Drawing.Point(662, 91);
            this.applyFilters.Name = "applyFilters";
            this.applyFilters.Size = new System.Drawing.Size(138, 75);
            this.applyFilters.TabIndex = 9;
            this.applyFilters.Text = "Применить фильтры";
            this.applyFilters.UseVisualStyleBackColor = true;
            this.applyFilters.Click += new System.EventHandler(this.ApplyFilters_Click);
            // 
            // price
            // 
            this.price.Location = new System.Drawing.Point(186, 110);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(267, 31);
            this.price.TabIndex = 7;
            // 
            // computerName
            // 
            this.computerName.Location = new System.Drawing.Point(186, 38);
            this.computerName.Name = "computerName";
            this.computerName.Size = new System.Drawing.Size(267, 31);
            this.computerName.TabIndex = 6;
            // 
            // Статус
            // 
            this.Статус.AutoSize = true;
            this.Статус.Location = new System.Drawing.Point(11, 182);
            this.Статус.Name = "Статус";
            this.Статус.Size = new System.Drawing.Size(63, 25);
            this.Статус.TabIndex = 5;
            this.Статус.Text = "Статус";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Цена";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Имя устройства";
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 34);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(981, 287);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "О программе";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(5, 324);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 62;
            this.dgv.RowTemplate.Height = 33;
            this.dgv.Size = new System.Drawing.Size(977, 223);
            this.dgv.TabIndex = 1;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellClick);
            // 
            // position
            // 
            this.position.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.position.FormattingEnabled = true;
            this.position.Location = new System.Drawing.Point(214, 116);
            this.position.Name = "position";
            this.position.Size = new System.Drawing.Size(218, 33);
            this.position.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 547);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.mainFormTabPage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Учет персональных компьютеров ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_Closed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainFormTabPage.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private TabControl mainFormTabPage;
    private TabPage tabPage1;
    private TabPage tabPage2;
    private TextBox phoneTextBox;
    private TextBox nameTextBox;
    private Label label3;
    private Label label2;
    private Label label1;
    private TabPage tabPage3;
    private TabPage tabPage4;
    private DataGridView dgv;
    private Button deleteEmployee;
    private Button addEmployee;
    private Button updateEmployee;
    private Button addComputer;
    private Button deleteComputer;
    private Button getComputer;
    private Button chengeOperation;
    private Button applyFilters;
    private TextBox price;
    private TextBox computerName;
    private Label Статус;
    private Label label5;
    private Label label4;
    private Label label7;
    private DateTimePicker regDatePicker;
    private CheckBox regCheckBox;
    private ComboBox status;
    private ComboBox position;
}