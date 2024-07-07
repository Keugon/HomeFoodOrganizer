using Essensausgleich.Infra;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Essensausgleich.Data
{
    /// <summary>
    /// Type save List of Inhabitant Obejct
    /// </summary>
    public class Inhabitants : List<Inhabitant>
    {

    }
    /// <summary>
    /// Class for the Userobject Inhabitant 
    /// </summary>
    public class Inhabitant
    {
        /// <summary>
        /// Gets or Sets the Name propertie of Inhabitant
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Internal Field
        /// </summary>
        private ObservableCollection<Expense> _ListOfExpenses = null!;
        /// <summary>
        /// List of Expenses for the Inhabitant
        /// </summary>
        public ObservableCollection<Expense> ListOfExpenses
        {
            get
            {
                if (this._ListOfExpenses == null)
                {
                    this._ListOfExpenses = new ObservableCollection<Expense>();
                }
                return this._ListOfExpenses;
            }
            set
            {
                this._ListOfExpenses = value;
            }
        }
        /// <summary>
        /// Internal Field
        /// </summary>
        private decimal _TotalExpense;
        /// <summary>
        /// Access to TotalExpense decimal prevents input of negativ numbers
        /// </summary>
        public decimal TotalExpense
        {
            get
            {
                this._TotalExpense = 0;
                foreach (var expense in ListOfExpenses)
                {
                    this._TotalExpense += expense.ValueExpense;
                }
                return this._TotalExpense;
            }
            set
            {
                this._TotalExpense = value;
            }
        }
        /// <summary>
        /// Method to Add new Entries in the <c>ListBetrag</c>
        /// </summary>
        /// <param name="categorie"></param>
        /// <param name="valueExpense"></param>
        public void AddBetrag(string categorie, decimal valueExpense)
        {

            if (categorie != "")
            {
                ListOfExpenses.Add(new Expense
                {
                    Categorie = categorie,
                    ValueExpense = valueExpense
                });
                _TotalExpense += valueExpense;
            }
            else
            {
                ListOfExpenses.Add(new Expense
                {
                    Categorie = categorie,
                    ValueExpense = valueExpense
                });
                _TotalExpense += valueExpense;
            }
        }
    }
}
