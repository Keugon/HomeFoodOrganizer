namespace Essensausgleich
{
    partial class settingsForm
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
            RbtnNoSave = new RadioButton();
            RbtnFileSave = new RadioButton();
            RbtnSaveDatabase = new RadioButton();
            BtnApplyFilesystemChange = new Button();
            label1 = new Label();
            txtBoxFileNameXML = new TextBox();
            SuspendLayout();
            // 
            // RbtnNoSave
            // 
            RbtnNoSave.AutoSize = true;
            RbtnNoSave.Enabled = false;
            RbtnNoSave.Location = new Point(12, 12);
            RbtnNoSave.Name = "RbtnNoSave";
            RbtnNoSave.Size = new Size(142, 19);
            RbtnNoSave.TabIndex = 0;
            RbtnNoSave.TabStop = true;
            RbtnNoSave.Text = "No Data Management";
            RbtnNoSave.UseVisualStyleBackColor = true;
            // 
            // RbtnFileSave
            // 
            RbtnFileSave.AutoSize = true;
            RbtnFileSave.Location = new Point(12, 37);
            RbtnFileSave.Name = "RbtnFileSave";
            RbtnFileSave.Size = new Size(195, 19);
            RbtnFileSave.TabIndex = 1;
            RbtnFileSave.TabStop = true;
            RbtnFileSave.Text = "Data Management to Filesystem";
            RbtnFileSave.UseVisualStyleBackColor = true;
            // 
            // RbtnSaveDatabase
            // 
            RbtnSaveDatabase.AutoSize = true;
            RbtnSaveDatabase.Enabled = false;
            RbtnSaveDatabase.Location = new Point(12, 62);
            RbtnSaveDatabase.Name = "RbtnSaveDatabase";
            RbtnSaveDatabase.Size = new Size(217, 19);
            RbtnSaveDatabase.TabIndex = 2;
            RbtnSaveDatabase.TabStop = true;
            RbtnSaveDatabase.Text = "Aktiv Datamanagement by Database";
            RbtnSaveDatabase.UseVisualStyleBackColor = true;
            // 
            // BtnApplyFilesystemChange
            // 
            BtnApplyFilesystemChange.Location = new Point(12, 111);
            BtnApplyFilesystemChange.Name = "BtnApplyFilesystemChange";
            BtnApplyFilesystemChange.Size = new Size(75, 23);
            BtnApplyFilesystemChange.TabIndex = 3;
            BtnApplyFilesystemChange.Text = "Apply";
            BtnApplyFilesystemChange.UseVisualStyleBackColor = true;
            BtnApplyFilesystemChange.Click += BtnApplyFilesystemChange_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(293, 20);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 4;
            label1.Text = "Filename";
            // 
            // txtBoxFileNameXML
            // 
            txtBoxFileNameXML.Location = new Point(364, 17);
            txtBoxFileNameXML.Name = "txtBoxFileNameXML";
            txtBoxFileNameXML.Size = new Size(157, 23);
            txtBoxFileNameXML.TabIndex = 5;
            // 
            // settingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(550, 169);
            Controls.Add(txtBoxFileNameXML);
            Controls.Add(label1);
            Controls.Add(BtnApplyFilesystemChange);
            Controls.Add(RbtnSaveDatabase);
            Controls.Add(RbtnFileSave);
            Controls.Add(RbtnNoSave);
            Name = "settingsForm";
            Text = "settingsForm";
            Load += settingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton RbtnNoSave;
        private RadioButton RbtnFileSave;
        private RadioButton RbtnSaveDatabase;
        private Button BtnApplyFilesystemChange;
        private Label label1;
        private TextBox txtBoxFileNameXML;
    }
}