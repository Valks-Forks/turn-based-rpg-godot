namespace TurnBasedRPG;

public partial class GameData : Resource
{
    [Export]
    public CharacterData CharacterData = new();

    [Export]
    public string World = "World0";
}
