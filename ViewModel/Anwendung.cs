using Essensausgleich.Controller;
using Essensausgleich.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Log = System.Diagnostics.Debug;

namespace Essensausgleich.ViewModel
{
    /// <summary>
    /// blablablaAnwednung
    /// </summary>
    public class Anwendung : Essensausgleich.Infra.ViewModel
    {
        /// <summary>
        /// inits the Viewmodel and pulls object referenzes
        /// </summary>
        public void Initialize()
        {
            this._XMLPersistance = this.Kontext.FilesSystemManagerService.GetXMLPersistance();
            bewohner1 = this.Kontext.bewohner1;
            bewohner2 = this.Kontext.bewohner2;
            _InhabitantsController = this.Kontext.InhabitantsManager.InhabitantsController;
        }
        #region Hauptview
        /// <summary>
        /// Opens the &lt;T&gt; Window
        /// </summary>
        /// <typeparam name="T">Ein WPF Fenster das als 
        /// Hauptfenster der Anwendung benutzt werden soll</typeparam>       
        /// <remarks>Acepts only Obejects of Type Window with an IAppObjekt Interface without a custom Ctor</remarks>
        public void Anzeigen<T>() where T : System.Windows.Window, IAppObjekt, new()
        {
            var f = new T();
            Essensausgleich.App.Current.MainWindow = f;
            //Binds View and Viewmodel (Viewmodel:Anwendung.cs)
            f.DataContext = this;

            //Attach Kontext to the new Window (Infrastructur)
            f.Kontext = this.Kontext;
            f.Show();
        }
        #endregion
        #region Objectimport
        private Bewohner bewohner1 = null!;
        private Bewohner bewohner2 = null!;
        private InhabitantsController _InhabitantsController = null!;
        private XMLPersistence _XMLPersistance = null!;
        #endregion
        #region PropertieBinding

#pragma warning disable 1591
       
        public ObservableCollection<Betrag> BeitragsListe
        {
            get => this._BeitragsListe;
            set
            {
                this._BeitragsListe = value;
                OnPropertyChanged(nameof(BeitragsListe));               
                Log.WriteLine($"{BeitragsListe.GetType().Name} has changed");
            }
        }

        private ObservableCollection<Betrag> _BeitragsListe = new ObservableCollection<Betrag>();

       
        public string InhabitantsSelected
        {
            get => _InhabitansSelected;
            set
            {
                _InhabitansSelected = value;
                OnPropertyChanged();
                Log.WriteLine($"User:{_InhabitansSelected} Selected");
            }
        }
        private string _InhabitansSelected = null!;

        public string LblBillContent
        {
            get => _lblBillContent;
            set
            {
                _lblBillContent = value;
                OnPropertyChanged();
            }
        }
        private string _lblBillContent = null!;

        public string LblZuBezahlenderContent
        {
            get => _lblZuBezahlenderContent;
            set
            {
                _lblZuBezahlenderContent = value;
                OnPropertyChanged();
            }
        }
        private string _lblZuBezahlenderContent = null!;

        public string LblToolStripContent
        {
            get => _lblToolStripContent;
            set
            {
                _lblToolStripContent = value;
                OnPropertyChanged();
            }
        }
        private string _lblToolStripContent = null!;

        public string txtBoxAddUserContent
        {
            get => _txtBoxAddUserContent;
            set
            {
                _txtBoxAddUserContent = value;
                OnPropertyChanged();
            }
        }
        private string _txtBoxAddUserContent = null!;

        public string Bewohner1Name
        {
            get => bewohner1.Name;
            set
            {
                bewohner1.Name = value;
                OnPropertyChanged();
            }
        }
        //private string _Bewohner1Name = null!;

        public decimal AusgabenBewohner1
        {
            get => bewohner1.Ausgaben;
            set
            {
                bewohner1.Ausgaben = value;
                OnPropertyChanged();
                CalcOutcome();
            }
        }
        //private decimal _AusgabenBewohner1;

