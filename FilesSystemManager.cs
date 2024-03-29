using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Essensausgleich.Controller;
using Essensausgleich.Infra;

namespace Essensausgleich
{
    /// <summary>
    /// Service to Handle interactions with the XMLPersistence
    /// </summary>
    public   class FilesSystemManager :AppObjekt
    {
       
        /// <summary>
        /// Returns the Initialized Object
        /// </summary>
        /// <returns></returns>
        public XMLPersistence GetXMLPersistance()
        {
            if (this._XMLPersistence == null)
            {
                this._XMLPersistence = this.Kontext.
                    Produziere<XMLPersistence>();
            }
            return this._XMLPersistence;
        }
        private XMLPersistence _XMLPersistence = null!;
        /// <summary>
        /// ruft den Dienst zum Lesen und Schreiben der Anwendungssprachen ab
        /// </summary>
        private  XMLPersistence XMLPersistence
        {
            get
            {
                if (this._XMLPersistence == null)
                {
                    this._XMLPersistence = this.Kontext.
                        Produziere<XMLPersistence>();
                }
                return this._XMLPersistence;
            }
        }
    }
}
