using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich
{
    internal class Bewohner
    {
        private decimal ausgaben;
        public string? name;
        private List<Betrag> Einzelbetraege = new List<Betrag>();

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
        public Bewohner(string? n)
        {
            name = n;
        }
        public string GetBewohnerName()
        {
            if (name != null)
            {
                return name;
            }
            else
            {
                return "NotNamed";
            }

        }
        public void AddBetrag(string k, decimal b)
        {
            if (k != "")
            {
                Einzelbetraege.Add(new Betrag(k, b));
                ausgaben += b;
                
            }
            else
            {
                Einzelbetraege.Add(new Betrag("unkategorisiert", b));
                ausgaben += b;
            }
        }
        
        public string EinzelbetraegeAusgeben()
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
