using Godot;
using System;

public partial class InGameScreen : Screen
{
    private PackedScene _loadGameScreen = GD.Load<PackedScene>("res://src/scenes/ui/screens/load_game/LoadGameScreen.tscn");
    private PackedScene _mainMenuScreen = GD.Load<PackedScene>("res://src/scenes/ui/screens/main_menu/MainMenuScreen.tscn");
    private Control _gameMenu;

    public override void _Ready()
    {
        base._Ready();
        _gameMenu = GetNode<Control>("GameMenu");
        _gameMenu.Visible = false;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("Exit"))
        {
            _gameMenu.Visible = !_gameMenu.Visible;
        }
    }

    private void onContinueButtonPressed()
    {
        _gameMenu.Visible = false;
    }

    private void onExitButtonPressed()
    {
        GetTree().GetFirstNodeInGroup("World").QueueFree();
        UI.SetScreen(_mainMenuScreen.Instantiate<Screen>());
    }

    private void onLoadGameButtonPressed()
    {
        UI.SetScreen(_loadGameScreen.Instantiate<Screen>());
    }
}
