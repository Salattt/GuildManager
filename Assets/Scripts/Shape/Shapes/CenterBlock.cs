using System.Collections.Generic;
using UnityEngine;

public class CenterBlock : Shape
{
    private float _mapWidthPercent = 5;

    public override bool CheckPointInclude(Vector2 point)
    {
        if (Mathf.Abs(point.x) <= MapSize / 100 * _mapWidthPercent && Mathf.Abs(point.y) >= 1)
            return true;

        return false;
    }

    public override List<Vector2> GetShapeStructurePoints()
    {
        return new List<Vector2> {new Vector2(0,0), new Vector2(MapSize / 100 * _mapWidthPercent, 0), new Vector2(-(MapSize / 100 * _mapWidthPercent),0)};
    }
}
