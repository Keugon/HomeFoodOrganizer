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
    public class InhabitantsController : AppObjekt, INotifyPropertyChanged
    {
        private ObservableCollection<string> _InhabitantsList = new ObservableCollection<string>();
        /// <summary>
        /// Gets or Sets a ObservableList of Strings 
        /// </summary>
        public ObservableCollection<string> InhabitantsList
        {
            get
            {
                return _InhabitantsList;
            }
            set
            {
                _InhabitantsList = value;
               // OnPropertyChanged();
                Log.WriteLine("InhabitantsList got Set");
                
            }
        }
        /// <summary>
        /// Clears the content of InhabitantsList
        /// </summary>
        public void ClearInhabitants()
        {
            _InhabitantsList.Clear();
            
           OnPropertyChanged("InhabitantsList");
            Log.WriteLine("InhabitantsList cleared");
        }
        /// <summary>
        /// Adds a bewohner Obj to the List
        /// </summary>
        /// <param name="bewohner"></param>
        public void AddInhabitant(string bewohner)
        {
            InhabitantsList.Add(bewohner);
            Log.WriteLine($"{bewohner} Added to InhabitantsList");
        }

        #region WPF über Änderungen Informieren
        /// <summary>
        /// Wird ausgelöst, wenn sich der Inhalt
        /// einer Eigenschaft geändert hat
        /// </summary>
        public event PropertyChangedEventHandler?
            PropertyChanged = null!;

        /// <summary>
        /// Löst das Ereignis PropertyChanged aus
        /// </summary>
        /// <param name="e">Ereginisdaten mit
        /// dem Namen der geänderten Eigenschaft</param>
        protected virtual void OnPropertyChanged(
            PropertyChangedEventArgs e)
        {
            var BehandlerKopie = PropertyChanged;
            if (BehandlerKopie != null)
            {
                BehandlerKopie(this, e);
            }
        }

        /// <summary>
        /// Löst das Ereignis PropertyChanged aus
        /// </summary>
        /// <param name="nameEigenschaft">optional: Die Bezeichnung
        /// der Eigenschaft, deren Inhalt geändert wurde</param>
        /// <remarks>Fehlt der Name der Eigenschaft,
        /// wird der Name vom Aufrufer eingesetzt</remarks>
        protected virtual void OnPropertyChanged(
            [System.Runtime.CompilerServices.CallerMemberName] string nameEigenschaft = null!)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameEigenschaft));
            System.Diagnostics.Debug.WriteLine($"OnPropertyChanged ausgelöst bei:{nameEigenschaft}");
        }
        #endregion WPF über Änderungen Informieren
    }
}
