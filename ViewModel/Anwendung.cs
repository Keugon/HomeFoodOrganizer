using Essensausgleich.Controller;
using Essensausgleich.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
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

            inhabitant1 = this.Context.InhabitantsManager.Inhabitant1;
            this.Context.InhabitantsManager.Inhabitants.Add(inhabitant1);
            inhabitant2 = this.Context.InhabitantsManager.Inhabitant2;
            this.Context.InhabitantsManager.Inhabitants.Add(inhabitant2);
            _InhabitantsController = this.Context.InhabitantsManager.InhabitantsController;

        }
        #region Hauptview
        /// <summary>
        /// Opens the &lt;T&gt; Window
        /// </summary>
        /// <typeparam name="T">Ein WPF Fenster das als 
        /// Hauptfenster der Anwendung benutzt werden soll</typeparam>       
        /// <remarks>Acepts only Obejects of Type Window with an IAppObjekt Interface without a custom Ctor</remarks>
        public void Anzeigen<T>() where T : System.Windows.Window, new()
        {
            var f = new T();
            Essensausgleich.App.Current.MainWindow = f;
            //Binds View and Viewmodel (Viewmodel:Anwendung.cs)
            f.DataContext = this;

            //Attach Context to the new Window (Infrastructur)

            f.Show();
        }
        #endregion
        #region Objectimport
        private Inhabitant inhabitant1 = null!;
        private Inhabitant inhabitant2 = null!;
        private InhabitantsController _InhabitantsController = null!;


        #endregion
        #region RelayCommand
#pragma warning disable 1591
        public RelayCommand DeleteEntry => new RelayCommand(execute => DeleteDataGridEntry());
        public RelayCommand BtnAddUser => new RelayCommand(execute => AddUser());
        public RelayCommand OnEnterAddUser => new RelayCommand(execute => AddUser());
        public RelayCommand BtnAddBill => new(execute => AddBill());
        public RelayCommand OnEnterBill => new(execute => AddBill());
        public RelayCommand MenueWPFNew => new(execute => MenueNew());
        public RelayCommand MenueWPFLoad => new(execute => MenueLoad());
        public RelayCommand MenueWPFSave => new(execute => MenueSave());
        public RelayCommand MenueWPFSaveAs => new(execute => MenueSaveAs());
        public RelayCommand MenueWPFSettings => new(execute => OpenSettingsWindow());
        public RelayCommand OpenContributionWindow => new(execute => OpenContributioWindow());


#pragma warning restore 1591
        #endregion
        #region PropertieBinding
