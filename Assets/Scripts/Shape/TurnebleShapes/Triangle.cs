using System.Collections.Generic;
using UnityEngine;

public class Triangle : TurnebleShape
{
    private static float _sqrtFromTwo = 1.41421356237f;
    private float _hypotenuse;

    public Triangle(int level, float angle, Vector2 position, float scaleToMap) : base(level, angle, position, scaleToMap)
    {
        _hypotenuse = MapSize * ScaleToMap * _sqrtFromTwo;
    }

    protected override bool CheckPointIncludeInLocalCoordinates(Vector2 pointInLocalCoordinates)
    {
        if((pointInLocalCoordinates.y > 0  || IsEquals(pointInLocalCoordinates.y,0)) 
            && (-(Mathf.Abs(pointInLocalCoordinates.x)) + _hypotenuse / 2 > pointInLocalCoordinates.y || 
            IsEquals(-(Mathf.Abs(pointInLocalCoordinates.x)) + _hypotenuse / 2, pointInLocalCoordinates.y))
            && (Mathf.Abs(pointInLocalCoordinates.x) <= _hypotenuse / 2) || IsEquals(Mathf.Abs(pointInLocalCoordinates.x), _hypotenuse / 2))
        {
            return true;
        }

        return false;
    }

    protected override List<Vector2> GetShapeStructurePointsInLocalCoordinates()
    {
        return new List<Vector2> {new Vector2(_hypotenuse / 2, 0), new Vector2(-(_hypotenuse / 2), 0), new Vector2(0, _hypotenuse / 2) };
    }
}
