using Microsoft.VisualBasic;

namespace Essensausgleich
{
    public partial class Form1 : Form
    {
        private Bewohner bewohner1 = new(null);
        private Bewohner bewohner2 = new(null);

        public Form1()
        {
            InitializeComponent();
            LblToolStrip.Text = "";
        }

        public void WriteLineS(string s)
        {
            System.Diagnostics.Debug.WriteLine(s);
        }
        private void btnCalc_Click(object sender, EventArgs e)
        {
            decimal Endwert = 0;
            string? zBezahlender;
            if (bewohner1.name != null && bewohner2.name != null)
            {
                Endwert = (bewohner1.Ausgaben + bewohner2.Ausgaben) / 2;
                if (bewohner1.Ausgaben > 0 || bewohner2.Ausgaben > 0)
                {
                    if (bewohner1.Ausgaben > bewohner2.Ausgaben)
                    {
                        Endwert = bewohner1.Ausgaben - Endwert;
                        zBezahlender = bewohner2.name;
                        LblBill.Text = Convert.ToString(Endwert);
                        LblZuBezahlender.Text = zBezahlender;
                    }
                    else
                    {
                        Endwert = bewohner2.Ausgaben - Endwert;
                        zBezahlender = bewohner1.name;
                        LblBill.Text = Convert.ToString(Endwert);
                        LblZuBezahlender.Text = zBezahlender;
                    }
                }
                else LblToolStrip.Text = $"Mindestens eine Partei muss Ausgaben hinterlegen";
            }
            else LblToolStrip.Text = $"Es wurden nicht mindestens 2 User Angelegt";
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

            if (txtBoxAddUser.Text != "")
            {
                if (bewohner1.name == null || bewohner2.name == null)
                {
                    if (bewohner1.name == null)
                    {
                        bewohner1.name = txtBoxAddUser.Text;
                        LblBewohner1.Text = bewohner1.name;
                        cBoxUser.Items.Add(txtBoxAddUser.Text);
                        cBoxUser.SelectedIndex = cBoxUser.Items.Count - 1;
                        LblToolStrip.Text = $"Bewohner {bewohner1.name} wurde angelegt";
                    }
                    else if (bewohner2.name == null && txtBoxAddUser.Text != bewohner1.name)
                    {
                        bewohner2.name = txtBoxAddUser.Text;
                        LblBewohner2.Text = bewohner2.name;
                        cBoxUser.Items.Add(txtBoxAddUser.Text);
                        cBoxUser.SelectedIndex = cBoxUser.Items.Count - 1;
                        LblToolStrip.Text = $"Bewohner {bewohner2.name} wurde angelegt";
                    }
                    else LblToolStrip.Text = $"Name gleich wie User1 bitte anderen waehlen";
                }
                else LblToolStrip.Text = $"Maximale User anzahl bereits Angelegt";
            }
            else LblToolStrip.Text = $"Kein User Name eingegeben";

        }
        
        private void BtnAddBill_Click(object sender, EventArgs e)
        {
            if (cBoxUser.Text != "")
            {
                decimal bill = 0;

                bill = Convert.ToDecimal(txtBoxAddBill.Text);
                if (bewohner1.name == cBoxUser.Text && cBoxUser.Text != "")
                {
                    bewohner1.AddBetrag(txtBoxCategorie.Text, bill);
                    LblTotalAmountBew1.Text = Convert.ToString(bewohner1.Ausgaben);
                    LblToolStrip.Text = $"Betrag {bill} der Kategorie {txtBoxCategorie.Text} hinzugefuegt";
                }
                else if (bewohner2.name == cBoxUser.Text && cBoxUser.Text != "")
                {
                    bewohner2.AddBetrag(txtBoxCategorie.Text, bill);
                    LblTotalAmountBew2.Text = Convert.ToString(bewohner2.Ausgaben);
                    LblToolStrip.Text = $"Betrag {bill} der Kategorie {txtBoxCategorie.Text} hinzugefuegt";
                }
                else
                {
                    LblToolStrip.Text = $"Error keine Bewohner wurde mit der im Dropdown ausgewaehlten User identifiziert";
                }
            }
            else LblToolStrip.Text = $"Missing Username";
        }

        private void BtnAuflisten_Click(object sender, EventArgs e)
        {
            if (cBoxUser.Text != "")
            {

                if (bewohner1.name == cBoxUser.Text)
                {
                    LblToolStrip.Text = $"{bewohner1.EinzelbetraegeAusgeben()}";

                }
                else if (bewohner2.name == cBoxUser.Text)
                {                    
                    LblToolStrip.Text = $"{bewohner2.EinzelbetraegeAusgeben()}";
                }
                else
                {
                    LblToolStrip.Text = $"Error keine Bewohner wurde mit der im Dropdown ausgewaehlten User identifiziert";                    
                }
            }
            else LblToolStrip.Text = $"Kein User Vorhanden bzw Ausgewaehlt";
        }
        
    }
}
