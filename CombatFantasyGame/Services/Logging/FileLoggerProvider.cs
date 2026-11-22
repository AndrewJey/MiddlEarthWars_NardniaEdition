using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text;

namespace CombatFantasyGame.Services.Logging;

public sealed class FileLoggerProvider : ILoggerProvider
{
    private readonly ILogFileService _logFileService;
    private readonly object _lock = new();
    private bool _disposed;

    public FileLoggerProvider(ILogFileService logFileService)
    {
        _logFileService = logFileService;
    }

    public ILogger CreateLogger(string categoryName)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(FileLoggerProvider));
        }

        return new FileLogger(categoryName, this);
    }

    internal void WriteLog(string categoryName, LogLevel logLevel, string message, Exception? exception)
    {
        if (_disposed)
        {
            return;
        }

        try
        {
            var builder = new StringBuilder();
            builder.Append('[')
                .Append(DateTimeOffset.UtcNow.ToString("O"))
                .Append("] [")
                .Append(logLevel)
                .Append("] ")
                .Append(categoryName)
                .Append(": ")
                .Append(message);

            if (exception is not null)
            {
                builder.AppendLine();
                builder.Append(exception);
            }

            WriteToTargets(builder.ToString());
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to persist log message: {ex}");
        }
    }

    public void Dispose()
    {
        _disposed = true;
    }

    private void WriteToTargets(string message)
    {
        lock (_lock)
        {
            foreach (var path in _logFileService.LogFilePaths)
            {
                try
                {
                    File.AppendAllText(path, message + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Unable to append to log file {path}: {ex}");
                }
            }
        }
    }

    private sealed class FileLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly FileLoggerProvider _provider;

        public FileLogger(string categoryName, FileLoggerProvider provider)
        {
            _categoryName = categoryName;
            _provider = provider;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return NullScope.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var message = formatter?.Invoke(state, exception) ?? state?.ToString() ?? string.Empty;
            _provider.WriteLog(_categoryName, logLevel, message, exception);
        }

        private sealed class NullScope : IDisposable
        {
            public static NullScope Instance { get; } = new();

            public void Dispose()
            {
            }
        }
    }
}
