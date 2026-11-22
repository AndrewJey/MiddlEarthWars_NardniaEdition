using CombatFantasyGame.Services;
using CombatFantasyGame.Services.Logging;
using Microsoft.Extensions.Logging;

namespace CombatFantasyGame;

public partial class DocsPage : ContentPage
{
    private readonly LocalizationService _localizationService;
    private readonly ILogFileService _logFileService;
    private readonly ILogger<DocsPage> _logger;
    private string? _hostLogPath;

    public DocsPage(LocalizationService localizationService, ILogFileService logFileService, ILogger<DocsPage> logger)
    {
        InitializeComponent();
        _localizationService = localizationService;
        _logFileService = logFileService;
        _logger = logger;

        ApplyLocalization();
        _localizationService.LanguageChanged += (_, _) => ApplyLocalization();
    }

    private void ApplyLocalization()
    {
        Title = _localizationService["Docs_Title"];
        DocsTitleLabel.Text = _localizationService["Docs_Title"];
        DocsIntroLabel.Text = _localizationService["Docs_Intro"];
        DocsRacesHeaderLabel.Text = _localizationService["Docs_RacesHeader"];
        DocsHumansLabel.Text = _localizationService["Docs_Humans"];
        DocsElvesLabel.Text = _localizationService["Docs_Elves"];
        DocsOrcsLabel.Text = _localizationService["Docs_Orcs"];
        DocsBeastsLabel.Text = _localizationService["Docs_Beasts"];
        DocsDistanceHeaderLabel.Text = _localizationService["Docs_DistanceHeader"];
        DocsDistanceLabel.Text = _localizationService["Docs_Distance"];
        DocsTurnLogicHeaderLabel.Text = _localizationService["Docs_TurnLogicHeader"];
        DocsTurnLogicLabel.Text = _localizationService["Docs_TurnLogic"];
        DocsDiagramHeaderLabel.Text = _localizationService["Docs_DiagramHeader"];
        DocsDiagramDescriptionLabel.Text = _localizationService["Docs_DiagramDescription"];
        DocsDiagramPlayerOneLabel.Text = _localizationService["Docs_DiagramPlayerOne"];
        DocsDiagramPlayerTwoLabel.Text = _localizationService["Docs_DiagramPlayerTwo"];
        DocsDiagramTurnEngineLabel.Text = _localizationService["Docs_DiagramTurnEngine"];
        DocsDiagramDataStoreLabel.Text = _localizationService["Docs_DiagramDataStore"];
        DocsVictoryHeaderLabel.Text = _localizationService["Docs_VictoryHeader"];
        DocsVictoryLabel.Text = _localizationService["Docs_Victory"];
        DocsPersistenceHeaderLabel.Text = _localizationService["Docs_PersistenceHeader"];
        DocsPersistenceLabel.Text = _localizationService["Docs_Persistence"];
        DocsArchitectureHeaderLabel.Text = _localizationService["Docs_ArchitectureHeader"];
        DocsArchitectureLabel.Text = _localizationService["Docs_Architecture"];
        DocsDiagnosticsHeaderLabel.Text = _localizationService["Docs_DiagnosticsHeader"];
        DocsDiagnosticsLabel.Text = _localizationService["Docs_DiagnosticsDescription"];
        DocsLogPathHeaderLabel.Text = _localizationService["Docs_LogPathHeader"];
        DocsLogPathValueLabel.Text = _logFileService.CurrentLogFilePath;
        CopyLogPathButton.Text = _localizationService["Docs_CopyLogButton"];
        ShareLogButton.Text = _localizationService["Docs_ShareLogButton"];

        _hostLogPath = _logFileService.LogFilePaths
            .FirstOrDefault(path => !string.Equals(path, _logFileService.CurrentLogFilePath, StringComparison.OrdinalIgnoreCase));

        var hasHostLog = !string.IsNullOrWhiteSpace(_hostLogPath);
        DocsHostLogPathHeaderLabel.IsVisible = hasHostLog;
        DocsHostLogPathValueLabel.IsVisible = hasHostLog;
        CopyHostLogPathButton.IsVisible = hasHostLog;

        if (hasHostLog)
        {
            DocsHostLogPathHeaderLabel.Text = _localizationService["Docs_HostLogPathHeader"];
            DocsHostLogPathValueLabel.Text = _hostLogPath;
            CopyHostLogPathButton.Text = _localizationService["Docs_CopyHostLogButton"];
        }
    }

    private async void OnCopyLogPathClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(async () =>
        {
            await Clipboard.SetTextAsync(_logFileService.CurrentLogFilePath);
            await DisplayAlert(
                _localizationService["Docs_Title"],
                _localizationService["Docs_CopyLogSuccess"],
                _localizationService["Common_Accept"]);
        }, "copying log path");
    }

    private async void OnCopyHostLogPathClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_hostLogPath))
        {
            await DisplayGenericCopyErrorAsync();
            return;
        }

        await RunSafeAsync(async () =>
        {
            await Clipboard.SetTextAsync(_hostLogPath);
            await DisplayAlert(
                _localizationService["Docs_Title"],
                _localizationService["Docs_CopyLogSuccess"],
                _localizationService["Common_Accept"]);
        }, "copying host log path");
    }

    private Task DisplayGenericCopyErrorAsync()
    {
        return DisplayAlert(
            _localizationService["Docs_Title"],
            _localizationService["Docs_CopyLogError"],
            _localizationService["Common_Accept"]);
    }

    private async void OnShareLogClicked(object? sender, EventArgs e)
    {
        await RunSafeAsync(async () =>
        {
            var file = _logFileService.CurrentLogFilePath;
            if (!File.Exists(file))
            {
                await DisplayGenericCopyErrorAsync();
                return;
            }

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = _localizationService["Docs_ShareLogTitle"],
                File = new ShareFile(file)
            });
        }, "sharing log file");
    }

    private async Task RunSafeAsync(Func<Task> action, string operation)
    {
        try
        {
            await action();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Diagnostics action failed while {Operation}.", operation);
            await DisplayAlert(
                _localizationService["Docs_Title"],
                _localizationService["Docs_CopyLogError"],
                _localizationService["Common_Accept"]);
        }
    }
}
