using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich
{
     public struct Betrag
    {
        public string kategorie;
        public decimal wert;
        public Betrag(string k, decimal w)
        {
            kategorie = k;
            wert = w;
        }       
    }
}
