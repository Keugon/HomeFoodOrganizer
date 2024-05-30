using Essensausgleich.Views;

namespace Essensausgleich
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ContributionPage), typeof(ContributionPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(StoragePage), typeof(StoragePage));
            Routing.RegisterRoute(nameof(InvoiceViewSidePage), typeof(InvoiceViewSidePage));
            Routing.RegisterRoute(nameof(InvoiceViewPage), typeof(InvoiceViewPage));
        }
        
    }
}
