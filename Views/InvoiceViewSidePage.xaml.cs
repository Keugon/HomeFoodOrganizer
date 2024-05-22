using Essensausgleich.ViewModel;
namespace Essensausgleich.Views;

public partial class InvoiceViewSidePage : ContentPage
{
    private readonly Anwendung _ViewmodelAnwendung;
    public InvoiceViewSidePage(Anwendung ViewmodelAnwendung)
	{
		InitializeComponent();
		BindingContext = _ViewmodelAnwendung = ViewmodelAnwendung;
	}
}