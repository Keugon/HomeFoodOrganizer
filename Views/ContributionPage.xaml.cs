using Essensausgleich.ViewModel;

namespace Essensausgleich.Views;

public partial class ContributionPage : ContentPage
{
	public ContributionPage(Anwendung thisDataContext)
	{
		InitializeComponent();
        BindingContext = thisDataContext;
    }
}