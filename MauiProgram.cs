using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ToDoList.Maui.Helpers;
using ToDoList.Maui.Services;

namespace ToDoList.Maui
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
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            ServiceContainer.Register<INavigationService>(() => new NavigationService());

            return builder.Build();
        }
    }
}