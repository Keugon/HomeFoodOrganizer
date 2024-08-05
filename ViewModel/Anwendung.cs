using CommunityToolkit.Mvvm.Input;
using DRAXNET.Core;
using Essensausgleich.Data;
using Essensausgleich.Views;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Log = System.Diagnostics.Debug;

namespace Essensausgleich.ViewModel
{
    /// <summary>
    /// blablablaAnwednung
    /// </summary>

    public partial class Anwendung : DRAXNET.Core.ViewModel
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
        #region Services
        private DataSharingController _DataSharingController = null!;
        /// <summary>
        /// Gets or sets the Service for Data sharing between Apps
        /// </summary>
        public DataSharingController DataSharingController
        {
            get
            {
                if (this._DataSharingController == null)
                {
                    this._DataSharingController = this.Context.Fabricate<DataSharingController>();
                }
                return this._DataSharingController;
            }
            set => this._DataSharingController = value;
        }

        private InvoiceManager _InvoiceManager = null!;
        /// <summary>
        /// InvoiceManager Service
        /// </summary>
        public InvoiceManager InvoiceManager
        {
            get
            {
                if (this._InvoiceManager == null)
                {
                    this._InvoiceManager = new InvoiceManager();
                }
                return this._InvoiceManager;
            }
        }
        #endregion Services

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
                if (this.CurrentInvoice.Inhabitants.Count == 2)
                {
                    InhabitantsSelected = this.CurrentInvoice.Inhabitants[0].Name;
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

                    Invoices i = this.InvoiceManager.Load(file);
                    if (i != null)
                    {
                        ObsListe.Add(i);
                    }
                }
            }
            return ObsListe;
        }
        private string _InhabitansSelected = null!;
        /// <summary>
        /// Gets or sets the SelectedInhabitant for adding Expenses
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
        private string _lblBillContent = null!;
        /// <summary>
        /// Gets or sets the amount that needs to be
        /// payed to the other inhabitant
        /// </summary>
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
        /// <summary>
        /// Gets or sets the disyplayed Inhabitant
        /// name that needs to pay the other one
        /// </summary>
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
        /// <summary>
        /// Gets or sets the Expense Object that
        /// is used to add a new Expense to a Inhabitant
        /// </summary>
        public Expense ExpenseToAdd
        {
            get
            {
                if (this._ExpenseToAdd == null)
                {
                    this._ExpenseToAdd = new Expense();
                    ExpenseToAdd.PropertyChanged += (sender, e) => AddBillCommand.NotifyCanExecuteChanged();
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
                    //On initialize the Invoice object attach the Methode that ultimatly checks for valid User Input
                    InvoiceToCreate.Inhabitants[0].PropertyChanged += (sender, e) => NewInvoiceCommand.NotifyCanExecuteChanged();
                    InvoiceToCreate.Inhabitants[1].PropertyChanged += (sender, e) => NewInvoiceCommand.NotifyCanExecuteChanged();
                    InvoiceToCreate.PropertyChanging += (sender, e) => NewInvoiceCommand.NotifyCanExecuteChanged();
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

        #region Charts

        /// <summary>
        /// Gets the Y axis for the chart
        /// </summary>
        public List<LiveChartsCore.SkiaSharpView.Axis> ProjectChartYaxis
        {
            get
            {
                if (this.CurrentInvoices.HasInvoices)
                {
                    SKColor YAxisColorTheme = new SKColor();
                    if (CurrentAppTheme == AppTheme.Light)
                    {
                        YAxisColorTheme = SKColors.Black;
                    }
                    else if (CurrentAppTheme == AppTheme.Dark)
                    {
                        YAxisColorTheme = SKColors.LightGray;
                    }
                    return new List<LiveChartsCore.SkiaSharpView.Axis>
                                {
                                    new Axis
                                    {
                                        NameTextSize = 14,
                                        NamePaint = new SolidColorPaint(YAxisColorTheme),
                                        NamePadding = new LiveChartsCore.Drawing.Padding(0, 20),
                                        Padding =  new LiveChartsCore.Drawing.Padding(0, 0, 20, 0),
                                        TextSize = 12,
                                        LabelsPaint = new SolidColorPaint(YAxisColorTheme),
                                        TicksPaint = new SolidColorPaint(YAxisColorTheme),
                                        SubticksPaint = new SolidColorPaint(SKColors.Blue),
                                        DrawTicksPath = true,
                                        Labeler = Labelers.Currency
                                    },
                                };
                }
                return null!;

            }
        }

        /// <summary>
        /// Gets the X axis for the chart
        /// </summary>
        public List<LiveChartsCore.SkiaSharpView.Axis> ProjectChartXaxis
        {
            get
            {
                return new List<LiveChartsCore.SkiaSharpView.Axis>
                {
                    new Axis
                    {
                     //  // Use the labels property to define named labels.
                     //Labels = new string[] { CurrentInvoice.Inhabitants[0].Name, CurrentInvoice.Inhabitants[1].Name}
                     MaxLimit = 4,
                     MinLimit = 0
                    }
                };
            }
        }

        /// <summary>
        /// Internal Cache
        /// </summary>
        private ISeries[] _ProjectChart = null!;
        /// <summary>
        /// Gets the Datatype for visualize the chart
        /// </summary>
        public ISeries[] ProjectChart
        {
            get
            {

                //31.07.2024 On Enter a new/ empty Project
                //"Exception has been thrown by the target of an invocation."
                //happens due to the chart trys to access data that does not exist yet no Invoices!
                //Fix Interestingly the Yaxis info gets befor the ISeries info, Fixed by imple and check for the Invoices.HasInvoices bool else send null!

                //26.07.2024 Event for redraw the graph if the TotalExpense of any Inhabitant has changed
                if (this._ProjectChart == null && this.CurrentInvoices.HasInvoices)
                {
                    //Sub to Apptheme Changed for redraw
                    Application.Current!.RequestedThemeChanged += ExpenseDataChart_CollectionChanged!;
                    //Sub to event
                    foreach (var invoice in CurrentInvoices.InvoiceList)
                    {
                        foreach (var inhabitant in invoice.Inhabitants)
                        {
                            inhabitant.ListOfExpenses.CollectionChanged -= ExpenseDataChart_CollectionChanged!;
                            inhabitant.ListOfExpenses.CollectionChanged += ExpenseDataChart_CollectionChanged!;
                        }
                    }
                    //get data from invoices
                    decimal[] user1Exp = new decimal[CurrentInvoices.InvoiceList.Count];
                    decimal[] user2Exp = new decimal[CurrentInvoices.InvoiceList.Count];
                    for (int i = 0; i < CurrentInvoices.InvoiceList.Count; i++)
                    {
                        user1Exp[i] = CurrentInvoices.InvoiceList[i].Inhabitants[0].TotalExpense;
                        user2Exp[i] = CurrentInvoices.InvoiceList[i].Inhabitants[1].TotalExpense;
                    }
                    //Generate Lines from CurrentInvoice(s)
                    this._ProjectChart = new ISeries[]
                    {
                        new LineSeries<decimal>
                        {
                        Values = user1Exp,
                        Fill = null,
                        Name = CurrentInvoices.InvoiceList[0].Inhabitants[0].Name,
                        Stroke = new SolidColorPaint(SKColors.Blue, 2),
                        ScalesYAt = 0
                        },
                        new LineSeries<decimal>
                        {
                        Values = user2Exp,
                        Fill = null,
                        Name = CurrentInvoices.InvoiceList[0].Inhabitants[1].Name,
                        Stroke = new SolidColorPaint(SKColors.SteelBlue, 2),
                        ScalesYAt = 0
                        },
                    };

                }
                return this._ProjectChart!;
            }
        }
        #endregion Charts
        #region AppTheme
        /// <summary>
        /// Gets the Current AppTheme
        /// </summary>
        /// <remarks>Enu Light, Dark, Unspecified</remarks>
        public AppTheme CurrentAppTheme
        {
            get
            {
                //Reakt to AppTheme
                AppTheme currentTheme = Application.Current!.RequestedTheme;
                Log.WriteLine(currentTheme.ToString());
                return currentTheme;
            }
        }
        #endregion AppTheme
        #endregion PropertieBinding

        #region Methods       
        /// <summary>
        /// Adds a Expens struct to the dedicated Inhabitant object
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanExecuteAddBill))]
        public void AddBill()
        {
            //26.07.2024 renew Methode no longer the option to select a inhabitant, entry converter prohibits invalid input only numerics
            if (this.CurrentInvoice.Inhabitants[0].Name == InhabitantsSelected)
            {
                CurrentInvoice.Inhabitants[0].AddBetrag(ExpenseToAdd.Categorie, ExpenseToAdd.ValueExpense);
                OnPropertyChanged(nameof(CurrentInvoice));
            }
            else if (this.CurrentInvoice.Inhabitants[1].Name == InhabitantsSelected)
            {
                CurrentInvoice.Inhabitants[1].AddBetrag(ExpenseToAdd.Categorie, ExpenseToAdd.ValueExpense);
                //Neuer Expense und Total expense wurde geändet -> auf UI pushen
                OnPropertyChanged(nameof(CurrentInvoice));
            }
            //26.07.2024 Canexecute
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
                //30.07.2024 Fixed Chart stuck on first loaded Project
                ExpenseDataChart_CollectionChanged(this, EventArgs.Empty);
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
        /// Saves the Current activ Project (Invoces) to file, also changes 
        /// the DateTime Changed for the Single Invoices as well of the Project
        /// </summary>
        [RelayCommand]
        public async Task UpdateCurrentInvoice()
        {
            //maybe not necessari
            //this.CurrentInvoices.InvoiceList[CurrentInvoicesIndex] = this.CurrentInvoice;
            this.CurrentInvoice.DateTimeChanged = DateTime.Now;
            this.InvoiceManager.Save(this.CurrentInvoices);
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
            this.InvoiceManager.Delete(this.CurrentInvoices);
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
            this.InvoiceManager.Save(this.CurrentInvoices);
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
                this.InvoiceManager.Save(NewProject);
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
            else
            {
                this.IsInputFormularVisible = false;
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
            this.CurrentInvoices.InvoiceList.Add(InvoiceToCreate);
            //Save The New but not Edited Invoice to File in case of not directly
            //editing and Save via update there, also makes sure that the File
            //DateTime Changed gets updated.
            this.InvoiceManager.Save(this.CurrentInvoices);
            //Switch to EditView
            await LoadSelectedInvoiceToCurrent(InvoiceToCreate);
            //After Switch Null InvoiceToCreate to be Ready for the next and vanish the Input View
            InvoiceToCreate = null!;
            IsInputFormularVisible = false;
            //27.07.2024 After Creation of the new invoice rebuilt the chart
            //Reset the chart 
            ExpenseDataChart_CollectionChanged(this, EventArgs.Empty);
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
        /// <summary>
        /// Logs a given string to a File a a form of protocoll
        /// </summary>
        /// <param name="message">string to 
        /// be saved to the protocoll</param>
        public void LogToFile(string message)
        {
            //folder
            string LogName = "LogFile";

            File.AppendAllText(Path.Combine(InvoicesFolderPath, LogName), $"\nProtocol LogTime {DateTime.Now}\nMessage:{message} ");

            System.Diagnostics.Debug.WriteLine("Writen to LogFile");
        }
        /// <summary>
        /// Shares the invoice informations in string form
        /// </summary>
        /// <param name="invoiceToShare"></param>
        [RelayCommand]
        public async Task ShareInvoice(object? invoiceToShare)
        {
            if (invoiceToShare is Invoice invoice)
            {
                await this.DataSharingController.RequestAsync(new ShareTextRequest
                {
                    Text = $"Invoice Name: {invoice.InvoiceName}\n" +
                    $"{invoice.Inhabitants[0].Name}:{invoice.Inhabitants[0].TotalExpense}\n" +
                    $"{invoice.Inhabitants[1].Name}:{invoice.Inhabitants[1].TotalExpense}",
                    Title = "Share Text"
                });

            }
        }
        /// <summary>
        /// Nulls the current Chart and forces the UI to recall it with current Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExpenseDataChart_CollectionChanged(object sender, EventArgs e)
        {
            //Invalidate the cache to force re-creation
            Log.WriteLine("Chart will redraw");
            _ProjectChart = null!;
            OnPropertyChanged(nameof(this.ProjectChart));
            OnPropertyChanged(nameof(this.ProjectChartYaxis));
        }
        /// <summary>
        /// TestMethode
        /// </summary>
        [RelayCommand]
        public void TestMethode()
        {
            try
            {
                this.Context.DatenManager.SqlMariaDBController.AddUser();
            }
            catch (Exception ex)
            {
                OnFehlerAufgetreten(new FehlerAufgetretenEventArgs(ex));
            }
        }
        #endregion Methods
        #region canExecute
        //new invoice can execute
        /// <summary>
        /// checks if the inputs for creating the new Invoice are valid
        /// </summary>
        /// <returns>true - if InvoiceName and both 
        /// Inhabitant Names have a valid input
        /// (Names only Letters, Invoice alphanumerical)</returns>
        public bool CanExecuteNewInvoice()
        {
            if (Regex.IsMatch(InvoiceToCreate.Inhabitants[0].Name.Trim(), @"^[a-zA-Z]+$") &&
                Regex.IsMatch(InvoiceToCreate.Inhabitants[1].Name.Trim(), @"^[a-zA-Z]+$") &&
                Regex.IsMatch(InvoiceToCreate.InvoiceName!.Trim(), @"^[a-zA-Z0-9]+$"))
            {
                return true;
            }
            Log.WriteLine("CanExecuteNewInvoice false");
            return false;
        }
        /// <summary>
        /// Checks if the input is valid to create a new Bill
        /// </summary>
        /// <returns></returns>
        public bool CanExecuteAddBill()
        {
            if (this.ExpenseToAdd.ValueExpense > 0 & !string.IsNullOrEmpty(this.ExpenseToAdd.Categorie))
            {
                return true;
            }
            return false;
        }
        #endregion canExecute
    }
}
