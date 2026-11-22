namespace CombatFantasyGame.Models;

public record SavedGameInfo(
    string Id,
    string Name,
    DateTime CreatedAt,
    string Player1Name,
    string Player2Name,
    int TurnNumber);
