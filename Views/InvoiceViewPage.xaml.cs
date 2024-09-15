using Essensausgleich.ViewModel;

namespace Essensausgleich.Views;
/// <summary>
/// InvoiceViewPage
/// </summary>
public partial class InvoiceViewPage : ContentPage
{
    /// <summary>
    /// InvoiceViewPage
    /// </summary>
    public InvoiceViewPage(Anwendung viewmodel)
    {
        InitializeComponent();
        BindingContext = viewmodel;
    }
}