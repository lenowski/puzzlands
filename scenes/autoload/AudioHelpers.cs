using System.Collections.Generic;
using Godot;

namespace Game.Autoload;

public partial class AudioHelpers : Node
{
    private static AudioHelpers instance;

    private AudioStreamPlayer musicAudioStreamPlayer;
    private AudioStreamPlayer explosionAudioStreamPlayer;
    private AudioStreamPlayer clickAudioStreamPlayer;
    private AudioStreamPlayer victoryAudioStreamPlayer;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            instance = this;
        }
    }

    public override void _Ready()
    {
        musicAudioStreamPlayer = GetNode<AudioStreamPlayer>("MusicAudioStreamPlayer");
        explosionAudioStreamPlayer = GetNode<AudioStreamPlayer>("ExplosionAudioStreamPlayer");
        clickAudioStreamPlayer = GetNode<AudioStreamPlayer>("ClickAudioStreamPlayer");
        victoryAudioStreamPlayer = GetNode<AudioStreamPlayer>("VictoryAudioStreamPlayer");

        musicAudioStreamPlayer.Finished += OnMusicFinished;
    }

    public static void PlayBuildingDestruction()
    {
        instance.explosionAudioStreamPlayer.Play();
    }

    public static void PlayVictory()
    {
        instance.victoryAudioStreamPlayer.Play();
    }

    public static void RegisteredButtons(IEnumerable<Button> buttons)
    {
        foreach (var button in buttons)
        {
            button.Pressed += instance.OnButtonPressed;
        }
    }

    private void OnButtonPressed()
    {
        clickAudioStreamPlayer.Play();
    }

    private void OnMusicFinished()
    {
        GetTree().CreateTimer(5).Timeout += OnMusicDelayTimerTimeout;
    }

    private void OnMusicDelayTimerTimeout()
    {
        musicAudioStreamPlayer.Play();
    }
}
