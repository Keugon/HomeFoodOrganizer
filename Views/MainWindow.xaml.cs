using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using Essensausgleich.Controller;
using Essensausgleich.Infra;
using Essensausgleich.ViewModel;
using Log = System.Diagnostics.Debug;

namespace Essensausgleich
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IAppObjekt
    {


        private Bewohner bewohner1 = null!;
        private Bewohner bewohner2 = null!;

        //Todo Mvvm
        private XMLPersistence _XMLPersistance = null!;
        /// <summary>
        /// Gets or Sets the Infrastructur obj for the MainWindow
        /// </summary>
        public Infrastruktur Kontext { get; set; } = null!;


        /// <summary>
        /// MainForm for UI Interactions
        /// </summary>
        /// <summary>
        /// MainWindowWPF handels all the User interaction
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //LblToolStrip.Content = "";          
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _XMLPersistance = Kontext.FilesSystemManagerService.GetXMLPersistance();

            bewohner1 = this.Kontext.bewohner1;
            bewohner2 = this.Kontext.bewohner2;
            this.DataContext = bewohner1;
            //Todo impl Viewmodel to house every value that will be changed 
            //this.DataContext = bewohner2;
        }
        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            decimal Endwert = 0;
            string zBezahlender;
            if (bewohner1.Name != "" && bewohner2.Name != "")
            {
                Endwert = (bewohner1.Ausgaben + bewohner2.Ausgaben) / 2;
                if (bewohner1.Ausgaben > 0 || bewohner2.Ausgaben > 0)
                {
                    if (bewohner1.Ausgaben > bewohner2.Ausgaben)
                    {
                        Endwert = bewohner1.Ausgaben - Endwert;
                        zBezahlender = bewohner2.Name;
                        LblBill.Content = Convert.ToString(Endwert);
                        LblZuBezahlender.Content = zBezahlender;
                    }
                    else
                    {
                        Endwert = bewohner2.Ausgaben - Endwert;
                        zBezahlender = bewohner1.Name;
                        LblBill.Content = Convert.ToString(Endwert);
                        LblZuBezahlender.Content = zBezahlender;
                    }
                }
                else
                {
                    LblToolStrip.Content = $"Mindestens eine Partei muss Ausgaben hinterlegen";
                }
            }
            else
            {
                LblToolStrip.Content = $"Es wurden nicht mindestens 2 User Angelegt";
            }
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxAddUser.Text == "")
            {
                LblToolStrip.Content = $"Kein User Name eingegeben";
                return;
            }
            if (bewohner1.Name == "" || bewohner2.Name == "")
            {
                if (bewohner1.Name == "")
                {
                    //replaceToMehtod
                    bewohner1.Name = txtBoxAddUser.Text;
                    //LblBewohner1.Content = bewohner1.name;
                    cBoxUser.Items.Add(txtBoxAddUser.Text);
                    cBoxUser.SelectedIndex = cBoxUser.Items.Count - 1;
                    LblToolStrip.Content = $"Bewohner {bewohner1.Name} wurde angelegt";
                }
                else if (bewohner2.Name == "" && txtBoxAddUser.Text != bewohner1.Name)
                {
                    bewohner2.Name = txtBoxAddUser.Text;
                    LblBewohner2.Content = bewohner2.Name;
                    cBoxUser.Items.Add(txtBoxAddUser.Text);
                    cBoxUser.SelectedIndex = cBoxUser.Items.Count - 1;
                    LblToolStrip.Content = $"Bewohner {bewohner2.Name} wurde angelegt";
                }
                else
                {
                    LblToolStrip.Content = $"Name gleich wie User1 bitte anderen waehlen";
                }
            }
            else
            {
                LblToolStrip.Content = $"Maximale User anzahl bereits Angelegt";
            }
            //DataContext = customerViewModel;
        }

        private void BtnAddBill_Click(object sender, RoutedEventArgs e)
        {
            if (cBoxUser.Text != "")
            {
                decimal bill = 0;
                if (decimal.TryParse(txtBoxAddBill.Text, out bill) == false)
                {
                    LblToolStrip.Content = $"Not a valid Numver";
                    return;
                }
                if (bewohner1.Name == cBoxUser.Text && cBoxUser.Text != "")
                {
                    bewohner1.AddBetrag(txtBoxCategorie.Text, bill);
                    LblTotalAmountBew1.Content = Convert.ToString(bewohner1.Ausgaben);
                    LblToolStrip.Content = $"Betrag {bill} der Kategorie {txtBoxCategorie.Text} hinzugefuegt";
                }
                else if (bewohner2.Name == cBoxUser.Text && cBoxUser.Text != "")
                {
                    bewohner2.AddBetrag(txtBoxCategorie.Text, bill);
                    LblTotalAmountBew2.Content = Convert.ToString(bewohner2.Ausgaben);
                    LblToolStrip.Content = $"Betrag {bill} der Kategorie {txtBoxCategorie.Text} hinzugefuegt";
                }
                else
                {
                    LblToolStrip.Content = $"Error keine Bewohner wurde mit der im Dropdown ausgewaehlten User identifiziert";
                }
            }
            else LblToolStrip.Content = $"Missing Username";
        }

        private void BtnAuflisten_Click(object sender, RoutedEventArgs e)
        {
            if (cBoxUser.Text != "")
            {
                if (bewohner1.Name == cBoxUser.Text)
                {
                    contributionWindow contributionWindow = new();
                    contributionWindow.FillDataGrid(bewohner1.Einzelbetraege);
                    contributionWindow.ShowDialog();
                }
                else if (bewohner2.Name == cBoxUser.Text)
                {
                    contributionWindow contributionWindow = new();
                    contributionWindow.FillDataGrid(bewohner2.Einzelbetraege);
                    contributionWindow.ShowDialog();
                }
                else
                {
                    LblToolStrip.Content = $"Error keine Bewohner wurde mit der im Dropdown ausgewaehlten User identifiziert";
                }
            }
            else LblToolStrip.Content = $"Kein User Vorhanden bzw Ausgewaehlt";
        }

        private void MenuWPFLoad_Click(object sender, RoutedEventArgs e)
        {

            _XMLPersistance.Reset(bewohner1, bewohner2);
            _XMLPersistance.Load(bewohner1, bewohner2);
            //LblBewohner1.Content = bewohner1.name;
            LblBewohner2.Content = bewohner2.Name;
            LblTotalAmountBew1.Content = bewohner1.Ausgaben.ToString();
            LblTotalAmountBew2.Content = bewohner2.Ausgaben.ToString();
            cBoxUser.Items.Add(bewohner1.Name);
            cBoxUser.Items.Add(bewohner2.Name);
            cBoxUser.SelectedIndex = cBoxUser.Items.Count - 1;
        }

        private void MenuWPFSave_Click(object sender, RoutedEventArgs e)
        {
            Kontext.FilesSystemManagerService.GetXMLPersistance().Save(bewohner1, bewohner2);
        }
        private void MenuWPFNew_Click(object sender, RoutedEventArgs e)
        {
            _XMLPersistance.Reset(bewohner1, bewohner2);
            //LblBewohner1.Content = "Bew1";
            LblBewohner2.Content = "Bew2";
            LblBill.Content = "0";
            LblTotalAmountBew1.Content = "0";
            LblTotalAmountBew2.Content = "0";
            cBoxUser.Items.Clear();
            cBoxUser.Text = "";
        }
        private void MenuWPFSettings_Click(object sender, RoutedEventArgs e)
        {

            //Das View Model Initialisieren
            var vm = this.Kontext.Produziere<ViewModel.Anwendung>();
            //Die Hauptfenster View als Oberfläche benutzen
            vm.Anzeigen<settingsWindow>();


            //Anwendung settingswindow = new Anwendung();
            //settingswindow.Anzeigen<settingsWindow>();

            //settingsWindow settingsWindow = new settingsWindow();
            //settingsWindow.Show();
        }


    }
}
