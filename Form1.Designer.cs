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
            txtBoxAusgabeFD = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtBoxAusgabeMW = new TextBox();
            btnCalc = new Button();
            LblBill = new Label();
            LblZuBezahlender = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // txtBoxAusgabeFD
            // 
            txtBoxAusgabeFD.Location = new Point(137, 12);
            txtBoxAusgabeFD.Name = "txtBoxAusgabeFD";
            txtBoxAusgabeFD.Size = new Size(100, 23);
            txtBoxAusgabeFD.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(86, 12);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 1;
            label1.Text = "Florian";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(86, 37);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 2;
            label2.Text = "Marion";
            // 
            // txtBoxAusgabeMW
            // 
            txtBoxAusgabeMW.Location = new Point(137, 37);
            txtBoxAusgabeMW.Name = "txtBoxAusgabeMW";
            txtBoxAusgabeMW.Size = new Size(100, 23);
            txtBoxAusgabeMW.TabIndex = 3;
            // 
            // btnCalc
            // 
            btnCalc.Location = new Point(137, 77);
            btnCalc.Name = "btnCalc";
            btnCalc.Size = new Size(75, 23);
            btnCalc.TabIndex = 4;
            btnCalc.Text = "Berechnen";
            btnCalc.UseVisualStyleBackColor = true;
            btnCalc.Click += btnCalc_Click;
            // 
            // LblBill
            // 
            LblBill.AutoSize = true;
            LblBill.Location = new Point(103, 227);
            LblBill.Name = "LblBill";
            LblBill.Size = new Size(54, 15);
            LblBill.TabIndex = 5;
            LblBill.Text = "Bezahlen";
            // 
            // LblZuBezahlender
            // 
            LblZuBezahlender.AutoSize = true;
            LblZuBezahlender.Location = new Point(12, 227);
            LblZuBezahlender.Name = "LblZuBezahlender";
            LblZuBezahlender.Size = new Size(85, 15);
            LblZuBezahlender.TabIndex = 6;
            LblZuBezahlender.Text = "ZuBezahlender";
            // 
            // label3
            // 
            label3.Location = new Point(424, 161);
            label3.Name = "label3";
            label3.Size = new Size(72, 107);
            label3.TabIndex = 7;
            label3.Text = "Formel: FD+ MW /2 - (höhere ausgaben)";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(508, 267);
            Controls.Add(label3);
            Controls.Add(LblZuBezahlender);
            Controls.Add(LblBill);
            Controls.Add(btnCalc);
            Controls.Add(txtBoxAusgabeMW);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtBoxAusgabeFD);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Essensausgleich";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBoxAusgabeFD;
        private Label label1;
        private Label label2;
        private TextBox txtBoxAusgabeMW;
        private Button btnCalc;
        private Label LblBill;
        private Label LblZuBezahlender;
        private Label label3;
    }
}
