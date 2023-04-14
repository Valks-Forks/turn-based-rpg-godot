using Godot;
using Godot.Collections;

public partial class LineRange : Range
{
    public override Array<Vector2> GetCells()
    {
        Array<Vector2> cells = new Array<Vector2>();

        for (int i = 1; i < Value + 1; i++)
        {
            cells.Add(Vector2.Up * i);
            cells.Add(Vector2.Down * i);
            cells.Add(Vector2.Left * i);
            cells.Add(Vector2.Right * i);
        }

        return cells;
    }
}
