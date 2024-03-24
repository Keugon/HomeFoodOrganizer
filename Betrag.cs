using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich
{
    /// <summary>
    /// struct to serve as Sigle entrys for the Bewohner object
    /// </summary>
     public struct Betrag
    {
        /// <summary>
        /// Categorie string of Betrag
        /// </summary>
        public string kategorie {  get; set; }
        /// <summary>
        /// Amount in decimal of Betrag
        /// </summary>
        public decimal wert {  get; set; }
        /// <summary>
        /// constructor for the Betrag Struct
        /// </summary>
        /// <param name="k"></param>
        /// <param name="w"></param>
        public Betrag(string k, decimal w)
        {
            kategorie = k;
            wert = w;
        }       
    }
}
