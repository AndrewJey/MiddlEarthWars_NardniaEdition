using CombatFantasyGame.Services;

namespace CombatFantasyGame;

public partial class AppShell : Shell
{
    private readonly LocalizationService _localizationService;

    public AppShell(LocalizationService localizationService)
    {
        InitializeComponent();
        _localizationService = localizationService;

        RegisterRoutes();
        ApplyLocalization();

        _localizationService.LanguageChanged += (_, _) => ApplyLocalization();
    }

    private void ApplyLocalization()
    {
        Title = string.Empty;
        AppTitleLabel.Text = _localizationService["App_Title"];
        MainShellContent.Title = _localizationService["Shell_Home"];
    }

    private static void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(PlayerSetupPage), typeof(PlayerSetupPage));
        Routing.RegisterRoute(nameof(BattlePage), typeof(BattlePage));
        Routing.RegisterRoute(nameof(SummaryPage), typeof(SummaryPage));
        Routing.RegisterRoute(nameof(StatsPage), typeof(StatsPage));
        Routing.RegisterRoute(nameof(CreditsPage), typeof(CreditsPage));
        Routing.RegisterRoute(nameof(DocsPage), typeof(DocsPage));
        Routing.RegisterRoute(nameof(LoadGamePage), typeof(LoadGamePage));
    }
}
