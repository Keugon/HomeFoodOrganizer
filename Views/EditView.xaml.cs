namespace Essensausgleich.Views;
using Essensausgleich.ViewModel;
/// <summary>
/// EditView
/// </summary>
public partial class EditView : ContentPage
{
    private readonly Anwendung _ViewmodelAnwendung;
    /// <summary>
    /// EditView
    /// </summary>
    /// <param name="viewmodelAnwendung"></param>
    public EditView(Anwendung viewmodelAnwendung)
    {
        InitializeComponent();
        BindingContext = _ViewmodelAnwendung = viewmodelAnwendung;

    }
}