using Godot;
using System;

public partial class GameDataController : Node
{
    [Signal]
    public delegate void GameLoadedEventHandler();

    private Character _activeCharacter;
    private GameData _activeGameData;

    public async void LoadGame(GameData gameData)
    {
        GD.Print($"GameDataController: loading game ({gameData.ResourceName})");

        _activeGameData = gameData;
        Node existingWorld = GetTree().GetFirstNodeInGroup("World");

        if (existingWorld != null)
        {
            existingWorld.QueueFree();
            await ToSignal(existingWorld, "tree_exited");
        }

        PackedScene world = GD.Load<PackedScene>($"res://src/scenes/world/worlds/{gameData.World}.tscn");
        Node2D worldInstance = world.Instantiate<Node2D>();

        // load character
        PackedScene character = GD.Load<PackedScene>("res://src/character/Character.tscn");
        _activeCharacter = character.Instantiate<Character>();
        _activeCharacter.GetNode<Sprite2D>("Sprite2D").Modulate = gameData.CharacterData.Color;
        _activeCharacter.GetNode<Sprite2D>("Sprite2D").Texture = gameData.CharacterData.Texture;
        _activeCharacter.Position = gameData.CharacterData.Position;

        // add character to world
        worldInstance.AddChild(_activeCharacter);
        GetNode("/root/Main").AddChild(worldInstance);

        // add in game screen to ui
        GetNode<UIController>("/root/UIController").SetScreen("InGame");
        EmitSignal(nameof(GameLoaded));
    }

    public void SaveGame()
    {
        GD.Print($"GameDataController: saving game ({_activeGameData.ResourceName})");

        _activeGameData.CharacterData.Position = _activeCharacter.Position;
        ResourceSaver.Save(_activeGameData, _activeGameData.ResourcePath);
    }

    public void DeleteGame(GameData gameData)
    {
        GD.Print($"GameDataController: deleting game ({gameData.ResourceName})");

        DirAccess.RemoveAbsolute(gameData.ResourcePath);
    }
}
