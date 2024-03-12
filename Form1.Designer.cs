namespace Essensausgleich
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnCalc = new Button();
            LblBill = new Label();
            LblZuBezahlender = new Label();
            label3 = new Label();
            label1 = new Label();
            cBoxUser = new ComboBox();
            txtBoxAddUser = new TextBox();
            BtnAddUser = new Button();
            lVbills = new ListView();
            txtBoxAddBill = new TextBox();
            BtnAddBill = new Button();
            LblBewohner1 = new Label();
            LblBewohner2 = new Label();
            LblTotalAmountBew1 = new Label();
            LblTotalAmountBew2 = new Label();
            BtnAuflisten = new Button();
            txtBoxCategorie = new TextBox();
            statusStrip1 = new StatusStrip();
            LblToolStrip = new ToolStripStatusLabel();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btnCalc
            // 
            btnCalc.Location = new Point(59, 105);
            btnCalc.Name = "btnCalc";
            btnCalc.Size = new Size(75, 23);
            btnCalc.TabIndex = 4;
            btnCalc.TabStop = false;
            btnCalc.Text = "Berechnen";
            btnCalc.UseVisualStyleBackColor = true;
            btnCalc.Click += btnCalc_Click;
            // 
            // LblBill
            // 
            LblBill.AutoSize = true;
            LblBill.Location = new Point(93, 131);
            LblBill.Name = "LblBill";
            LblBill.Size = new Size(53, 15);
            LblBill.TabIndex = 5;
            LblBill.Text = "Benutzer";
            // 
            // LblZuBezahlender
            // 
            LblZuBezahlender.AutoSize = true;
            LblZuBezahlender.Location = new Point(0, 131);
            LblZuBezahlender.Name = "LblZuBezahlender";
            LblZuBezahlender.Size = new Size(85, 15);
            LblZuBezahlender.TabIndex = 6;
            LblZuBezahlender.Text = "ZuBezahlender";
            // 
            // label3
            // 
            label3.Location = new Point(557, 87);
            label3.Name = "label3";
            label3.Size = new Size(72, 107);
            label3.TabIndex = 7;
            label3.Text = "Formel: FD+ MW /2 - (höhere ausgaben)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 9);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 8;
            label1.Text = "Person";
            // 
            // cBoxUser
            // 
            cBoxUser.FormattingEnabled = true;
            cBoxUser.Location = new Point(13, 31);
            cBoxUser.Name = "cBoxUser";
            cBoxUser.Size = new Size(121, 23);
            cBoxUser.TabIndex = 9;
            cBoxUser.TabStop = false;
            // 
            // txtBoxAddUser
            // 
            txtBoxAddUser.Location = new Point(451, 28);
            txtBoxAddUser.Name = "txtBoxAddUser";
            txtBoxAddUser.PlaceholderText = "Name Eingeben";
            txtBoxAddUser.Size = new Size(100, 23);
            txtBoxAddUser.TabIndex = 10;
            txtBoxAddUser.TabStop = false;
            
            // 
            // BtnAddUser
            // 
            BtnAddUser.Location = new Point(557, 27);
            BtnAddUser.Name = "BtnAddUser";
            BtnAddUser.Size = new Size(75, 23);
            BtnAddUser.TabIndex = 11;
            BtnAddUser.TabStop = false;
            BtnAddUser.Text = "AddUser";
            BtnAddUser.UseVisualStyleBackColor = true;
            BtnAddUser.Click += BtnAddUser_Click;
            // 
            // lVbills
            // 
            lVbills.Location = new Point(430, 72);
            lVbills.MultiSelect = false;
            lVbills.Name = "lVbills";
            lVbills.Size = new Size(121, 97);
            lVbills.TabIndex = 12;
            lVbills.TabStop = false;
            lVbills.UseCompatibleStateImageBehavior = false;
            // 
            // txtBoxAddBill
            // 
            txtBoxAddBill.Location = new Point(264, 29);
            txtBoxAddBill.Name = "txtBoxAddBill";
            txtBoxAddBill.PlaceholderText = "Betrag Eingeben";
            txtBoxAddBill.Size = new Size(100, 23);
            txtBoxAddBill.TabIndex = 2;
            // 
            // BtnAddBill
            // 
            BtnAddBill.Location = new Point(370, 28);
            BtnAddBill.Name = "BtnAddBill";
            BtnAddBill.Size = new Size(75, 23);
            BtnAddBill.TabIndex = 14;
            BtnAddBill.TabStop = false;
            BtnAddBill.Text = "AddBill";
            BtnAddBill.UseVisualStyleBackColor = true;
            BtnAddBill.Click += BtnAddBill_Click;
            // 
            // LblBewohner1
            // 
            LblBewohner1.AutoSize = true;
            LblBewohner1.Location = new Point(36, 72);
            LblBewohner1.Name = "LblBewohner1";
            LblBewohner1.Size = new Size(20, 15);
            LblBewohner1.TabIndex = 15;
            LblBewohner1.Text = "B1";
            // 
            // LblBewohner2
            // 
            LblBewohner2.AutoSize = true;
            LblBewohner2.Location = new Point(36, 87);
            LblBewohner2.Name = "LblBewohner2";
            LblBewohner2.Size = new Size(20, 15);
            LblBewohner2.TabIndex = 16;
            LblBewohner2.Text = "B2";
            // 
            // LblTotalAmountBew1
            // 
            LblTotalAmountBew1.AutoSize = true;
            LblTotalAmountBew1.Location = new Point(93, 72);
            LblTotalAmountBew1.Name = "LblTotalAmountBew1";
            LblTotalAmountBew1.Size = new Size(39, 15);
            LblTotalAmountBew1.TabIndex = 17;
            LblTotalAmountBew1.Text = "ABW1";
            // 
            // LblTotalAmountBew2
            // 
            LblTotalAmountBew2.AutoSize = true;
            LblTotalAmountBew2.Location = new Point(93, 87);
            LblTotalAmountBew2.Name = "LblTotalAmountBew2";
            LblTotalAmountBew2.Size = new Size(39, 15);
            LblTotalAmountBew2.TabIndex = 18;
            LblTotalAmountBew2.Text = "ABW2";
            // 
            // BtnAuflisten
            // 
            BtnAuflisten.Location = new Point(273, 68);
            BtnAuflisten.Name = "BtnAuflisten";
            BtnAuflisten.Size = new Size(112, 23);
            BtnAuflisten.TabIndex = 19;
            BtnAuflisten.TabStop = false;
            BtnAuflisten.Text = "Betragsauflistung";
            BtnAuflisten.UseVisualStyleBackColor = true;
            BtnAuflisten.Click += BtnAuflisten_Click;
            // 
            // txtBoxCategorie
            // 
            txtBoxCategorie.Location = new Point(153, 29);
            txtBoxCategorie.Name = "txtBoxCategorie";
            txtBoxCategorie.PlaceholderText = "Kategorie";
            txtBoxCategorie.Size = new Size(100, 23);
            txtBoxCategorie.TabIndex = 1;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { LblToolStrip });
            statusStrip1.Location = new Point(0, 248);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(643, 22);
            statusStrip1.TabIndex = 21;
            statusStrip1.Text = "statusStrip1";
            // 
            // LblToolStrip
            // 
            LblToolStrip.Name = "LblToolStrip";
            LblToolStrip.Size = new Size(118, 17);
            LblToolStrip.Text = "toolStripStatusLabel1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(643, 270);
            Controls.Add(statusStrip1);
            Controls.Add(txtBoxCategorie);
            Controls.Add(BtnAuflisten);
            Controls.Add(LblTotalAmountBew2);
            Controls.Add(LblTotalAmountBew1);
            Controls.Add(LblBewohner2);
            Controls.Add(LblBewohner1);
            Controls.Add(BtnAddBill);
            Controls.Add(txtBoxAddBill);
            Controls.Add(lVbills);
            Controls.Add(BtnAddUser);
            Controls.Add(txtBoxAddUser);
            Controls.Add(cBoxUser);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(LblZuBezahlender);
            Controls.Add(LblBill);
            Controls.Add(btnCalc);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Essensausgleich";
            Load += Form1_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnCalc;
        private Label LblBill;
        private Label LblZuBezahlender;
        private Label label3;
        private Label label1;
        private ComboBox cBoxUser;
        private TextBox txtBoxAddUser;
        private Button BtnAddUser;
        private ListView lVbills;
        private TextBox txtBoxAddBill;
        private Button BtnAddBill;
        private Label LblBewohner1;
        private Label LblBewohner2;
        private Label LblTotalAmountBew1;
        private Label LblTotalAmountBew2;
        private Button BtnAuflisten;
        private TextBox txtBoxCategorie;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel LblToolStrip;
    }
}
