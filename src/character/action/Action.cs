using Godot;
using Godot.Collections;

public abstract partial class Action : Node
{
    [Export]
    private int _cost;

    public abstract void Do(ActionProperties properties);

    public int Cost(ActionProperties properties)
    {
        return _cost;
    }
}