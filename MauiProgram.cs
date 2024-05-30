using CommunityToolkit.Maui;
using Essensausgleich.ViewModel;
using Essensausgleich.Views;
using Microsoft.Extensions.Logging;

namespace Essensausgleich
{
    public static class MauiProgram
    {

        public static MauiApp CreateMauiApp()
        {

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Segoe UI Symbol.ttf", "Segoe UI Symbol");
                });
            // builder.Services.AddSingleton<Anwendung>();
            builder.Services.AddSingleton<Infra.Infrastructur>();
            builder.Services.AddSingleton(provider =>
            {
                var context = provider.GetRequiredService<Infra.Infrastructur>();
                ViewModel.Anwendung Anwendung = context.Fabricate<ViewModel.Anwendung>();
                Anwendung.Initialize();
                return Anwendung;
            });
            

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<StoragePage>();
            builder.Services.AddTransient<ContributionPage>();
            builder.Services.AddTransient<InvoiceViewSidePage>();
            builder.Services.AddSingleton<InvoiceViewPage>();
            builder.Services.AddSingleton<EditView>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

    }
}
