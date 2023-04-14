using Godot;
using Godot.Collections;

public abstract partial class Range : Resource
{
    [Export]
    public int Value
    {
        get { return _value; }
        set { setValue(value); }
    }

    private int _value;

    public abstract Array<Vector2> GetCells();

    private void setValue(int value)
    {
        _value = value;
    }
}
