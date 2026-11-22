using SQLite;

namespace CombatFantasyGame.Models;

[Table("PlayerStats")]
public class PlayerStats
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Unique]
    public string Name { get; set; } = string.Empty;

    public string LastRace { get; set; } = string.Empty;

    public string LastWeaponOrElement { get; set; } = string.Empty;

    public int Wins { get; set; }

    public int Losses { get; set; }

    public int Draws { get; set; }
}

