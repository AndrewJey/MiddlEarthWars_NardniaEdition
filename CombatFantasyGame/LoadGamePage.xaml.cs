using CombatFantasyGame.Services;
using Microsoft.Extensions.Logging;

namespace CombatFantasyGame;

public partial class LoadGamePage : ContentPage
{
    private readonly LocalizationService _localizationService;
    private readonly SavedGameService _savedGameService;
    private readonly GameService _gameService;
    private readonly ILogger<LoadGamePage> _logger;

    private IReadOnlyList<SavedGameDisplay> _currentItems = Array.Empty<SavedGameDisplay>();

    public LoadGamePage(
        LocalizationService localizationService,
        SavedGameService savedGameService,
        GameService gameService,
        ILogger<LoadGamePage> logger)
    {
        InitializeComponent();
        _localizationService = localizationService;
        _savedGameService = savedGameService;
        _gameService = gameService;
        _logger = logger;
        ApplyLocalization();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadSavedGamesAsync();
    }

    private void ApplyLocalization()
    {
        Title = _localizationService["LoadGame_Title"];
        HeaderLabel.Text = _localizationService["LoadGame_Header"];
        EmptyLabel.Text = _localizationService["LoadGame_Empty"];
        CloseButton.Text = _localizationService["LoadGame_Close"];
    }

    private async Task LoadSavedGamesAsync()
    {
        try
        {
            var entries = await _savedGameService.GetSavedGamesAsync();
            _currentItems = entries
                .Select(entry => new SavedGameDisplay(
                    entry.Id,
                    entry.Name,
                    _localizationService.GetString("LoadGame_PlayersFormat", entry.Player1Name, entry.Player2Name),
                    _localizationService.GetString("LoadGame_TurnFormat", entry.TurnNumber),
                    _localizationService.GetString("LoadGame_CreatedAtFormat",
                        entry.CreatedAt.ToLocalTime().ToString("g", _localizationService.CurrentCulture))))
                .ToList();

            SavesCollectionView.ItemsSource = _currentItems;
            var hasItems = _currentItems.Count > 0;
            SavesCollectionView.IsVisible = hasItems;
            EmptyLabel.IsVisible = !hasItems;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to read saved games.");
            _currentItems = Array.Empty<SavedGameDisplay>();
            SavesCollectionView.ItemsSource = _currentItems;
            SavesCollectionView.IsVisible = false;
            EmptyLabel.IsVisible = true;
            await DisplayGenericErrorAsync();
        }
    }

    private async void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not SavedGameDisplay selected)
        {
            return;
        }

        SavesCollectionView.SelectedItem = null;

        try
        {
            var confirm = await DisplayAlert(
                _localizationService["LoadGame_Title"],
                _localizationService.GetString("LoadGame_Confirm", selected.Title),
                _localizationService["Common_Accept"],
                _localizationService["LoadGame_Cancel"]);

            if (!confirm)
            {
                return;
            }

            var loaded = await _gameService.LoadSavedSessionAsync(selected.Id);
            if (!loaded)
            {
                await DisplayAlert(
                    _localizationService["LoadGame_Title"],
                    _localizationService["LoadGame_Error"],
                    _localizationService["Common_Accept"]);
                return;
            }

            await Shell.Current.GoToAsync(nameof(BattlePage));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load saved game {Id}.", selected.Id);
            await DisplayGenericErrorAsync();
            return;
        }
    }

    private async void OnCloseClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(
            () => Shell.Current.GoToAsync(".."),
            "closing load game modal");
    }

    private async Task RunSafeAsync(Func<Task> action, string operation)
    {
        try
        {
            await action();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected failure while {Operation}.", operation);
            await DisplayGenericErrorAsync();
        }
    }

    private Task DisplayGenericErrorAsync()
    {
        return DisplayAlert(
            _localizationService["LoadGame_Title"],
            _localizationService["Common_GenericError"],
            _localizationService["Common_Accept"]);
    }

    private sealed record SavedGameDisplay(
        string Id,
        string Title,
        string Players,
        string TurnInfo,
        string CreatedAt);
}
