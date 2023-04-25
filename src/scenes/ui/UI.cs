using Godot;
using System.Collections;

public partial class UI : CanvasLayer
{
    private Screen _activeScreen;
    private Stack _screens = new Stack();

    public override void _Ready()
    {
        _activeScreen = GetChild<Screen>(0);
    }

    public void SetScreen(Screen screen)
    {
        GD.Print($"UI: setting screen ({screen.Name})");
        _screens.Push(_activeScreen);
        _activeScreen.Hide();
        _activeScreen = screen;

        AddChild(_activeScreen);
    }

    public void Back()
    {
        GD.Print($"UI: back to screen ({_activeScreen.Name})");
        _activeScreen.QueueFree();
        _activeScreen = (Screen)_screens.Pop();
        _activeScreen.Show();
    }

    public void Refresh()
    {
        GD.Print($"UI: refreshing screen ({_activeScreen.Name})");
        _activeScreen.Refresh();
    }
}
