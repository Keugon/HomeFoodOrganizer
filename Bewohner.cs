using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich
{
    class Bewohner
    {
        private decimal ausgaben; // if you do it right - you could calculate the "ausgaben" total by adding up all "list<Betrag>" entries. f
        public string? name;
        private List<Betrag> Einzelbetraege = new List<Betrag>();

        //public decimal Ausgaben { get; set;}
        public decimal Ausgaben // these can be inlined 
        // decimal should not have an issue with the value
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
        public Bewohner(string? n) // Constructor should be on top of the class. 
        {
            //Why would the "n" parameter (which is name i guess) be null? If you initialize a Bewohner object you should already have a name. 
            // Other idea: create an empty constructor (without parameter) and 
            // Bewohner bewohner = new();
            // bewohner.setName("Name");

            name = n;
        }
        public string GetBewohnerName() // why is there a difference here? Why not create a Name with {get;set;}?

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
        public void AddBetrag(string k, decimal b) // please be careful to use speaking variables. What is K? What is b? 
        // Based on code i'd guess its "kategorie" and "betrag" right? 
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
        public void RefreshBetrag() // why should this be public? 
        // You could just call the method "AddBetrag" and refresh it here - making the RefreshBetrag private. 
        {
            foreach (var Betrag in Einzelbetraege)
            {
                ausgaben += Betrag.wert;
            }
        }

        public string EinzelbetraegeAusgeben()
        {
            String aus = "Einzelbetraege:\n";
            foreach (Betrag b in Einzelbetraege)
            {
                aus += $"{b.wert}€ -- '{b.kategorie}'\n"; // you could override the ".ToString()" method of Einzelbetraege object. 

            }
            return aus;

        }
        public List<Betrag> BewohnerAusgabenListe()
        {
            List<Betrag> BewohnerAusgabenListe = new List<Betrag>();
            BewohnerAusgabenListe = Einzelbetraege; //why not just returning the existing object? 
            return BewohnerAusgabenListe;
        }
        public string BewohnerName()
        {
            if (name != null)
            {
                return name;
            }
            return "NoNmae"; // typo

        }
        public void LoadBewohnerDataXML(string LoadName, List<Betrag> AusgabenListe) // should be abstracted into a seperate class that creates Bewohner and their "ausgaben"
        {
            name = LoadName;
            Einzelbetraege = AusgabenListe;
        }
        public void ResetBewohnerData()
        {
            name = null;
            Einzelbetraege.Clear();
            ausgaben = 0;
        }

    }
}
