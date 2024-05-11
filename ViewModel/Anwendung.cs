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
            //inhabitants
            // inhabitant1 = this.Context.InhabitantsManager.Inhabitant1;
            // inhabitant2 = this.Context.InhabitantsManager.Inhabitant2;
            //CurrentInvoice = this.Context.InvoiceManager.Invoice;

            // CurrentInvoice.AddInhabitantToList(inhabitant1);
            // CurrentInvoice.AddInhabitantToList(inhabitant2);
            //_InhabitantsController = this.Context.InhabitantsManager.InhabitantsController;



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

        //private Inhabitant inhabitant1 = null!;
        //private Inhabitant inhabitant2 = null!;
        private InhabitantsController _InhabitantsController = null!;

        private int CurrentInvoicesIndex = 0;




        #endregion
        #region RelayCommand
#pragma warning disable 1591
        public RelayCommand DeleteEntry => new RelayCommand(execute => DeleteDataGridEntry());
        public RelayCommand BtnAddUser => new RelayCommand(execute => AddUser());
        public RelayCommand OnEnterAddUser => new RelayCommand(execute => AddUser());
        public RelayCommand BtnAddBill => new(execute => AddBill());
        public RelayCommand NextInvoice => new RelayCommand(execute =>
        {
            this.CurrentInvoice = this.Context.InvoiceManager.Invoices[++CurrentInvoicesIndex];
        }, canExecute =>
                        {
                            if (CurrentInvoicesIndex < this.Context.InvoiceManager.Invoices.Count - 1)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        });
        public RelayCommand PreviousInvoice => new RelayCommand(execute =>
        {
            this.CurrentInvoice = this.Context.InvoiceManager.Invoices[--CurrentInvoicesIndex];
        }, canExecute =>
                        {
                            if (CurrentInvoicesIndex > 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        });
        public RelayCommand OnEnterBill => new(execute => AddBill());
        public RelayCommand MenueWPFNew => new(execute => MenueNew());
        public RelayCommand MenueWPFLoad => new(execute => MenueLoad());
        public RelayCommand MenueWPFLoadProject => new(execute => MenueLoadProject());
        public RelayCommand MenueWPFSave => new(execute => MenueSave());
        public RelayCommand MenueWPFSaveAs => new(execute => MenueSaveAs());
        public RelayCommand MenueWPFSettings => new(execute => OpenSettingsWindow());
        public RelayCommand OpenContributionWindow => new(execute => OpenContributioWindow());


#pragma warning restore 1591
        #endregion
        #region PropertieBinding
#pragma warning disable 1591
        private Invoice _CurrentInvoice = null!;
        public Invoice CurrentInvoice
        {
            get
            {
                if (this._CurrentInvoice == null)
                {
                    this._CurrentInvoice = new Invoice();
                }
                return this._CurrentInvoice;
            }
            set
            {

                System.Diagnostics.Debug.WriteLine("CurrentInvoice Beginn Set");
                this._CurrentInvoice = value;
                OnPropertyChanged(nameof(Inhabitant1Name));
                OnPropertyChanged(nameof(Inhabitant2Name));
                OnPropertyChanged(nameof(InhabitantsNameList));
                if (this.CurrentInvoice.InhabitantsNameList.Count == 2)
                {
                    InhabitantsSelected = this.CurrentInvoice.InhabitantsNameList[0];
                }
                OnPropertyChanged(nameof(ExpenseInhabitant1));
                OnPropertyChanged(nameof(ExpenseInhabitant2));
                System.Diagnostics.Debug.WriteLine("CurrentInvoice End Set");
            }
        }
        public ObservableCollection<Expense> ListOfExpenses
        {
            get
            {

                if (this._ListofExpenses == null)
                {
                    this._ListofExpenses = new ObservableCollection<Expense>();
                }
                return this._ListofExpenses;
            }

            set
            {
                this._ListofExpenses = value;
                //OnPropertyChanged(nameof(ListOfExpenses));
                Log.WriteLine($"{ListOfExpenses.GetType().Name} has changed");
            }
        }
        private ObservableCollection<Expense>? _ListofExpenses;
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
            get
            {

                return this._InhabitansSelected;
            }
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
            get => CurrentInvoice.Inhabitants[0].Name;
            set
            {
                CurrentInvoice.Inhabitants[0].Name = value;
                OnPropertyChanged();
            }
        }
        public decimal ExpenseInhabitant1
        {
            get => CurrentInvoice.Inhabitants[0].TotalExpense;
            //set
            //{
            //    this.CurrentInvoice.Inhabitants[0].TotalExpense = value;
            //    OnPropertyChanged();
            //    CalcOutcome();
            //}
        }
        public decimal ExpenseInhabitant2
        {
            get => CurrentInvoice.Inhabitants[1].TotalExpense;
            //set
            //{
            //    this.CurrentInvoice.Inhabitants[1].TotalExpense = value;
            //    OnPropertyChanged();
            //    CalcOutcome();
            //}
        }
        //private decimal _AusgabenBewohner2;
        public string Inhabitant2Name
        {
            get => CurrentInvoice.Inhabitants[1].Name;
            set
            {
                CurrentInvoice.Inhabitants[1].Name = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _InhabitantsNameList = new ObservableCollection<string>();
        public ObservableCollection<string> InhabitantsNameList
        {
            get => this.CurrentInvoice.InhabitantsNameList;


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

                return this.CurrentInvoice.FileName!;
            }
            set
            {
                this.CurrentInvoice.FileName = value;
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
            if (CurrentInvoice.Inhabitants[0].Name != "" && CurrentInvoice.Inhabitants[1].Name != "")
            {
                Endwert = (CurrentInvoice.Inhabitants[0].TotalExpense + CurrentInvoice.Inhabitants[1].TotalExpense) / 2;
                if (CurrentInvoice.Inhabitants[0].TotalExpense > 0 || CurrentInvoice.Inhabitants[1].TotalExpense > 0)
                {
                    if (CurrentInvoice.Inhabitants[0].TotalExpense > CurrentInvoice.Inhabitants[1].TotalExpense)
                    {
                        Endwert = CurrentInvoice.Inhabitants[0].TotalExpense - Endwert;
                        zBezahlender = CurrentInvoice.Inhabitants[1].Name;
                        LblBillContent = Convert.ToString(Endwert);
                        LblpayingInhabitantContent = zBezahlender;
                    }
                    else
                    {
                        Endwert = CurrentInvoice.Inhabitants[1].TotalExpense - Endwert;
                        zBezahlender = CurrentInvoice.Inhabitants[0].Name;
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

                        Inhabitant1Name = _txtBoxAddUserContent;
                        CurrentInvoice.InhabitantsNameList.Add(Inhabitant1Name);
                        InhabitantsSelected = Inhabitant1Name;
                        LblToolStripContent = $"Inhabitant {Inhabitant1Name} wurde angelegt";
                    }
                    else if (Inhabitant2Name == string.Empty && _txtBoxAddUserContent != Inhabitant1Name && Regex.IsMatch(_txtBoxAddUserContent, @"^[a-zA-Z]+$"))
                    {
                        Inhabitant2Name = _txtBoxAddUserContent;
                        CurrentInvoice.InhabitantsNameList.Add(Inhabitant2Name);
                        InhabitantsSelected = Inhabitant2Name;
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
                    if (this.CurrentInvoice.Inhabitants[0].Name == CboxUserText && CboxUserText != string.Empty)
                    {
                        CurrentInvoice.Inhabitants[0].AddBetrag(_TxtBoxCategorieText, bill);
                        OnPropertyChanged(nameof(ExpenseInhabitant1));
                        //ExpenseInhabitant1 = this.CurrentInvoice.Inhabitants[0].TotalExpense;
                        LblToolStripContent = $"Expense {bill} der Kategorie {_TxtBoxCategorieText} hinzugefuegt";
                    }
                    else if (this.CurrentInvoice.Inhabitants[1].Name == CboxUserText && CboxUserText != string.Empty)
                    {
                        CurrentInvoice.Inhabitants[1].AddBetrag(_TxtBoxCategorieText, bill);
                        OnPropertyChanged(nameof(ExpenseInhabitant2));
                        //ExpenseInhabitant2 = this.CurrentInvoice.Inhabitants[1].TotalExpense;
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
                    foreach (var item in this.CurrentInvoice.Inhabitants[0].ListOfExpenses)
                    {
                        ListOfExpenses.Add(item);
                    }

                    this.ListOfExpenses = new ObservableCollection<Expense>(this.CurrentInvoice.Inhabitants[0].ListOfExpenses);

                }
                else if (InhabitantsSelected == Inhabitant2Name)
                {
                    ListOfExpenses.Clear();
                    foreach (var item in this.CurrentInvoice.Inhabitants[1].ListOfExpenses)
                    {
                        ListOfExpenses.Add(item);
                    }
                    this.ListOfExpenses = new ObservableCollection<Expense>(this.CurrentInvoice.Inhabitants[1].ListOfExpenses);
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

                if (!File.Exists(dialog.FileName))
                {
                    Log.WriteLine($"{dialog.FileName} not Found");
                    return;
                }
                //CurrentInvoice = null!;
                Invoice i = this.Context.InvoiceManager.Load(dialog.FileName);
                i.FileName = dialog.FileName;
                CurrentInvoice = i;

            }
            else
            {
                Log.WriteLine("OpenFile Dialog cancelt = False");
                return;
            }


            //Todo this.Context.InhabitantsManager.InhabitantsController.Reset(inhabitant1, inhabitant2);



            //inhabitant1 = this.Context.InhabitantsManager.Inhabitants[0];
            //inhabitant2 = this.Context.InhabitantsManager.Inhabitants[1];

            //Inhabitant1Name = inhabitant1.Name;
            //Inhabitant2Name = inhabitant2.Name;
            //ExpenseInhabitant1 = inhabitant1.TotalExpense;
            //ExpenseInhabitant2 = inhabitant2.TotalExpense;
            //this.Context.InhabitantsManager.InhabitantsController.AddInhabitant(inhabitant1.Name);
            //this.Context.InhabitantsManager.InhabitantsController.AddInhabitant(inhabitant2.Name);
            //InhabitantsSelected = this.Context.InhabitantsManager.InhabitantsController.InhabitantsNameList[0];
        }
        public void MenueLoadProject()
        {
            var dialog = new OpenFolderDialog();
            bool? dialogResult = dialog.ShowDialog();

            if (dialogResult == true)
            {
                if (!Directory.Exists(dialog.FolderName))
                {
                    Log.WriteLine($"The Directory: {dialog.FolderName} does not exist or cant be accsessd");
                    return;
                }
                Invoice.FolderPath = dialog.FolderName;
                string[] files = Directory.GetFiles(dialog.FolderName);
                Invoices FileLoadList = new Invoices();
                foreach (string file in files)
                {
                    Invoice i = this.Context.InvoiceManager.Load(file);
                    i.FileName = file;
                    FileLoadList.Add(i);
                }
                this.Context.InvoiceManager.Invoices = FileLoadList;

                if (this.Context.InvoiceManager.Invoices != null)
                {
                    this.CurrentInvoice = this.Context.InvoiceManager.Invoices[0];
                }

            }

            System.Diagnostics.Debug.WriteLine("MenueLoadProject End");
        }
        public void MenueSave()
        {
            if (this.CurrentInvoice.FileName != null)
            {
                if (!Path.Exists(this.FileName))
                {
                    {
                        this.Context.InvoiceManager.Save(CurrentInvoice);
                    }
                }
                else
                {
                    Log.WriteLine("FileExits!");
                    MessageBoxResult mr = MessageBox.Show("File Exists, Overwrite?", "File Name Exits", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (mr == MessageBoxResult.Yes)
                    {
                        this.Context.InvoiceManager.Save(CurrentInvoice);
                    }
                    else
                    {
                        Log.WriteLine("Not Saved");
                    }

                }
            }
            else
            {

                System.Diagnostics.Debug.WriteLine("No FilePath exist therefore a new \"Not Loaded File\"");
                MenueSaveAs();
            }



        }
        public void MenueSaveAs()
        {

            var dialog = new SaveFileDialog();
            dialog.Filter = "Json Files|*.json";
            dialog.AddExtension = true;
            dialog.DefaultExt = ".json";
            bool? dialogResult = dialog.ShowDialog();

            if (dialogResult == true)
            {
                this.CurrentInvoice.FileName = dialog.FileName;
                this.Context.InvoiceManager.Save(CurrentInvoice);
            }
            else
            {
                Log.WriteLine("OpenFile Dialog cancelt = False");
                return;
            }



        }
        public void MenueNew()
        {
            this.CurrentInvoice = null!;
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
                //this.CurrentInvoice.Inhabitants[0].TotalExpense -= SelectedExpenseItem.valueExpense;
                this.CurrentInvoice.Inhabitants[0].ListOfExpenses.Clear();
                foreach (var item in ListOfExpenses)
                {

                    this.CurrentInvoice.Inhabitants[0].ListOfExpenses.Add(item);
                }
                OnPropertyChanged(nameof(ExpenseInhabitant1));
                OnPropertyChanged(nameof(ListOfExpenses));
            }
            else if (InhabitantsSelected == Inhabitant2Name)
            {
                //this.CurrentInvoice.Inhabitants[1].TotalExpense -= SelectedExpenseItem.valueExpense;
                this.CurrentInvoice.Inhabitants[1].ListOfExpenses.Clear();
                foreach (var item in ListOfExpenses)
                {

                    this.CurrentInvoice.Inhabitants[1].ListOfExpenses.Add(item);
                }
                OnPropertyChanged(nameof(ExpenseInhabitant2));
                OnPropertyChanged(nameof(ListOfExpenses));
            }
        }
        #endregion
    }
}
