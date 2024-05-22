using Essensausgleich.ViewModel;

namespace Essensausgleich.Views;

public partial class MainPage : ContentPage
{
    private readonly Anwendung _ViewmodelAnwendung;

    public MainPage(Anwendung viewmodelAnwendung)
    {
        InitializeComponent();
        BindingContext = _ViewmodelAnwendung = viewmodelAnwendung;
        
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //System.Diagnostics.Debug.WriteLine($"Hash BindingContext:{this.BindingContext.GetHashCode()}, Thistype:{this.GetType()} ThisHash {this.GetHashCode()}");
    }
    private void Entry_Completed(object sender, EventArgs e)
    {

    }
}