        public decimal AusgabenBewohner2
        {
            get => bewohner2.Ausgaben;
            set
            {
                bewohner2.Ausgaben = value;
                OnPropertyChanged();
                CalcOutcome();
            }
        }
        //private decimal _AusgabenBewohner2;
        public string Bewohner2Name
        {
            get => bewohner2.Name;
            set
            {
                bewohner2.Name = value;
                OnPropertyChanged();
            }
        }
        //private string _Bewohner2Name = null!;

        public decimal TxtBoxAddBillText
        {
            get => _TxtBoxAddBillText;
            set
            {
                _TxtBoxAddBillText = value;
                OnPropertyChanged();
            }
        }
        private decimal _TxtBoxAddBillText;
        public string TxtBoxCategorieText
        {
            get => _TxtBoxCategorieText;
            set
            {
                _TxtBoxCategorieText = value;
                OnPropertyChanged();
            }
        }
        private string _TxtBoxCategorieText = null!;
        public string CboxUserText
        {
            get => _CboxUserText;
            set
            {
                _CboxUserText = value;
                OnPropertyChanged();
            }
        }
        private string _CboxUserText = null!;
        public string FileName
        {
            get
            {
                return this.Kontext.FilesSystemManagerService.GetXMLPersistance().XMLFileName;
            }
            set
            {
                this.Kontext.FilesSystemManagerService.GetXMLPersistance().ChangePath(value);
            }
        }

        #endregion
        #region MainWindow


