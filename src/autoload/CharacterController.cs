namespace TurnBasedRPG;

public partial class CharacterController : Node
{
    private Character _activeCharacter;

    public override void _Ready()
    {
        GetNode<GameDataController>("/root/GameDataController").GameLoaded += onGameDataControllerGameLoaded;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("PrimaryAction"))
        {
            _activeCharacter.Position = _activeCharacter.GetGlobalMousePosition();
        }

        // PrimaryAction in FreeRoam - Move
        // PrimaryAction in Battle - Do Action
    }

    private void onGameDataControllerGameLoaded()
    {
        _activeCharacter = GetNode<Character>("/root/Main/World/Character");
    }
}
