using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
namespace AplikacjaPogodowa
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
                    fonts.AddFont("JetBrainsMonoNerdFontMono-Regular.ttf", "JetBrainsMonoNerdRegular");
                    fonts.AddFont("JetBrainsMonoNerdFontMono-Semibold.ttf", "JetBrainsMonoNerdSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
