using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ToDoList.Shared.Helpers;
using ToDoList.Shared.Services;

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
            Akavache.Registrations.Start(AppInfo.Current.Name);

            ServiceContainer.Register<INavigationService>(() => new NavigationService());
            ServiceContainer.Register<ICacheService>(() => new CacheService());
            ServiceContainer.Register<IDisplayPromptService>(() => new DisplayPromptService());

            return builder.Build();
        }
    }
}