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

        long modifiedTime = (long)FileAccess.GetModifiedTime(GameData.ResourcePath);
        string date = Time.GetDatetimeStringFromUnixTime(modifiedTime);
        date = date.Replace("T", " ");
        GetNode<Label>("Date").Text = date;
    }

    private void onButtonPressed() =>
        EmitSignal(nameof(Pressed), GameData);

    private void onDeleteButtonPressed() =>
        EmitSignal(nameof(DeletePressed), GameData);
}
