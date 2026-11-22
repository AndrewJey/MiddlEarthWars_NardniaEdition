using CombatFantasyGame.Services.Logging;
using Microsoft.Extensions.Logging;
#if WINDOWS
using WinUIApplication = Microsoft.UI.Xaml.Application;
using WinUIUnhandledExceptionEventArgs = Microsoft.UI.Xaml.UnhandledExceptionEventArgs;
#endif

namespace CombatFantasyGame.Services;

public sealed class AppDiagnostics
{
    private readonly ILogger<AppDiagnostics> _logger;
    private readonly ILogFileService _logFileService;
    private bool _initialized;

    public AppDiagnostics(ILogger<AppDiagnostics> logger, ILogFileService logFileService)
    {
        _logger = logger;
        _logFileService = logFileService;
    }

    public string LogFilePath => _logFileService.CurrentLogFilePath;

    public void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        _initialized = true;
        _logger.LogInformation("Diagnostics initialized. Log file: {LogFile}", _logFileService.CurrentLogFilePath);

        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
        {
            var exception = args.ExceptionObject as Exception
                            ?? new Exception("Unhandled exception without Exception instance.");
            _logger.LogCritical(exception, "Unhandled exception captured by AppDomain.");
        };

        TaskScheduler.UnobservedTaskException += (_, args) =>
        {
            _logger.LogCritical(args.Exception, "Unobserved TaskScheduler exception.");
            args.SetObserved();
        };

#if WINDOWS
        if (WinUIApplication.Current is not null)
        {
            WinUIApplication.Current.UnhandledException += OnWinUiUnhandledException;
        }
#endif
    }

#if WINDOWS
    private void OnWinUiUnhandledException(object? sender, WinUIUnhandledExceptionEventArgs e)
    {
        _logger.LogCritical(e.Exception, "WinUI dispatcher unhandled exception.");
        e.Handled = true;
    }
#endif
}
