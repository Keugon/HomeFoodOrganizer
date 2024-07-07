using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich
{
    /// <summary>
    /// Class to serve as Sigle entrys for the Inhabitant object
    /// </summary>
    public class Expense
    {
        /// <summary>
        /// Categorie string of Expense
        /// </summary>
        public string Categorie { get; set; } = string.Empty;
        /// <summary>
        /// Amount in decimal of Expense
        /// </summary>
        public decimal ValueExpense { get; set; }
    }
}
