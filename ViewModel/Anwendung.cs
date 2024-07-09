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
using System.ComponentModel;
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
        /// <summary>
        /// Internal Field
        /// </summary>
        private Invoice _CurrentInvoice = null!;
        /// <summary>
        /// Gets or sets the CurrentInvoice displayed on EditView to modify
        /// </summary>
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
                OnPropertyChanged();
                if (this.CurrentInvoice.InhabitantsNameList.Count == 2)
                {
                    InhabitantsSelected = this.CurrentInvoice.InhabitantsNameList[0];
                }
                OnPropertyChanged(nameof(LblpayingInhabitantContent));
                OnPropertyChanged(nameof(LblBillContent));

                System.Diagnostics.Debug.WriteLine("CurrentInvoice End Set");
            }
        }
        /// <summary>
        /// Internal Field
        /// </summary>
        private Invoices _CurrentInvoices = null!;
        /// <summary>
        /// Gets or Sets the List of Invoices aka Projects that are saved on the Device 
        /// </summary>
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
        private string _InhabitansSelected = null!;
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
        private string _lblBillContent = null!;
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
        private string _LblpayingInhabitantContent = null!;
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
        private Expense _ExpenseToAdd = null!;
        public Expense ExpenseToAdd
        {
            get
            {
                if (this._ExpenseToAdd == null)
                {
                    this._ExpenseToAdd = new Expense();
                }
                return this._ExpenseToAdd;
            }
            set
            {
                this._ExpenseToAdd = value;
                OnPropertyChanged();
            }
        }
        #region NewInvoice
        /// <summary>
        /// Intenal Field
        /// </summary>
        private bool _IsInputFormularVisible = false;
        /// <summary>
        /// Gets or set the visibility of 
        /// the "New Invoice Input Formular"
        /// </summary>
        public bool IsInputFormularVisible
        {
            get => this._IsInputFormularVisible;
            set
            {
                this._IsInputFormularVisible = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Internal Field
        /// </summary>
        private Invoice _InvoiceToCreate = null!;
        /// <summary>
        /// Gets or sets the Invoice to be created
        /// </summary>
        public Invoice InvoiceToCreate
        {
            get
            {
                if (this._InvoiceToCreate == null)
                {
                    this._InvoiceToCreate = new Invoice();
                    //Attach Event
                    InvoiceToCreate.Inhabitants[0].PropertyChanged += TestingEvent_OnPropertyChangedFromInvoiceToCreate!;
                    InvoiceToCreate.Inhabitants[1].PropertyChanged += TestingEvent_OnPropertyChangedFromInvoiceToCreate!;
                    InvoiceToCreate.PropertyChanging += TestingEvent_OnPropertyChangedFromInvoiceToCreate!;
                }
                return this._InvoiceToCreate;
            }
            set
            {
                this._InvoiceToCreate = value;
                OnPropertyChanged();
            }
        }
        
        #endregion NewInvoice
        #endregion PropertieBinding

        #region Methods       
        /// <summary>
        /// Adds a Expens struct to the dedicated Inhabitant object
        /// </summary>
        [RelayCommand]
        public void AddBill()
        {
            if (InhabitantsSelected != string.Empty)
            {


                if (ExpenseToAdd.ValueExpense > 0)
                {
                    if (this.CurrentInvoice.Inhabitants[0].Name == InhabitantsSelected && InhabitantsSelected != string.Empty)
                    {
                        CurrentInvoice.Inhabitants[0].AddBetrag(ExpenseToAdd.Categorie, ExpenseToAdd.ValueExpense);
                        OnPropertyChanged(nameof(CurrentInvoice));
                    }
                    else if (this.CurrentInvoice.Inhabitants[1].Name == InhabitantsSelected && InhabitantsSelected != string.Empty)
                    {
                        CurrentInvoice.Inhabitants[1].AddBetrag(ExpenseToAdd.Categorie, ExpenseToAdd.ValueExpense);
                        //Neuer Expense und Total expense wurde geändet -> auf UI pushen
                        OnPropertyChanged(nameof(CurrentInvoice));
                    }
                    else
                    {
                        Log.WriteLine($"Error keine Inhabitant wurde mit der im Dropdown ausgewaehlten User identifiziert");
                    }
                }
                else
                {
                    Log.WriteLine("Invalide Value Input");
                }
            }
            //Todo Canexecute
            //else LblToolStripContent = $"Missing Username";
            //Null after adding the bill to clear the UI
            ExpenseToAdd = null!;

            OnPropertyChanged(nameof(LblpayingInhabitantContent));
            OnPropertyChanged(nameof(LblBillContent));
        }
        /// <summary>
        /// Opens The window and fills the Datagrid with the Current
        /// selectedInhabitant to display the total Expenses
        /// </summary>
        [RelayCommand]
        public void FillContributioWindow(object parameter)
        {
            if (parameter is Microsoft.Maui.Controls.Label label)
            {

                if (label.Text == CurrentInvoice.Inhabitants[0].Name)
                {
                    InhabitantsSelected = CurrentInvoice.Inhabitants[0].Name;

                }
                else if (label.Text == CurrentInvoice.Inhabitants[1].Name)
                {
                    InhabitantsSelected = CurrentInvoice.Inhabitants[1].Name;

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
                                    ListOfExpensesInhabitant1.Clear();
                                    foreach (var item in this.CurrentInvoice.Inhabitants[0].ListOfExpensesInhabitant1)
                                    {
                                        ListOfExpensesInhabitant1.Add(item);
                                    }
                                    this.ListOfExpensesInhabitant1 = new ObservableCollection<Expense>(this.CurrentInvoice.Inhabitants[0].ListOfExpensesInhabitant1);
                                    contributionWindow.Show();
                                    contributionWindow.SizeToContent = SizeToContent.Height;
                                }
                                else if (label.Content.ToString() == Inhabitant2Name)
                                {
                                    ListOfExpensesInhabitant1.Clear();
                                    foreach (var item in this.CurrentInvoice.Inhabitants[1].ListOfExpensesInhabitant1)
                                    {
                                        ListOfExpensesInhabitant1.Add(item);
                                    }
                                    this.ListOfExpensesInhabitant1 = new ObservableCollection<Expense>(this.CurrentInvoice.Inhabitants[1].ListOfExpensesInhabitant1);
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
                LogToFile(ex.Message);
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
                //find the Index of the given Item in the CurrentInvoices List
                this.CurrentInvoice = SelectedInvoice;
                try
                {
                    await Shell.Current.GoToAsync($"{nameof(EditView)}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    LogToFile(ex.Message);
                    return;
                }



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
            string NewInvoiceName = await AskForDialogOkCancel(
                titel: "Input",
                message: "Input Name for new Project",
                placeholder: "Projectname here");

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
        /// Shows the Input Formular for a new Invoice
        /// </summary>
        [RelayCommand]
        public void ShowInvoiceFormular()
        {
            if (this.IsInputFormularVisible == false)
            {
                this.IsInputFormularVisible = true;
            }
        }
        /// <summary>
        /// Starts a new Invoice gives it via Dialog a Name
        /// then gets added CurrentInvoices List
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanExecuteNewInvoice))]
        public async Task NewInvoice()
        {
            //Set Date of creation for the new Invoice
            InvoiceToCreate.DateTimeCreation = DateTime.Now;
            this.CurrentInvoice = InvoiceToCreate;
            this.CurrentInvoices.InvoiceList.Add(InvoiceToCreate);
            System.Diagnostics.Debug.WriteLine(
                $"New Invoice:{InvoiceToCreate.InvoiceName} created and " +
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
            InvoiceToCreate = null!;
            IsInputFormularVisible = false;
        }
        /// <summary>
        /// Ask via Dialog for Information
        /// </summary>
        /// <param name="message">The Message to Display</param>
        /// <param name="placeholder">Text in the Entryfield for additional Information</param>
        /// <param name="titel">Titel of the DialogBox</param>
        /// <returns>returns null if cancel, returns a 
        /// string on accept can be string.empty</returns>
        public async Task<string> AskForDialogOkCancel(string titel, string message, string placeholder)
        {

            return await Microsoft.Maui.Controls.Application.Current!.MainPage!.DisplayPromptAsync(
                  title: titel,
                  message: message,
                  accept: "Ok",
                  cancel: "Cancel",
                  placeholder: placeholder);

        }
        /// <summary>
        /// Delets via Context Menue a DataGrid item 
        /// and convays the change down to the Inhabitant object 
        /// </summary>
        [RelayCommand]
        public void DeleteDataGridEntry(object? selectedItem)
        {
            if (selectedItem is Expense expenseItem)
            {


                // delet Entry and updates source
                //ListOfExpensesInhabitant1.Remove(SelectedExpenseItem);
                if (InhabitantsSelected == CurrentInvoice.Inhabitants[0].Name)
                {
                    this.CurrentInvoice.Inhabitants[0].ListOfExpenses.Remove(expenseItem);
                }
                else if (InhabitantsSelected == CurrentInvoice.Inhabitants[1].Name)
                {
                    this.CurrentInvoice.Inhabitants[1].ListOfExpenses.Remove(expenseItem);
                }
                OnPropertyChanged(nameof(LblpayingInhabitantContent));
                OnPropertyChanged(nameof(LblBillContent));
            }
        }
        public void LogToFile(string message)
        {
            //folder
            string LogName = "LogFile";

            File.AppendAllText(Path.Combine(InvoicesFolderPath, LogName), $"Protocol LogTime {DateTime.Now}, Message:{message} ");

            System.Diagnostics.Debug.WriteLine("Writen to LogFile");
        }
        #endregion Methods

        #region canExecute
        //new invoice can execute
        public bool CanExecuteNewInvoice()
        {
            if (Regex.IsMatch(InvoiceToCreate.Inhabitants[0].Name, @"^[a-zA-Z]+$") &&
                Regex.IsMatch(InvoiceToCreate.Inhabitants[1].Name, @"^[a-zA-Z]+$") &&
                Regex.IsMatch(InvoiceToCreate.InvoiceName!, @"^[a-zA-Z0-9]+$"))
            {
                return true;
            }
            Log.WriteLine("CanExecuteNewInvoice false");
            return false;
           

        }
        //Act on Event
        public void TestingEvent_OnPropertyChangedFromInvoiceToCreate(object sender, EventArgs e)
        {
            Console.WriteLine("Event got called");
            NewInvoiceCommand.NotifyCanExecuteChanged();
        }
        #endregion canExecute
    }
}
