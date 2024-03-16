namespace Essensausgleich
{
    partial class beitragsauflistungForm
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
            dGridVbeitraege = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dGridVbeitraege).BeginInit();
            SuspendLayout();
            // 
            // dGridVbeitraege
            // 
            dGridVbeitraege.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dGridVbeitraege.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dGridVbeitraege.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dGridVbeitraege.Dock = DockStyle.Fill;
            dGridVbeitraege.Location = new Point(0, 0);
            dGridVbeitraege.Name = "dGridVbeitraege";
            dGridVbeitraege.Size = new Size(408, 267);
            dGridVbeitraege.TabIndex = 0;
            // 
            // beitragsauflistungForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(408, 267);
            Controls.Add(dGridVbeitraege);
            Name = "beitragsauflistungForm";
            Text = "beitragsauflistungForm";
            ((System.ComponentModel.ISupportInitialize)dGridVbeitraege).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dGridVbeitraege;
    }
}