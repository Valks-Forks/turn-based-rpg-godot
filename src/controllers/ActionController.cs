using Godot;

public partial class ActionController : Controller
{
    const int ActionLayer = 1;
    const int ActionSource = 1;

    private TileMap _tileMap;

    public override void Run()
    {
        _tileMap = GetNode<TileMap>("/root/Main/World/TileMap");
    }

    public void DoAction(Character character, Action action)
    {
        ActionProperties properties = getActionProperties(character);
        Stat actionPoints = character.GetNode<Stat>("Stats/ActionPoints");
        int actionCost = action.Cost(properties);
        int ActionLayer = 1;

        if (actionCost > actionPoints.Value)
        {
            GD.Print($"failed to do action ({action.Name}), not enough action points");
            return;
        }

        if (!_tileMap.GetUsedCells(ActionLayer).Contains((Vector2I)properties.GridPosition))
        {
            GD.Print($"failed to do action ({action.Name}), not within range");
            return;
        }

        action.Do(properties);

        // remove action cost from action points value
        actionPoints.Value -= actionCost;

        RefreshActionCells(character, action);
    }

    public void RefreshActionCells(Character character, Action action)
    {
        _tileMap.ClearLayer(ActionLayer);

        if (action.Cost(getActionProperties(character)) > character.GetNode<Stat>("Stats/ActionPoints").Value)
        {
            return;
        }

        Vector2 activeCharacterGridPosition = _tileMap.LocalToMap(character.Position);

        foreach (Vector2 cell in action.Range.GetCells())
        {
            Vector2I cellGridPosition = (Vector2I)(activeCharacterGridPosition + cell);
            _tileMap.SetCell(ActionLayer, cellGridPosition, ActionSource, Vector2I.Zero);
        }
    }

    private ActionProperties getActionProperties(Character character)
    {
        ActionProperties properties = new ActionProperties();
        properties.Character = character;
        properties.GridPosition = _tileMap.LocalToMap(_tileMap.GetGlobalMousePosition());
        properties.WorldPosition = _tileMap.MapToLocal((Vector2I)properties.GridPosition);

        return properties;
    }
}
