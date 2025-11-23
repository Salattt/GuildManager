using System.Collections.Generic;
using UnityEngine;
using MapCfg;
using MapShape;

public abstract class TurnebleShape : Shape
{
    private float _angle;
    private Vector2 _position;
    private float _sin;
    private float _cos;
    protected float ScaleToMap;
    public Vector2 Position => _position;
    public int Level { get; private set; }


    protected TurnebleShape(int level, float angle, Vector2 position, float scaleToMap) 
    {
        Level = level;
        _angle = angle;
        _position = new Vector2(position.x, position.y * IslandShape.GenerateMaxYfromX(position.x)) * HalfMapSize;
        _sin = Mathf.Sin(Mathf.Deg2Rad * _angle);
        _cos = Mathf.Cos(Mathf.Deg2Rad * _angle);
        ScaleToMap = scaleToMap;
    }

    public override bool CheckPointInclude(Vector2 point)
    {
        return CheckPointIncludeInLocalCoordinates(GetPointLocalPosition(point));
    }

    public override List<Vector2> GetShapeStructurePoints()
    {
        List<Vector2> points = GetShapeStructurePointsInLocalCoordinates();

        for (int i = 0; i < points.Count; i++)
        {
            points[i] = GetPointGlobalPosition(points[i]);
        }

        return points;
    }

    protected Vector2 GetPointLocalPosition(Vector2 point)
    {
        
        Vector2 localPosition = point - _position;
        localPosition = new Vector2(localPosition.x * _cos - localPosition.y * _sin, localPosition.x * _sin + localPosition.y * _cos);

        return localPosition;
    }

    protected Vector2 GetPointGlobalPosition(Vector2 point) 
    {
        Vector2 globalPosition = point;
        globalPosition = new Vector2(globalPosition.x * _cos + globalPosition.y * _sin, -globalPosition.x * _sin + globalPosition.y * _cos);
        globalPosition = globalPosition + _position;

        return globalPosition;
    }

    protected bool IsEquals(float a, float b)
    {
        return Mathf.Abs(Mathf.Abs(a) - Mathf.Abs(b)) < MapConfig.MaxInaccurace;
    }

    protected abstract bool CheckPointIncludeInLocalCoordinates(Vector2 pointInLocalCoordinates);
    protected abstract List<Vector2> GetShapeStructurePointsInLocalCoordinates();
}
