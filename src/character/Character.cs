using Godot;
using Godot.Collections;

public partial class Character : Node2D
{
    [Signal]
    public delegate void TurnStartedEventHandler(Character character);

    [Signal]
    public delegate void TurnEndedEventHandler(Character character);

    [Export]
    public Dictionary<string, Stat> Stats = new Dictionary<string, Stat>();

    public void StartTurn()
    {
        EmitSignal(nameof(TurnStarted), this);
    }

    public void EndTurn()
    {
        EmitSignal(nameof(TurnEnded), this);
    }
}
