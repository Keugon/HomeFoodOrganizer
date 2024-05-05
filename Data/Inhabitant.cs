using Essensausgleich.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Essensausgleich.Data
{
    public class Inhabitants: List<Inhabitant> 
    {

    }
    /// <summary>
    /// Class for the Userobject Inhabitant 
    /// </summary>
    public class Inhabitant : AppObjekt, INotifyPropertyChanged
    {

        /// <summary>
        /// inits the Name of the Inhabitant to ""
        /// </summary>
        private string _Name = string.Empty;
        /// <summary>
        /// Gets or Sets the Name propertie of Inhabitant
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                //OnPropertyChanged();
            }
        }
        /// <summary>
        /// List of Entrys for the bewohner
        /// </summary>
        
        public List<Expense> ListOfExpenses = new List<Expense>();
        /// <summary>
        /// creates and obj of Inhabitant
        /// </summary>
        public Inhabitant()
        {

        }
        /// <summary>
        /// Access to TotalExpense decimal prevents input of negativ numbers
        /// </summary>
        public decimal TotalExpense
        {
            get
            {
               
                return _TotalExpense;
            }
            set
            {
                _TotalExpense = value < 0 ? 0 : value;
                //OnPropertyChanged();
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
                ListOfExpenses.Add(new Expense(categorie, valueExpense));
                _TotalExpense += valueExpense;
            }
            else
            {
                ListOfExpenses.Add(new Expense("unkategorisiert", valueExpense));
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
                _TotalExpense += Betrag.valueExpense;
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


        #region WPF über Änderungen Informieren
        /// <summary>
        /// Wird ausgelöst, wenn sich der Inhalt
        /// einer Eigenschaft geändert hat
        /// </summary>
        public event PropertyChangedEventHandler?
            PropertyChanged = null!;

        /// <summary>
        /// Löst das Ereignis PropertyChanged aus
        /// </summary>
        /// <param name="e">Ereginisdaten mit
        /// dem Namen der geänderten Eigenschaft</param>
        protected virtual void OnPropertyChanged(
            PropertyChangedEventArgs e)
        {
            var BehandlerKopie = PropertyChanged;
            if (BehandlerKopie != null)
            {
                BehandlerKopie(this, e);
            }
        }

        /// <summary>
        /// Löst das Ereignis PropertyChanged aus
        /// </summary>
        /// <param name="nameEigenschaft">optional: Die Bezeichnung
        /// der Eigenschaft, deren Inhalt geändert wurde</param>
        /// <remarks>Fehlt der Name der Eigenschaft,
        /// wird der Name vom Aufrufer eingesetzt</remarks>
        protected virtual void OnPropertyChanged(
            [System.Runtime.CompilerServices.CallerMemberName] string nameEigenschaft = null!)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameEigenschaft));
        }
        #endregion WPF über Änderungen Informieren
    }
}
