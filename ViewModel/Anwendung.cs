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
        /// Fixed Path
        /// </summary>
        private readonly string InvoicesFolderPath = Path.Combine(FileSystem.AppDataDirectory, "Invoices");
        /// <summary>
        /// inits the Viewmodel and pulls object referenzes
        /// </summary>
        public void Initialize()
        {
            System.Diagnostics.Debug.WriteLine("Initialize Start");
            App.Current!.BindingContext = this;
            //Check on startup if first time then Create the "Invoices" Folder
            if (!Directory.Exists(InvoicesFolderPath))
            {
                System.IO.Directory.CreateDirectory(InvoicesFolderPath);
            }
            System.Diagnostics.Debug.WriteLine("Initialize End");
        }
        #region PropertieBinding
        private int _CurrentInvoicesIndex = 0;
        public int CurrentInvoicesIndex
        {
            get => this._CurrentInvoicesIndex;
            set
            {
                this._CurrentInvoicesIndex = value;                
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
                OnPropertyChanged(nameof(InvoiceName));

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
            }
        }
        /// <summary>
        /// Cache for the Propertie
        /// </summary>
        private ObservableCollection<Invoices> _ListOfInvoicesInStorage = null!;
        /// <summary>
        /// Gets the List of Files in Storage, on First Time Readout 
        /// </summary>
        public ObservableCollection<Invoices> ListOfInvoicesInStorage
        {
            get
            {
                if (this._ListOfInvoicesInStorage == null)
                {                  
                   this._ListOfInvoicesInStorage = ReadInvoiceFilesFromFolder(InvoicesFolderPath);
                }
                return this._ListOfInvoicesInStorage;
            }
        }
        /// <summary>
        /// Deserialize all Files that suits a Invoices Object to a ObsColletion
        /// </summary>
        /// <param name="folderToReadFrom"></param>
        /// <returns>ObserveableCollection of Invoices</returns>
        private ObservableCollection<Invoices> ReadInvoiceFilesFromFolder(string folderToReadFrom)
        {
                        var ObsListe = new ObservableCollection<Invoices>();
            if (!Directory.Exists(folderToReadFrom))
            {
                System.IO.Directory.CreateDirectory(folderToReadFrom);
            }
            else
            {
                string[] FileNames = Directory.GetFiles(folderToReadFrom);
                //Load All Single Invoices to a ObsList
                foreach (string file in FileNames)
                {

                    Invoices i = Context.InvoiceManager.Load(file);
                    if (i != null)
                    {
                        ObsListe.Add(i);
                    }
                }
            }
            return ObsListe;
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
        public string TxtBoxAddUserContent
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
        public string InvoiceName
        {
            get => this.CurrentInvoice.InvoiceName!;
            set
            {
                this.CurrentInvoice.InvoiceName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods       
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
                TxtBoxAddUserContent = string.Empty;
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
        /// Sets the Clicked Item to the CurrentInvoices and switches to InvoiceViewPage
        /// </summary>
        [RelayCommand]
        public async Task LoadSelectedInvoicesToCurrent(object parameter)
        {
            //Take the (Selected) Clicked on Item and
            //set it to CurrentInvoices to work with
            if (parameter is Invoices SelectedInvoices && SelectedInvoices != null)
            {
                this.CurrentInvoices = SelectedInvoices;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Error on Casting CommandParams");
            }

            //Move to new Page that Displays all the Single Invoices that are in there 
            try
            {
                await Shell.Current.GoToAsync($"{nameof(InvoiceViewPage)}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
        }
        /// <summary>
        /// Sets the Clicked on Item to the CurrentInvoice and switches to EditView
        /// </summary>
        [RelayCommand]
        public async Task LoadSelectedInvoiceToCurrent(object parameter)
        {
            //Take the (Selected) Clicked on Item and
            //set it to CurrentInvoices to work with
            if (parameter is Invoice SelectedInvoice && SelectedInvoice != null)
            {
                try
                {
                    await Shell.Current.GoToAsync($"{nameof(EditView)}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return;
                }

                //find the Index of the given Item in the CurrentInvoices List
                CurrentInvoicesIndex = this.CurrentInvoices.InvoiceList.IndexOf(SelectedInvoice);
                this.CurrentInvoice = SelectedInvoice;

            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Error on Casting CommandParams");
            }

            //Move to new Page that Displays all the Single Invoices that are in there 

        }
        /// <summary>
        /// This ask if the currentInvoice should be Updated 
        /// is Yes gets pushed to CurrentInvoices at index
        /// </summary>
        [RelayCommand]
        public async Task UpdateCurrentInvoice()
        {
            //maybe not necessari
            //this.CurrentInvoices.InvoiceList[CurrentInvoicesIndex] = this.CurrentInvoice;
            this.CurrentInvoices.InvoiceList[CurrentInvoicesIndex].DateTimeChanged = DateTime.Now;
            
            this.Context.InvoiceManager.Save(this.CurrentInvoices);
            try
            {
                await Shell.Current.GoToAsync($"..");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Asynchronously deletes the current project, removing the associated
        /// invoices from the storage list and navigating back to the previous page.
        /// </summary>
        [RelayCommand]
        public async Task DeleteCurrentProject()
        {
            //Delete the CurrentInvoices File and remove it from the
            //ListofInvoicesInStorage List to stay consistant
            this.Context.InvoiceManager.Delete(this.CurrentInvoices);
            ListOfInvoicesInStorage.Remove(this.CurrentInvoices);
            try
            {
                await Shell.Current.GoToAsync($"..");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
        }
        /// <summary>
        /// Asynchronously deletes the current invoice being edited, removes it from the 
        /// invoice list, saves the updated list, and navigates back to the previous page.
        /// </summary>
        [RelayCommand]
        public async Task DeleteCurrentInvoiceInEdit()
        {
            //Delete the CurrentInvoice Item from the Invoices and save it
            this.CurrentInvoices.InvoiceList.Remove(this.CurrentInvoice);
            this.Context.InvoiceManager.Save(this.CurrentInvoices);
            try
            {
                await Shell.Current.GoToAsync($"..");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
        }
        /// <summary>
        /// Creates a new File for Invoices
        /// </summary>
        [RelayCommand]
        public async Task NewProject()
        {
            string NewInvoiceName = await AskForInvoiceName("Input Name for new Project");
            if (!string.IsNullOrEmpty(NewInvoiceName))
            {
                Invoices NewProject = new Invoices
                {
                    DateTimeCreation = DateTime.Now,
                    InvoicesProjectName = NewInvoiceName
                };
                NewProject.PathAndFileName = Path.Combine(InvoicesFolderPath, NewProject.Guid!.Value.ToString());

                System.Diagnostics.Debug.WriteLine($"Pre Save Count:{this.ListOfInvoicesInStorage.Count}");
                this.Context.InvoiceManager.Save(NewProject);
                System.Diagnostics.Debug.WriteLine($"Aft Save Count:{this.ListOfInvoicesInStorage.Count}");
                this.CurrentInvoices = NewProject;
                try
                {
                    await Shell.Current.GoToAsync(nameof(InvoiceViewPage));
                }
                catch (Exception ex)
                {

                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return;
                }
                //For some reasen on InvoiceManager.Save the ListOfInvoicesInStorage
                //invokes a get renders the .add unnesesery
                this.ListOfInvoicesInStorage.Add(NewProject);
            }

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
                NewInvoice.DateTimeCreation = DateTime.Now;
                this.CurrentInvoice = NewInvoice;
                this.CurrentInvoices.InvoiceList.Add(NewInvoice);
                CurrentInvoicesIndex = this.CurrentInvoices.InvoiceList.Count - 1;
                System.Diagnostics.Debug.WriteLine(
                    $"New Invoice:{NewInvoice.InvoiceName} created and " +
                    $"added to:{this.CurrentInvoices.InvoicesProjectName} " +
                    $"on position:{this.CurrentInvoices.InvoiceList.Count - 1}");
                //Save The New but not Edited Invoice to File in case of not directly
                //editing and Save via update there, also makes sure that the File
                //DateTime Changed gets updated.
                this.Context.InvoiceManager.Save(this.CurrentInvoices);
                try
                {
                    await Shell.Current.GoToAsync($"{nameof(EditView)}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return;
                }
            }
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
        #endregion Methods
    }
}
