using CombatFantasyGame.Services;
using CombatFantasyGame.Services.Logging;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;

namespace CombatFantasyGame;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        var localizationService = new LocalizationService();
        LocalizationResources.Seed(localizationService);
        var logFileService = new LogFileService();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("EXL.TTF", "MiddearthBodyFont");
                fonts.AddFont("Starcraft Normal.ttf", "MiddearthButtonFont");
                fonts.AddFont("Exocet Heavy.ttf", "MiddearthWinnerFont");
                fonts.AddFont("LifeCraft_Font.ttf", "MiddearthTitleFont");
            });

        // Servicios de juego, base de datos y localización
        builder.Services.AddSingleton(localizationService);
        builder.Services.AddSingleton<ILogFileService>(logFileService);
        builder.Services.AddSingleton(AudioManager.Current);
        builder.Services.AddSingleton<GameDatabase>();
        builder.Services.AddSingleton<SavedGameService>();
        builder.Services.AddSingleton<BackgroundMusicService>();
        builder.Services.AddSingleton<GameService>();
        builder.Services.AddSingleton<AppDiagnostics>();

        // Páginas (para navegación con inyección de dependencias)
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<Func<AppShell>>(sp => () => sp.GetRequiredService<AppShell>());
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<PlayerSetupPage>();
        builder.Services.AddTransient<BattlePage>();
        builder.Services.AddTransient<SummaryPage>();
        builder.Services.AddTransient<StatsPage>();
        builder.Services.AddTransient<CreditsPage>();
        builder.Services.AddTransient<DocsPage>();
        builder.Services.AddTransient<LoadGamePage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Logging.AddProvider(new FileLoggerProvider(logFileService));

        return builder.Build();
    }
}
