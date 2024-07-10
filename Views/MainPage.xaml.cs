using Essensausgleich.ViewModel;

namespace Essensausgleich.Views;
/// <summary>
/// MainPage
/// </summary>
public partial class MainPage : ContentPage
{
    private readonly Anwendung _ViewmodelAnwendung;
    /// <summary>
    /// MainPage
    /// </summary>
    /// <param name="viewmodelAnwendung"></param>
    public MainPage(Anwendung viewmodelAnwendung)
    {
        InitializeComponent();
        BindingContext = _ViewmodelAnwendung = viewmodelAnwendung;
    }
}
