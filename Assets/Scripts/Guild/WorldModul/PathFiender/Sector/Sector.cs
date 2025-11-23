using PathCfg;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sector 
{
    private Dictionary<(int, int), Path> _paths;
    private Dictionary<int, List<(int, float)>> _pointConnections;
    private Dictionary<Vector2, int> _pointIndexes;
    public List<Vector2> _points;
    private List<Vector2> _leftEdgePoints;
    private List<Vector2> _rightEdgePoints;

    public IReadOnlyList<Vector2> RightEdgePoints => _rightEdgePoints;
    public IReadOnlyList<Vector2> LeftEdgePoints => _leftEdgePoints;

    public Sector(List<Vector2> leftEdgePoints, List<Vector2> rightEdgePoints)
    {
        _points = new List<Vector2>();
        _leftEdgePoints = leftEdgePoints;
        _rightEdgePoints = rightEdgePoints;

        _points.AddRange(leftEdgePoints);
        _points.AddRange(rightEdgePoints);
    }

    public void AddPoints(List<Vector2> points,SectorPathGenerator pathGenerator)
    {
        if(_paths == null)
        {
            _points.AddRange(points);
            UpdatePathIndexes();
            CalculatePathFromZero(pathGenerator);
            return;
        }
    }

    public Path GetPath(Vector2 start,Vector2 finish)
    {
        return _paths[(_pointIndexes[start],_pointIndexes[finish])];
    }

    private void CalculatePathFromZero(SectorPathGenerator pathGenerator)
    {
        _paths = pathGenerator.GeneratePathsInSector(_points, out Dictionary<int,List<(int, float)>> connections);
        _pointConnections = connections;
    }

    private void UpdatePathIndexes()
    {
        _pointIndexes = new Dictionary<Vector2, int>();

        for (int i = 0; i < _points.Count; i++)
        {
            if(_pointIndexes.ContainsKey(_points[i]) == false)
                _pointIndexes.Add(_points[i], i);
        }
    }
}
