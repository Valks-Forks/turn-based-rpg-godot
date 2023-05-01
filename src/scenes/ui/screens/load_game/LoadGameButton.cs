namespace TurnBasedRPG;

public partial class LoadGameButton : Control
{
    [Signal]
    public delegate void DeletePressedEventHandler(GameData gameData);

    [Signal]
    public delegate void PressedEventHandler(GameData gameData);

    public GameData GameData = new();

    public override void _Ready()
    {
        GetNode<Label>("Title").Text = GameData.ResourceName;

        var modifiedTime = (long)FileAccess.GetModifiedTime(GameData.ResourcePath);
        var date = Time.GetDatetimeStringFromUnixTime(modifiedTime);
        date = date.Replace("T", " ");
        GetNode<Label>("Date").Text = date;
    }

    private void onButtonPressed() =>
        EmitSignal(nameof(Pressed), GameData);

    private void onDeleteButtonPressed() =>
        EmitSignal(nameof(DeletePressed), GameData);
}
