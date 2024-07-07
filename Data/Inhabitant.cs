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
        /// List of Entrys for the bewohner
        /// </summary>
        public ObservableCollection<Expense> ListOfExpenses { get; set; } = null!;
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
        private decimal _TotalExpense;
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
        /// <summary>
        /// Adds the all Items of List to the TotalExpense variable
        /// </summary>
        public void RefreshExpense()
        {
            foreach (var Betrag in ListOfExpenses)
            {
                _TotalExpense += Betrag.ValueExpense;
            }
        }
        /// <summary>
        /// Resets Name, List and Ausgabe to Zero
        /// </summary>
        public void ResetInhabitantData()
        {
            _Name = string.Empty;
            ListOfExpenses.Clear();
            _TotalExpense = 0;
            //OnPropertyChanged();
        }
        /// <summary>
        /// Outputs All Items in List of Expense to String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString()!;
        }
    }
}
