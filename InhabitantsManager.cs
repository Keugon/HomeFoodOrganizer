using Essensausgleich.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Essensausgleich.Data;
using Essensausgleich.Controller;

namespace Essensausgleich
{
    /// <summary>
    /// InhabitansManger Service
    /// </summary>
    public class InhabitantsManager : AppObjekt
    {
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
    }
    

}
