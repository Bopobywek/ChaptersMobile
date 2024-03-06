using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ChaptersMobileApp.ViewModels;
using CommunityToolkit.Maui;
using ChaptersMobileApp.Views;
using ChaptersMobileApp.Services.Interfaces;
using ChaptersMobileApp.Services;
using ChaptersMobileApp.Extensions;
using ChaptersMobileApp.Settings;
using Microsoft.Extensions.Configuration;
using System.Reflection;
namespace ChaptersMobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiCommunityToolkit();
            builder.Services.AddTransient<HttpClient>();
            builder.Services.AddPages().AddViewModels();
            builder.Services.Configure<WebApiSettings>(_ => builder.Configuration.GetSection("WebApiSettings"));
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
