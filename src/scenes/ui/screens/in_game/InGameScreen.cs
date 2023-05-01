namespace TurnBasedRPG;

public partial class InGameScreen : Screen
{
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

    private void onSaveButtonPressed()
    {
        GetNode<GameDataController>("/root/GameDataController").SaveGame();
    }

    private void onExitButtonPressed()
    {
        GetTree().GetFirstNodeInGroup("World").QueueFree();
        UIController.SetScreen("MainMenu");
    }

    private void onLoadGameButtonPressed()
    {
        UIController.SetScreen("LoadGame");
    }
}
