using System.Security.Cryptography.X509Certificates;

namespace Essensausgleich
{
    /// <summary>
    /// Entrypoint if the Application
    /// </summary>
    public class Program : System.Windows.Application
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            FilesSystemManager.InitializeXMLFileSystem();
            Essensausgleich.Program app = new Essensausgleich.Program();
            app.Run( new MainWindow());
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new MainWindow());
            
            

        }

    }

}