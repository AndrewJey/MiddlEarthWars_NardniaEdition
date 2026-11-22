namespace CombatFantasyGame.Models;

public class SavedGameState
{
    public SavedPlayerCharacter Player1 { get; set; } = new();

    public SavedPlayerCharacter Player2 { get; set; } = new();

    public int CurrentPlayerIndex { get; set; }

    public int Distance { get; set; }

    public int TurnNumber { get; set; }

    public static SavedGameState FromSession(GameSession session)
    {
        return new SavedGameState
        {
            Player1 = SavedPlayerCharacter.FromPlayer(session.Player1),
            Player2 = SavedPlayerCharacter.FromPlayer(session.Player2),
            CurrentPlayerIndex = session.CurrentPlayerIndex,
            Distance = session.Distance,
            TurnNumber = session.TurnNumber
        };
    }

    public GameSession ToSession()
    {
        var session = new GameSession(
            Player1.ToPlayerCharacter(),
            Player2.ToPlayerCharacter(),
            CurrentPlayerIndex,
            Distance,
            TurnNumber);

        return session;
    }
}

public class SavedPlayerCharacter
{
    public string PlayerName { get; set; } = string.Empty;

    public Race Race { get; set; }

    public ElfElement Element { get; set; }

    public HumanWeapon? HumanWeapon { get; set; }

    public OrcWeapon? OrcWeapon { get; set; }

    public BeastWeapon? BeastWeapon { get; set; }

    public int MaxHealth { get; set; }

    public int CurrentHealth { get; set; }

    public int BleedTurnsRemaining { get; set; }

    public int BleedDamagePerTurn { get; set; }

    public double PendingHealPercentNextTurn { get; set; }

    public static SavedPlayerCharacter FromPlayer(PlayerCharacter player)
    {
        return new SavedPlayerCharacter
        {
            PlayerName = player.PlayerName,
            Race = player.Race,
            Element = player.Element,
            HumanWeapon = player.HumanWeapon,
            OrcWeapon = player.OrcWeapon,
            BeastWeapon = player.BeastWeapon,
            MaxHealth = player.MaxHealth,
            CurrentHealth = player.CurrentHealth,
            BleedTurnsRemaining = player.BleedTurnsRemaining,
            BleedDamagePerTurn = player.BleedDamagePerTurn,
            PendingHealPercentNextTurn = player.PendingHealPercentNextTurn
        };
    }

    public PlayerCharacter ToPlayerCharacter()
    {
        return new PlayerCharacter
        {
            PlayerName = PlayerName,
            Race = Race,
            Element = Element,
            HumanWeapon = HumanWeapon,
            OrcWeapon = OrcWeapon,
            BeastWeapon = BeastWeapon,
            MaxHealth = MaxHealth,
            CurrentHealth = CurrentHealth,
            BleedTurnsRemaining = BleedTurnsRemaining,
            BleedDamagePerTurn = BleedDamagePerTurn,
            PendingHealPercentNextTurn = PendingHealPercentNextTurn
        };
    }
}
