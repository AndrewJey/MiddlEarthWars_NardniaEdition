using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;

namespace CombatFantasyGame.Services;

public class BackgroundMusicService
{
    private readonly IAudioManager _audioManager;
    private readonly ILogger<BackgroundMusicService> _logger;
    private readonly SemaphoreSlim _playerLock = new(1, 1);
    private IAudioPlayer? _player;

    public BackgroundMusicService(IAudioManager audioManager, ILogger<BackgroundMusicService> logger)
    {
        _audioManager = audioManager;
        _logger = logger;
    }

    public async Task PlayAsync()
    {
        try
        {
            await EnsurePlayerAsync();
            if (_player is null)
            {
                return;
            }

            if (_player.IsPlaying)
            {
                return;
            }

            _player.Loop = true;
            _player.Volume = 0.35;
            _player.Play();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to play background music.");
        }
    }

    public void Pause()
    {
        try
        {
            if (_player?.IsPlaying == true)
            {
                _player.Pause();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to pause background music.");
        }
    }

    private async Task EnsurePlayerAsync()
    {
        if (_player is not null)
        {
            return;
        }

        await _playerLock.WaitAsync();
        try
        {
            if (_player is not null)
            {
                return;
            }

            try
            {
                var stream = await FileSystem.OpenAppPackageFileAsync("theme.mp3");
                _player = _audioManager.CreatePlayer(stream);
                _player.Loop = true;
                _player.Volume = 0.35;
            }
            catch (FileNotFoundException)
            {
                // The theme file is optional; if it's not present we silently ignore playback.
                _player = null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create background music player.");
        }
        finally
        {
            _playerLock.Release();
        }
    }
}
