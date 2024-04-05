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
using Essensausgleich.Data;
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
        //private XMLPersistence _XMLPersistance = null!;
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
            var viewModel = App.ViewModelAnwendung;
            if (viewModel != null)
            {
                btnCalc.Click += (sender, e) => viewModel.CalcOutcome();
                BtnAddUser.Click += (sender, e) => viewModel.AddUser();
                BtnAddBill.Click += (sender, e) => viewModel.AddBill();
                BtnAuflisten.Click += (sender, e) => viewModel.OpenContributioWindow();
                MenuWPFLoad.Click += (sender, e) => viewModel.MenueLoad();
                MenuWPFSave.Click += (sender, e) => viewModel.MenueSave();
                MenuWPFNew.Click += (sender, e) => viewModel.MenueNew();
                MenuWPFSettings.Click += (sender, e) => viewModel.OpenSettingsWindow();

            }


        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // _XMLPersistance = Kontext.FilesSystemManagerService.GetXMLPersistance();

            bewohner1 = this.Kontext.bewohner1;
            bewohner2 = this.Kontext.bewohner2;

            //Todo impl Viewmodel to house every value that will be changed 
            //this.DataContext = bewohner2;
        }
        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            Log.WriteLine("ButtonCalc Clicked");
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Log.WriteLine("ButtonAddUser Clicked");
        }
        private void BtnAddBill_Click(object sender, RoutedEventArgs e)
        {
            Log.WriteLine("ButtonAddBill Clicked");
        }

        private void BtnContributionWindow(object sender, RoutedEventArgs e)
        {

        }
        private void MenuWPFLoad_Click(object sender, RoutedEventArgs e)
        {
            Log.WriteLine("WPFLoad Clicked");
        }
        private void MenuWPFSave_Click(object sender, RoutedEventArgs e)
        {
            //Todo MenueWPFSave
            Log.WriteLine("MenueWPFSave Clicked");
            Kontext.FilesSystemManagerService.GetXMLPersistance().Save(bewohner1, bewohner2);
        }
        private void MenuWPFNew_Click(object sender, RoutedEventArgs e)
        {
            Log.WriteLine("MenueWPFNew Clicked");
            /*
            _XMLPersistance.Reset(bewohner1, bewohner2);
            ////LblBewohner1.Content = "Bew1";
            //LblBewohner2.Content = "Bew2";
            //LblBill.Content = "0";
            //LblTotalAmountBew1.Content = "0";
            //LblTotalAmountBew2.Content = "0";
            //cBoxUser.Items.Clear();
            //cBoxUser.Text = "";
            */
        }

        private void MenuWPFSettings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
