using CombatFantasyGame.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace CombatFantasyGame.Services;

public record LanguageOption(GameLanguage Language, string DisplayName);

internal sealed class LocalizationCatalog
{
    public LocalizationCatalog(CultureInfo culture)
    {
        Culture = culture;
    }

    public CultureInfo Culture { get; }

    public Dictionary<string, string> Strings { get; } = new(StringComparer.OrdinalIgnoreCase);
}

public class LocalizationService
{
    private readonly Dictionary<GameLanguage, LocalizationCatalog> _catalogs;
    private readonly ReadOnlyCollection<LanguageOption> _languageOptions;

    public LocalizationService()
    {
        _catalogs = new Dictionary<GameLanguage, LocalizationCatalog>
        {
            { GameLanguage.Spanish, new LocalizationCatalog(new CultureInfo("es")) },
            { GameLanguage.English, new LocalizationCatalog(new CultureInfo("en")) },
            { GameLanguage.Portuguese, new LocalizationCatalog(new CultureInfo("pt")) },
            { GameLanguage.Japanese, new LocalizationCatalog(new CultureInfo("ja")) }
        };

        _languageOptions = new ReadOnlyCollection<LanguageOption>(
            new[]
            {
                new LanguageOption(GameLanguage.Spanish, "Español"),
                new LanguageOption(GameLanguage.English, "English"),
                new LanguageOption(GameLanguage.Portuguese, "Português"),
                new LanguageOption(GameLanguage.Japanese, "日本語")
            });
    }

    public event EventHandler? LanguageChanged;

    public GameLanguage CurrentLanguage { get; private set; } = GameLanguage.Spanish;

    public CultureInfo CurrentCulture => _catalogs[CurrentLanguage].Culture;

    public ReadOnlyCollection<LanguageOption> SupportedLanguages => _languageOptions;

    public void SetLanguage(GameLanguage language)
    {
        if (language == CurrentLanguage)
        {
            return;
        }

        CurrentLanguage = language;
        LanguageChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RegisterString(GameLanguage language, string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return;
        }

        if (_catalogs.TryGetValue(language, out var catalog))
        {
            catalog.Strings[key] = value;
        }
    }

    public string this[string key] => GetString(key);

    public string GetString(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return string.Empty;
        }

        if (_catalogs.TryGetValue(CurrentLanguage, out var catalog) &&
            catalog.Strings.TryGetValue(key, out var translated) &&
            !string.IsNullOrEmpty(translated))
        {
            return translated;
        }

        var fallbackCatalog = _catalogs[GameLanguage.English];
        if (fallbackCatalog.Strings.TryGetValue(key, out var fallback) && !string.IsNullOrEmpty(fallback))
        {
            return fallback;
        }

        return key;
    }

    public string GetString(string key, params object[] args)
    {
        var format = GetString(key);
        return string.Format(CurrentCulture, format, args);
    }

    public string GetRaceName(Race race) => GetString($"Race_{race}");

    public string GetElfElementName(ElfElement element) => GetString($"Element_{element}");

    public string GetHumanWeaponName(HumanWeapon weapon) => GetString($"Weapon_{weapon}");

    public string GetOrcWeaponName(OrcWeapon weapon) => GetString($"Weapon_{weapon}");

    public string GetBeastWeaponName(BeastWeapon weapon) => GetString($"Weapon_{weapon}");
}
