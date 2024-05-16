using Essensausgleich.Controller;
using Essensausgleich.Data;
using Essensausgleich.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
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
        private int CurrentInvoicesIndex = 0;
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
        public RelayCommand OpenInvoiceViewSideWindow => new(execute => InvoiceViewSideWindow(), canExecute =>
        {
            if (this.Context.InvoiceManager.Invoices.Count > 0)
            {
                return true;

            }
            return false;

        });
        public RelayCommand OnEnterBill => new(execute => AddBill());
        public RelayCommand MenueWPFNew => new(execute => MenueNew());
        public RelayCommand MenueWPFLoad => new(execute => MenueLoad());
        public RelayCommand MenueWPFLoadProject => new(execute => MenueLoadProject());
        public RelayCommand MenueWPFSave => new(execute => MenueSave());
        public RelayCommand MenueWPFSaveAs => new(execute => MenueSaveAs());
        public RelayCommand MenueWPFSettings => new(execute => OpenSettingsWindow());
        public RelayCommand OpenContributionWindow => new(execute => OpenContributioWindow(execute), canExecute =>
        {
            if (canExecute is System.Windows.Controls.Label label)
            {
                if (label.Content.ToString() == string.Empty)
                {
                    return false;
                }
                return true;
            }
            return false;
        });
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
                OnPropertyChanged(nameof(InvoiceCommentary));
                OnPropertyChanged(nameof(LblpayingInhabitantContent));
                OnPropertyChanged(nameof(LblBillContent));
                OnPropertyChanged(nameof(FileName));
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
            get
            {
                decimal result = (this.CurrentInvoice.Inhabitants[0].TotalExpense + this.CurrentInvoice.Inhabitants[1].TotalExpense) / 2;
                if (this.CurrentInvoice.Inhabitants[0].TotalExpense > this.CurrentInvoice.Inhabitants[1].TotalExpense)
                {
                    result = this.CurrentInvoice.Inhabitants[0].TotalExpense - result;
                }
                else
                {

                    result = this.CurrentInvoice.Inhabitants[1].TotalExpense - result;

                }
                return result.ToString();
            }
            set
            {
                _lblBillContent = value;
                OnPropertyChanged();
            }
        }
        private string _lblBillContent = null!;
        public string LblpayingInhabitantContent
        {
            get
            {
                if (this.CurrentInvoice.Inhabitants[0].TotalExpense > this.CurrentInvoice.Inhabitants[1].TotalExpense)
                {
                    return this._LblpayingInhabitantContent = this.CurrentInvoice.Inhabitants[1].Name;
                }
                else if (this.CurrentInvoice.Inhabitants[1].TotalExpense > this.CurrentInvoice.Inhabitants[0].TotalExpense)
                {
                    return this._LblpayingInhabitantContent = this.CurrentInvoice.Inhabitants[0].Name;
                }
                return string.Empty;
            }
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
        }
        public decimal ExpenseInhabitant2
        {
            get => CurrentInvoice.Inhabitants[1].TotalExpense;
        }
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
        private string _InvoiceCommentary = string.Empty!;
        /// <summary>
        /// Gets or Sets the Commentary for the CurrentInvoice Object
        /// </summary>
        public string InvoiceCommentary
        {
            get => this.CurrentInvoice.InvoiceComment;
            set => this.CurrentInvoice.InvoiceComment = value;
        }
