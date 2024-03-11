using Microsoft.VisualBasic;

namespace Essensausgleich
{
    public partial class Form1 : Form
    {
        private Bewohner bewohner1 = new("");
        private Bewohner bewohner2 = new("");
        
        public Form1()
        {
            InitializeComponent();
            LblStatus.Text = "";
        }
        
        public void WriteLineS(string s)
        {
            System.Diagnostics.Debug.WriteLine(s);
        }
        private void btnCalc_Click(object sender, EventArgs e)
        {
            //int FD = Convert.ToInt32(txtBoxAusgabeFD.Text);
            //int MW = Convert.ToInt32(txtBoxAusgabeMW.Text);
            //int Endwert = 0;
            //string zBezahlender;
            //Endwert = (FD + MW) / 2;
            //if (FD > MW)
            //{
            //    Endwert = FD - Endwert;
            //    zBezahlender = "MW";
            //}
            //else
            //{
            //    Endwert = MW - Endwert;
            //    zBezahlender = "FD";
            //}

            //LblBill.Text = Convert.ToString(Endwert);
            //LblZuBezahlender.Text = zBezahlender;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            AddUser();
        }
        private void AddUser()
        {
            if (bewohner1.name == "" || bewohner2.name == "")
            {
                cBoxUser.Items.Add(txtBoxAddUser.Text);
                cBoxUser.SelectedIndex = cBoxUser.Items.Count - 1;
                if (bewohner1.name == "")
                {
                    bewohner1.name = txtBoxAddUser.Text;
                    LblBewohner1.Text = bewohner1.name;
                    LblStatus.Text = $"Bewohner {bewohner1.name} wurde angelegt\n{LblStatus.Text}";
                }
                else
                {
                    bewohner2.name = txtBoxAddUser.Text;
                    LblBewohner2.Text = bewohner2.name;
                    LblStatus.Text = $"Bewohner {bewohner2.name} wurde angelegt\n{LblStatus.Text}";
                }
            }
            else
            {
                WriteLineS("User bereits angelegt");
            }

        }
        // List<Rechnung> Lbills = new List<Rechnung>();

        private void BtnAddBill_Click(object sender, EventArgs e)
        {
            if (cBoxUser.Text != "")
            {
                decimal bill = 0;
                try
                {
                    bill = Convert.ToDecimal(txtBoxAddBill.Text);
                    if (bewohner1.name == cBoxUser.Text && cBoxUser.Text != "")
                    {
                        bewohner1.AddBetrag(txtBoxCategorie.Text,bill);
                        LblTotalAmountBew1.Text = Convert.ToString(bewohner1.Ausgaben);
                        LblStatus.Text = $"Betrag {bill} der Kategorie {txtBoxCategorie.Text} hinzugefuegt\n{LblStatus.Text}";

                    }
                    else if (bewohner2.name == cBoxUser.Text && cBoxUser.Text != "")
                    {
                        bewohner2.AddBetrag(txtBoxCategorie.Text, bill);
                        LblTotalAmountBew2.Text = Convert.ToString(bewohner2.Ausgaben);
                        LblStatus.Text = $"Betrag {bill} der Kategorie {txtBoxCategorie.Text} hinzugefuegt\n{LblStatus.Text}";
                    }
                    else
                    {
                        WriteLineS("Error keine Bewohner wurde mit der im Dropdown ausgewaehlten User identifiziert");
                    }

                }
                catch (Exception ex)
                {
                    WriteLineS(ex.Message);

                }
            }
            else WriteLineS("Missing Username");


        }

        private void BtnAuflisten_Click(object sender, EventArgs e)
        {
            if (cBoxUser.Text != "")
            {

                if (bewohner1.name == cBoxUser.Text && cBoxUser.Text != "")
                {
                    bewohner1.EinzelbetraegeAusgeben();

                }
                else if (bewohner2.name == cBoxUser.Text && cBoxUser.Text != "")
                {
                    bewohner2.EinzelbetraegeAusgeben();
                }
                else
                {
                    WriteLineS("Error keine Bewohner wurde mit der im Dropdown ausgewaehlten User identifiziert");
                }
            }
            else WriteLineS("Missing Username");
        }
    }
}
