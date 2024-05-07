using Essensausgleich.Controller;
using Essensausgleich.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich
{
    public class InvoiceManager :AppObjekt
    {

        private InvoiceController _InvoiceController = null!;

        public InvoiceController InvoiceController
        {
            get
            {
                if(this._InvoiceController == null)
                {
                    this._InvoiceController = new InvoiceController();
                }
                return this._InvoiceController;
            }
        }
        /// <summary>
        /// Method to save the Inhabits (List) to Jsonfile
        /// </summary>
        public void Save()
        {
            try
            {
               // this.InvoiceController.Save(this.JsonFileName, this.Invoice);
            }
            catch (Exception ex)
            {


                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Method to save the Inhabits (List) to Jsonfile
        /// </summary>
        public void Load()
        {
            try
            {
               // this.InvoiceController.Load(this.JsonFileName);
            }
            catch (Exception ex)
            {


                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
