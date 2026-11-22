using SQLite;

namespace CombatFantasyGame.Models;

public class SavedGameRecord
{
    [PrimaryKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public string StateJson { get; set; } = string.Empty;
}
