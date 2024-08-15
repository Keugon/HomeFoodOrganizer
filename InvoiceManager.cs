using Essensausgleich.Controller;
using Essensausgleich.Data;
using System.ComponentModel;

namespace Essensausgleich
{
    /// <summary>
    /// Manages Project
    /// </summary>
    public class InvoiceManager : DRAXNET.Core.AppObjekt, INotifyPropertyChanged
    {
        #region Invoice and List
        /// <summary>
        /// Field Cache
        /// </summary>
        private DRAXNET.AusgabenBuddy.Models.Project _Invoices = null!;
        /// <summary>
        /// Gets or Sets a List of Invoice
        /// </summary>
        public DRAXNET.AusgabenBuddy.Models.Project Invoices
        {
            get
            {
                if (this._Invoices == null)
                {
                    this._Invoices = new DRAXNET.AusgabenBuddy.Models.Project();
                }
                return this._Invoices;
            }
            set
            {
                this._Invoices = value;
                this.OnPropertyChanged(nameof(this.Invoices));


            }
        }
        /// <summary>
        /// Field Cache
        /// </summary>
        private DRAXNET.AusgabenBuddy.Models.Invoice _Invoice = null!;
        /// <summary>
        /// New Invoice Object
        /// </summary>
        public DRAXNET.AusgabenBuddy.Models.Invoice Invoice
        {
            get
            {
                if (this._Invoice == null)
                {
                    this._Invoice = new DRAXNET.AusgabenBuddy.Models.Invoice();
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

        private InvoicesController _InvoiceController = null!;
        /// <summary>
        /// Service for Serializing and deserializing of Invoice Obejcts
        /// </summary>
        public InvoicesController InvoicesController
        {
            get
            {
                if (this._InvoiceController == null)
                {
                    this._InvoiceController = this.Context.Fabricate<InvoicesController>();
                }
                return this._InvoiceController;
            }
        }
        /// <summary>
        /// Method to save the Inhabits (List) to Jsonfile
        /// </summary>
        public void Save(DRAXNET.AusgabenBuddy.Models.Project invoiceToSave)
        {
            try
            {
                //Update DateTime Changed on every Save
                invoiceToSave.DateTimeChanged = DateTime.Now;
                this.InvoicesController.Save(invoiceToSave.PathAndFileName!, invoiceToSave);


                System.Diagnostics.Debug.WriteLine($"This Invoice got saved to:{invoiceToSave.PathAndFileName}");
            }
            catch (Exception ex)
            {


                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Deletes the given InvoiceList
        /// </summary>
        /// <param name="invoiceToDelete">Project (Project) to delete</param>
        public void Delete(DRAXNET.AusgabenBuddy.Models.Project invoiceToDelete)
        {
            try
            {
                File.Delete(invoiceToDelete.PathAndFileName!);
                System.Diagnostics.Debug.WriteLine($"Project: {invoiceToDelete.InvoicesProjectName} of with File: {invoiceToDelete.PathAndFileName} got deleted");
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Method to Load a Invoice from a give Path
        /// </summary>
        public DRAXNET.AusgabenBuddy.Models.Project Load(string pathWithFileName)
        {
            try
            {

                System.Diagnostics.Debug.WriteLine($"Try load File: {pathWithFileName}");
                var Invoices = this.InvoicesController.Load(pathWithFileName);
                Invoices.PathAndFileName = pathWithFileName;

                System.Diagnostics.Debug.WriteLine($"Invoice Sucsesfully read from File:{pathWithFileName}");
                return Invoices;
            }
            catch (Exception ex)
            {

                var Invoice = new Invoice();
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null!;
            }
        }
        //#region WPF über Änderungen Informieren
        ///// <summary>
        ///// Event PropertyChanged
        ///// </summary>
        //public event PropertyChangedEventHandler? PropertyChanged;
        ///// <summary>
        ///// On PropertyChanged
        ///// </summary>
        ///// <param name="propertyName"></param>
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        //#endregion WPF über Änderungen Informieren
    }
}
