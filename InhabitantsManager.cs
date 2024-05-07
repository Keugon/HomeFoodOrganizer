using Essensausgleich.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Essensausgleich.Data;
using Essensausgleich.Controller;
using Essensausgleich.Tools;
using Log = System.Diagnostics.Debug;
namespace Essensausgleich
{


    /// <summary>
    /// InhabitansManger Service
    /// </summary>
    public class InhabitantsManager : AppObjekt
    {

        #region Bewohner und BewohnerListe


        private Inhabitant _Inhabitant1 = null!;
        /// <summary>
        /// Gets the object Bewohner1
        /// </summary>
        public Inhabitant Inhabitant1
        {
            get
            {
                if (this._Inhabitant1 == null)
                {
                    this._Inhabitant1 = new Inhabitant();

                }
                return this._Inhabitant1;
            }
        }

        private Inhabitant _Inhabitant2 = null!;
        /// <summary>
        /// Gets the object Bewohner2
        /// </summary>
        public Inhabitant Inhabitant2
        {
            get
            {
                if (this._Inhabitant2 == null)
                {
                    this._Inhabitant2 = new Inhabitant();
                }
                return this._Inhabitant2;
            }
        }


        #endregion

        private Inhabitants _Inhabitants = null!;

        public Inhabitants Inhabitants
        {
            get
            {
                if (this._Inhabitants == null)
                {
                    this._Inhabitants = new Inhabitants();
                }
                return this._Inhabitants;
            }
            set
            {
                _Inhabitants = value;
            }
        }

        private Essensausgleich.Controller.InhabitantsController _InhabitantsController = null!;
        /// <summary>
        /// Gets the Controller for Inhabitants
        /// </summary>
        public InhabitantsController InhabitantsController
        {
            get
            {
                if (_InhabitantsController == null)
                {
                    this._InhabitantsController = this.Context.Fabricate<Essensausgleich.Controller.InhabitantsController>();
                }
                return _InhabitantsController;
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
        /// <summary>
        /// Method to save the Inhabits (List) to Jsonfile
        /// </summary>
        public void Save()
        {
            try
            {
                this.InhabitantsController.Save(this.JsonFileName, this.Inhabitants);
            }
            catch (Exception ex)
            {


                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

    }


}
