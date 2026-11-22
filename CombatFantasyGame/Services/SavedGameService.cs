using CombatFantasyGame.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CombatFantasyGame.Services;

public class SavedGameService
{
    private readonly GameDatabase _database;
    private readonly ILogger<SavedGameService> _logger;

    public SavedGameService(GameDatabase database, ILogger<SavedGameService> logger)
    {
        _database = database;
        _logger = logger;
    }

    public async Task<string?> SaveSessionAsync(GameSession session, string name)
    {
        try
        {
            var state = SavedGameState.FromSession(session);
            var record = new SavedGameRecord
            {
                Id = Guid.NewGuid().ToString(),
                Name = string.IsNullOrWhiteSpace(name) ? "Partida guardada" : name.Trim(),
                CreatedAt = DateTime.UtcNow,
                StateJson = JsonSerializer.Serialize(state)
            };

            await _database.UpsertSavedGameAsync(record);
            return record.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save session {Name}.", name);
            return null;
        }
    }

    public async Task<IReadOnlyList<SavedGameInfo>> GetSavedGamesAsync()
    {
        try
        {
            var records = await _database.GetSavedGamesAsync();

            var list = new List<SavedGameInfo>();

            foreach (var record in records.OrderByDescending(r => r.CreatedAt))
            {
                var state = Deserialize(record.StateJson);
                if (state is null)
                {
                    continue;
                }

                list.Add(new SavedGameInfo(
                    record.Id,
                    record.Name,
                    record.CreatedAt,
                    state.Player1.PlayerName,
                    state.Player2.PlayerName,
                    state.TurnNumber));
            }

            return list;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve saved games.");
            return Array.Empty<SavedGameInfo>();
        }
    }

    public async Task<GameSession?> LoadSavedGameAsync(string id)
    {
        try
        {
            var record = await _database.GetSavedGameAsync(id);
            if (record is null)
            {
                return null;
            }

            return Deserialize(record.StateJson)?.ToSession();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load saved game {Id}.", id);
            return null;
        }
    }

    public async Task<GameSession?> LoadMostRecentAsync()
    {
        try
        {
            var records = await _database.GetSavedGamesAsync();
            var latest = records
                .OrderByDescending(r => r.CreatedAt)
                .FirstOrDefault();

            if (latest is null)
            {
                return null;
            }

            return Deserialize(latest.StateJson)?.ToSession();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load most recent saved game.");
            return null;
        }
    }

    public async Task<bool> HasAnySavedGamesAsync()
    {
        try
        {
            var records = await _database.GetSavedGamesAsync();
            return records.Count > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to determine if any saved games exist.");
            return false;
        }
    }

    public async Task DeleteSavedGameAsync(string id)
    {
        try
        {
            await _database.DeleteSavedGameAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete saved game {Id}.", id);
        }
    }

    private static SavedGameState? Deserialize(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<SavedGameState>(json);
        }
        catch
        {
            return null;
        }
    }
}
