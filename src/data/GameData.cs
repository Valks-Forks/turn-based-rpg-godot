namespace TurnBasedRPG;

public partial class GameData : Resource
{
    [Export]
    public CharacterData CharacterData = new CharacterData();

    [Export]
    public string World = "World0";
}
