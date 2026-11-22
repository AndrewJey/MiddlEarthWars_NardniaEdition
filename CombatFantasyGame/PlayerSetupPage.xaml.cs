using CombatFantasyGame.Models;
using CombatFantasyGame.Services;
using Microsoft.Extensions.Logging;
using System.Text;

namespace CombatFantasyGame;

public partial class PlayerSetupPage : ContentPage
{
    private readonly GameService _gameService;
    private readonly LocalizationService _localizationService;
    private readonly PlayerSetupState _player1State = new();
    private readonly PlayerSetupState _player2State = new();
    private readonly ILogger<PlayerSetupPage> _logger;

    private IReadOnlyList<SelectionOption<Race>> _raceOptions = Array.Empty<SelectionOption<Race>>();
    private IReadOnlyList<SelectionOption<ElfElement>> _elementOptions = Array.Empty<SelectionOption<ElfElement>>();
    private IReadOnlyList<SelectionOption<HumanWeapon>> _humanWeaponOptions = Array.Empty<SelectionOption<HumanWeapon>>();
    private IReadOnlyList<SelectionOption<OrcWeapon>> _orcWeaponOptions = Array.Empty<SelectionOption<OrcWeapon>>();
    private IReadOnlyList<SelectionOption<BeastWeapon>> _beastWeaponOptions = Array.Empty<SelectionOption<BeastWeapon>>();

    public PlayerSetupPage(GameService gameService, LocalizationService localizationService, ILogger<PlayerSetupPage> logger)
    {
        InitializeComponent();
        _gameService = gameService;
        _localizationService = localizationService;
        _logger = logger;

        ApplyLocalization();
        RefreshPickerData();

        _localizationService.LanguageChanged += OnLanguageChanged;
    }

    private void OnLanguageChanged(object? sender, EventArgs e)
    {
        try
        {
            ApplyLocalization();
            RefreshPickerData();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to refresh localization in player setup.");
        }
    }

    private void ApplyLocalization()
    {
        Title = _localizationService["Setup_Title"];
        SetupHeaderLabel.Text = _localizationService["Setup_Header"];
        Player1TitleLabel.Text = _localizationService["Setup_Player1Title"];
        Player2TitleLabel.Text = _localizationService["Setup_Player2Title"];
        Player1NameEntry.Placeholder = _localizationService["Setup_Player1Placeholder"];
        Player2NameEntry.Placeholder = _localizationService["Setup_Player2Placeholder"];
        Player1RacePicker.Title = _localizationService["Setup_RacePickerTitle"];
        Player2RacePicker.Title = _localizationService["Setup_RacePickerTitle"];
        Player1ElementPicker.Title = _localizationService["Setup_ElementPickerTitle"];
        Player2ElementPicker.Title = _localizationService["Setup_ElementPickerTitle"];
        Player1WeaponPicker.Title = _localizationService["Setup_WeaponPickerTitle"];
        Player2WeaponPicker.Title = _localizationService["Setup_WeaponPickerTitle"];
        StartBattleButton.Text = _localizationService["Setup_StartButton"];
        LoadGameButton.Text = _localizationService["Setup_LoadButton"];
    }

