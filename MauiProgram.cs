using CommunityToolkit.Maui;
using DRAXNET.Core.Models;
using Essensausgleich.Data;
using Essensausgleich.ViewModel;
using Essensausgleich.Views;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Essensausgleich
{
    /// <summary>
    /// MauiProgram
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// CreateMauiApp here it is to configure resources like fints and initiate Views/Pages
        /// </summary>
        /// <returns></returns>
        public static MauiApp CreateMauiApp()
        {

            var builder = MauiApp.CreateBuilder();
            builder
                .UseSkiaSharp(true)
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Segoe UI Symbol.ttf", "Segoe UI Symbol");
                });
            builder.Services.AddSingleton<Anwendung>();
            builder.Services.AddSingleton<DRAXNET.Core.Services.SqlController>();
            builder.Services.AddSingleton<DRAXNET.Core.Services.UserManagement>();
            builder.Services.AddSingleton<DataSharingController>();

            builder.Services.AddLogging();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<ContributionView>();
            builder.Services.AddSingleton<InvoiceViewPage>();
            builder.Services.AddSingleton<EditView>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

    }
}
