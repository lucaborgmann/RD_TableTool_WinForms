namespace RD_TableTool_WinForms
{
    partial class ModelSelectionForm
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
            SelectModelButton = new Button();
            ChooseModelLabel = new Label();
            CancelButton = new Button();
            InputTextBox = new TextBox();
            FileOpenButton = new Button();
            SuspendLayout();
            // 
            // SelectModelButton
            // 
            SelectModelButton.Location = new Point(213, 165);
            SelectModelButton.Name = "SelectModelButton";
            SelectModelButton.Size = new Size(156, 29);
            SelectModelButton.TabIndex = 0;
            SelectModelButton.Text = "Select";
            SelectModelButton.UseVisualStyleBackColor = true;
            // 
            // ChooseModelLabel
            // 
            ChooseModelLabel.AutoSize = true;
            ChooseModelLabel.Location = new Point(12, 86);
            ChooseModelLabel.Name = "ChooseModelLabel";
            ChooseModelLabel.Size = new Size(112, 20);
            ChooseModelLabel.TabIndex = 1;
            ChooseModelLabel.Text = "Choose model: ";
            ChooseModelLabel.Click += label1_Click;
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(375, 165);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(130, 29);
            CancelButton.TabIndex = 2;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += button1_Click;
            // 
            // InputTextBox
            // 
            InputTextBox.Location = new Point(130, 83);
            InputTextBox.Name = "InputTextBox";
            InputTextBox.Size = new Size(330, 27);
            InputTextBox.TabIndex = 3;
            // 
            // FileOpenButton
            // 
            FileOpenButton.Location = new Point(475, 83);
            FileOpenButton.Name = "FileOpenButton";
            FileOpenButton.Size = new Size(30, 29);
            FileOpenButton.TabIndex = 4;
            FileOpenButton.Text = "...";
            FileOpenButton.UseVisualStyleBackColor = true;
            // 
            // ModelSelectionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(545, 229);
            Controls.Add(FileOpenButton);
            Controls.Add(InputTextBox);
            Controls.Add(CancelButton);
            Controls.Add(ChooseModelLabel);
            Controls.Add(SelectModelButton);
            Name = "ModelSelectionForm";
            Text = "ModelSelectionForm";
            Load += ModelSelectionForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SelectModelButton;
        private Label ChooseModelLabel;
        private Button CancelButton;
        private TextBox InputTextBox;
        private Button FileOpenButton;
    }
}