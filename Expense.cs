using CommunityToolkit.Mvvm.ComponentModel;

namespace Essensausgleich
{
    /// <summary>
    /// Class to serve as Sigle entrys for the Inhabitant object
    /// </summary>
    public partial class Expense : ObservableObject
    {
        private string _Categorie = string.Empty;
        /// <summary>
        /// Categorie string of Expense
        /// </summary>
        public string Categorie
        {
            get => this._Categorie;
            set
            {
                this._Categorie = value;
                OnPropertyChanged();
            }
        }
        private decimal _ValueExpense;
        /// <summary>
        /// Amount in decimal of Expense
        /// </summary>
        public decimal ValueExpense
        {
            get => this._ValueExpense;
            set
            {
                this._ValueExpense = value;
                OnPropertyChanged();
            }
        }
    }
}
