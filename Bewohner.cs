using Essensausgleich.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich
{
    /// <summary>
    /// Class for the Userobject Bewohner 
    /// </summary>
   public class Bewohner : AppObjekt, INotifyPropertyChanged
    {
        private decimal ausgaben;
        /// <summary>
        /// inits the Name of the Bewohner to ""
        /// </summary>
        private string _Name = string.Empty;
        /// <summary>
        /// Gets or Sets the Name propertie of Bewohner
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
                OnPropertyChanged();
            }
        }      
        /// <summary>
        /// List of Entrys for the bewohner
        /// </summary>
        public List<Betrag> Einzelbetraege = new List<Betrag>();
        /// <summary>
        /// creates and obj of Bewohner
        /// </summary>
        public Bewohner()
        {
            
        }       
        /// <summary>
        /// Access to Ausgaben decimal prevents input of negativ numbers
        /// </summary>
        public decimal Ausgaben
        {
            get
            {
                return ausgaben;
            }
            set
            {
                ausgaben = value < 0 ? 0 : value;
            }
        }     

        /// <summary>
        /// Method to Add new Entries in the <c>ListBetrag</c>
        /// </summary>
        /// <param name="kategorie"></param>
        /// <param name="betrag"></param>
        public void AddBetrag(string kategorie, decimal betrag)
        {
            if (kategorie != "")
            {
                Einzelbetraege.Add(new Betrag(kategorie, betrag));
                ausgaben += betrag;
            }
            else
            {
                Einzelbetraege.Add(new Betrag("unkategorisiert", betrag));
                ausgaben += betrag;
            }
        }
        /// <summary>
        /// Adds the all Items of List to the Ausgaben variable
        /// </summary>
        public void RefreshBetrag()
        {
            foreach (var Betrag in Einzelbetraege)
            {
                ausgaben += Betrag.wert;
            }
        }           
        /// <summary>
        /// Resets Name, List and Ausgabe to Zero
        /// </summary>
        public void ResetBewohnerData()
        {
            Name = string.Empty;
            Einzelbetraege.Clear();
            ausgaben = 0;
        }
        /// <summary>
        /// Outputs All Items in List of Betrag to String
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
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            var BehandlerKopie = this.PropertyChanged;
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
            this.OnPropertyChanged(new PropertyChangedEventArgs(nameEigenschaft));
        }
        #endregion WPF über Änderungen Informieren
    }
}
