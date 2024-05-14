﻿using Essensausgleich.Controller;
using Essensausgleich.Data;
using Essensausgleich.Infra;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log = System.Diagnostics.Debug;

namespace Essensausgleich
{
    /// <summary>
    /// Manages Invoices
    /// </summary>
    public class InvoiceManager :AppObjekt ,INotifyPropertyChanged
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
               this.OnPropertyChanged(nameof(this.Invoices));

                
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
        /// <summary>
        /// Service for Serializing and deserializing of Invoice Obejcts
        /// </summary>
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
               this.InvoiceController.Save(invoiceToSave.FileName!, invoiceToSave);
                

                System.Diagnostics.Debug.WriteLine($"This Invoice got saved to:{invoiceToSave.FileName}") ;
            }
            catch (Exception ex)
            {


                System.Diagnostics.Debug.WriteLine(ex.Message);
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
        #region WPF über Änderungen Informieren
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            System.Diagnostics.Debug.WriteLine($"OnPropertyChanged Called in InvoiceManager:{propertyName}");
        }
        #endregion WPF über Änderungen Informieren
    }
}
