using Essensausgleich.Controller;
using Essensausgleich.Data;
using Essensausgleich.Views;
using Essensausgleich.ViewModel;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace Essensausgleich
{
    public partial class App : Application
    {
        const int WindowWidth = 380;
        const int WindowHeight = 620;
        public App()
        {
            InitializeComponent();
            Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
            {
#if WINDOWS
            var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new SizeInt32(WindowWidth, WindowHeight));
#endif
            });

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
