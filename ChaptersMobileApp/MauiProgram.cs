using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ChaptersMobileApp.ViewModels;
namespace ChaptersMobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.Services.AddTransient<HttpClient>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<RegistrationPage>();
            builder.Services.AddSingleton<RegistrationViewModel>();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
