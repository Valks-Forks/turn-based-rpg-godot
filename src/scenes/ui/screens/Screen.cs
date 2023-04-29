using Godot;

public partial class Screen : Control
{
    protected UIController UIController;

    public override void _Ready()
    {
        UIController = GetNode<UIController>("/root/UIController");
    }

    public virtual void Refresh()
    {
        // use this to refresh screen
    }
}
