using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log = System.Diagnostics.Debug;

namespace Essensausgleich.Data
{
    /// <summary>
    /// List of Invoices
    /// </summary>
    public class Invoices : System.Collections.Generic.List<Invoice>
    {

    }
    /// <summary>
    /// Object to handle one Invoice including 2 Inhabitants
    /// </summary>
    public class Invoice
    {
        private ObservableCollection<string> _InhabitantsNameList = new ObservableCollection<string>();
        /// <summary>
        /// Gets or Sets a ObservableList of Strings 
        /// </summary>
        public ObservableCollection<string> InhabitantsNameList
        {
            get
            {

                System.Diagnostics.Debug.WriteLine($"InhabitantsNameList Get");
                return this._InhabitantsNameList;
            }
            set
            {

                this._InhabitantsNameList = value;
                System.Diagnostics.Debug.WriteLine($"InhabitantsNameList got Set");
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
                }
                return this._Inhabitants;
            }
            set
            {
                this._Inhabitants = value;
            }
        }

        private string _InvoiceComment = "TestKommentar";
        /// <summary>
        /// Text Commentary for this Invoice
        /// </summary>
        public string InvoiceComment
        {
            get => this._InvoiceComment;
            set => this._InvoiceComment = value;
        }


        /// <summary>
        /// FileName gets set on load from File
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Adds a Inhabitant to the End of the Inhabitants List
        /// </summary>
        /// <param name="inhabitantToAdd"></param>
        public void AddInhabitantToList(Inhabitant inhabitantToAdd)
        {
            this.Inhabitants.Add(inhabitantToAdd);
        }
        
    }


}
