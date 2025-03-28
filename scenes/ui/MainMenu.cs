using Godot;

namespace Game.UI;

public partial class MainMenu : Node
{
    private Button playButton;
    private Control mainMenuContainer;
    private LevelSelectScreen levelSelectScreen;
    private Button quitButton;

    public override void _Ready()
    {
        playButton = GetNode<Button>("%PlayButton");
        quitButton = GetNode<Button>("%QuitButton");
        mainMenuContainer = GetNode<Control>("%MainMenuContainer");
        levelSelectScreen = GetNode<LevelSelectScreen>("%LevelSelectScreen");

        mainMenuContainer.Visible = true;
        levelSelectScreen.Visible = false;

        playButton.Pressed += OnPlayButtonPressed;
        quitButton.Pressed += OnQuitButtonPressed;
        levelSelectScreen.BackPressed += OnLevelSelectBackPressed;
    }

    private void OnPlayButtonPressed()
    {
        mainMenuContainer.Visible = false;
        levelSelectScreen.Visible = true;
    }

    private void OnLevelSelectBackPressed()
    {
        mainMenuContainer.Visible = true;
        levelSelectScreen.Visible = false;
    }

    private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }
}
