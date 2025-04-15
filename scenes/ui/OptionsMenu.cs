using Game.Autoload;
using Godot;

namespace Game.UI;

public partial class OptionsMenu : CanvasLayer
{
    private const string SFX_BUS_NAME = "SFX";
    private const string MUSIC_BUS_NAME = "Music";

    [Signal]
    public delegate void DonePressedEventHandler();

    private Button musicUpButton;
    private Button musicDownButton;
    private Label musicLabel;

    private Button sfxUpButton;
    private Button sfxDownButton;
    private Label sfxLabel;

    private Button windowButton;
    private Button doneButton;

    public override void _Ready()
    {
        musicUpButton = GetNode<Button>("%MusicUpButton");
        musicDownButton = GetNode<Button>("%MusicDownButton");
        musicLabel = GetNode<Label>("%MusicLabel");

        sfxUpButton = GetNode<Button>("%SFXUpButton");
        sfxDownButton = GetNode<Button>("%SFXDownButton");
        sfxLabel = GetNode<Label>("%SFXLabel");

        windowButton = GetNode<Button>("%WindowButton");
        doneButton = GetNode<Button>("%DoneButton");

        AudioHelpers.RegisteredButtons(
            new Button[]
            {
                sfxUpButton,
                sfxDownButton,
                musicUpButton,
                musicDownButton,
                windowButton,
                doneButton,
            }
        );

        UpdateDisplay();

        musicUpButton.Pressed += () =>
        {
            ChangeBusVolume(MUSIC_BUS_NAME, .1f);
        };

        musicDownButton.Pressed += () =>
        {
            ChangeBusVolume(MUSIC_BUS_NAME, -.1f);
        };

        sfxUpButton.Pressed += () =>
        {
            ChangeBusVolume(SFX_BUS_NAME, .1f);
        };

        sfxDownButton.Pressed += () =>
        {
            ChangeBusVolume(SFX_BUS_NAME, -.1f);
        };

        windowButton.Pressed += OnWindowButtonPressed;
        doneButton.Pressed += OnDoneButtonPressed;
    }

    private void UpdateDisplay()
    {
        sfxLabel.Text = Mathf
            .Round(OptionsHelper.GetBudVolumePercent(SFX_BUS_NAME) * 10)
            .ToString();

        musicLabel.Text = Mathf
            .Round(OptionsHelper.GetBudVolumePercent(MUSIC_BUS_NAME) * 10)
            .ToString();

        windowButton.Text = OptionsHelper.IsFullscreen() ? "Fullscreen" : "Windowed";
    }

    private void ChangeBusVolume(string busName, float change)
    {
        var busVolumePercent = OptionsHelper.GetBudVolumePercent(busName);
        busVolumePercent = Mathf.Clamp(busVolumePercent + change, 0, 1);
        OptionsHelper.SetBusVolumePercent(busName, busVolumePercent);
        UpdateDisplay();
    }

    private void OnWindowButtonPressed()
    {
        OptionsHelper.ToggleWindowMode();
        UpdateDisplay();
    }

    private void OnDoneButtonPressed()
    {
        EmitSignal(SignalName.DonePressed);
    }
}
