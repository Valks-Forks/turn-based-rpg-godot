using Godot;
using System;

public partial class DataListItem : Control
{
    [Signal]
    public delegate void SelectedEventHandler(Data data);

    public Data Data = new Data();

    private Button _button;

    public override void _Ready()
    {
        _button = GetNode<Button>("Button");
        _button.Text = Data.ResourceName;
        _button.Pressed += onButtonPressed;
    }

    private void onButtonPressed()
    {
        EmitSignal("Selected", Data);
    }
}
