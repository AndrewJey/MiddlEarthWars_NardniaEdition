using SQLite;

namespace CombatFantasyGame.Models;

[Table("MatchResult")]
public class MatchResult
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public DateTime PlayedAt { get; set; }

    public string Player1Name { get; set; } = string.Empty;

    public string Player2Name { get; set; } = string.Empty;

    public string Player1Race { get; set; } = string.Empty;

    public string Player2Race { get; set; } = string.Empty;

    public string Player1Weapon { get; set; } = string.Empty;

    public string Player2Weapon { get; set; } = string.Empty;

    public string WinnerName { get; set; } = string.Empty;

    public bool IsDraw { get; set; }

    public int Player1RemainingHealth { get; set; }

    public int Player2RemainingHealth { get; set; }
}

