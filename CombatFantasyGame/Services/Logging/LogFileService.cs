using System.Text;

namespace CombatFantasyGame.Services.Logging;

public sealed class LogFileService : ILogFileService
{
    private const int MaxLogFiles = 5;
    private const long MaxSingleLogSizeBytes = 512 * 1024;
    private readonly object _initLock = new();
    private string? _logDirectory;
    private string? _currentLogFilePath;
    private IReadOnlyList<string> _logFilePaths = Array.Empty<string>();
    private string? _hostLogFilePath;
    private bool _initialized;

    public string DirectoryPath
    {
        get
        {
            EnsureInitialized();
            return _logDirectory!;
        }
    }

    public string CurrentLogFilePath
    {
        get
        {
            EnsureInitialized();
            return _currentLogFilePath!;
        }
    }

    public IReadOnlyList<string> LogFilePaths
    {
        get
        {
            EnsureInitialized();
            return _logFilePaths;
        }
    }

    public async Task<string> ReadAsync(CancellationToken cancellationToken = default)
    {
        EnsureInitialized();

        if (!File.Exists(_currentLogFilePath))
        {
            return string.Empty;
        }

        await using var stream = new FileStream(
            _currentLogFilePath!,
            FileMode.Open,
            FileAccess.Read,
            FileShare.ReadWrite);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        var builder = new StringBuilder();

        while (!reader.EndOfStream)
        {
            cancellationToken.ThrowIfCancellationRequested();
            builder.AppendLine(await reader.ReadLineAsync() ?? string.Empty);
        }

        return builder.ToString();
    }

    public Task ClearAsync(CancellationToken cancellationToken = default)
    {
        EnsureInitialized();

        foreach (var path in _logFilePaths)
        {
            try
            {
                File.WriteAllText(path, string.Empty, Encoding.UTF8);
            }
            catch
            {
                // ignore
            }
        }

        return Task.CompletedTask;
    }

    private void EnsureInitialized()
    {
        if (_initialized)
        {
            return;
        }

        lock (_initLock)
        {
            if (_initialized)
            {
                return;
            }

            var fileName = $"combatfantasy-{DateTime.UtcNow:yyyyMMdd}.log";

            _logDirectory = Path.Combine(FileSystem.AppDataDirectory, "logs");
            Directory.CreateDirectory(_logDirectory);

            _currentLogFilePath = Path.Combine(_logDirectory, fileName);
            _hostLogFilePath = TryCreateHostLogFile(fileName);

            var paths = new List<string> { _currentLogFilePath };
            if (!string.IsNullOrWhiteSpace(_hostLogFilePath))
            {
                paths.Add(_hostLogFilePath);
            }

            _logFilePaths = paths;

            PruneOldLogs(_logDirectory);
            if (!string.IsNullOrWhiteSpace(_hostLogFilePath))
            {
                var hostDirectory = Path.GetDirectoryName(_hostLogFilePath)!;
                PruneOldLogs(hostDirectory);
                EnsureFileWithinLimits(_hostLogFilePath);
            }

            EnsureFileWithinLimits(_currentLogFilePath);

            _initialized = true;
        }
    }

    private void PruneOldLogs(string directory)
    {
        if (!Directory.Exists(directory))
        {
            return;
        }

        var files = Directory.GetFiles(directory, "combatfantasy-*.log")
            .OrderByDescending(File.GetCreationTimeUtc)
            .ToList();

        foreach (var file in files.Skip(MaxLogFiles - 1))
        {
            TryDelete(file);
        }
    }

    private void EnsureFileWithinLimits(string path)
    {
        if (!File.Exists(path))
        {
            return;
        }

        var info = new FileInfo(path);
        if (info.Length <= MaxSingleLogSizeBytes)
        {
            return;
        }

        var archiveName = Path.Combine(
            Path.GetDirectoryName(path)!,
            $"combatfantasy-{DateTime.UtcNow:yyyyMMdd_HHmmss}.log");
        File.Move(path, archiveName, overwrite: true);
    }

    private static void TryDelete(string file)
    {
        try
        {
            File.Delete(file);
        }
        catch
        {
            // We silently swallow IO failures; logging will continue in the primary file.
        }
    }

    private string? TryCreateHostLogFile(string fileName)
    {
        try
        {
            if (OperatingSystem.IsAndroid() || OperatingSystem.IsIOS() || OperatingSystem.IsMacCatalyst())
            {
                return null;
            }

            var baseDir = AppContext.BaseDirectory;
            if (string.IsNullOrWhiteSpace(baseDir))
            {
                baseDir = Environment.CurrentDirectory;
            }

            var hostDirectory = Path.Combine(baseDir, "host-logs");
            Directory.CreateDirectory(hostDirectory);
            return Path.Combine(hostDirectory, fileName);
        }
        catch
        {
            return null;
        }
    }
}
