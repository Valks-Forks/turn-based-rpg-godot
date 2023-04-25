using Godot;

public partial class Screen : Control
{
    protected UI UI;

    public override void _Ready()
    {
        UI = GetNode<UI>("/root/Main/UI");
    }

    public virtual void Refresh()
    {
        // use this to refresh screen
    }
}
