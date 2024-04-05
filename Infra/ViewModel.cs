using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich.Infra
{
    /// <summary>
    /// Stellt Basisfunktionalitäten für
    /// ein MVVM ViewModel bereit
    /// </summary>
    public abstract class ViewModel        : Essensausgleich.Infra.AppObjekt,
        System.ComponentModel.INotifyPropertyChanged, INotifyCollectionChanged
    {
        #region WPF über Änderungen Informieren
        /// <summary>
        /// Wird ausgelöst, wenn sich der Inhalt
        /// einer Eigenschaft geändert hat
        /// </summary>
        public event PropertyChangedEventHandler?
            PropertyChanged = null!;
        /// <summary>
        /// Wird ausgelöst, wenn sich der Inhalt
        /// einer Obsverablecollegtion geändert hat
        /// </summary>
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        /// <summary>
        /// Löst das Ereignis PropertyChanged aus
        /// </summary>
        /// <param name="e">Ereginisdaten mit
        /// dem Namen der geänderten Eigenschaft</param>
        protected virtual void OnPropertyChanged(
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            var BehandlerKopie = this.PropertyChanged;
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
            this.OnPropertyChanged(new PropertyChangedEventArgs(nameEigenschaft));
            System.Diagnostics.Debug.WriteLine($"OnPropertyChanged ausgelöst bei:{nameEigenschaft}");
        }
        #endregion WPF über Änderungen Informieren
        /// <summary>
        /// Löst das Ereignis PropertyChanged aus
        /// </summary>
        /// <param name="e">Ereginisdaten mit
        /// dem Namen der geänderten Eigenschaft</param>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var BehandlerKopie = this.CollectionChanged;
            if (BehandlerKopie != null)
            {
                BehandlerKopie(this, e);
            }
            System.Diagnostics.Debug.WriteLine($"OnCollectionChanged ausgelöst bei:{e.ToString}");
        }

        
    }
}
