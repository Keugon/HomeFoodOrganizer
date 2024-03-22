using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log = System.Diagnostics.Debug;
namespace Essensausgleich
{
    /// <summary>
    /// Class for Save,Load and Reset of the UserData
    /// </summary>
    public class XMLPersistence : Persistence
    {
        //Temporer path
        private string _XMLFileName = "abrechnung.xml";
        public string XMLFileName
        {
            get
            {
                return _XMLFileName;
            }            
        }
        /// <summary>
        /// Methode to Save data from the User Object bewohner to an XML File
        /// </summary>
        /// <param name="bewohner1"></param>
        /// <param name="bewohner2"></param>
        public void Save(Bewohner bewohner1, Bewohner bewohner2)
        {
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Async = true;
                settings.ConformanceLevel = ConformanceLevel.Auto;
                XmlWriter writer = XmlWriter.Create(_XMLFileName, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("Abrechnung");

                foreach (var Betrag in bewohner1.Einzelbetraege)
                {
                    writer.WriteStartElement("a");
                    writer.WriteAttributeString("BewohnerName", bewohner1.name);
                    writer.WriteAttributeString("kategorie", Betrag.kategorie);
                    writer.WriteAttributeString("Betrag", Betrag.wert.ToString());
                    writer.WriteEndElement();
                }
                foreach (var Betrag in bewohner2.Einzelbetraege)
                {
                    writer.WriteStartElement("b");
                    writer.WriteAttributeString("BewohnerName", bewohner2.name);
                    writer.WriteAttributeString("kategorie", Betrag.kategorie);
                    writer.WriteAttributeString("Betrag", Betrag.wert.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Close();
            }
            catch (Exception writerException)
            {
                Log.WriteLine(writerException.Message);
            }
        }
        /// <summary>
        /// Methode to Load data from the User Object bewohner to an XML File and Calls the Refrehs method of each Bewohner objects itself
        /// </summary>
        /// <param name="bewohner1"></param>
        /// <param name="bewohner2"></param>
        public void Load(Bewohner bewohner1, Bewohner bewohner2)
        {
            if (!File.Exists("abrechnung.xml"))
            {
                return;
            }
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Async = true;
                XmlReader reader = XmlReader.Create(_XMLFileName, settings);
                string b1Name = "";
                string b2Name = "";
                decimal b1betrag = 0;
                decimal b2betrag = 0;
                string kat1 = "";
                string kat2 = "";
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name != "Abrechnung")
                        {
                            if (reader.Name == "a")
                            {
                                if (reader.AttributeCount > 0)
                                {
                                    while (reader.MoveToNextAttribute())
                                    {
                                        if (reader.Name != "a")
                                            switch (reader.Name)
                                            {
                                                case "BewohnerName":
                                                    b1Name = reader.Value;
                                                    break;
                                                case "kategorie":
                                                    kat1 = reader.Value;
                                                    break;
                                                case "Betrag":
                                                    b1betrag = Convert.ToDecimal(reader.Value);
                                                    break;
                                            }
                                    }
                                    bewohner1.name = b1Name;
                                    bewohner1.Einzelbetraege.Add(new Betrag(kat1, b1betrag));
                                }
                            }
                            if (reader.Name == "b")
                            {
                                if (reader.AttributeCount > 0)
                                {
                                    while (reader.MoveToNextAttribute())
                                    {
                                        if (reader.Name != "b")
                                            switch (reader.Name)
                                            {
                                                case "BewohnerName":
                                                    b2Name = reader.Value;
                                                    break;
                                                case "kategorie":
                                                    kat2 = reader.Value;
                                                    break;
                                                case "Betrag":
                                                    b2betrag = Convert.ToDecimal(reader.Value);
                                                    break;
                                            }
                                    }
                                    bewohner2.name = b2Name;
                                    bewohner2.Einzelbetraege.Add(new Betrag(kat2, b2betrag));

                                }
                            }
                        }
                    }
                }
                reader.Close();
                //Formular refresh            
                bewohner1.RefreshBetrag();
                bewohner2.RefreshBetrag();
            }
            catch (Exception exceptionRead)
            {
                Log.WriteLine(exceptionRead.Message);
                
            }
        }
        /// <summary>
        /// Calls itself the Reset Method of Bewohner for both Users
        /// </summary>
        /// <param name="bewohner1"></param>
        /// <param name="bewohner2"></param>
        public void Reset(Bewohner bewohner1, Bewohner bewohner2)
        {
            bewohner1.ResetBewohnerData();
            bewohner2.ResetBewohnerData();
        }
        public void ChangePath(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                _XMLFileName = path;
                Log.WriteLine("FileName changed");
                Log.WriteLine($"NewFileName{path}");

            }
            else
            {
                Log.WriteLine("FileName Missing or Null");
            }
        }
    }
}
