using Godot;

public partial class Character : Node2D
{
    [Signal]
    public delegate void TurnStartedEventHandler(Character character);

    [Signal]
    public delegate void TurnEndedEventHandler(Character character);
}
