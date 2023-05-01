namespace TurnBasedRPG;

public partial class LoadGameScreen : Screen
{
    private PackedScene _loadGameButton = GD.Load<PackedScene>("res://src/scenes/ui/screens/load_game/LoadGameButton.tscn");

    public override void _Ready()
    {
        base._Ready();
        setupLoadGameButtons();
    }

    public override void Refresh() => setupLoadGameButtons();

    private void setupLoadGameButtons()
    {
        Node container = GetNode("Layout/LoadGameButtons/Layout");

        foreach (Node child in container.GetChildren())
        {
            child.QueueFree();
        }

        foreach (string file in DirAccess.GetFilesAt("data/game_data"))
        {
            LoadGameButton loadGameButtonInstance = _loadGameButton.Instantiate<LoadGameButton>();
            loadGameButtonInstance.GameData = GD.Load<GameData>($"data/game_data/{file}");
            loadGameButtonInstance.Pressed += onLoadGameButtonPressed;
            loadGameButtonInstance.DeletePressed += onLoadGameButtonDeletePressed;
            container.AddChild(loadGameButtonInstance);
        }
    }

    private void onBackButtonPressed() => UIController.Back();

    private void onLoadGameButtonPressed(GameData gameData) =>
        GetNode<GameDataController>("/root/GameDataController").LoadGame(gameData);

    private void onLoadGameButtonDeletePressed(GameData gameData)
    {
        GetNode<GameDataController>("/root/GameDataController").DeleteGame(gameData);
        UIController.Refresh();
    }
}
