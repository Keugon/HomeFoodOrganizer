using Essensausgleich.ViewModel;

namespace Essensausgleich.Views
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(Anwendung thisDataContext)
        {
            InitializeComponent();
            BindingContext = thisDataContext;
        }

       
    }

}
