namespace RD_TableTool_WinForms
{
    partial class RD_TableToolForm
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
            FormPatternCombobox = new ComboBox();
            PropertyTextBox = new TextBox();
            PropertyLabel = new Label();
            LabelTextBox = new TextBox();
            LabelLabel = new Label();
            NameTextBox = new TextBox();
            NameLabel = new Label();
            CancelButton = new Button();
            OkButton = new Button();
            DataGrid = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGrid).BeginInit();
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
            groupBox1.Controls.Add(FormPatternCombobox);
            groupBox1.Controls.Add(PropertyTextBox);
            groupBox1.Controls.Add(PropertyLabel);
            groupBox1.Controls.Add(LabelTextBox);
            groupBox1.Controls.Add(LabelLabel);
            groupBox1.Controls.Add(NameTextBox);
            groupBox1.Controls.Add(NameLabel);
            groupBox1.Location = new Point(12, 45);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(758, 225);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "TableSetup";
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
            CreateMenuItemsCheckBox.CheckedChanged += CreateMenuItemsCheckBox_CheckedChanged;
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
            CreateEntityCheckBox.CheckedChanged += CreateEntityCheckBox_CheckedChanged;
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
            // FormPatternCombobox
            // 
            FormPatternCombobox.FormattingEnabled = true;
            FormPatternCombobox.Location = new Point(18, 116);
            FormPatternCombobox.Name = "FormPatternCombobox";
            FormPatternCombobox.Size = new Size(151, 28);
            FormPatternCombobox.TabIndex = 6;
            // 
            // PropertyTextBox
            // 
            PropertyTextBox.Location = new Point(393, 47);
            PropertyTextBox.Name = "PropertyTextBox";
            PropertyTextBox.Size = new Size(186, 27);
            PropertyTextBox.TabIndex = 5;
            PropertyTextBox.Visible = false;
            // 
            // PropertyLabel
            // 
            PropertyLabel.AutoSize = true;
            PropertyLabel.Location = new Point(393, 23);
            PropertyLabel.Name = "PropertyLabel";
            PropertyLabel.Size = new Size(65, 20);
            PropertyLabel.TabIndex = 4;
            PropertyLabel.Text = "Property";
            PropertyLabel.Visible = false;
            PropertyLabel.Click += PropertyLabel_Click;
            // 
            // LabelTextBox
            // 
            LabelTextBox.Location = new Point(185, 47);
            LabelTextBox.Name = "LabelTextBox";
            LabelTextBox.Size = new Size(188, 27);
            LabelTextBox.TabIndex = 3;
            LabelTextBox.TextChanged += LabelTextBox_TextChanged;
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
            NameTextBox.TextChanged += NameTextBox_TextChanged;
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
            // CancelButton
            // 
            CancelButton.Location = new Point(655, 568);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(115, 35);
            CancelButton.TabIndex = 4;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // OkButton
            // 
            OkButton.Location = new Point(534, 568);
            OkButton.Name = "OkButton";
            OkButton.Size = new Size(115, 35);
            OkButton.TabIndex = 5;
            OkButton.Text = "Ok";
            OkButton.UseVisualStyleBackColor = true;
            OkButton.Click += OkButton_Click;
            // 
            // DataGrid
            // 
            DataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGrid.Location = new Point(12, 292);
            DataGrid.Name = "DataGrid";
            DataGrid.RowHeadersWidth = 51;
            DataGrid.Size = new Size(758, 238);
            DataGrid.TabIndex = 6;
            // 
            // RD_TableToolForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 615);
            Controls.Add(DataGrid);
            Controls.Add(OkButton);
            Controls.Add(CancelButton);
            Controls.Add(groupBox1);
            Controls.Add(menuStrip3);
            Name = "RD_TableToolForm";
            Text = "Visionet TableTool";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DataGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip3;
        private GroupBox groupBox1;
        private Label NameLabel;
        private TextBox NameTextBox;
        private Label LabelLabel;
        private TextBox LabelTextBox;
        private Label PropertyLabel;
        private TextBox PropertyTextBox;
        private Label FormPatternLabel;
        private ComboBox FormPatternCombobox;
        private CheckBox CreateEntityCheckBox;
        private CheckBox CreatePrivilegesCheckBox;
        private CheckBox CreateMenuItemsCheckBox;
        private Button CancelButton;
        private Button OkButton;
        private DataGridView DataGrid;
    }
}
