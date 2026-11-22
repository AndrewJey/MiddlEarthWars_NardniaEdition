using CombatFantasyGame.Services;

namespace CombatFantasyGame;

public partial class CreditsPage : ContentPage
{
    private readonly LocalizationService _localizationService;

    public CreditsPage(LocalizationService localizationService)
    {
        InitializeComponent();
        _localizationService = localizationService;

        ApplyLocalization();
        _localizationService.LanguageChanged += (_, _) => ApplyLocalization();
    }

    private void ApplyLocalization()
    {
        Title = _localizationService["Credits_Title"];
        CreditsHeaderLabel.Text = _localizationService["Credits_Header"];
        TeamHeaderLabel.Text = _localizationService["Credits_TeamHeader"];
        Member1Label.Text = _localizationService["Credits_Member1"];
        Member2Label.Text = _localizationService["Credits_Member2"];
        Member3Label.Text = _localizationService["Credits_Member3"];
        Member4Label.Text = _localizationService["Credits_Member4"];
        TechHeaderLabel.Text = _localizationService["Credits_TechHeader"];
        Tech1Label.Text = _localizationService["Credits_Tech1"];
        Tech2Label.Text = _localizationService["Credits_Tech2"];
        Tech3Label.Text = _localizationService["Credits_Tech3"];
        Tech4Label.Text = _localizationService["Credits_Tech4"];
        ImagesHeaderLabel.Text = _localizationService["Credits_ImagesHeader"];
        ImagesNoteLabel.Text = _localizationService["Credits_ImagesNote"];
    }
}
