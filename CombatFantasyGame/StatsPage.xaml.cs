using CombatFantasyGame.Models;
using CombatFantasyGame.Services;
using Microsoft.Extensions.Logging;

namespace CombatFantasyGame;

public partial class StatsPage : ContentPage
{
    private readonly GameDatabase _database;
    private readonly LocalizationService _localizationService;
    private readonly ILogger<StatsPage> _logger;

    public StatsPage(GameDatabase database, LocalizationService localizationService, ILogger<StatsPage> logger)
    {
        InitializeComponent();
        _database = database;
        _localizationService = localizationService;
        _logger = logger;

        ApplyLocalization();
        _localizationService.LanguageChanged += async (_, _) =>
        {
            ApplyLocalization();
            await LoadDataAsync();
        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadDataAsync();
    }

    private void ApplyLocalization()
    {
        Title = _localizationService["Stats_Title"];
        StatsHeaderLabel.Text = _localizationService["Stats_Header"];
        MatchHistoryHeaderLabel.Text = _localizationService["Stats_HistoryHeader"];
    }

    private async Task LoadDataAsync()
    {
        try
        {
            var players = await _database.GetPlayerStatsAsync();
            PlayersCollectionView.ItemsSource = players
                .Select(ConvertPlayer)
                .ToList();

            var matches = await _database.GetMatchHistoryAsync();
            MatchesCollectionView.ItemsSource = matches
                .Select(ConvertMatch)
                .ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load statistics data.");
            PlayersCollectionView.ItemsSource = Array.Empty<PlayerStatsView>();
            MatchesCollectionView.ItemsSource = Array.Empty<MatchResultView>();
            await DisplayGenericErrorAsync();
        }
    }

    private PlayerStatsView ConvertPlayer(PlayerStats stats)
    {
        var race = ParseEnum<Race>(stats.LastRace);
        var weaponDisplay = GetWeaponOrElementDisplay(race, stats.LastWeaponOrElement);

        return new PlayerStatsView(
            stats.Name,
            _localizationService.GetString("Stats_RaceFormat",
                race is null ? _localizationService["Stats_UnknownValue"] : _localizationService.GetRaceName(race.Value)),
            _localizationService.GetString("Stats_WeaponFormat", weaponDisplay),
            _localizationService.GetString("Stats_WinsFormat", stats.Wins),
            _localizationService.GetString("Stats_LossesFormat", stats.Losses),
            _localizationService.GetString("Stats_DrawsFormat", stats.Draws));
    }

    private MatchResultView ConvertMatch(MatchResult match)
    {
        var playedAt = match.PlayedAt.ToLocalTime().ToString("f", _localizationService.CurrentCulture);
        var player1 = BuildMatchPlayerSummary(
            match.Player1Name,
            ParseEnum<Race>(match.Player1Race),
            match.Player1Weapon,
            _localizationService["Stats_Player1Label"]);
        var player2 = BuildMatchPlayerSummary(
            match.Player2Name,
            ParseEnum<Race>(match.Player2Race),
            match.Player2Weapon,
            _localizationService["Stats_Player2Label"]);

        var winnerText = match.IsDraw
            ? _localizationService["Summary_DrawLabel"]
            : match.WinnerName;

        return new MatchResultView(
            _localizationService.GetString("Stats_DateFormat", playedAt),
            player1,
            player2,
            _localizationService.GetString("Stats_WinnerFormat", winnerText));
    }

    private string BuildMatchPlayerSummary(
        string playerName,
        Race? race,
        string weaponOrElement,
        string playerLabel)
    {
        var raceName = race is null ? _localizationService["Stats_UnknownValue"] : _localizationService.GetRaceName(race.Value);
        var weaponName = GetWeaponOrElementDisplay(race, weaponOrElement);

        var detail = string.IsNullOrEmpty(weaponName) ? raceName : $"{raceName} ({weaponName})";
        var summary = _localizationService.GetString("Stats_PlayerSummaryFormat", playerName, detail);

        return _localizationService.GetString("Stats_PlayerEntryFormat", playerLabel, summary);
    }

    private string GetWeaponOrElementDisplay(Race? race, string value)
    {
        if (race is null || string.IsNullOrWhiteSpace(value))
        {
            return _localizationService["Stats_UnknownValue"];
        }

        return race switch
        {
            Race.Human when Enum.TryParse(value, out HumanWeapon human) =>
                _localizationService.GetHumanWeaponName(human),
            Race.Elf when Enum.TryParse(value, out ElfElement element) =>
                _localizationService.GetElfElementName(element),
            Race.Orc when Enum.TryParse(value, out OrcWeapon orc) =>
                _localizationService.GetOrcWeaponName(orc),
            Race.Beast when Enum.TryParse(value, out BeastWeapon beast) =>
                _localizationService.GetBeastWeaponName(beast),
            _ => _localizationService["Stats_UnknownValue"]
        };
    }

    private static TEnum? ParseEnum<TEnum>(string value) where TEnum : struct
    {
        if (Enum.TryParse(value, out TEnum parsed))
        {
            return parsed;
        }

        return null;
    }

    private sealed record PlayerStatsView(
        string Name,
        string RaceDisplay,
        string WeaponDisplay,
        string WinsDisplay,
        string LossesDisplay,
        string DrawsDisplay);

    private sealed record MatchResultView(
        string DateDisplay,
        string Player1Display,
        string Player2Display,
        string WinnerDisplay);

    private Task DisplayGenericErrorAsync()
    {
        return DisplayAlert(
            _localizationService["Stats_Title"],
            _localizationService["Common_GenericError"],
            _localizationService["Common_Accept"]);
    }
}