    private async void OnLoadGameClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(
            () => Shell.Current.GoToAsync(nameof(LoadGamePage)),
            "opening load game modal");
    }

    private void RefreshPickerData()
    {
        _raceOptions = new[]
        {
            Option(Race.Human, "Race_Human"),
            Option(Race.Elf, "Race_Elf"),
            Option(Race.Orc, "Race_Orc"),
            Option(Race.Beast, "Race_Beast")
        };

        _elementOptions = new[]
        {
            Option(ElfElement.Fire, "Element_Fire"),
            Option(ElfElement.Earth, "Element_Earth"),
            Option(ElfElement.Air, "Element_Air"),
            Option(ElfElement.Water, "Element_Water")
        };

        _humanWeaponOptions = new[]
        {
            Option(HumanWeapon.Shotgun, "Weapon_Shotgun"),
            Option(HumanWeapon.SniperRifle, "Weapon_SniperRifle")
        };

        _orcWeaponOptions = new[]
        {
            Option(OrcWeapon.Axe, "Weapon_Axe"),
            Option(OrcWeapon.Hammer, "Weapon_Hammer")
        };

        _beastWeaponOptions = new[]
        {
            Option(BeastWeapon.Fists, "Weapon_Fists"),
            Option(BeastWeapon.Sword, "Weapon_Sword")
        };

        SetPickerSource(Player1RacePicker, _raceOptions, _player1State.Race);
        SetPickerSource(Player2RacePicker, _raceOptions, _player2State.Race);

        HandleRaceChanged(_player1State, Player1RacePicker, Player1ElementPicker, Player1WeaponPicker);
        HandleRaceChanged(_player2State, Player2RacePicker, Player2ElementPicker, Player2WeaponPicker);
    }

    private SelectionOption<T> Option<T>(T value, string key)
    {
        return new SelectionOption<T>(value, _localizationService[key]);
    }

    private static void SetPickerSource<T>(
        Picker picker,
        IReadOnlyList<SelectionOption<T>> options,
        T? selectedValue) where T : struct
    {
        var items = options.ToList();
        picker.ItemsSource = items;

        if (selectedValue is null)
        {
            picker.SelectedItem = null;
            return;
        }

        var match = items.FirstOrDefault(opt => EqualityComparer<T>.Default.Equals(opt.Value, selectedValue.Value));
        picker.SelectedItem = match;
    }

    private void OnPlayer1RaceChanged(object? sender, EventArgs e)
    {
        HandleRaceChanged(_player1State, Player1RacePicker, Player1ElementPicker, Player1WeaponPicker);
    }

    private void OnPlayer2RaceChanged(object? sender, EventArgs e)
    {
        HandleRaceChanged(_player2State, Player2RacePicker, Player2ElementPicker, Player2WeaponPicker);
    }

    private void HandleRaceChanged(
        PlayerSetupState state,
        Picker racePicker,
        Picker elementPicker,
        Picker weaponPicker)
    {
        var selected = racePicker.SelectedItem as SelectionOption<Race>;
        var newRace = selected?.Value;

        if (state.Race != newRace)
        {
            state.Race = newRace;
            state.Element = null;
            state.HumanWeapon = null;
            state.OrcWeapon = null;
            state.BeastWeapon = null;
            weaponPicker.SelectedItem = null;
            elementPicker.SelectedItem = null;
        }

        var isElf = state.Race == Race.Elf;
        elementPicker.IsVisible = isElf;
        weaponPicker.IsVisible = !isElf && state.Race is not null;

        if (isElf)
        {
            SetPickerSource(elementPicker, _elementOptions, state.Element);
        }
        else
        {
            switch (state.Race)
            {
                case Race.Human:
                    SetPickerSource(weaponPicker, _humanWeaponOptions, state.HumanWeapon);
                    break;
                case Race.Orc:
                    SetPickerSource(weaponPicker, _orcWeaponOptions, state.OrcWeapon);
                    break;
                case Race.Beast:
                    SetPickerSource(weaponPicker, _beastWeaponOptions, state.BeastWeapon);
                    break;
                default:
                    weaponPicker.ItemsSource = null;
                    break;
            }
        }
    }

    private void OnPlayer1ElementChanged(object? sender, EventArgs e)
    {
        HandleElementChanged(_player1State, Player1ElementPicker);
    }

    private void OnPlayer2ElementChanged(object? sender, EventArgs e)
    {
        HandleElementChanged(_player2State, Player2ElementPicker);
    }

    private static void HandleElementChanged(PlayerSetupState state, Picker elementPicker)
    {
        if (elementPicker.SelectedItem is SelectionOption<ElfElement> option)
        {
            state.Element = option.Value;
        }
    }

    private void OnPlayer1WeaponChanged(object? sender, EventArgs e)
    {
        HandleWeaponChanged(_player1State, Player1WeaponPicker);
    }

    private void OnPlayer2WeaponChanged(object? sender, EventArgs e)
    {
        HandleWeaponChanged(_player2State, Player2WeaponPicker);
    }

    private static void HandleWeaponChanged(PlayerSetupState state, Picker weaponPicker)
    {
        if (state.Race is null)
        {
            return;
        }

        switch (state.Race)
        {
            case Race.Human:
                state.HumanWeapon = (weaponPicker.SelectedItem as SelectionOption<HumanWeapon>)?.Value;
                break;
            case Race.Orc:
                state.OrcWeapon = (weaponPicker.SelectedItem as SelectionOption<OrcWeapon>)?.Value;
                break;
            case Race.Beast:
                state.BeastWeapon = (weaponPicker.SelectedItem as SelectionOption<BeastWeapon>)?.Value;
                break;
        }
    }

    private async void OnStartBattleClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(async () =>
        {
            var player1 = BuildPlayerFromState(Player1NameEntry.Text, _player1State);
            var player2 = BuildPlayerFromState(Player2NameEntry.Text, _player2State);

            if (player1 is null || player2 is null)
            {
                await DisplayAlert(
                    _localizationService["Common_MissingDataTitle"],
                    _localizationService["Setup_IncompleteMessage"],
                    _localizationService["Common_Accept"]);
                return;
            }

            _gameService.StartNewSession(player1, player2);
            await Shell.Current.GoToAsync(nameof(BattlePage));
        }, "starting new battle");
    }

    private PlayerCharacter? BuildPlayerFromState(string? name, PlayerSetupState state)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(name) || state.Race is null)
            {
                return null;
            }

            var normalizedName = name.Normalize(NormalizationForm.FormC).Trim();
            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                return null;
            }

            var character = new PlayerCharacter
            {
                PlayerName = normalizedName,
                Race = state.Race.Value,
                MaxHealth = 100
            };

            if (character.Race == Race.Elf)
            {
                if (state.Element is null)
                {
                    return null;
                }

                character.Element = state.Element.Value;
                if (character.Element == ElfElement.Water)
                {
                    character.MaxHealth = 115;
                }
            }
            else
            {
                switch (character.Race)
                {
                    case Race.Human when state.HumanWeapon is not null:
                        character.HumanWeapon = state.HumanWeapon.Value;
                        break;
                    case Race.Orc when state.OrcWeapon is not null:
                        character.OrcWeapon = state.OrcWeapon.Value;
                        break;
                    case Race.Beast when state.BeastWeapon is not null:
                        character.BeastWeapon = state.BeastWeapon.Value;
                        break;
                    default:
                        return null;
                }
            }

            character.CurrentHealth = character.MaxHealth;
            return character;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to build player data.");
            return null;
        }
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
                _localizationService["Setup_Title"],
                _localizationService["Common_GenericError"],
                _localizationService["Common_Accept"]);
        }
    }

    private sealed class PlayerSetupState
    {
        public Race? Race { get; set; }
        public ElfElement? Element { get; set; }
        public HumanWeapon? HumanWeapon { get; set; }
        public OrcWeapon? OrcWeapon { get; set; }
        public BeastWeapon? BeastWeapon { get; set; }
    }
}
