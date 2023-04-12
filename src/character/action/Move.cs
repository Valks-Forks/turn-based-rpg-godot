using Godot;

public partial class Move : Action
{
    public override void Do(ActionProperties properties)
    {
        properties.Character.Position = properties.WorldPosition;
    }
}
