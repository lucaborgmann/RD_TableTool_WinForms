namespace RD_TableTool_WinForms
{
    partial class Form1
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
            menuStrip3 = new MenuStrip();
            groupBox1 = new GroupBox();
            CreateMenuItemsCheckBox = new CheckBox();
            CreatePrivilegesCheckBox = new CheckBox();
            CreateEntityCheckBox = new CheckBox();
            FormPatternLabel = new Label();
            comboBox1 = new ComboBox();
            textBox2 = new TextBox();
            PropertyLabel = new Label();
            textBox1 = new TextBox();
            LabelLabel = new Label();
            NameTextBox = new TextBox();
            NameLabel = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip3
            // 
            menuStrip3.ImageScalingSize = new Size(20, 20);
            menuStrip3.Location = new Point(0, 0);
            menuStrip3.Name = "menuStrip3";
            menuStrip3.Size = new Size(782, 24);
            menuStrip3.TabIndex = 2;
            menuStrip3.Text = "menuStrip3";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(CreateMenuItemsCheckBox);
            groupBox1.Controls.Add(CreatePrivilegesCheckBox);
            groupBox1.Controls.Add(CreateEntityCheckBox);
            groupBox1.Controls.Add(FormPatternLabel);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(PropertyLabel);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(LabelLabel);
            groupBox1.Controls.Add(NameTextBox);
            groupBox1.Controls.Add(NameLabel);
            groupBox1.Location = new Point(12, 45);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(758, 225);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "TableSetup";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // CreateMenuItemsCheckBox
            // 
            CreateMenuItemsCheckBox.AutoSize = true;
            CreateMenuItemsCheckBox.Location = new Point(393, 104);
            CreateMenuItemsCheckBox.Name = "CreateMenuItemsCheckBox";
            CreateMenuItemsCheckBox.Size = new Size(147, 24);
            CreateMenuItemsCheckBox.TabIndex = 10;
            CreateMenuItemsCheckBox.Text = "CreateMenuItems";
            CreateMenuItemsCheckBox.UseVisualStyleBackColor = true;
            // 
            // CreatePrivilegesCheckBox
            // 
            CreatePrivilegesCheckBox.AutoSize = true;
            CreatePrivilegesCheckBox.Location = new Point(199, 134);
            CreatePrivilegesCheckBox.Name = "CreatePrivilegesCheckBox";
            CreatePrivilegesCheckBox.Size = new Size(141, 24);
            CreatePrivilegesCheckBox.TabIndex = 9;
            CreatePrivilegesCheckBox.Text = "Create Privileges";
            CreatePrivilegesCheckBox.UseVisualStyleBackColor = true;
            CreatePrivilegesCheckBox.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // CreateEntityCheckBox
            // 
            CreateEntityCheckBox.AutoSize = true;
            CreateEntityCheckBox.Location = new Point(199, 104);
            CreateEntityCheckBox.Name = "CreateEntityCheckBox";
            CreateEntityCheckBox.Size = new Size(115, 24);
            CreateEntityCheckBox.TabIndex = 8;
            CreateEntityCheckBox.Text = "Create Entity";
            CreateEntityCheckBox.UseVisualStyleBackColor = true;
            // 
            // FormPatternLabel
            // 
            FormPatternLabel.AutoSize = true;
            FormPatternLabel.Location = new Point(18, 93);
            FormPatternLabel.Name = "FormPatternLabel";
            FormPatternLabel.Size = new Size(93, 20);
            FormPatternLabel.TabIndex = 7;
            FormPatternLabel.Text = "Form Pattern";
            FormPatternLabel.Click += label1_Click_2;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(18, 116);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(393, 47);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(186, 27);
            textBox2.TabIndex = 5;
            // 
            // PropertyLabel
            // 
            PropertyLabel.AutoSize = true;
            PropertyLabel.Location = new Point(393, 23);
            PropertyLabel.Name = "PropertyLabel";
            PropertyLabel.Size = new Size(65, 20);
            PropertyLabel.TabIndex = 4;
            PropertyLabel.Text = "Property";
            PropertyLabel.Click += PropertyLabel_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(185, 47);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(188, 27);
            textBox1.TabIndex = 3;
            // 
            // LabelLabel
            // 
            LabelLabel.AutoSize = true;
            LabelLabel.Location = new Point(185, 23);
            LabelLabel.Name = "LabelLabel";
            LabelLabel.Size = new Size(45, 20);
            LabelLabel.TabIndex = 2;
            LabelLabel.Text = "Label";
            LabelLabel.Click += label1_Click_1;
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(18, 47);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(137, 27);
            NameTextBox.TabIndex = 1;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(18, 24);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(49, 20);
            NameLabel.TabIndex = 0;
            NameLabel.Text = "Name";
            NameLabel.Click += label1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 615);
            Controls.Add(groupBox1);
            Controls.Add(menuStrip3);
            Name = "Form1";
            Text = "RD_TableTool";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip3;
        private GroupBox groupBox1;
        private Label NameLabel;
        private TextBox NameTextBox;
        private Label LabelLabel;
        private TextBox textBox1;
        private Label PropertyLabel;
        private TextBox textBox2;
        private Label FormPatternLabel;
        private ComboBox comboBox1;
        private CheckBox CreateEntityCheckBox;
        private CheckBox CreatePrivilegesCheckBox;
        private CheckBox CreateMenuItemsCheckBox;
    }
}
