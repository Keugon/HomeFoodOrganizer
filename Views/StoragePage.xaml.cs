namespace Essensausgleich.Views;
using Essensausgleich.ViewModel;

public partial class StoragePage : ContentPage
{
	public StoragePage()
	{
		InitializeComponent();
		BindingContext = App.Current!.BindingContext;
    }
   
}