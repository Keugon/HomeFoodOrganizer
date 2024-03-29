using System;
using System.Collections.Generic;
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
        System.ComponentModel.INotifyPropertyChanged
    {
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
        }
        #endregion WPF über Änderungen Informieren
    }
}
