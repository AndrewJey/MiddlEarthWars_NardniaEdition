using CombatFantasyGame.Models;
using Microsoft.Extensions.Logging;

namespace CombatFantasyGame.Services;

public class GameService
{
    private readonly GameDatabase _database;
    private readonly LocalizationService _localizationService;
    private readonly SavedGameService _savedGameService;
    private readonly ILogger<GameService> _logger;
    private readonly Random _random = new();

    public GameSession? CurrentSession { get; private set; }

    public GameService(
        GameDatabase database,
        LocalizationService localizationService,
        SavedGameService savedGameService,
        ILogger<GameService> logger)
    {
        _database = database;
        _localizationService = localizationService;
        _savedGameService = savedGameService;
        _logger = logger;
    }

    public GameSession StartNewSession(PlayerCharacter player1, PlayerCharacter player2)
    {
        CurrentSession = new GameSession(player1, player2);
        return CurrentSession;
    }

    public BattleResultType GetBattleResultType()
    {
        if (CurrentSession is null)
        {
            return BattleResultType.None;
        }

        var p1Dead = CurrentSession.Player1.IsDead;
        var p2Dead = CurrentSession.Player2.IsDead;

        if (p1Dead && p2Dead)
        {
            return BattleResultType.Draw;
        }

        if (p1Dead)
        {
            return BattleResultType.Player2Wins;
        }

        if (p2Dead)
        {
            return BattleResultType.Player1Wins;
        }

        return BattleResultType.None;
    }

