using Godot;
using Godot.Collections;

// CharacterController
// handles character actions
public partial class CharacterController : Controller
{
    private Character _activeCharacter;
    private Action _activeCharacterAction;
    private TileMap _tileMap;

    public override void Run()
    {
        _tileMap = GetNode<TileMap>("/root/Main/World/TileMap");
        Node characters = GetNode("/root/Main/World/Characters");
        characters.ChildEnteredTree += onCharactersChildEnteredTree;

        foreach (Character character in characters.GetChildren())
        {
            character.TurnStarted += onCharacterTurnStarted;
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (_activeCharacter == null)
        {
            return;
        }

        if (@event.IsActionPressed("PrimaryAction"))
        {
            ActionProperties properties = getActionProperties();
            Stat actionPoints = _activeCharacter.GetNode<Stat>("Stats/ActionPoints");
            int actionCost = _activeCharacterAction.Cost(properties);

            if (actionCost > actionPoints.Value)
            {
                GD.Print($"failed to do action ({_activeCharacterAction.Name})");
                return;
            }

            _activeCharacterAction.Do(properties);

            // remove action cost from action points value
            actionPoints.Value -= actionCost;
        }
        else if (@event.IsActionPressed("EndTurn"))
        {
            _activeCharacter.EndTurn();
        }
    }

    private ActionProperties getActionProperties()
    {
        ActionProperties properties = new ActionProperties();
        properties.Character = _activeCharacter;
        properties.GridPosition = _tileMap.LocalToMap(_tileMap.GetGlobalMousePosition());
        properties.WorldPosition = _tileMap.MapToLocal((Vector2I)properties.GridPosition);

        return properties;
    }

    private void onCharactersChildEnteredTree(Node node)
    {
        ((Character)node).TurnStarted += onCharacterTurnStarted;
    }

    private void onCharacterTurnStarted(Character character)
    {
        _activeCharacter = character;
        _activeCharacterAction = _activeCharacter.GetNode("Actions").GetChild<Action>(0);
    }
}
