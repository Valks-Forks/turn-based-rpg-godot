namespace TurnBasedRPG;

public partial class CharacterData : Resource
{
    [Export]
    public Color Color = Colors.White;

    [Export]
    public Vector2 Position = Vector2.Zero;

    [Export]
    public Texture2D Texture = GD.Load<Texture2D>("res://assets/textures/characters/circle.svg");
}
