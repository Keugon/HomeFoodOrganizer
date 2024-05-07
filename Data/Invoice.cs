using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                return _InhabitantsNameList;
            }
            set
            {
                _InhabitantsNameList = value;
                Log.WriteLine("InhabitantsNameList got Set");

            }
        }
        private Inhabitants _Inhabitants = null!;

        public Inhabitants Inhabitants { get; set; }

        private string _InvoiceComment = "TestKommentar";
        public string InvoiceComment { get; set; }
    }
}
