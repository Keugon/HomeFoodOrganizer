namespace Essensausgleich.Views;
using Essensausgleich.ViewModel;

public partial class EditView : ContentPage
{
    private readonly Anwendung _ViewmodelAnwendung;

    public EditView(Anwendung viewmodelAnwendung)
    {
        InitializeComponent();
        BindingContext = _ViewmodelAnwendung = viewmodelAnwendung;

    }
}