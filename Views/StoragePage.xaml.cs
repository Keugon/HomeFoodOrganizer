namespace Essensausgleich.Views;
using Essensausgleich.ViewModel;

public partial class StoragePage : ContentPage
{
	public StoragePage(Anwendung thisDataContext)
	{
		InitializeComponent();
        BindingContext = thisDataContext;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        
    }
}