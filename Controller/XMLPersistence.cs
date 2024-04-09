
using Essensausgleich.Data;
using Essensausgleich.Infra;
using Essensausgleich.Tools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

using Log = System.Diagnostics.Debug;
namespace Essensausgleich.Controller
{
    /// <summary>
    /// Class for Save,Load and Reset of the UserData
    /// </summary>
    public class XMLPersistence : AppObjekt, Ipersistence
    {
        /// <summary>
        /// Intern cache for filename
        /// </summary>
        private string _XMLFileName = $"abrechnung_{General.GetCurrentDate()}.xml";
        /// <summary>
        /// Gets the XMLFilename in String
        /// </summary>
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
        /// <param name="inhabitant1"></param>
        /// <param name="inhabitant2"></param>
        public void Save(Inhabitant inhabitant1, Inhabitant inhabitant2)
        {
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Async = true;
                settings.ConformanceLevel = ConformanceLevel.Auto;
                XmlWriter writer = XmlWriter.Create(XMLFileName, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("Billing");

                foreach (var Expense in inhabitant1.ListOfExpenses)
                {
                    writer.WriteStartElement("a");
                    writer.WriteAttributeString("inhabitantName", inhabitant1.Name);
                    writer.WriteAttributeString("categorie", Expense.categorie);
                    writer.WriteAttributeString("valueExpense", Expense.valueExpense.ToString());
                    writer.WriteEndElement();
                }
                foreach (var Expense in inhabitant2.ListOfExpenses)
                {
                    writer.WriteStartElement("b");
                    writer.WriteAttributeString("inhabitantName", inhabitant2.Name);
                    writer.WriteAttributeString("categorie", Expense.categorie);
                    writer.WriteAttributeString("valueExpense", Expense.valueExpense.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Close();
                Log.WriteLine($"{XMLFileName} saved");
            }
            catch (Exception writerException)
            {
                Log.WriteLine(writerException.Message);
            }
        }
        /// <summary>
        /// Methode to Load data from the User Object bewohner to an XML File and Calls the Refrehs method of each Inhabitant objects itself
        /// </summary>
        /// <param name="inhabitant1"></param>
        /// <param name="inhabitant2"></param>
        public void Load(Inhabitant inhabitant1, Inhabitant inhabitant2)
        {          
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Async = true;
                XmlReader reader = XmlReader.Create(XMLFileName, settings);
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
                        if (reader.Name != "Billing")
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
                                                case "inhabitantName":
                                                    b1Name = reader.Value;
                                                    break;
                                                case "categorie":
                                                    kat1 = reader.Value;
                                                    break;
                                                case "valueExpense":
                                                    b1betrag = Convert.ToDecimal(reader.Value);
                                                    break;
                                            }
                                    }
                                    inhabitant1.Name = b1Name;
                                    inhabitant1.ListOfExpenses.Add(new Expense(kat1, b1betrag));
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
                                                case "inhabitantName":
                                                    b2Name = reader.Value;
                                                    break;
                                                case "categorie":
                                                    kat2 = reader.Value;
                                                    break;
                                                case "valueExpense":
                                                    b2betrag = Convert.ToDecimal(reader.Value);
                                                    break;
                                            }
                                    }
                                    inhabitant2.Name = b2Name;
                                    inhabitant2.ListOfExpenses.Add(new Expense(kat2, b2betrag));

                                }
                            }
                        }
                    }
                }
                reader.Close();
                //Formular refresh
                this.Context.InhabitantsManager.InhabitantsController.AddInhabitant(inhabitant1.Name);
                this.Context.InhabitantsManager.InhabitantsController.AddInhabitant(inhabitant2.Name);
                inhabitant1.RefreshExpense();
                inhabitant2.RefreshExpense();
                Log.WriteLine($"{XMLFileName} Loaded");
            }
            catch (Exception exceptionRead)
            {
                Log.WriteLine(exceptionRead.Message);

            }
            
        }
        /// <summary>
        /// Calls itself the Reset Method of Inhabitant for both Users
        /// </summary>
        /// <param name="inhabitants1"></param>
        /// <param name="inhabitants2"></param>
        public void Reset(Inhabitant inhabitants1, Inhabitant inhabitants2)
        {
            inhabitants1.ResetInhabitantData();
            inhabitants2.ResetInhabitantData();
            Log.WriteLine("ResetBewData1,2");
            this.Context.InhabitantsManager.InhabitantsController.ClearInhabitants();
            Log.WriteLine("ClearInhabsMehtod aftr");
        }
        /// <summary>
        /// Method that changes the XMLFileName (Path) in the XMLPersistance Obj
        /// </summary>
        /// <param name="path"></param>
        public void ChangePath(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                _XMLFileName = path;
                Log.WriteLine("FileName changed");
                Log.WriteLine($"NewFileName: {path}");

            }
            else
            {
                Log.WriteLine("FileName Missing or Null");
            }
        }
    }
}
