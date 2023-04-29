using Godot;

public partial class LoadGameScreen : Screen
{
    private PackedScene _loadGameButton = GD.Load<PackedScene>("res://src/scenes/ui/screens/load_game/LoadGameButton.tscn");

    public override void _Ready()
    {
        base._Ready();
        setupLoadGameButtons();
    }

    public override void Refresh()
    {
        setupLoadGameButtons();
    }

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

    private void onBackButtonPressed()
    {
        UIController.Back();
    }

    private void onLoadGameButtonPressed(GameData gameData)
    {
        GD.Print($"LoadGameScreen: loading game ({gameData.ResourceName})");
        Node existingWorld = GetTree().GetFirstNodeInGroup("World");

        if (existingWorld != null)
        {
            existingWorld.QueueFree();
        }

        PackedScene world = GD.Load<PackedScene>($"res://src/scenes/world/worlds/{gameData.World}.tscn");
        Node2D worldInstance = world.Instantiate<Node2D>();

        // load character
        PackedScene character = GD.Load<PackedScene>("res://src/character/Character.tscn");
        Node2D characterInstance = character.Instantiate<Node2D>();
        characterInstance.GetNode<Sprite2D>("Sprite2D").Modulate = gameData.CharacterData.Color;
        characterInstance.GetNode<Sprite2D>("Sprite2D").Texture = gameData.CharacterData.Texture;
        characterInstance.Position = gameData.CharacterData.Position;

        // add character to world
        worldInstance.AddChild(characterInstance);
        GetNode("/root/Main").AddChild(worldInstance);

        // add in game screen to ui
        UIController.SetScreen("InGame");
    }

    private void onLoadGameButtonDeletePressed(GameData gameData)
    {
        GD.Print($"LoadGameScreen: deleting game ({gameData.ResourceName})");
        DirAccess.RemoveAbsolute(gameData.ResourcePath);
        UIController.Refresh();
    }
}
