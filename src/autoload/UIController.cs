namespace TurnBasedRPG;

using Godot.Collections;
using System.Collections;

public partial class UIController : Node
{
    private Screen _activeScreen;
    private Stack _screenStack = new();
    private Dictionary<string, PackedScene> _screens = new()
    {
        {"MainMenu", GD.Load<PackedScene>("res://src/scenes/ui/screens/main_menu/MainMenuScreen.tscn")},
        {"NewGame", GD.Load<PackedScene>("res://src/scenes/ui/screens/new_game/NewGameScreen.tscn")},
        {"LoadGame", GD.Load<PackedScene>("res://src/scenes/ui/screens/load_game/LoadGameScreen.tscn")},
        {"InGame", GD.Load<PackedScene>("res://src/scenes/ui/screens/in_game/InGameScreen.tscn")},
    };
    private Node _ui;

    public override void _Ready()
    {
        _ui = GetNode("/root/Main/UI");
        _activeScreen = _ui.GetChild<Screen>(0);
    }

    public void SetScreen(string screenName)
    {
        if (!_screens.ContainsKey(screenName))
        {
            GD.Print($"UIController: screen does not exist ({screenName})");
            return;
        }

        _screenStack.Push(_activeScreen);
        _activeScreen.Hide();
        _activeScreen = _screens[screenName].Instantiate<Screen>();
        _ui.AddChild(_activeScreen);

        GD.Print($"UIController: set screen ({screenName})");
    }

    public void Back()
    {
        _activeScreen.QueueFree();
        _activeScreen = (Screen)_screenStack.Pop();
        _activeScreen.Show();

        GD.Print($"UIController: back to screen ({_activeScreen.Name})");
    }

    public void Refresh()
    {
        _activeScreen.Refresh();

        GD.Print($"UIController: refreshed screen ({_activeScreen.Name})");
    }
}
