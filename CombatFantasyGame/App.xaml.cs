using CombatFantasyGame.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.ApplicationModel;

namespace CombatFantasyGame;

public partial class App : Application
{
    private readonly AppShell _appShell;
    private readonly BackgroundMusicService _musicService;
    private readonly ILogger<App> _logger;
    private readonly AppDiagnostics _diagnostics;
    private readonly LocalizationService _localizationService;
    private Window? _window;
    private bool _shellPresented;

	public App(
        Func<AppShell> shellFactory,
        BackgroundMusicService musicService,
        ILogger<App> logger,
        AppDiagnostics diagnostics,
        LocalizationService localizationService)
	{
		InitializeComponent();

		_appShell = shellFactory();
		_musicService = musicService;
		_logger = logger;
		_diagnostics = diagnostics;
        _localizationService = localizationService;
		_diagnostics.Initialize();
	}

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var splashPage = new SplashPage(_localizationService, SwitchToShellAsync);
        _window = new Window(splashPage);
        return _window;
    }

    protected override async void OnStart()
    {
        base.OnStart();
        await PlayMusicSafelyAsync("app start");
    }

    protected override async void OnResume()
    {
        base.OnResume();
        await PlayMusicSafelyAsync("app resume");
    }

    protected override void OnSleep()
    {
        base.OnSleep();
        try
        {
            _musicService.Pause();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to pause background music on sleep.");
        }
    }

    private async Task PlayMusicSafelyAsync(string reason)
    {
        try
        {
            await _musicService.PlayAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start background music during {Reason}.", reason);
        }
    }

    private async Task SwitchToShellAsync()
    {
        if (_window is null || _shellPresented)
        {
            return;
        }

        _shellPresented = true;
        try
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                _window.Page = _appShell;
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to transition from splash screen to shell.");
        }
    }
}
