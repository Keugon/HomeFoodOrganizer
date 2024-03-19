using Microsoft.VisualBasic;
using Log = System.Diagnostics.Debug;
namespace Essensausgleich
{
    public partial class MainForm : Form
    {
        public Bewohner bewohner1 = new();
        public Bewohner bewohner2 = new();
        public MainForm()
        {
            InitializeComponent();
            LblToolStrip.Text = "";
        }
        private void btnCalc_Click(object sender, EventArgs e)
        {
            decimal Endwert = 0;
            string zBezahlender;
            if (bewohner1.name != "" && bewohner2.name != "")
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
                else
                {
                    LblToolStrip.Text = $"Mindestens eine Partei muss Ausgaben hinterlegen";
                }
            }
            else
            {
                LblToolStrip.Text = $"Es wurden nicht mindestens 2 User Angelegt";
            }            
        }       
        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            if (txtBoxAddUser.Text == "")
            {
                LblToolStrip.Text = $"Kein User Name eingegeben";
                return;
            }
            if (bewohner1.name == "" || bewohner2.name == "")
            {
                if (bewohner1.name == "")
                {
                    //replaceToMehtod
                    bewohner1.name = txtBoxAddUser.Text;
                    LblBewohner1.Text = bewohner1.name;
                    cBoxUser.Items.Add(txtBoxAddUser.Text);
                    cBoxUser.SelectedIndex = cBoxUser.Items.Count - 1;
                    LblToolStrip.Text = $"Bewohner {bewohner1.name} wurde angelegt";
                }
                else if (bewohner2.name == "" && txtBoxAddUser.Text != bewohner1.name)
                {
                    bewohner2.name = txtBoxAddUser.Text;
                    LblBewohner2.Text = bewohner2.name;
                    cBoxUser.Items.Add(txtBoxAddUser.Text);
                    cBoxUser.SelectedIndex = cBoxUser.Items.Count - 1;
                    LblToolStrip.Text = $"Bewohner {bewohner2.name} wurde angelegt";
                }
                else
                {
                    LblToolStrip.Text = $"Name gleich wie User1 bitte anderen waehlen";
                }
            }
            else
            {
                LblToolStrip.Text = $"Maximale User anzahl bereits Angelegt";
            }
        }
        private void BtnAddBill_Click(object sender, EventArgs e)
        {
            if (cBoxUser.Text != "")
            {
                decimal bill = 0;
                if (decimal.TryParse(txtBoxAddBill.Text, out bill) == false)
                {
                    LblToolStrip.Text = $"Not a valid Numver";
                    return;
                }
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
        public void BtnAuflisten_Click(object sender, EventArgs e)
        {
            if (cBoxUser.Text != "")
            {
                if (bewohner1.name == cBoxUser.Text)
                {
                    beitragsauflistungForm beitragsauflistung = new(this);
                    beitragsauflistung.FillDataGrid(bewohner1.Einzelbetraege);
                    beitragsauflistung.ShowDialog();
                }
                else if (bewohner2.name == cBoxUser.Text)
                {
                    beitragsauflistungForm beitragsauflistung = new(this);
                    beitragsauflistung.FillDataGrid(bewohner2.Einzelbetraege);
                    beitragsauflistung.ShowDialog();
                }
                else
                {
                    LblToolStrip.Text = $"Error keine Bewohner wurde mit der im Dropdown ausgewaehlten User identifiziert";
                }
            }
            else LblToolStrip.Text = $"Kein User Vorhanden bzw Ausgewaehlt";
        }
        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm settingsForm = new();
            settingsForm.ShowDialog();
        }
        public void SaveFileXML_Click(object sender, EventArgs e)
        {
            XMLPersistence xMLPersistence = new XMLPersistence();
            xMLPersistence.Save(bewohner1, bewohner2);
        }
        private void OfdXML_Click(object sender, EventArgs e)
        {
            XMLPersistence xmlPersistence = new XMLPersistence();
            xmlPersistence.Reset(bewohner1, bewohner2);
            xmlPersistence.Load(bewohner1, bewohner2);
            LblBewohner1.Text = bewohner1.name;
            LblBewohner2.Text = bewohner2.name;
            LblTotalAmountBew1.Text = bewohner1.Ausgaben.ToString();
            LblTotalAmountBew2.Text = bewohner2.Ausgaben.ToString();
            cBoxUser.Items.Add(bewohner1.name);
            cBoxUser.Items.Add(bewohner2.name);
            cBoxUser.SelectedIndex = cBoxUser.Items.Count - 1;
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XMLPersistence newXMLPersistence = new XMLPersistence();
            newXMLPersistence.Reset(bewohner1,bewohner2);
            LblBewohner1.Text = "Bew1";
            LblBewohner2.Text = "Bew2";
            LblBill.Text = "0";
            LblTotalAmountBew1.Text = "0";
            LblTotalAmountBew2.Text = "0";
            cBoxUser.Items.Clear();
            cBoxUser.Text = "";
        }
    }
}
