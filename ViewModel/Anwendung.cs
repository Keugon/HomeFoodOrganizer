using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Essensausgleich.ViewModel
{
    /// <summary>
    /// blablablaAnwednung
    /// </summary>
    public class Anwendung : Essensausgleich.Infra.ViewModel
    {
        #region Hauptview
        /// <summary>
        /// Öffnet die Hauptoberfläche der Anwendung
        /// </summary>
        /// <typeparam name="T">Ein WPF Fenster das als 
        /// Hauptfenster der Anwendung benutzt werden soll</typeparam>       
        public void Anzeigen<T>() where T : System.Windows.Window, IAppObjekt, new()
        {


            var f = new T();
            Essensausgleich.App.Current.MainWindow = f;

            //View und Viewmodel verbinden
            //f.DataContext = this;
            //Attach Kontext to the new Window
            f.Kontext = this.Kontext;

            f.Show();



        }

        #endregion

       
        
        
    }
}
