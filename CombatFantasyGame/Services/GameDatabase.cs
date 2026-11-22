using CombatFantasyGame.Models;
using SQLite;

namespace CombatFantasyGame.Services;

public class GameDatabase
{
    private readonly string _dbPath;
    private SQLiteAsyncConnection? _connection;

    public GameDatabase()
    {
        _dbPath = Path.Combine(FileSystem.AppDataDirectory, "combatfantasy.db3");
    }

    private async Task InitAsync()
    {
        if (_connection is not null)
        {
            return;
        }

        _connection = new SQLiteAsyncConnection(_dbPath);
        await _connection.CreateTableAsync<PlayerStats>();
        await _connection.CreateTableAsync<MatchResult>();
        await _connection.CreateTableAsync<SavedGameRecord>();
    }

    public async Task<List<PlayerStats>> GetPlayerStatsAsync()
    {
        await InitAsync();
        return await _connection!.Table<PlayerStats>()
            .OrderByDescending(p => p.Wins)
            .ThenBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<PlayerStats> UpsertPlayerAsync(string name, string lastRace, string lastWeaponOrElement)
    {
        await InitAsync();

        var existing = await _connection!.Table<PlayerStats>()
            .Where(p => p.Name == name)
            .FirstOrDefaultAsync();

        if (existing is null)
        {
            existing = new PlayerStats
            {
                Name = name,
                LastRace = lastRace,
                LastWeaponOrElement = lastWeaponOrElement,
                Wins = 0,
                Losses = 0,
                Draws = 0
            };
            await _connection.InsertAsync(existing);
        }
        else
        {
            existing.LastRace = lastRace;
            existing.LastWeaponOrElement = lastWeaponOrElement;
            await _connection.UpdateAsync(existing);
        }

        return existing;
    }

    public async Task UpdateStatsForMatchAsync(
        PlayerCharacter player1,
        PlayerCharacter player2,
        BattleResultType resultType)
    {
        await InitAsync();

        var p1 = await UpsertPlayerAsync(
            player1.PlayerName,
            player1.Race.ToString(),
            GetWeaponOrElementForPlayer(player1));
        var p2 = await UpsertPlayerAsync(
            player2.PlayerName,
            player2.Race.ToString(),
            GetWeaponOrElementForPlayer(player2));

        switch (resultType)
        {
            case BattleResultType.Player1Wins:
                p1.Wins++;
                p2.Losses++;
                break;
            case BattleResultType.Player2Wins:
                p2.Wins++;
                p1.Losses++;
                break;
            case BattleResultType.Draw:
                p1.Draws++;
                p2.Draws++;
                break;
        }

        await _connection!.UpdateAsync(p1);
        await _connection.UpdateAsync(p2);

        var match = new MatchResult
        {
            PlayedAt = DateTime.UtcNow,
            Player1Name = player1.PlayerName,
            Player2Name = player2.PlayerName,
            Player1Race = player1.Race.ToString(),
            Player2Race = player2.Race.ToString(),
            Player1Weapon = GetWeaponOrElementForPlayer(player1),
            Player2Weapon = GetWeaponOrElementForPlayer(player2),
            Player1RemainingHealth = player1.CurrentHealth,
            Player2RemainingHealth = player2.CurrentHealth,
            WinnerName = resultType switch
            {
                BattleResultType.Player1Wins => player1.PlayerName,
                BattleResultType.Player2Wins => player2.PlayerName,
                _ => string.Empty
            },
            IsDraw = resultType == BattleResultType.Draw
        };

        await _connection.InsertAsync(match);
    }

    public async Task<List<MatchResult>> GetMatchHistoryAsync()
    {
        await InitAsync();
        return await _connection!.Table<MatchResult>()
            .OrderByDescending(m => m.PlayedAt)
            .ToListAsync();
    }

    public async Task UpsertSavedGameAsync(SavedGameRecord record)
    {
        await InitAsync();
        await _connection!.InsertOrReplaceAsync(record);
    }

    public async Task<List<SavedGameRecord>> GetSavedGamesAsync()
    {
        await InitAsync();
        return await _connection!.Table<SavedGameRecord>()
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<SavedGameRecord?> GetSavedGameAsync(string id)
    {
        await InitAsync();
        return await _connection!.Table<SavedGameRecord>()
            .Where(r => r.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteSavedGameAsync(string id)
    {
        await InitAsync();
        await _connection!.Table<SavedGameRecord>()
            .DeleteAsync(r => r.Id == id);
    }

    private static string GetWeaponOrElementForPlayer(PlayerCharacter player)
    {
        return player.Race switch
        {
            Race.Human => player.HumanWeapon?.ToString() ?? string.Empty,
            Race.Elf => player.Element.ToString(),
            Race.Orc => player.OrcWeapon?.ToString() ?? string.Empty,
            Race.Beast => player.BeastWeapon?.ToString() ?? string.Empty,
            _ => string.Empty
        };
    }
}
