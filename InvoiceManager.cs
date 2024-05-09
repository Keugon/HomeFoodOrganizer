using Essensausgleich.Controller;
using Essensausgleich.Data;
using Essensausgleich.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log = System.Diagnostics.Debug;

namespace Essensausgleich
{
    public class InvoiceManager :AppObjekt
    {
        #region Invoice and List
        /// <summary>
        /// Field Cache
        /// </summary>
        private Invoices _Invoices = null!;
        /// <summary>
        /// Gets or Sets a List of Invoice
        /// </summary>
        public Invoices Invoices
        {
            get
            {
                if(this._Invoices == null)
                {
                    this._Invoices = new Invoices();
                }
                return this._Invoices;
            }
            set
            {
                this._Invoices = value;
            }
        }
        /// <summary>
        /// Field Cache
        /// </summary>
        private Invoice _Invoice = null!;
        /// <summary>
        /// New Invoice Object
        /// </summary>
        public Invoice Invoice
        {
            get
            {
                if(this._Invoice == null)
                {
                    this._Invoice = new Invoice();
                }
                return this._Invoice;
                
            }
            set
            {
                this._Invoice = value;

                System.Diagnostics.Debug.WriteLine("Invoice Changed");
            }
        }
        #endregion

        private InvoiceController _InvoiceController = null!;

        public InvoiceController InvoiceController
        {
            get
            {
                if(this._InvoiceController == null)
                {
                    this._InvoiceController = this.Context.Fabricate<InvoiceController>();
                }
                return this._InvoiceController;
            }
        }
        /// <summary>
        /// Method to save the Inhabits (List) to Jsonfile
        /// </summary>
        public void Save(Invoice invoiceToSave)
        {
            try
            {
               this.InvoiceController.Save(this.JsonFileName, invoiceToSave);

                System.Diagnostics.Debug.WriteLine($"This Invoice got saved to:{this.JsonFileName}") ;
            }
            catch (Exception ex)
            {


                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Method to Load a Invoice from the JsonFileName Path
        /// </summary>
        public Invoice Load()
        {
            try
            {

                System.Diagnostics.Debug.WriteLine($"Try load File: {this.JsonFileName}");
               var Invoice = this.InvoiceController.Load(this.JsonFileName);

                System.Diagnostics.Debug.WriteLine($"Invoice Sucsesfully read from File:{this.JsonFileName}");
                return Invoice;
            }
            catch (Exception ex)
            {

                var Invoice = new Invoice();
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return Invoice;
            }
        }
        /// <summary>
        /// Method to Load a Invoice from a give Path
        /// </summary>
        public Invoice Load(string pathWithFileName)
        {
            try
            {

                System.Diagnostics.Debug.WriteLine($"Try load File: {pathWithFileName}");
                var Invoice = this.InvoiceController.Load(pathWithFileName);

                System.Diagnostics.Debug.WriteLine($"Invoice Sucsesfully read from File:{pathWithFileName}");
                return Invoice;
            }
            catch (Exception ex)
            {

                var Invoice = new Invoice();
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return Invoice;
            }
        }
        /// <summary>
        /// Intern cache for filename
        /// </summary>
        private string _JsonFileName = $"abrechnung.json";
        //$"abrechnung_{General.GetCurrentDate()}.json";
        /// <summary>
        /// Gets the XMLFilename in String
        /// </summary>
        public string JsonFileName
        {
            get
            {
                return _JsonFileName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _JsonFileName = value;
                    Log.WriteLine("FileName changed");
                    Log.WriteLine($"NewFileName: {value}");

                }
                else
                {
                    Log.WriteLine("FileName Missing or Null");
                }
            }

        }
    }
}
