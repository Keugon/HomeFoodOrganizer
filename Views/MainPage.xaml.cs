﻿using Essensausgleich.ViewModel;

namespace Essensausgleich.Views;

public partial class MainPage : ContentPage
{
    

    public MainPage(Anwendung thisDataContext)
    {
        InitializeComponent();
        BindingContext = thisDataContext;
    }

    private void Entry_Completed(object sender, EventArgs e)
    {

    }
}
