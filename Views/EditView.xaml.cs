namespace Essensausgleich.Views;
using Essensausgleich.ViewModel;
/// <summary>
/// EditView
/// </summary>
public partial class EditView : ContentPage
{
    /// <summary>
    /// EditView
    /// </summary>
    /// <param name="viewmodelAnwendung"></param>
    public EditView(Anwendung viewmodelAnwendung)
    {
        InitializeComponent();
        BindingContext = viewmodelAnwendung;

    }
}