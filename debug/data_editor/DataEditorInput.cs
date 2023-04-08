using Godot;
// using System;

public partial class DataEditorInput : Control
{
    public Data Data;
    public string DataProperty;

    private Label _label;
    private LineEdit _lineEdit;

    public override void _Ready()
    {
        _label = GetNode<Label>("Label");
        _label.Text = DataProperty;

        _lineEdit = GetNode<LineEdit>("LineEdit");
        GD.Print(DataProperty + " // " + Data.ResourceName);
        _lineEdit.Text = (string)Data.Get(DataProperty);
        _lineEdit.TextChanged += onLineEditTextChanged;
    }

    private void onLineEditTextChanged(string newText)
    {
        Data.Set(DataProperty, newText);
    }
}