    public async Task SaveBattleResultAsync()
    {
        if (CurrentSession is null)
        {
            return;
        }

        var resultType = GetBattleResultType();
        if (resultType == BattleResultType.None)
        {
            return;
        }

        try
        {
            await _database.UpdateStatsForMatchAsync(
                CurrentSession.Player1,
                CurrentSession.Player2,
                resultType);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Failed to persist battle result for {Player1} vs {Player2}.",
                CurrentSession.Player1.PlayerName,
                CurrentSession.Player2.PlayerName);
        }
    }

    public string PerformAttack(out int damageDealt, out bool wasHit)
    {
        if (CurrentSession is null)
        {
            damageDealt = 0;
            wasHit = false;
            return _localizationService["Common_NoSession"];
        }

        var attacker = CurrentSession.CurrentPlayer;
        var defender = CurrentSession.OtherPlayer;

        // Verificar reglas de distancia
        if (CurrentSession.Distance > 0)
        {
            var isSniper = attacker.Race == Race.Human && attacker.HumanWeapon == HumanWeapon.SniperRifle;
            var isAirMage = attacker.Race == Race.Elf && attacker.Element == ElfElement.Air;

            if (!isSniper && !isAirMage)
            {
                damageDealt = 0;
                wasHit = false;
                return _localizationService["Battle_AttackOutOfRange"];
            }
        }

        // Precisión base
        var baseHitChance = 0.85;

        // Tierra tiene mejor precisión
        if (attacker.Race == Race.Elf && attacker.Element == ElfElement.Earth)
        {
            baseHitChance = 0.95;
        }

        // Magos de aire tienen posibilidad de evasión cuando son defensores
        if (defender.Race == Race.Elf && defender.Element == ElfElement.Air)
        {
            // El defensor esquiva una parte de los ataques
            baseHitChance -= 0.15;
        }

        wasHit = _random.NextDouble() <= baseHitChance;
        if (!wasHit)
        {
            damageDealt = 0;
            return _localizationService.GetString("Battle_AttackMissed", attacker.PlayerName);
        }

        damageDealt = CalculateDamage(attacker, defender, CurrentSession.Distance);
        defender.CurrentHealth -= damageDealt;

        var resultMessage = _localizationService.GetString("Battle_AttackDamage", attacker.PlayerName, damageDealt);

        // Efectos secundarios como sangrado o daño propio
        if (attacker.Race == Race.Orc && attacker.OrcWeapon == OrcWeapon.Axe)
        {
            defender.BleedTurnsRemaining = 2;
            defender.BleedDamagePerTurn = 3;
            resultMessage += " " + _localizationService["Battle_BleedApplied"];
        }

        if (attacker.Race == Race.Beast && attacker.BeastWeapon == BeastWeapon.Fists)
        {
            attacker.CurrentHealth -= 10;
            resultMessage += " " + _localizationService.GetString("Battle_BeastSelfDamage", attacker.PlayerName);
        }

        return resultMessage;
    }

    private int CalculateDamage(PlayerCharacter attacker, PlayerCharacter defender, int distance)
    {
        switch (attacker.Race)
        {
            case Race.Human:
                return CalculateHumanDamage(attacker, distance);
            case Race.Elf:
                return CalculateElfDamage(attacker, distance);
            case Race.Orc:
                return CalculateOrcDamage(attacker);
            case Race.Beast:
                return CalculateBeastDamage(attacker);
            default:
                return 0;
        }
    }

    private int CalculateHumanDamage(PlayerCharacter attacker, int distance)
    {
        var baseDamage = _random.Next(1, 6); // 1-5

        if (attacker.HumanWeapon == HumanWeapon.Shotgun)
        {
            // Escopeta: daño base con pequeño porcentaje extra (20%)
            var extra = (int)Math.Round(baseDamage * 0.2);
            return baseDamage + extra;
        }

        if (attacker.HumanWeapon == HumanWeapon.SniperRifle)
        {
            if (distance > 0)
            {
                // A distancia el francotirador hace mucho más daño
                return _random.Next(10, 21); // 10-20
            }

            return baseDamage;
        }

        return baseDamage;
    }

    private int CalculateElfDamage(PlayerCharacter attacker, int distance)
    {
        var baseDamage = _random.Next(1, 6); // 1-5

        switch (attacker.Element)
        {
            case ElfElement.Fire:
                // Fuego: más daño porcentual (+40%)
                return (int)Math.Round(baseDamage * 1.4);
            case ElfElement.Earth:
                // Tierra: ligero bono de ataque (+1)
                return baseDamage + 1;
            case ElfElement.Air:
                // Aire: daño base normal, pero si está a distancia, aumenta un poco
                if (distance > 0)
                {
                    return (int)Math.Round(baseDamage * 1.15);
                }
                return baseDamage;
            case ElfElement.Water:
                // Agua: daño normal
                return baseDamage;
            default:
                return baseDamage;
        }
    }

    private int CalculateOrcDamage(PlayerCharacter attacker)
    {
        if (attacker.OrcWeapon == OrcWeapon.Axe)
        {
            return _random.Next(1, 6); // 1-5
        }

        if (attacker.OrcWeapon == OrcWeapon.Hammer)
        {
            return _random.Next(2, 8); // 2-7
        }

        return _random.Next(1, 6);
    }

    private int CalculateBeastDamage(PlayerCharacter attacker)
    {
        if (attacker.BeastWeapon == BeastWeapon.Fists)
        {
            return _random.Next(20, 31); // 20-30
        }

        if (attacker.BeastWeapon == BeastWeapon.Sword)
        {
            return _random.Next(1, 11); // 1-10
        }

        return _random.Next(1, 6);
    }

    public string PerformHeal(out int healAmount)
    {
        if (CurrentSession is null)
        {
            healAmount = 0;
            return _localizationService["Common_NoSession"];
        }

        var player = CurrentSession.CurrentPlayer;
        var lostHealth = player.MaxHealth - player.CurrentHealth;

        if (lostHealth <= 0)
        {
            healAmount = 0;
            return _localizationService.GetString("Battle_AlreadyFullHealth", player.PlayerName);
        }

        double percentNow;

        switch (player.Race)
        {
            case Race.Human:
                // Comer: entre 42% y 48% de la vida perdida
                percentNow = RandomRange(0.42, 0.48);
                healAmount = (int)Math.Round(lostHealth * percentNow);
                player.CurrentHealth = Math.Min(player.MaxHealth, player.CurrentHealth + healAmount);
                return _localizationService.GetString("Battle_HumanHeal", player.PlayerName, healAmount);

            case Race.Elf:
                if (player.Element == ElfElement.Water)
                {
                    // Elfo de agua: 75-90% de la vida perdida
                    percentNow = RandomRange(0.75, 0.9);
                }
                else
                {
                    // Elfo normal: ~65% de la vida perdida
                    percentNow = 0.65;
                }

                healAmount = (int)Math.Round(lostHealth * percentNow);
                player.CurrentHealth = Math.Min(player.MaxHealth, player.CurrentHealth + healAmount);
                return _localizationService.GetString("Battle_ElfHeal", player.PlayerName, healAmount);

            case Race.Orc:
                // Poción de orco: parte ahora, parte en el siguiente turno
                var percentInitial = RandomRange(0.25, 0.45);
                var percentNextTurn = RandomRange(0.05, 0.25);

                var healNow = (int)Math.Round(lostHealth * percentInitial);
                player.CurrentHealth = Math.Min(player.MaxHealth, player.CurrentHealth + healNow);

                // Guardamos porcentaje para el próximo turno
                player.PendingHealPercentNextTurn = percentNextTurn;

                healAmount = healNow;
                return _localizationService.GetString("Battle_OrcHeal", player.PlayerName, healNow);

            case Race.Beast:
                // Dormir: exactamente 50% de la vida perdida
                healAmount = (int)Math.Round(lostHealth * 0.5);
                player.CurrentHealth = Math.Min(player.MaxHealth, player.CurrentHealth + healAmount);
                return _localizationService.GetString("Battle_BeastHeal", player.PlayerName, healAmount);

            default:
                healAmount = 0;
                return _localizationService.GetString("Battle_CannotHeal", player.PlayerName);
        }
    }

    private double RandomRange(double min, double max)
    {
        return min + _random.NextDouble() * (max - min);
    }

    public string MoveForward()
    {
        if (CurrentSession is null)
        {
            return _localizationService["Common_NoSession"];
        }

        if (CurrentSession.Distance <= 0)
        {
            return _localizationService["Battle_AlreadyMelee"];
        }

        CurrentSession.Distance--;
        return _localizationService.GetString(
            "Battle_MoveForwardResult",
            CurrentSession.CurrentPlayer.PlayerName,
            CurrentSession.Distance);
    }

    public string MoveBack()
    {
        if (CurrentSession is null)
        {
            return _localizationService["Common_NoSession"];
        }

        // Permitimos una distancia máxima simbólica de 3
        if (CurrentSession.Distance >= 3)
        {
            return _localizationService["Battle_AlreadyMaxDistance"];
        }

        CurrentSession.Distance++;
        return _localizationService.GetString(
            "Battle_MoveBackResult",
            CurrentSession.CurrentPlayer.PlayerName,
            CurrentSession.Distance);
    }

    public async Task<bool> HasAnySavedGamesAsync()
    {
        try
        {
            return await _savedGameService.HasAnySavedGamesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to query saved game availability.");
            return false;
        }
    }

    public async Task<bool> SaveCurrentSessionAsync(string name)
    {
        if (CurrentSession is null)
        {
            return false;
        }

        try
        {
            var id = await _savedGameService.SaveSessionAsync(CurrentSession, name);
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogWarning("Save session returned an empty identifier for {Name}.", name);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save current session as {Name}.", name);
            return false;
        }
    }

    public async Task<bool> LoadSavedSessionAsync(string id)
    {
        try
        {
            var session = await _savedGameService.LoadSavedGameAsync(id);
            if (session is null)
            {
                return false;
            }

            CurrentSession = session;
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load saved session {Id}.", id);
            return false;
        }
    }

    public async Task<bool> LoadMostRecentSessionAsync()
    {
        try
        {
            var session = await _savedGameService.LoadMostRecentAsync();
            if (session is null)
            {
                return false;
            }

            CurrentSession = session;
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load most recent session.");
            return false;
        }
    }
}
