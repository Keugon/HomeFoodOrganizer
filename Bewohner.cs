using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich
{
    /// <summary>
    /// Class for the Userobject Bewohner 
    /// </summary>
   public class Bewohner
    {
        private decimal ausgaben;
        /// <summary>
        /// inits the Name of the Bewohner to ""
        /// </summary>
        public string name = "";
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
            name = "";
            Einzelbetraege.Clear();
            ausgaben = 0;
        }
        /// <summary>
        /// Outputs All Items in List of Betrag to String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            String aus = "Einzelbetraege:\n";
            foreach (Betrag b in Einzelbetraege)
            {
                aus += $"{b.wert}€ -- '{b.kategorie}'\n";
            }
            return aus;
        }
    }
}
