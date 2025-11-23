using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public struct Path
{
    private Dictionary<Vector2,Vector2> _pathPoints;
    private Dictionary<(Vector2, Vector2), float> _edgesCost ;
    private List<Vector2> _points;

    private float _pathCost;
    private Vector2 _start;
    private Vector2 _end;

    public float Cost => _pathCost;

    public IReadOnlyList<Vector2> PathPoints => _points;

    public Path(Dictionary<int,List<(int, float)>> edges, List<int> pathPointsIndex, List<Vector2> points)
    {
        if (edges.Count < 2)
            throw new ArgumentException("path");

        if (pathPointsIndex.Count < 2)
            throw new ArgumentException(nameof(pathPointsIndex), $"{pathPointsIndex.Count}");

        if (pathPointsIndex is null)
            throw new ArgumentNullException(nameof(pathPointsIndex));

        if (edges == null)
            throw new ArgumentNullException("path");

        _pathPoints = new Dictionary<Vector2, Vector2>();
        _edgesCost = new Dictionary<(Vector2, Vector2), float>();
        _points = new List<Vector2>();

        _pathCost = 0;

        float edgeCost;

        _points = new List<Vector2>();
        _start = points[pathPointsIndex[0]];
        _end = points[pathPointsIndex[pathPointsIndex.Count - 1]];

        foreach (int index in pathPointsIndex)
        {
            _points.Add(points[index]);
        }

        for (int i = 1; i < pathPointsIndex.Count; i++)
        {
            edgeCost = edges[pathPointsIndex[i - 1]].First(index => index.Item1 == pathPointsIndex[i]).Item2;

            _pathPoints.Add(points[pathPointsIndex[i - 1]], points[pathPointsIndex[i]]);
            _edgesCost.Add((points[pathPointsIndex[i - 1]], points[pathPointsIndex[i]]), edgeCost);

            _pathCost += edgeCost;
        }
    }

    private Path(List<Vector2> points,Dictionary<Vector2, Vector2> pathPoints, Dictionary<(Vector2, Vector2), float> edgesCost, float pathCost,Vector2 start,Vector2 end)
    {
        _points = points;
        _edgesCost = new Dictionary<(Vector2, Vector2), float>();
        _pathPoints = new Dictionary<Vector2, Vector2>();

        _pathCost = pathCost;
        _start = start;
        _end = end;

        foreach (var pathEdge in pathPoints)
        {
            _pathPoints.Add(pathEdge.Value, pathEdge.Key);
            _edgesCost.Add((pathEdge.Value, pathEdge.Key), edgesCost[(pathEdge.Key, pathEdge.Value)]);
        }
    }

    public Path Reverse()
    {
        return new Path(_points,_pathPoints,_edgesCost,_pathCost,_end,_start);
    }

    public bool GetNextPathPoint(Vector2 lastPoint, out Vector2 point)
    {
        point = Vector2.zero;

        if(_pathPoints.TryGetValue(lastPoint,out Vector2 nextPoint))
        {
            point = nextPoint;
            return true;
        }

        return false;
    }

    public float GetEdgeCost(Vector2 edgeStartPoint, Vector2 edgeEndPoint)
    {
        if(_edgesCost.TryGetValue((edgeStartPoint,edgeEndPoint),out float cost))
            return cost;

        throw new System.ArgumentException();
    }

    public static Path operator +(Path firstPath,Path seconsPath)
    {
        if (firstPath._end != seconsPath._start)
            throw new ArgumentException();

        foreach(var points in seconsPath._pathPoints)
        {
            firstPath._pathPoints.Add(points.Key,points.Value);
        }

        foreach(Vector2 point in seconsPath._points)
        {
            firstPath._points.Add(point);
        }

        foreach(var edges in seconsPath._edgesCost)
        {
            firstPath._edgesCost.Add(edges.Key,edges.Value);
        }

        firstPath._end = seconsPath._end;
        firstPath._pathCost += seconsPath._pathCost;

        return firstPath;
    }
}