        public void CalcOutcome()
        {
            Log.WriteLine("CalcOutcome entert");
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
                        LblBillContent = Convert.ToString(Endwert);
                        LblZuBezahlenderContent = zBezahlender;
                    }
                    else
                    {
                        Endwert = bewohner2.Ausgaben - Endwert;
                        zBezahlender = bewohner1.Name;
                        LblBillContent = Convert.ToString(Endwert);
                        LblZuBezahlenderContent = zBezahlender;
                    }
                }
                else
                {
                    LblToolStripContent = $"Mindestens eine Partei muss Ausgaben hinterlegen";
                }
            }
            else
            {
                LblToolStripContent = $"Es wurden nicht mindestens 2 User Angelegt";
            }
        }
        public void AddUser()
        {
            if (_txtBoxAddUserContent == "")
            {
                LblToolStripContent = $"Kein User Name eingegeben";
                return;
            }
            if (Bewohner1Name == "" || Bewohner2Name == "")
            {
                if (Bewohner1Name == "")
                {
                    //replaceToMehtod
                    Bewohner1Name = _txtBoxAddUserContent;
                    _InhabitantsController.AddInhabitant(Bewohner1Name);
                    InhabitantsSelected = Bewohner1Name;
                    //cBoxUser.Items.Add(txtBoxAddUserContent);
                    //cBoxUser.SelectedIndex = cBoxUser.Items.Count - 1;
                    LblToolStripContent = $"Bewohner {Bewohner1Name} wurde angelegt";
                }
                else if (Bewohner2Name == "" && _txtBoxAddUserContent != Bewohner1Name)
                {
                    Bewohner2Name = _txtBoxAddUserContent;
                    _InhabitantsController.AddInhabitant(Bewohner2Name);
                    InhabitantsSelected = Bewohner2Name;
                    //cBoxUser.Items.Add(txtBoxAddUserContent);
                    //cBoxUser.SelectedIndex = cBoxUser.Items.Count - 1;
                    LblToolStripContent = $"Bewohner {Bewohner2Name} wurde angelegt";
                }
                else
                {
                    LblToolStripContent = $"Name gleich wie User1 bitte anderen waehlen";
                }
            }
            else
            {
                LblToolStripContent = $"Maximale User anzahl bereits Angelegt";
            }
        }
        public void AddBill()
        {
            if (CboxUserText != "")
            {
                decimal bill = 0;
                bill = _TxtBoxAddBillText;

                if (bewohner1.Name == CboxUserText && CboxUserText != "")
                {
                    bewohner1.AddBetrag(_TxtBoxCategorieText, bill);
                    AusgabenBewohner1 = bewohner1.Ausgaben;
                    LblToolStripContent = $"Betrag {bill} der Kategorie {_TxtBoxCategorieText} hinzugefuegt";
                }
                else if (bewohner2.Name == CboxUserText && CboxUserText != "")
                {
                    bewohner2.AddBetrag(_TxtBoxCategorieText, bill);
                    AusgabenBewohner2 = bewohner2.Ausgaben;
                    LblToolStripContent = $"Betrag {bill} der Kategorie {_TxtBoxCategorieText} hinzugefuegt";
                }
                else
                {
                    LblToolStripContent = $"Error keine Bewohner wurde mit der im Dropdown ausgewaehlten User identifiziert";
                }
            }
            else LblToolStripContent = $"Missing Username";
        }
       /// <summary>
       /// Opens The window and fills the Datagrid with the Current
       /// selectedInhabitant to display the total Expenses
       /// </summary>
        public void OpenContributioWindow()
        {
           var contributionWindow = new contributionWindow();
            contributionWindow.DataContext = this;
            contributionWindow.Show(); 

            if (!string.IsNullOrEmpty(InhabitantsSelected))
            {
                if (InhabitantsSelected == Bewohner1Name)
                {       
                    BeitragsListe.Clear();
                    foreach (var item in bewohner1.Einzelbetraege)
                    {
                        BeitragsListe.Add(item);
                    }
                   
                    this.BeitragsListe = new ObservableCollection<Betrag>(bewohner1.Einzelbetraege);

                }
                else if (InhabitantsSelected == Bewohner2Name)
                {
                    BeitragsListe.Clear();
                    foreach (var item in bewohner2.Einzelbetraege)
                    {
                        BeitragsListe.Add(item);
                    }
                   this.BeitragsListe = new ObservableCollection<Betrag>(bewohner2.Einzelbetraege);
                }
                else
                {
                    Log.WriteLine($"No Inhabitant selected or not found, Selcted:{InhabitantsSelected}");
                }
            }
Log.WriteLine("ButtonAuflistung Clicked");
            
            //Hengt datakontext nicht richtig an ak
            /*
            var vm = this.Kontext.Produziere<ViewModel.Anwendung>();
            vm.Anzeigen<contributionWindow>();
            OnPropertyChanged("BeitragsListe");
            */

            

        }


        public void MenueLoad()
        {
            _XMLPersistance.Reset(bewohner1, bewohner2);
            _XMLPersistance.Load(bewohner1, bewohner2);
            Bewohner1Name = bewohner1.Name;
            Bewohner2Name = bewohner2.Name;
            AusgabenBewohner1 = bewohner1.Ausgaben;
            AusgabenBewohner2 = bewohner2.Ausgaben;
            InhabitantsSelected = this.Kontext.InhabitantsManager.InhabitantsController.InhabitantsList[0];
            //foreach (var item in BeitragsListe)
            //{
            //    this.BeitragsListeObs.Add(item);
            //}
            //this.BeitragsListe = bewohner1.Einzelbetraege;
        }
        public void MenueSave()
        {
            _XMLPersistance.Save(bewohner1, bewohner2);
            // Kontext.FilesSystemManagerService.GetXMLPersistance().Save(bewohner1, bewohner2);
        }
        public void MenueNew()
        {
            _XMLPersistance.Reset(bewohner1, bewohner2);
            TxtBoxAddBillText = 0;
            TxtBoxCategorieText = string.Empty;

            ////LblBewohner1.Content = "Bew1";
            //LblBewohner2.Content = "Bew2";
            //LblBill.Content = "0";
            //LblTotalAmountBew1.Content = "0";
            //LblTotalAmountBew2.Content = "0";
            //cBoxUser.Items.Clear();
            //cBoxUser.Text = "";
        }
        public void OpenSettingsWindow()
        {
            var settingsWindow = new settingsWindow();
            settingsWindow.DataContext = this;
            settingsWindow.Show();
        }
#pragma warning restore 1591
        #endregion
        #region settingsWinow

        #endregion
        #region contributionWindow

        #endregion
    }
}
