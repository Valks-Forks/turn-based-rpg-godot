using Godot;

public partial class MainMenuScreen : Screen
{
    private PackedScene _loadGameScreen = GD.Load<PackedScene>("res://src/scenes/ui/screens/load_game/LoadGameScreen.tscn");
    private PackedScene _newGameScreen = GD.Load<PackedScene>("res://src/scenes/ui/screens/new_game/NewGameScreen.tscn");

    private void onLoadGameButtonPressed()
    {
        UI.SetScreen(_loadGameScreen.Instantiate<Screen>());
    }

    private void onNewGameButtonPressed()
    {
        UI.SetScreen(_newGameScreen.Instantiate<Screen>());
    }
}
