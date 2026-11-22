using CombatFantasyGame.Models;
using CombatFantasyGame.Services;
using Microsoft.Extensions.Logging;

namespace CombatFantasyGame;

public partial class BattlePage : ContentPage
{
    private readonly GameService _gameService;
    private readonly LocalizationService _localizationService;
    private readonly ILogger<BattlePage> _logger;

    public BattlePage(GameService gameService, LocalizationService localizationService, ILogger<BattlePage> logger)
    {
        InitializeComponent();
        _gameService = gameService;
        _localizationService = localizationService;
        _logger = logger;

        ApplyLocalization();
        _localizationService.LanguageChanged += (_, _) =>
        {
            ApplyLocalization();
            UpdateUi();
        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateUi();
    }

    private void ApplyLocalization()
    {
        Title = _localizationService["Battle_Title"];
        AttackButton.Text = _localizationService["Battle_Attack"];
        HealButton.Text = _localizationService["Battle_Heal"];
        MoveForwardButton.Text = _localizationService["Battle_MoveForward"];
        MoveBackButton.Text = _localizationService["Battle_MoveBack"];
        SaveButton.Text = _localizationService["Battle_SaveButton"];
    }

    private async Task<bool> EnsureSessionAsync()
    {
        try
        {
            if (_gameService.CurrentSession is not null)
            {
                return true;
            }

            await DisplayAlert(
                _localizationService["Common_NoBattleTitle"],
                _localizationService["Common_NoBattleMessage"],
                _localizationService["Common_Accept"]);
            await Shell.Current.GoToAsync("//MainPage");
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to ensure session state.");
            await DisplayGenericErrorAsync();
            return false;
        }
    }

    private async void OnAttackClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(async () =>
        {
            if (!await EnsureSessionAsync())
            {
                return;
            }

            var message = _gameService.PerformAttack(out var damage, out var wasHit);
            LastActionLabel.Text = message;

            var resultType = _gameService.GetBattleResultType();
            if (resultType != BattleResultType.None)
            {
                await _gameService.SaveBattleResultAsync();
                await NavigateToSummary(resultType);
                return;
            }

            if (damage > 0 || wasHit)
            {
                _gameService.CurrentSession!.NextTurn();
            }

            UpdateUi();
        }, "processing attack");
    }

