namespace CombatFantasyGame.Services.Logging;

public interface ILogFileService
{
    string DirectoryPath { get; }

    string CurrentLogFilePath { get; }

    IReadOnlyList<string> LogFilePaths { get; }

    Task<string> ReadAsync(CancellationToken cancellationToken = default);

    Task ClearAsync(CancellationToken cancellationToken = default);
}