#pragma warning disable 1591
        public ObservableCollection<Expense> ListOfExpenses
        {
            get => this._ListofExpenses;
            set
            {
                this._ListofExpenses = value;
                //OnPropertyChanged(nameof(ListOfExpenses));
                Log.WriteLine($"{ListOfExpenses.GetType().Name} has changed");
            }
        }
        private ObservableCollection<Expense> _ListofExpenses = new ObservableCollection<Expense>();

        public Expense SelectedExpenseItem
        {
            get => _SelectedExpenseItem;
            set
            {
                _SelectedExpenseItem = value;
                OnPropertyChanged();
            }
        }
        private Expense _SelectedExpenseItem;
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
        public string LblpayingInhabitantContent
        {
            get => _LblpayingInhabitantContent;
            set
            {
                _LblpayingInhabitantContent = value;
                OnPropertyChanged();
            }
        }
        private string _LblpayingInhabitantContent = null!;
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
        public string Inhabitant1Name
        {
            get => inhabitant1.Name;
            set
            {
                inhabitant1.Name = value;
                OnPropertyChanged();
            }
        }
        public decimal ExpenseInhabitant1
        {
            get => inhabitant1.TotalExpense;
            set
            {
                inhabitant1.TotalExpense = value;
                OnPropertyChanged();
                CalcOutcome();
            }
        }
        public decimal ExpenseInhabitant2
        {
            get => inhabitant2.TotalExpense;
            set
            {
                inhabitant2.TotalExpense = value;
                OnPropertyChanged();
                CalcOutcome();
            }
        }
        //private decimal _AusgabenBewohner2;
        public string Inhabitant2Name
        {
            get => inhabitant2.Name;
            set
            {
                inhabitant2.Name = value;
                OnPropertyChanged();
            }
        }
        //private string _Bewohner2Name = null!;
        public string TxtBoxAddBillText
        {
            get => _TxtBoxAddBillText;
            set
            {
                _TxtBoxAddBillText = value;
                OnPropertyChanged();
            }
        }
        private string _TxtBoxAddBillText = null!;
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
                return this.Context.InhabitantsManager.JsonFileName;
            }
            set
            {
                this.Context.InhabitantsManager.JsonFileName = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region MainWindow


        public void CalcOutcome()
        {
            Log.WriteLine("CalcOutcome entert");
            decimal Endwert = 0;
            string zBezahlender;
            if (inhabitant1.Name != "" && inhabitant2.Name != "")
            {
                Endwert = (inhabitant1.TotalExpense + inhabitant2.TotalExpense) / 2;
                if (inhabitant1.TotalExpense > 0 || inhabitant2.TotalExpense > 0)
                {
                    if (inhabitant1.TotalExpense > inhabitant2.TotalExpense)
                    {
                        Endwert = inhabitant1.TotalExpense - Endwert;
                        zBezahlender = inhabitant2.Name;
                        LblBillContent = Convert.ToString(Endwert);
                        LblpayingInhabitantContent = zBezahlender;
                    }
                    else
                    {
                        Endwert = inhabitant2.TotalExpense - Endwert;
                        zBezahlender = inhabitant1.Name;
                        LblBillContent = Convert.ToString(Endwert);
                        LblpayingInhabitantContent = zBezahlender;
                    }
                }
                else
                {
                    LblToolStripContent = $"Mindestens eine Partei muss TotalExpense hinterlegen";
                }
            }
            else
            {
                LblToolStripContent = $"Es wurden nicht mindestens 2 User Angelegt";
            }
        }
        public void AddUser()
        {
            if (!string.IsNullOrEmpty(_txtBoxAddUserContent))
            {
                if (Inhabitant1Name == string.Empty || Inhabitant2Name == string.Empty)
                {
                    if (Inhabitant1Name == string.Empty && Regex.IsMatch(_txtBoxAddUserContent, @"^[a-zA-Z]+$"))
                    {
                        //replaceToMehtod
                        Inhabitant1Name = _txtBoxAddUserContent;
                        _InhabitantsController.AddInhabitant(Inhabitant1Name);
                        InhabitantsSelected = Inhabitant1Name;
                        //cBoxUser.Items.Add(txtBoxAddUserContent);
                        //cBoxUser.SelectedIndex = cBoxUser.Items.Count - 1;
                        LblToolStripContent = $"Inhabitant {Inhabitant1Name} wurde angelegt";
                    }
                    else if (Inhabitant2Name == string.Empty && _txtBoxAddUserContent != Inhabitant1Name && Regex.IsMatch(_txtBoxAddUserContent, @"^[a-zA-Z]+$"))
                    {
                        Inhabitant2Name = _txtBoxAddUserContent;
                        _InhabitantsController.AddInhabitant(Inhabitant2Name);
                        InhabitantsSelected = Inhabitant2Name;
                        //cBoxUser.Items.Add(txtBoxAddUserContent);
                        //cBoxUser.SelectedIndex = cBoxUser.Items.Count - 1;
                        LblToolStripContent = $"Inhabitant {Inhabitant2Name} wurde angelegt";
                    }
                    else
                    {
                        LblToolStripContent = $"Invalide Username or Already Exists!";
                    }
                }
                else
                {
                    LblToolStripContent = $"Maximale User anzahl bereits Angelegt";
                }
                txtBoxAddUserContent = string.Empty;
            }
            else
            {
                LblToolStripContent = $"Kein User Name eingegeben";
                return;
            }

        }
        public void AddBill()
        {
            if (CboxUserText != string.Empty)
            {
                decimal bill = 0;

                if (decimal.TryParse(_TxtBoxAddBillText, out bill) && bill > 0)
                {
                    if (inhabitant1.Name == CboxUserText && CboxUserText != string.Empty)
                    {
                        inhabitant1.AddBetrag(_TxtBoxCategorieText, bill);
                        ExpenseInhabitant1 = inhabitant1.TotalExpense;
                        LblToolStripContent = $"Expense {bill} der Kategorie {_TxtBoxCategorieText} hinzugefuegt";
                    }
                    else if (inhabitant2.Name == CboxUserText && CboxUserText != string.Empty)
                    {
                        inhabitant2.AddBetrag(_TxtBoxCategorieText, bill);
                        ExpenseInhabitant2 = inhabitant2.TotalExpense;
                        LblToolStripContent = $"Expense {bill} der Kategorie {_TxtBoxCategorieText} hinzugefuegt";
                    }
                    else
                    {
                        LblToolStripContent = $"Error keine Inhabitant wurde mit der im Dropdown ausgewaehlten User identifiziert";
                    }
                }
                else
                {
                    Log.WriteLine("Invalide Value Input");
                }
            }
            else LblToolStripContent = $"Missing Username";
            TxtBoxAddBillText = string.Empty;
            TxtBoxCategorieText = string.Empty;
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
                if (InhabitantsSelected == Inhabitant1Name)
                {
                    ListOfExpenses.Clear();
                    foreach (var item in inhabitant1.ListOfExpenses)
                    {
                        ListOfExpenses.Add(item);
                    }

                    this.ListOfExpenses = new ObservableCollection<Expense>(inhabitant1.ListOfExpenses);

                }
                else if (InhabitantsSelected == Inhabitant2Name)
                {
                    ListOfExpenses.Clear();
                    foreach (var item in inhabitant2.ListOfExpenses)
                    {
                        ListOfExpenses.Add(item);
                    }
                    this.ListOfExpenses = new ObservableCollection<Expense>(inhabitant2.ListOfExpenses);
                }
                else
                {
                    Log.WriteLine($"No Inhabitant selected or not found, Selcted:{InhabitantsSelected}");
                }
            }
            Log.WriteLine("ButtonAuflistung Clicked");

            //Hengt datakontext nicht richtig an ak
            /*
            var vm = this.Context.Fabricate<ViewModel.Anwendung>();
            vm.Anzeigen<contributionWindow>();
            OnPropertyChanged("ListOfExpenses");
            */



        }
        public void MenueLoad()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Json Files|*.json|Xml Files|*.xml";
            bool? dialogResult = dialog.ShowDialog();

            if (dialogResult == true)
            {
                this.Context.InhabitantsManager.JsonFileName = dialog.FileName;
                if (!File.Exists(this.Context.InhabitantsManager.JsonFileName))
                {
                    Log.WriteLine($"{this.Context.InhabitantsManager.JsonFileName} not Found");
                    return;
                }
this.Context.InhabitantsManager.Inhabitants = this.Context.InhabitantsManager.InhabitantsController.Load(FilePathAndName:dialog.FileName);
            }
            else
            {
                Log.WriteLine("OpenFile Dialog cancelt = False");
                return;
            }


           //Todo this.Context.InhabitantsManager.InhabitantsController.Reset(inhabitant1, inhabitant2);

            

            inhabitant1 = this.Context.InhabitantsManager.Inhabitants[0];
            inhabitant2 = this.Context.InhabitantsManager.Inhabitants[1];

            Inhabitant1Name = inhabitant1.Name;
            Inhabitant2Name = inhabitant2.Name;
            ExpenseInhabitant1 = inhabitant1.TotalExpense;
            ExpenseInhabitant2 = inhabitant2.TotalExpense;
            this.Context.InhabitantsManager.InhabitantsController.AddInhabitant(inhabitant1.Name);
            this.Context.InhabitantsManager.InhabitantsController.AddInhabitant(inhabitant2.Name);
            InhabitantsSelected = this.Context.InhabitantsManager.InhabitantsController.InhabitantsNameList[0];
        }
        public void MenueSave()
        {

            if (inhabitant1.TotalExpense > 0 && inhabitant2.TotalExpense > 0)
            {
                if (!Path.Exists(this.Context.InhabitantsManager.JsonFileName))
                {
                    {
                        this.Context.InhabitantsManager.Save();
                    }
                }
                else
                {
                    Log.WriteLine("FileExits!");
                    MessageBoxResult mr = MessageBox.Show("File Exists, Overwrite?", "File Name Exits", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (mr == MessageBoxResult.Yes)
                    {
                        this.Context.InhabitantsManager.Save();
                    }
                    else
                    {
                        Log.WriteLine("Not Saved");
                    }

                }
            }
            else
            {
                Log.WriteLine("Both User needs Intput");
            }
            //if (inhabitant1.TotalExpense > 0 && inhabitant2.TotalExpense > 0)
            //{
            //    if (!Path.Exists(_XMLPersistance.XMLFileName))
            //    {
            //        {
            //            _XMLPersistance.Save(inhabitant1, inhabitant2);
            //        }
            //    }
            //    else
            //    {
            //        Log.WriteLine("FileExits!!");
            //        MessageBoxResult mr = MessageBox.Show("File Exists, Overwrite?", "File Name Exits", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            //        if (mr == MessageBoxResult.Yes)
            //        {
            //            _XMLPersistance.Save(inhabitant1, inhabitant2);
            //        }
            //        else
            //        {
            //            Log.WriteLine("Not Saved");
            //        }

            //    }
            //}
            //else
            //{
            //    Log.WriteLine("Both User needs Intput");
            //}
        }
        public void MenueSaveAs()
        {
            if (inhabitant1.TotalExpense > 0 && inhabitant2.TotalExpense > 0)
            {
                var dialog = new SaveFileDialog();
                dialog.Filter = "Xml Files|*.xml";
                dialog.AddExtension = true;
                dialog.DefaultExt = ".xml";
                bool? dialogResult = dialog.ShowDialog();

                if (dialogResult == true)
                {
                   //todo _XMLPersistance.ChangePath(dialog.SafeFileName);
                    MenueSave();
                }
                else
                {
                    Log.WriteLine("OpenFile Dialog cancelt = False");
                    return;
                }
            }
            else
            {
                Log.WriteLine("Both User needs Intput");
            }


        }
        public void MenueNew()
        {
            //todo _XMLPersistance.Reset(inhabitant1, inhabitant2);
            TxtBoxAddBillText = "0";
            TxtBoxCategorieText = string.Empty;
            Inhabitant1Name = inhabitant1.Name;
            Inhabitant2Name = inhabitant2.Name;
            ExpenseInhabitant1 = inhabitant1.TotalExpense;
            ExpenseInhabitant2 = inhabitant2.TotalExpense;
            LblpayingInhabitantContent = string.Empty;
            LblBillContent = string.Empty;
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
        /// <summary>
        /// Delets via Context Menue a DataGrid item 
        /// and convays the change down to the Inhabitant object 
        /// </summary>
        public void DeleteDataGridEntry()
        {// delet Entry and updates source
            ListOfExpenses.Remove(SelectedExpenseItem);
            if (InhabitantsSelected == Inhabitant1Name)
            {
                inhabitant1.TotalExpense -= SelectedExpenseItem.valueExpense;
                inhabitant1.ListOfExpenses.Clear();
                foreach (var item in ListOfExpenses)
                {

                    inhabitant1.ListOfExpenses.Add(item);
                }
                OnPropertyChanged("ExpenseInhabitant1");
            }
            else if (InhabitantsSelected == Inhabitant2Name)
            {
                inhabitant2.TotalExpense -= SelectedExpenseItem.valueExpense;
                inhabitant2.ListOfExpenses.Clear();
                foreach (var item in ListOfExpenses)
                {

                    inhabitant2.ListOfExpenses.Add(item);
                }
                OnPropertyChanged("ExpenseInhabitant2");
            }
        }
        #endregion
    }
}
