using CombatFantasyGame.Models;
using CombatFantasyGame.Services;
using Microsoft.Extensions.Logging;

namespace CombatFantasyGame;

public partial class SummaryPage : ContentPage, IQueryAttributable
{
    private readonly GameService _gameService;
    private readonly LocalizationService _localizationService;
    private readonly ILogger<SummaryPage> _logger;

    public string WinnerName { get; set; } = string.Empty;
    public string ResultType { get; set; } = string.Empty;

    public SummaryPage(GameService gameService, LocalizationService localizationService, ILogger<SummaryPage> logger)
    {
        InitializeComponent();
        _gameService = gameService;
        _localizationService = localizationService;
        _logger = logger;

        ApplyLocalization();
        _localizationService.LanguageChanged += (_, _) =>
        {
            ApplyLocalization();
            UpdateSummary();
        };
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        try
        {
            if (query.TryGetValue("WinnerName", out var winner))
            {
                WinnerName = winner?.ToString() ?? string.Empty;
            }

            if (query.TryGetValue("ResultType", out var result))
            {
                ResultType = result?.ToString() ?? string.Empty;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to apply query attributes.");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateSummary();
    }

    private void ApplyLocalization()
    {
        Title = _localizationService["Summary_Title"];
        BackToMenuButton.Text = _localizationService["Summary_BackButton"];
        ViewStatsButton.Text = _localizationService["Summary_ViewStatsButton"];
    }

    private void UpdateSummary()
    {
        try
        {
            var session = _gameService.CurrentSession;
            if (session is null)
            {
                TitleLabel.Text = _localizationService["Summary_NoData"];
                Player1SummaryLabel.Text = string.Empty;
                Player2SummaryLabel.Text = string.Empty;
                return;
            }

            TitleLabel.Text = ResultType switch
            {
                nameof(BattleResultType.Player1Wins) => _localizationService.GetString("Summary_WinnerFormat", WinnerName),
                nameof(BattleResultType.Player2Wins) => _localizationService.GetString("Summary_WinnerFormat", WinnerName),
                nameof(BattleResultType.Draw) => _localizationService["Summary_DrawTitle"],
                _ => _localizationService["Summary_DefaultTitle"]
            };

            Player1SummaryLabel.Text = FormatPlayerLine(session.Player1);
            Player2SummaryLabel.Text = FormatPlayerLine(session.Player2);

            var resultStyle = ResultType switch
            {
                nameof(BattleResultType.Player1Wins) => "ResultWinStyle",
                nameof(BattleResultType.Player2Wins) => "ResultWinStyle",
                nameof(BattleResultType.Draw) => "ResultDrawStyle",
                _ => "ResultNeutralStyle"
            };

            if (Resources.TryGetValue(resultStyle, out var styleObj) && styleObj is Style style)
            {
                TitleLabel.Style = style;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update summary UI.");
            TitleLabel.Text = _localizationService["Common_GenericError"];
        }
    }

    private string FormatPlayerLine(PlayerCharacter player)
    {
        var detail = DescribePlayer(player);
        return _localizationService.GetString(
            "Summary_PlayerLineFormat",
            player.PlayerName,
            detail,
            player.CurrentHealth,
            player.MaxHealth);
    }

    private string DescribePlayer(PlayerCharacter player)
    {
        var race = _localizationService.GetRaceName(player.Race);
        var detail = player.Race switch
        {
            Race.Human when player.HumanWeapon.HasValue => _localizationService.GetHumanWeaponName(player.HumanWeapon.Value),
            Race.Elf => _localizationService.GetElfElementName(player.Element),
            Race.Orc when player.OrcWeapon.HasValue => _localizationService.GetOrcWeaponName(player.OrcWeapon.Value),
            Race.Beast when player.BeastWeapon.HasValue => _localizationService.GetBeastWeaponName(player.BeastWeapon.Value),
            _ => string.Empty
        };

        return string.IsNullOrEmpty(detail) ? race : $"{race} ({detail})";
    }

    private async void OnBackToMenuClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(
            () => Shell.Current.GoToAsync("//MainPage"),
            "returning to main page");
    }

    private async void OnViewStatsClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(
            () => Shell.Current.GoToAsync(nameof(StatsPage)),
            "opening stats");
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
                _localizationService["Summary_Title"],
                _localizationService["Common_GenericError"],
                _localizationService["Common_Accept"]);
        }
    }
}
