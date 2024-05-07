using Essensausgleich.Data;
using Essensausgleich.Infra;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log = System.Diagnostics.Debug;

namespace Essensausgleich.Controller
{
    /// <summary>
    /// Controller for Inhabitants
    /// </summary>
    public class InhabitantsController : JsonController<Inhabitants>
    {
        private ObservableCollection<string> _InhabitantsNameList = new ObservableCollection<string>();
        /// <summary>
        /// Gets or Sets a ObservableList of Strings 
        /// </summary>
        public ObservableCollection<string> InhabitantsNameList
        {
            get
            {
                return _InhabitantsNameList;
            }
            set
            {
                _InhabitantsNameList = value;             
                Log.WriteLine("InhabitantsNameList got Set");
                
            }
        }
        /// <summary>
        /// Clears the content of InhabitantsNameList
        /// </summary>
        public void ClearInhabitants()
        {
            _InhabitantsNameList.Clear();
            
          // OnPropertyChanged("InhabitantsNameList");
            Log.WriteLine("InhabitantsNameList cleared");
        }
        /// <summary>
        /// Adds a inhabitant Obj to the List
        /// </summary>
        /// <param name="inhabitant"></param>
        public void AddInhabitant(string inhabitant)
        {
            InhabitantsNameList.Add(inhabitant);
            Log.WriteLine($"{inhabitant} Added to InhabitantsNameList");
        }

       
    }
}