#pragma warning restore 1591
        #endregion       
        /// <summary>
        /// Adds the Name to the Inhabitant object and Name List
        /// </summary>
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
        /// <summary>
        /// Adds a Expens struct to the dedicated Inhabitant object
        /// </summary>
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
                        LblToolStripContent = $"Expense {bill} der Kategorie {_TxtBoxCategorieText} hinzugefuegt";
                    }
                    else if (this.CurrentInvoice.Inhabitants[1].Name == CboxUserText && CboxUserText != string.Empty)
                    {
                        CurrentInvoice.Inhabitants[1].AddBetrag(_TxtBoxCategorieText, bill);
                        OnPropertyChanged(nameof(ExpenseInhabitant2));
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
            OnPropertyChanged(nameof(LblpayingInhabitantContent));
            OnPropertyChanged(nameof(LblBillContent));
        }
        /// <summary>
        /// Opens The window and fills the Datagrid with the Current
        /// selectedInhabitant to display the total Expenses
        /// </summary>
        public void OpenContributioWindow(object parameter)
        {


            if (parameter is System.Windows.Controls.Label label)
            {
                System.Diagnostics.Debug.WriteLine($"Label Content:{label.Content}");
                var contributionWindow = new contributionWindow();
                contributionWindow.DataContext = this;


                if (label.Content.ToString() != string.Empty)
                {
                    if (label.Content.ToString() == Inhabitant1Name)
                    {
                        ListOfExpenses.Clear();
                        foreach (var item in this.CurrentInvoice.Inhabitants[0].ListOfExpenses)
                        {
                            ListOfExpenses.Add(item);
                        }
                        this.ListOfExpenses = new ObservableCollection<Expense>(this.CurrentInvoice.Inhabitants[0].ListOfExpenses);
                        contributionWindow.Show();
                        contributionWindow.SizeToContent = SizeToContent.Height;
                    }
                    else if (label.Content.ToString() == Inhabitant2Name)
                    {
                        ListOfExpenses.Clear();
                        foreach (var item in this.CurrentInvoice.Inhabitants[1].ListOfExpenses)
                        {
                            ListOfExpenses.Add(item);
                        }
                        this.ListOfExpenses = new ObservableCollection<Expense>(this.CurrentInvoice.Inhabitants[1].ListOfExpenses);
                        contributionWindow.Show();
                        contributionWindow.SizeToContent = SizeToContent.Height;
                    }
                    else
                    {
                        Log.WriteLine($"No Inhabitant selected or not found, Selcted:{InhabitantsSelected}");
                    }
                }
            }
        }
        /// <summary>
        /// Loads a Single Invoice File
        /// </summary>
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
                Invoice i = this.Context.InvoiceManager.Load(dialog.FileName);
                i.FileName = dialog.FileName;
                CurrentInvoice = i;

            }
            else
            {
                Log.WriteLine("OpenFile Dialog cancelt = False");
                return;
            }
        }
        /// <summary>
        /// Loads all Fitting Invoice Files of a Choosen Folder via Dialog
        /// </summary>
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
                string[] FilesInFolder = Directory.GetFiles(dialog.FolderName);
                if (FilesInFolder.Length == 0)
                {
                    Log.WriteLine("EmpfyFolder");
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
                if (FileLoadList.Count > 0)
                {

                    Log.WriteLine("No Invoices Found");
                    Log.WriteLine($"Files in Folder:");
                    foreach (var file in files)
                    {
                        Log.WriteLine($"{Path.GetFileName(file)}");
                    }

                    return;
                }
                this.Context.InvoiceManager.Invoices = FileLoadList;
                //To Avoid Index Missmatch on reload
                CurrentInvoicesIndex = 0;
                if (this.Context.InvoiceManager.Invoices != null)
                {
                    this.CurrentInvoice = this.Context.InvoiceManager.Invoices[CurrentInvoicesIndex];
                }
            }

            System.Diagnostics.Debug.WriteLine("MenueLoadProject End");
        }
        /// <summary>
        /// Loads all Fitting Files from a Choosen Folder
        /// </summary>
        /// <param name="FolderName"></param>
        public void MenueLoadProject(string FolderName)
        {
            if (!Directory.Exists(FolderName))
            {
                Log.WriteLine($"The Directory: {FolderName} does not exist or cant be accsessd");
                return;
            }
            Invoice.FolderPath = FolderName;
            string[] files = Directory.GetFiles(FolderName);
            Invoices FileLoadList = new Invoices();
            foreach (string file in files)
            {
                Invoice i = this.Context.InvoiceManager.Load(file);
                i.FileName = file;
                FileLoadList.Add(i);
            }
            this.Context.InvoiceManager.Invoices = FileLoadList;
            //To Avoid Index Missmatch on reload
            CurrentInvoicesIndex = 0;
            this.CurrentInvoice = this.Context.InvoiceManager.Invoices[CurrentInvoicesIndex];
        }
        /// <summary>
        /// Saves the Current Invoice Object in the Current FileName if not null else switches to SaveAs()
        /// </summary>
        public void MenueSave()
        {
            if (this.CurrentInvoice.FileName != null)
            {
                if (!Path.Exists(this.FileName))
                {
                    {
                        //Only on creation of a invoice to keep the original datetime
                        if (this.CurrentInvoice.DateTimeCreation == null)
                        {
                            this.CurrentInvoice.DateTimeCreation = DateTime.Now;
                        }
                        this.CurrentInvoice.DateTimeChanged = DateTime.Now;
                        this.Context.InvoiceManager.Save(CurrentInvoice);
                    }
                }
                else
                {
                    Log.WriteLine("FileExits!");
                    MessageBoxResult mr = MessageBox.Show("File Exists, Overwrite?", "File Name Exits", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (mr == MessageBoxResult.Yes)
                    {
                        //Only on creation of a invoice to keep the original datetime
                        if (this.CurrentInvoice.DateTimeCreation == null)
                        {
                            this.CurrentInvoice.DateTimeCreation = DateTime.Now;
                        }
                        this.CurrentInvoice.DateTimeChanged = DateTime.Now;
                        this.Context.InvoiceManager.Save(CurrentInvoice);
                        if (this.Context.InvoiceManager.Invoices.Count > 0)
                        {
                            this.Context.InvoiceManager.Invoices = this.Context.InvoiceManager.Invoices;
                        }

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
        /// <summary>
        /// Saves the Current Invoice Object per Dialog to a Choosen FileName and Location
        /// </summary>
        public void MenueSaveAs()
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Json Files|*.json";
            dialog.AddExtension = true;
            dialog.DefaultExt = ".json";
            bool? dialogResult = dialog.ShowDialog();

            if (dialogResult == true)
            {
                var CurrenInvoiceFolder = Path.GetDirectoryName(this.CurrentInvoice.FileName);
                this.CurrentInvoice.FileName = dialog.FileName;

                //Only on creation of a invoice to keep the original datetime
                if (this.CurrentInvoice.DateTimeCreation == null)
                {
                    this.CurrentInvoice.DateTimeCreation = DateTime.Now;
                }
                this.CurrentInvoice.DateTimeChanged = DateTime.Now;
                this.Context.InvoiceManager.Save(CurrentInvoice);

                if (this.Context.InvoiceManager.Invoices.Count > 0 && Path.GetDirectoryName(dialog.FileName) == CurrenInvoiceFolder)
                {
                    MenueLoadProject(CurrenInvoiceFolder!);
                    System.Diagnostics.Debug.WriteLine("File Added to current Invoices Folder");
                }
            }
            else
            {
                Log.WriteLine("OpenFile Dialog cancelt = False");
                return;
            }

        }
        /// <summary>
        /// Nulls the CurrentInvoice Object to start Fresh over
        /// </summary>
        public void MenueNew()
        {
            this.CurrentInvoice = null!;
        }
        /// <summary>
        /// Opens and Closes a to the Right attached Window to Display a Datagrid with all Loaded Invoices
        /// </summary>
        public void InvoiceViewSideWindow()
        {
            var InvoiceView = App.Current.Windows.OfType<InvoiceViewSideWindow>().FirstOrDefault();
            if (InvoiceView != null)
            {
                InvoiceView.Close();
            }
            else
            {
                var InvoiceViewWindow = new InvoiceViewSideWindow();
                InvoiceViewWindow.DataContext = this;
                InvoiceViewWindow.Left = App.Current.MainWindow.Left + App.Current.MainWindow.Width;
                InvoiceViewWindow.Top = App.Current.MainWindow.Top;
                InvoiceViewWindow.Show();
            }
        }
        /// <summary>
        /// Opens the SettingsWindow to Access FileName and Comment
        /// </summary>
        public void OpenSettingsWindow()
        {
            var settingsWindow = new settingsWindow();
            settingsWindow.DataContext = this;
            settingsWindow.Show();
        }
        /// <summary>
        /// Delets via Context Menue a DataGrid item 
        /// and convays the change down to the Inhabitant object 
        /// </summary>
        public void DeleteDataGridEntry()
        {
            // delet Entry and updates source
            ListOfExpenses.Remove(SelectedExpenseItem);
            if (InhabitantsSelected == Inhabitant1Name)
            {
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
                this.CurrentInvoice.Inhabitants[1].ListOfExpenses.Clear();
                foreach (var item in ListOfExpenses)
                {
                    this.CurrentInvoice.Inhabitants[1].ListOfExpenses.Add(item);
                }
                OnPropertyChanged(nameof(ExpenseInhabitant2));
                OnPropertyChanged(nameof(ListOfExpenses));
            }
            OnPropertyChanged(nameof(LblpayingInhabitantContent));
            OnPropertyChanged(nameof(LblBillContent));
        }

    }
}
