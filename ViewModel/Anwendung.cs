using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Anzeigen<T>() where T : System.Windows.Window, new()
        {


            var f = new T();
            Essensausgleich.App.Current.MainWindow = f;

            //View und Viewmodel verbinden
            f.DataContext = this;
            // Die Infrastruktur an 
            // das neue Objekt übergeben

            if (f is MainWindow mainWindow)
            {
                System.Diagnostics.Debug.WriteLine("f is MainWindow");

                mainWindow.Kontext = this.Kontext;

            }
            else if (f is settingsWindow settingsWindow)
            {
                settingsWindow.Kontext = this.Kontext;
                System.Diagnostics.Debug.WriteLine("f is settingswindow");
            }

            f.Show();

        }

        #endregion
    }
}
