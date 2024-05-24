using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Essensausgleich.Controller;
using Essensausgleich.Data;
using Essensausgleich.Tools;
using Essensausgleich.Views;
using Microsoft.Maui.Controls.PlatformConfiguration.TizenSpecific;
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

    public partial class Anwendung : Essensausgleich.Infra.ViewModel
    {
        /// <summary>
        /// inits the Viewmodel and pulls object referenzes
        /// </summary>
        public void Initialize()
        {
            App.Current!.BindingContext = this;
            string InvoicesFolderPath = Path.Combine(FileSystem.AppDataDirectory, "Invoices");
            System.IO.Directory.CreateDirectory(InvoicesFolderPath);
            //Write Samples to Device
            Invoices SampleInvoices = new Invoices
            {
                DateTimeChanged = DateTime.Now,
                DateTimeCreation = new DateTime(year: 2024, month: 1, day: 1, hour: 6, minute: 10, second: 58),
                InvoicesProjectName = "SampleInvoicesProjectName",
                PathAndFileName = Path.Combine(Path.Combine(FileSystem.AppDataDirectory, "Invoices"), "File.json"),
                InvoiceList = new ObservableCollection<Invoice>
                {
                    new Invoice {
                    DateTimeChanged = DateTime.Now,
                    DateTimeCreation = new DateTime(year: 2024, month: 1, day: 1, hour: 6, minute: 10, second: 58),
                    //FileName = Path.Combine(Path.Combine(FileSystem.AppDataDirectory, "Invoices"), "InvoiceSample.json"),
                    InhabitantsNameList = new ObservableCollection<string> { "Florian", "Marion" },
                    Inhabitants = new Inhabitants
                    {
                        new Inhabitant
                        {
                            Name = "Florian",
                            TotalExpense = 60,
                            ListOfExpenses = new List<Expense>
                            {
                                new Expense { categorie = "ama", valueExpense = 10 },
                                new Expense { categorie = "tanken", valueExpense = 50 }
                            }
                        },
                        new Inhabitant
                        {
                            Name = "Marion",
                            TotalExpense = 22,
                            ListOfExpenses = new List<Expense>
                            {
                                new Expense { categorie = "hofa", valueExpense = 20 },
                                new Expense { categorie = "billa", valueExpense = 2 }
                            }
                        }                                            },
                    InvoiceComment = "ReloadNeu"
                }
                }
            }
            ;
            this.Context.InvoiceManager.Save(SampleInvoices);
        }
        private string InvoicesFolderPath = Path.Combine(FileSystem.AppDataDirectory, "Invoices");



        #region PropertieBinding
#pragma warning disable 1591

        private int _CurrentInvoicesIndex = 0;


        public int CurrentInvoicesIndex
        {
            get => this._CurrentInvoicesIndex;
            set
            {
                this._CurrentInvoicesIndex = value;
                NextInvoiceCommand.NotifyCanExecuteChanged();
                PreviousInvoiceCommand.NotifyCanExecuteChanged();
            }
        }
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
                nextInvoiceCommand!.NotifyCanExecuteChanged();
                previousInvoiceCommand!.NotifyCanExecuteChanged();
                //openInvoiceViewSidePageCommand!.NotifyCanExecuteChanged();
                System.Diagnostics.Debug.WriteLine("CurrentInvoice End Set");
            }
        }

        private Invoices _CurrentInvoices = null!;

        public Invoices CurrentInvoices
        {
            get
            {
                if (this._CurrentInvoices == null)
                {
                    this._CurrentInvoices = new Invoices();

                }
                return this._CurrentInvoices;
            }
            set
            {
                this._CurrentInvoices = value;
                openInvoiceViewSidePageCommand!.NotifyCanExecuteChanged();
            }
        }

        private ObservableCollection<Invoices> _ListOfInvoicesInStorage = new ObservableCollection<Invoices>();
        public ObservableCollection<Invoices> ListOfInvoicesInStorage
        {
            get
            {
                string InvoicesFolderPath = Path.Combine(FileSystem.AppDataDirectory, "Invoices");

                if (!Directory.Exists(InvoicesFolderPath))
                {

                    System.IO.Directory.CreateDirectory(InvoicesFolderPath);
                }
                else
                {

                    string[] FileNames = Directory.GetFiles(InvoicesFolderPath);
                    //Load All Single Invoices to a ObsList
                    this._ListOfInvoicesInStorage.Clear();
                    foreach (string file in FileNames)
                    {

                        Invoices i = Context.InvoiceManager.Load(file);
                        if (i != null)
                        {
                            i.PathAndFileName = file;
                            if (i.InvoiceList.Count > 0)
                            {
                                this._ListOfInvoicesInStorage.Add(i);
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine($"File:{file} is no valid Invoices object");
                            }

                        }
                    }
                }
                return this._ListOfInvoicesInStorage;
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

        private Invoices _SelectedInvoiceItem = null!;
        public Invoices SelectedInvoiceItem
        {
            get => this._SelectedInvoiceItem;
            set
            {
                this._SelectedInvoiceItem = value;
                OnPropertyChanged(nameof(this._SelectedInvoiceItem));
            }
        }
        /// <summary>
        /// Property to Bound on UI Picker Control
        /// </summary>
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
                if (this.CurrentInvoice.Inhabitants[0].TotalExpense > 0 && this.CurrentInvoice.Inhabitants[1].TotalExpense > 0)
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
                return string.Empty;
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
        private string _InvoiceCommentary = string.Empty!;
        /// <summary>
        /// Gets or Sets the Commentary for the CurrentInvoices Object
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
        [RelayCommand]
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
        [RelayCommand]
        public void AddBill()
        {
            if (InhabitantsSelected != string.Empty)
            {
                decimal bill = 0;

                if (decimal.TryParse(_TxtBoxAddBillText, out bill) && bill > 0)
                {
                    if (this.CurrentInvoice.Inhabitants[0].Name == InhabitantsSelected && InhabitantsSelected != string.Empty)
                    {
                        CurrentInvoice.Inhabitants[0].AddBetrag(_TxtBoxCategorieText, bill);
                        OnPropertyChanged(nameof(ExpenseInhabitant1));
                        LblToolStripContent = $"Expense {bill} der Kategorie {_TxtBoxCategorieText} hinzugefuegt";
                    }
                    else if (this.CurrentInvoice.Inhabitants[1].Name == InhabitantsSelected && InhabitantsSelected != string.Empty)
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
        [RelayCommand]
        public async Task OpenContributioWindow(object parameter)
        {
            if (parameter is Microsoft.Maui.Controls.Label label)
            {
                if (label.Text == Inhabitant1Name || label.Text == Inhabitant2Name)
                {
                    if (label.Text.ToString() == Inhabitant1Name)
                    {
                        InhabitantsSelected = Inhabitant1Name;
                        ListOfExpenses.Clear();
                        foreach (var item in this.CurrentInvoice.Inhabitants[0].ListOfExpenses)
                        {
                            ListOfExpenses.Add(item);
                        }
                        this.ListOfExpenses = new ObservableCollection<Expense>(this.CurrentInvoice.Inhabitants[0].ListOfExpenses);

                    }
                    else if (label.Text.ToString() == Inhabitant2Name)
                    {
                        InhabitantsSelected = Inhabitant2Name;
                        ListOfExpenses.Clear();
                        foreach (var item in this.CurrentInvoice.Inhabitants[1].ListOfExpenses)
                        {
                            ListOfExpenses.Add(item);
                        }
                        this.ListOfExpenses = new ObservableCollection<Expense>(this.CurrentInvoice.Inhabitants[1].ListOfExpenses);

                    }
                    try
                    {
                        await Shell.Current.GoToAsync($"{nameof(ContributionPage)}");
                    }
                    catch (Exception ex)
                    {


                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }

                }
                else
                {
                    Log.WriteLine($"No Inhabitant selected or not found, Selcted:{InhabitantsSelected}");
                }
            }

            /*

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
            */
        }
        /// <summary>
        /// Sets the SelectedInvoiceItem from the StoragePage
        /// to the CurrentInvoices
        /// </summary>
        [RelayCommand]
        public async Task LoadSelectedInvoiceToCurrent()
        {
            try
            {
                await Shell.Current.GoToAsync($"///MainPage");
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
            CurrentInvoices = this.Context.InvoiceManager.Load(SelectedInvoiceItem.PathAndFileName!);

            CurrentInvoicesIndex = 0;
            this.CurrentInvoice = CurrentInvoices.InvoiceList[CurrentInvoicesIndex];
            nextInvoiceCommand!.NotifyCanExecuteChanged();
            previousInvoiceCommand!.NotifyCanExecuteChanged();
            System.Diagnostics.Debug.WriteLine("LoadSelectedInvoiceToCurrent End");
        }
        /// <summary>
        /// This ask if the currentInvoice should be Updated 
        /// is Yes gets pushed to CurrentInvoices at index
        /// </summary>
        [RelayCommand]
        public async Task UpdateCurrentInvoice()
        {
            //Ask if CurrentInvoices has a Name if it hasent 
            if (string.IsNullOrEmpty(this.CurrentInvoices.InvoicesProjectName))
            {
                string InvoicesName = await AskForInvoiceName("Input Name for New InvoicesProject to save");
                if (!string.IsNullOrEmpty(InvoicesName))
                {
                    this.CurrentInvoices.InvoicesProjectName = InvoicesName;
                    //Todo Filenames
                    this.CurrentInvoices.PathAndFileName = Path.Combine(InvoicesFolderPath, InvoicesName);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Saving cancelt");
                    return;
                }
            }
            if (await AskIfOverwrite())
            {
                this.CurrentInvoices.InvoiceList[CurrentInvoicesIndex] = this.CurrentInvoice;
                this.Context.InvoiceManager.Save(this.CurrentInvoices);

                System.Diagnostics.Debug.WriteLine($"CurrentInvoice{this.CurrentInvoice.InvoiceName} got updated");
            }
            else
            {

                System.Diagnostics.Debug.WriteLine("Updating current Invoice cancelt");
            }
        }
        /// <summary>
        /// Sets the Current Invoice and Invoices to null
        /// </summary>
        [RelayCommand]
        public async Task NewInvoices()
        {
            try
            {
                await Shell.Current.GoToAsync($"///MainPage");
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
            this.CurrentInvoices = null!;
            this.CurrentInvoice = null!;

        }
        /// <summary>
        /// Starts a new Invoice gives it via Dialog a Name
        /// then gets added CurrentInvoices List
        /// </summary>
        [RelayCommand]
        public async Task NewInvoice()
        {
            Invoice NewInvoice = new Invoice();
            string NewInvoiceName = await AskForInvoiceName("Input Name for new Invoice");
            if (!string.IsNullOrEmpty(NewInvoiceName))
            {
                NewInvoice.InvoiceName = NewInvoiceName;
                this.CurrentInvoice = NewInvoice;
                this.CurrentInvoices.InvoiceList.Add(NewInvoice);
                CurrentInvoicesIndex = this.CurrentInvoices.InvoiceList.Count - 1;
                System.Diagnostics.Debug.WriteLine(
                    $"New Invoice:{NewInvoice.InvoiceName} created and " +
                    $"added to:{this.CurrentInvoices.InvoicesProjectName} " +
                    $"on position:{this.CurrentInvoices.InvoiceList.Count - 1}");
            }
        }
        /// <summary>
        /// Ask via Dialog if it should Overwrite
        /// </summary>
        /// <returns>Return true for Yes, Returns false for Cancel</returns>
        public async Task<bool> AskIfOverwrite()
        {
            return await Microsoft.Maui.Controls.Application.Current!.MainPage!.DisplayAlert(
                         title: "Warning",
                         message: "Overwrite this Invoices",
                         accept: "Yes",
                         cancel: "Cancel");
        }
        /// <summary>
        /// Ask via Dialog for a InvoiceName
        /// </summary>
        /// <returns>returns null if cancel, returns a 
        /// string on accept can be string.empty</returns>
        public async Task<string> AskForInvoiceName(string message)
        {

            return await Microsoft.Maui.Controls.Application.Current!.MainPage!.DisplayPromptAsync(
                  title: "Warning",
                  message: message,
                  accept: "Ok",
                  cancel: "Cancel",
                  placeholder: "InvoiceNameHere");

        }
        /// <summary>
        /// Checks for the CurrentInvoices List if there is already
        /// a InvoicesProjectName (therefore it has been loaded and 
        /// has a PathAndFileName to it)
        /// </summary>
        /// <returns>True if has PathAndFileName ready to save or have been given.
        /// False of has no name to it and got declined given a proper Name</returns>
        public async Task<bool> IsInvoicesNameAndPathSet()
        {
            if (string.IsNullOrEmpty(this.CurrentInvoices.InvoicesProjectName))
            {
                string InvoicesNameToSave
                    = await Microsoft.Maui.Controls.Application.Current!.MainPage!.DisplayPromptAsync(
                        title: "InvoicesName",
                        message: "InputNameToSave or cancel",
                        accept: "Ok",
                        cancel: "Cancel",
                        placeholder: "InvoiceNameHere");
                if (!string.IsNullOrEmpty(InvoicesNameToSave))
                {
                    this.CurrentInvoices.InvoicesProjectName = InvoicesNameToSave;
                    this.CurrentInvoices.PathAndFileName = Path.Combine(InvoicesFolderPath, $"{InvoicesNameToSave}.json");
                    return true;
                }
                else
                {

                    System.Diagnostics.Debug.WriteLine(
                        "On Dialog for naming CurrentInvoices got " +
                        "Cancelt or accept with no entry! Send False for abort");
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Opens and Closes a to the Right attached Window to Display a Datagrid with all Loaded Invoices
        /// </summary>
        [RelayCommand(CanExecute = nameof(canExecuteInvoiceViewSidePage))]
        public async Task OpenInvoiceViewSidePage()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(InvoiceViewSidePage)}");
            }
            catch (Exception ex)
            {


                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        public bool canExecuteInvoiceViewSidePage()
        {

            if (this.CurrentInvoices.InvoiceList.Count > 1)
            {
                return true;

            }
            return false;

        }
        /// <summary>
        /// Delets via Context Menue a DataGrid item 
        /// and convays the change down to the Inhabitant object 
        /// </summary>
        [RelayCommand]
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
        [RelayCommand(CanExecute = nameof(canExecuteNextInvoice))]
        public void NextInvoice()
        {
            this.CurrentInvoice = this.CurrentInvoices.InvoiceList[++CurrentInvoicesIndex];
        }
        public bool canExecuteNextInvoice()
        {

            //if (this.Context is null)
            //{
            //    return false;
            //}
            if (CurrentInvoicesIndex < this.CurrentInvoices.InvoiceList.Count - 1)
            {
                return true;
            }


            return false;
        }
        [RelayCommand(CanExecute = nameof(canExecutePreviousInvoice))]
        public void PreviousInvoice()
        {

            this.CurrentInvoice = this.CurrentInvoices.InvoiceList[--CurrentInvoicesIndex];
        }
        public bool canExecutePreviousInvoice()
        {
            //if (this.Context is null)
            //{
            //    return false;
            //}
            if (CurrentInvoicesIndex > 0)
            {
                return true;
            }

            return false;
        }
        [RelayCommand]
        public async Task GotoInvoices()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(StoragePage)}");
            }
            catch (Exception ex)
            {


                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
