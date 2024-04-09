using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich
{
    /// <summary>
    /// struct to serve as Sigle entrys for the Inhabitant object
    /// </summary>
     public struct Expense
    {
        /// <summary>
        /// Categorie string of Expense
        /// </summary>
        public string categorie {  get; set; }
        /// <summary>
        /// Amount in decimal of Expense
        /// </summary>
        public decimal valueExpense {  get; set; }
        /// <summary>
        /// constructor for the Expense Struct
        /// </summary>
        /// <param name="k"></param>
        /// <param name="w"></param>
        public Expense(string k, decimal w)
        {
            categorie = k;
            valueExpense = w;
        }       
    }
}
