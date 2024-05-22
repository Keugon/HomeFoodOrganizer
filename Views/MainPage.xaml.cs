using Essensausgleich.ViewModel;

namespace Essensausgleich.Views;

public partial class MainPage : ContentPage
{
    

    public MainPage()
    {
        InitializeComponent();
        BindingContext = App.Current!.BindingContext;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        System.Diagnostics.Debug.WriteLine($"Hash BindingContext:{this.BindingContext.GetHashCode()}, Thistype:{this.GetType()} ThisHash {this.GetHashCode()}");
    }
    private void Entry_Completed(object sender, EventArgs e)
    {

    }
}