    private async void OnHealClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(async () =>
        {
            if (!await EnsureSessionAsync())
            {
                return;
            }

            var message = _gameService.PerformHeal(out _);
            LastActionLabel.Text = message;

            _gameService.CurrentSession!.NextTurn();
            UpdateUi();
        }, "processing heal");
    }

    private async void OnMoveForwardClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(async () =>
        {
            if (!await EnsureSessionAsync())
            {
                return;
            }

            var message = _gameService.MoveForward();
            LastActionLabel.Text = message;

            _gameService.CurrentSession!.NextTurn();
            UpdateUi();
        }, "processing move forward");
    }

    private async void OnMoveBackClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(async () =>
        {
            if (!await EnsureSessionAsync())
            {
                return;
            }

            var message = _gameService.MoveBack();
            LastActionLabel.Text = message;

            _gameService.CurrentSession!.NextTurn();
            UpdateUi();
        }, "processing move back");
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(async () =>
        {
            if (!await EnsureSessionAsync())
            {
                return;
            }

            var session = _gameService.CurrentSession!;
            var defaultName = _localizationService.GetString(
                "Battle_SaveDefaultName",
                session.Player1.PlayerName,
                session.Player2.PlayerName,
                session.TurnNumber);

            var saveName = await DisplayPromptAsync(
                _localizationService["Battle_SavePromptTitle"],
                _localizationService["Battle_SavePromptMessage"],
                _localizationService["Battle_SavePromptAccept"],
                _localizationService["Battle_SavePromptCancel"],
                initialValue: defaultName,
                maxLength: 60,
                keyboard: Keyboard.Text);

            if (string.IsNullOrWhiteSpace(saveName))
            {
                return;
            }

            var success = await _gameService.SaveCurrentSessionAsync(saveName);
            var messageKey = success ? "Battle_SaveSuccess" : "Battle_SaveError";
            await DisplayAlert(
                _localizationService["Battle_Title"],
                _localizationService[messageKey],
                _localizationService["Common_Accept"]);
        }, "saving session");
    }

    private void UpdateUi()
    {
        try
        {
            var session = _gameService.CurrentSession;
            if (session is null)
            {
                TurnLabel.Text = _localizationService["Battle_NoSession"];
                DistanceLabel.Text = string.Empty;
                Player1HealthLabel.Text = string.Empty;
                Player2HealthLabel.Text = string.Empty;
                CurrentPlayerInfoLabel.Text = string.Empty;
                CurrentPlayerImage.Source = "dotnet_bot.png";
                return;
            }

            TurnLabel.Text = _localizationService.GetString(
                "Battle_TurnFormat",
                session.TurnNumber,
                session.CurrentPlayer.PlayerName);

            DistanceLabel.Text = _localizationService.GetString(
                "Battle_DistanceFormat",
                session.Distance);

            Player1HealthLabel.Text = _localizationService.GetString(
                "Battle_HealthFormat",
                session.Player1.PlayerName,
                session.Player1.CurrentHealth,
                session.Player1.MaxHealth);

            Player2HealthLabel.Text = _localizationService.GetString(
                "Battle_HealthFormat",
                session.Player2.PlayerName,
                session.Player2.CurrentHealth,
                session.Player2.MaxHealth);

            CurrentPlayerInfoLabel.Text = _localizationService.GetString(
                "Battle_CurrentPlayerInfoFormat",
                session.CurrentPlayer.PlayerName,
                DescribePlayer(session.CurrentPlayer));

            CurrentPlayerImage.Source = GetImageForPlayer(session.CurrentPlayer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update battle UI.");
            LastActionLabel.Text = _localizationService["Common_GenericError"];
        }
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

        if (string.IsNullOrEmpty(detail))
        {
            return race;
        }

        return $"{race} ({detail})";
    }

    private string GetImageForPlayer(PlayerCharacter player)
    {
        return player.Race switch
        {
            Race.Human when player.HumanWeapon == HumanWeapon.Shotgun => "human_shotgun.png",
            Race.Human when player.HumanWeapon == HumanWeapon.SniperRifle => "human_sniper.png",
            Race.Elf when player.Element == ElfElement.Fire => "elf_fire.png",
            Race.Elf when player.Element == ElfElement.Earth => "elf_earth.png",
            Race.Elf when player.Element == ElfElement.Air => "elf_air.png",
            Race.Elf when player.Element == ElfElement.Water => "elf_water.png",
            Race.Orc when player.OrcWeapon == OrcWeapon.Axe => "orc_axe.png",
            Race.Orc when player.OrcWeapon == OrcWeapon.Hammer => "orc_hammer.png",
            Race.Beast when player.BeastWeapon == BeastWeapon.Fists => "beast_fists.png",
            Race.Beast when player.BeastWeapon == BeastWeapon.Sword => "beast_sword.png",
            _ => "dotnet_bot.png"
        };
    }

    private async Task NavigateToSummary(BattleResultType resultType)
    {
        var session = _gameService.CurrentSession;
        if (session is null)
        {
            return;
        }

        var winnerName = resultType switch
        {
            BattleResultType.Player1Wins => session.Player1.PlayerName,
            BattleResultType.Player2Wins => session.Player2.PlayerName,
            BattleResultType.Draw => _localizationService["Summary_DrawLabel"],
            _ => string.Empty
        };

        await Shell.Current.GoToAsync(nameof(SummaryPage), new Dictionary<string, object>
        {
            { "WinnerName", winnerName },
            { "ResultType", resultType.ToString() }
        });
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
            await DisplayGenericErrorAsync();
        }
    }

    private Task DisplayGenericErrorAsync()
    {
        return DisplayAlert(
            _localizationService["Battle_Title"],
            _localizationService["Common_GenericError"],
            _localizationService["Common_Accept"]);
    }
}
