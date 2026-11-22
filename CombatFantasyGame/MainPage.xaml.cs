using CombatFantasyGame.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.ApplicationModel;

namespace CombatFantasyGame;

public partial class MainPage : ContentPage
{
    private readonly LocalizationService _localizationService;
    private readonly GameService _gameService;
    private readonly IReadOnlyList<LanguageOption> _languageOptions;
    private readonly ILogger<MainPage> _logger;
    private bool _languageUpdateInProgress;
    private bool _themeUpdateInProgress;

    public MainPage(GameService gameService, LocalizationService localizationService, ILogger<MainPage> logger)
    {
        InitializeComponent();
        _gameService = gameService;
        _localizationService = localizationService;
        _logger = logger;
        _languageOptions = _localizationService.SupportedLanguages;

        InitializeLanguagePicker();
        ApplyLocalization();

        _localizationService.LanguageChanged += OnLocalizationLanguageChanged;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await RunSafeAsync(UpdateContinueButtonAsync, "checking saved battles");
        UpdateThemeSwitchState();
    }

    private void InitializeLanguagePicker()
    {
        LanguagePicker.ItemsSource = _languageOptions.ToList();
        LanguagePicker.SelectedItem = _languageOptions
            .FirstOrDefault(opt => opt.Language == _localizationService.CurrentLanguage);
        LanguagePicker.SelectedIndexChanged += OnLanguagePickerChanged;
    }

    private void ApplyLocalization()
    {
        Title = _localizationService["App_Title"];
        GameTitleLabel.Text = _localizationService["Main_Title"];
        SubtitleLabel.Text = _localizationService["Main_Subtitle"];
        LanguagePromptLabel.Text = _localizationService["Main_LanguagePrompt"];
        LanguagePicker.Title = _localizationService["Main_LanguagePickerTitle"];
        ThemeToggleLabel.Text = _localizationService["Main_ThemeToggle"];
        NewGameButton.Text = _localizationService["Main_NewGame"];
        ContinueButton.Text = _localizationService["Main_Continue"];
        StatsButton.Text = _localizationService["Main_Stats"];
        CreditsButton.Text = _localizationService["Main_Credits"];
        DocsButton.Text = _localizationService["Main_Docs"];
    }

    private async Task UpdateContinueButtonAsync()
    {
        ContinueButton.IsVisible = await _gameService.HasAnySavedGamesAsync();
    }

    private void OnLocalizationLanguageChanged(object? sender, EventArgs e)
    {
        try
        {
            _languageUpdateInProgress = true;
            LanguagePicker.SelectedItem = _languageOptions
                .FirstOrDefault(opt => opt.Language == _localizationService.CurrentLanguage);
            _languageUpdateInProgress = false;

            ApplyLocalization();
        }
        catch (Exception ex)
        {
            _languageUpdateInProgress = false;
            _logger.LogError(ex, "Failed to refresh UI after language change.");
        }
    }

    private void OnLanguagePickerChanged(object? sender, EventArgs e)
    {
        if (_languageUpdateInProgress)
        {
            return;
        }

        try
        {
            if (LanguagePicker.SelectedItem is LanguageOption option)
            {
                _localizationService.SetLanguage(option.Language);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to switch language.");
        }
    }

    private async void OnNewGameClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(
            () => Shell.Current.GoToAsync(nameof(PlayerSetupPage)),
            "navigating to player setup");
    }

    private async void OnContinueClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(async () =>
        {
            if (!await _gameService.LoadMostRecentSessionAsync())
            {
                await DisplayAlert(
                    _localizationService["App_Title"],
                    _localizationService["Main_ContinueError"],
                    _localizationService["Common_Accept"]);
                await UpdateContinueButtonAsync();
                return;
            }

            await Shell.Current.GoToAsync(nameof(BattlePage));
        }, "continuing last battle");
    }

    private async void OnStatsClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(
            () => Shell.Current.GoToAsync(nameof(StatsPage)),
            "navigating to stats");
    }

    private async void OnCreditsClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(
            () => Shell.Current.GoToAsync(nameof(CreditsPage)),
            "navigating to credits");
    }

    private async void OnDocsClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(
            () => Shell.Current.GoToAsync(nameof(DocsPage)),
            "navigating to documentation");
    }

    private void UpdateThemeSwitchState()
    {
        if (Application.Current is null)
        {
            return;
        }

        _themeUpdateInProgress = true;
        ThemeSwitch.IsToggled = Application.Current.UserAppTheme != AppTheme.Light;
        _themeUpdateInProgress = false;
    }

    private void OnThemeToggled(object? sender, ToggledEventArgs e)
    {
        if (_themeUpdateInProgress || Application.Current is null)
        {
            return;
        }

        Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
    }

    private async Task RunSafeAsync(Func<Task> action, string operation)
    {
        try
        {
            await action();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while {Operation}.", operation);
            await DisplayAlert(
                _localizationService["App_Title"],
                _localizationService["Common_GenericError"],
                _localizationService["Common_Accept"]);
        }
    }
}
