namespace Essensausgleich
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            int FD = Convert.ToInt32(txtBoxAusgabeFD.Text);
            int MW = Convert.ToInt32(txtBoxAusgabeMW.Text);
            int Endwert = 0;
            string zBezahlender;
            Endwert = (FD + MW) / 2;
            if (FD > MW)
            {
                Endwert = FD - Endwert;
                zBezahlender = "MW";
            }
            else
            {
                Endwert = MW - Endwert;
                zBezahlender = "FD";
            }

            LblBill.Text = Convert.ToString(Endwert);
            LblZuBezahlender.Text = zBezahlender;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
