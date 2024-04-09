using Essensausgleich.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;
using Log = System.Diagnostics.Debug;

namespace Essensausgleich
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IAppObjekt
    {
        /// <summary>
        /// ViewModel
        /// </summary>
        public static Anwendung ViewModelAnwendung { get; set; } = null!;
        /// <summary>
        /// Stellt den Context für die APPobjekte bereit glaub ich
        /// </summary>
        public Essensausgleich.Infra.Infrastructur Context
        { get; set; } = null!;
        /// <summary>
        /// Overrides the Statup process and starts my infrastracture
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);
            this.Context = new Essensausgleich.Infra.Infrastructur();
            
            //Das View Model Initialisieren
             ViewModelAnwendung = this.Context.Fabricate<ViewModel.Anwendung>();
            ViewModelAnwendung.Initialize();
            //Die Hauptfenster View als Oberfläche benutzen
            ViewModelAnwendung.Anzeigen<MainWindow>();

            //Log.WriteLine("FilesystemManager Init");
        }
    }

}
