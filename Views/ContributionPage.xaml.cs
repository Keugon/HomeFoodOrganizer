using Essensausgleich.ViewModel;

namespace Essensausgleich.Views;

public partial class ContributionPage : ContentPage
{
	public ContributionPage(Anwendung thisDataContext)
	{
		InitializeComponent();
        BindingContext = thisDataContext;
    }

//    private async void TouchBehavior_LongPressCompleted(object sender, CommunityToolkit.Maui.Core.LongPressCompletedEventArgs e)
//    {
//        await DisplayAlert("Question?", "Delet or Multiple", "delete", "Multiselect");
//    }
}