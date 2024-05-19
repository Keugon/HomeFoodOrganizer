using Essensausgleich.ViewModel;

namespace Essensausgleich.Views;

public partial class MainPage : ContentPage
{
    

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void Entry_Completed(object sender, EventArgs e)
    {

    }
}
