using CommunityToolkit.Maui;
using Essensausgleich.Data;
using Essensausgleich.ViewModel;
using Essensausgleich.Views;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
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

            MauiAppBuilder builder = MauiApp.CreateBuilder();
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

            //Polly
            //ResiliencePipeline pipeline = new ResiliencePipelineBuilder()
            //    .AddRetry(new RetryStrategyOptions()) // Add retry using the default options
            //    .AddTimeout(TimeSpan.FromSeconds(10)) // Add 10 seconds timeout
            //    .Build(); // Builds the resilience pipeline

            // Define a resilience pipeline with the name "my-pipeline"
            builder.Services.AddResiliencePipeline("BuddyPipeline", builder =>
            {
                builder
                    .AddRetry(new RetryStrategyOptions
                    {
                        MaxRetryAttempts = 2,
                        Delay = TimeSpan.FromSeconds(1),
                        OnRetry = static args =>
                        {
                            Console.WriteLine("OnRetry, Attempt: {0}", args.AttemptNumber);
                            // Event handlers can be asynchronous; here, we return an empty ValueTask.
                            return default;
                        }
                    })
                    .AddTimeout(TimeSpan.FromSeconds(10));
            });



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
