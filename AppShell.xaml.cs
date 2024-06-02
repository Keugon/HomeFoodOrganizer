using Essensausgleich.Views;

namespace Essensausgleich
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            

            //Routing.RegisterRoute($"{nameof(MainPage)}", typeof(MainPage));
            Routing.RegisterRoute($"{nameof(MainPage)}/{nameof(InvoiceViewPage)}", typeof(InvoiceViewPage));
            Routing.RegisterRoute($"{nameof(MainPage)}/{nameof(InvoiceViewPage)}/{nameof(EditView)}", typeof(EditView));
            Routing.RegisterRoute($"{nameof(MainPage)}/{nameof(InvoiceViewPage)}/{nameof(EditView)}/{nameof(ContributionView)}", typeof(ContributionView));

            //Routing.RegisterRoute(nameof(StoragePage), typeof(StoragePage));
            //Routing.RegisterRoute(nameof(InvoiceViewSidePage), typeof(InvoiceViewSidePage));
            Items.Add(new ShellContent
            {
                ContentTemplate = new DataTemplate(typeof(MainPage)),
                Title = "Main",
                Route = "MainPage"
            });
            InitializeComponent();
        }

    }
}
