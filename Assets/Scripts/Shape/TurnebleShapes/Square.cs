using System.Collections.Generic;
using UnityEngine;

public class Square : TurnebleShape
{
    public Square(int level, float angle, Vector2 position, float scaleToMap) : base(level, angle, position, scaleToMap)
    {
    }

    protected override bool CheckPointIncludeInLocalCoordinates(Vector2 pointInLocalCoordinates)
    {
        if((Mathf.Abs(pointInLocalCoordinates.x) < MapSize * ScaleToMap || IsEquals(Mathf.Abs(pointInLocalCoordinates.x), MapSize * ScaleToMap))
            && (Mathf.Abs(pointInLocalCoordinates.y) <= MapSize * ScaleToMap) || IsEquals(Mathf.Abs(pointInLocalCoordinates.y), MapSize * ScaleToMap))
            return true;

        return false;
    }

    protected override List<Vector2> GetShapeStructurePointsInLocalCoordinates()
    {
        return new List<Vector2> {new Vector2(MapSize * ScaleToMap, MapSize * ScaleToMap), new Vector2(MapSize * -ScaleToMap, MapSize * ScaleToMap),
        new Vector2(MapSize * -ScaleToMap, MapSize * -ScaleToMap),new Vector2(MapSize * ScaleToMap, MapSize * -ScaleToMap)};
    }
}
