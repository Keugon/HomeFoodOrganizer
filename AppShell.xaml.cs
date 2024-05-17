using Essensausgleich.Views;

namespace Essensausgleich
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ContributionPage), typeof(ContributionPage));
        }
        
    }
}
