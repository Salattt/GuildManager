using System.Collections.Generic;
using UnityEngine;
using MapCfg;

public class Circle : TurnebleShape
{
    public float _radius;

    public Circle(int level, float angle, Vector2 position, float scaleToMap) : base(level, angle, position, scaleToMap)
    {
        _radius = MapSize * ScaleToMap;
    }

    protected override bool CheckPointIncludeInLocalCoordinates(Vector2 pointInLocalCoordinates)
    {
        if(Mathf.Pow(_radius,2) > pointInLocalCoordinates.sqrMagnitude || IsEquals(Mathf.Pow(_radius, 2), pointInLocalCoordinates.sqrMagnitude))
            return true;

        return false;
    }

    protected override List<Vector2> GetShapeStructurePointsInLocalCoordinates()
    {
        IReadOnlyList<float[]> sinCosList = SinCos.GetSinCos();
        List<Vector2> structurePoints = new List<Vector2>();

        foreach(var sinCos in sinCosList)
        {
            structurePoints.Add(new Vector2(_radius * sinCos[1], _radius * sinCos[0]) * (1-MapConfig.MaxInaccurace));
        }

        return structurePoints;
    }
}
