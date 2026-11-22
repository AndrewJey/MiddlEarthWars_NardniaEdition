using CombatFantasyGame.Services;

namespace CombatFantasyGame;

public partial class SplashPage : ContentPage
{
    private readonly LocalizationService _localizationService;
    private readonly Func<Task> _showShellAsync;
    private bool _navigated;

    public SplashPage(LocalizationService localizationService, Func<Task> showShellAsync)
    {
        InitializeComponent();
        _localizationService = localizationService;
        _showShellAsync = showShellAsync;

        ApplyLocalization();
        _localizationService.LanguageChanged += OnLanguageChanged;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_navigated)
        {
            return;
        }

        _navigated = true;
        await Task.Delay(1200);
        await _showShellAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _localizationService.LanguageChanged -= OnLanguageChanged;
    }

    private void OnLanguageChanged(object? sender, EventArgs e) => ApplyLocalization();

    private void ApplyLocalization()
    {
        SplashTitleLabel.Text = _localizationService["Main_Title"];
        SplashLoadingLabel.Text = _localizationService["Splash_Loading"];
    }
}
