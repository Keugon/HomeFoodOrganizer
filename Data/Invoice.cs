using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Log = System.Diagnostics.Debug;

namespace Essensausgleich.Data
{
    /// <summary>
    /// List of Invoices
    /// </summary>
    public class Invoices
    {
        private string? _InvoicesProjectName;
        public string? InvoicesProjectName
        {
            get => this._InvoicesProjectName;
            set => this._InvoicesProjectName = value;

        }
        private string? _PathAndFileName;
        /// <summary>
        /// PathAndFileName gets set on load from File
        /// </summary>
        public string? PathAndFileName
        {
            get => this._PathAndFileName;
            set
            {
                this._PathAndFileName = value;
                OnPropertyChanged(nameof(PathAndFileName));
            }
        }
        private ObservableCollection<Invoice> _InvoiceList = null!;
        
        public ObservableCollection<Invoice> InvoiceList
        {
            get
            {
                if (this._InvoiceList == null)
                {
                    this._InvoiceList = new ObservableCollection<Invoice>();
                }
                return this._InvoiceList;
            }
            set => this._InvoiceList = value;
        }
        private DateTime? _DateTimeCreation;
        /// <summary>
        /// Gets or Set the First Time this Invoice was Saved to File
        /// </summary>
        public DateTime? DateTimeCreation
        {
            get => this._DateTimeCreation;
            set
            {
                this._DateTimeCreation = value;
                OnPropertyChanged(nameof(DateTimeCreation));
            }
        }
        private DateTime? _DateTimeChanged;
        /// <summary>
        /// Gets or Set the Last Time this Invoice was Saved to File
        /// </summary>
        public DateTime? DateTimeChanged
        {
            get => this._DateTimeChanged;
            set
            {
                this._DateTimeChanged = value;
                OnPropertyChanged(nameof(DateTimeChanged));
            }
        }
        #region WPF über Änderungen Informieren
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            System.Diagnostics.Debug.WriteLine($"OnPropertyChanged Called in InvoiceManager:{propertyName}");
        }
        #endregion WPF über Änderungen Informieren
    }
    /// <summary>
    /// Object to handle one Invoice including 2 Inhabitants
    /// </summary>
    public partial class Invoice : INotifyPropertyChanged
    {
        private string _InvoiceName = string.Empty;
        public string InvoiceName
        {
            get => this._InvoiceName;
            set
            {
                this._InvoiceName = value;
                OnPropertyChanged(nameof(InvoiceName));
            }
        }
        
        /// <summary>
        /// Internal Field for Caching
        /// </summary>
        private ObservableCollection<string> _InhabitantsNameList = new ObservableCollection<string>();
        /// <summary>
        /// Gets or Sets a ObservableList of Strings 
        /// </summary>
        public ObservableCollection<string> InhabitantsNameList
        {
            get
            {
                return this._InhabitantsNameList;
            }
            set
            {
                this._InhabitantsNameList = value;
            }
        }
        private Inhabitants _Inhabitants = null!;
        /// <summary>
        /// Gets or Sets the Content for the Inhabitants List
        /// </summary>
        public Inhabitants Inhabitants
        {
            get
            {
                if (this._Inhabitants == null)
                {
                    this._Inhabitants = new Inhabitants();
                    this.Inhabitants.Add(new Inhabitant());
                    this.Inhabitants.Add(new Inhabitant());
                }
                return this._Inhabitants;
            }
            set
            {
                this._Inhabitants = value;
            }
        }

        private string _InvoiceComment = "";
        /// <summary>
        /// Text Commentary for this Invoice
        /// </summary>
        public string InvoiceComment
        {
            get => this._InvoiceComment;
            set
            {
                this._InvoiceComment = value;
                OnPropertyChanged(nameof(InvoiceComment));
            }
        }
       
        private DateTime? _DateTimeCreation;
        /// <summary>
        /// Gets or Set the First Time this Invoice was Saved to File
        /// </summary>
        public DateTime? DateTimeCreation
        {
            get => this._DateTimeCreation;
            set
            {
                this._DateTimeCreation = value;
                OnPropertyChanged(nameof(DateTimeCreation));
            }
        }
        private DateTime? _DateTimeChanged;
        /// <summary>
        /// Gets or Set the Last Time this Invoice was Saved to File
        /// </summary>
        public DateTime? DateTimeChanged
        {
            get => this._DateTimeChanged;
            set
            {
                this._DateTimeChanged = value;
                OnPropertyChanged(nameof(DateTimeChanged));
            }
        }
        /// <summary>
        /// Adds a Inhabitant to the End of the Inhabitants List
        /// </summary>
        /// <param name="inhabitantToAdd"></param>
        public void AddInhabitantToList(Inhabitant inhabitantToAdd)
        {
            this.Inhabitants.Add(inhabitantToAdd);
        }
        #region WPF über Änderungen Informieren
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            System.Diagnostics.Debug.WriteLine($"OnPropertyChanged Called in InvoiceManager:{propertyName}");
        }
        #endregion WPF über Änderungen Informieren
    }


}
