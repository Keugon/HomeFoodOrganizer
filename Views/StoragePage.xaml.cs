namespace Essensausgleich.Views;
using Essensausgleich.ViewModel;

public partial class StoragePage : ContentPage
{
    private readonly Anwendung _ViewmodelAnwendung;
    public StoragePage(Anwendung ViewmodelAnwendung)
    {
        InitializeComponent();
        BindingContext = _ViewmodelAnwendung = ViewmodelAnwendung;
        
    }

}