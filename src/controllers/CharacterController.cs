using Godot;
using Godot.Collections;

// CharacterController
// handles character actions
public partial class CharacterController : Controller
{
    private ActionController _actionController;
    private Character _activeCharacter;
    private Action _activeCharacterAction;
    private TileMap _tileMap;

    public override void Run()
    {
        _actionController = GetNode<ActionController>("/root/Main/Controllers/ActionController");
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
            _actionController.DoAction(_activeCharacter, _activeCharacterAction);
        }
        else if (@event.IsActionPressed("EndTurn"))
        {
            _activeCharacter.EndTurn();
        }
    }

    private void onCharactersChildEnteredTree(Node node)
    {
        ((Character)node).TurnStarted += onCharacterTurnStarted;
    }

    private void onCharacterTurnStarted(Character character)
    {
        _activeCharacter = character;
        _activeCharacterAction = _activeCharacter.GetNode("Actions").GetChild<Action>(0);
        _actionController.RefreshActionCells(_activeCharacter, _activeCharacterAction);
    }
}
