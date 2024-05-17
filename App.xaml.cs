using Essensausgleich.Controller;
using Essensausgleich.Data;
using Essensausgleich.Views;
using Essensausgleich.ViewModel;

namespace Essensausgleich
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

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
        protected override void OnStart()
        {

            base.OnStart();
            this.Context = new Essensausgleich.Infra.Infrastructur();

            //Das View Model Initialisieren
            ViewModelAnwendung = this.Context.Fabricate<ViewModel.Anwendung>();
            ViewModelAnwendung.Initialize();
           

            //Log.WriteLine("FilesystemManager Init");
        }
    }
}
