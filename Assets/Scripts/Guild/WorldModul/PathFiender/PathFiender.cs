using System.Collections.Generic;
using PathCfg;
using UnityEngine;
using System;
using System.Linq;

public class PathFiender 
{
    private Dictionary<int, Sector> _sectors;
    private Dictionary<Vector2, int> _pointsOnEdgesIndexes;
    private List<Vector2> _pointsOnEdges;

    private SectorPathGenerator _sectorPathGenerator;
    private GlobalPathGenerator _globalPathGenerator;

    public PathFiender(BiomLayer biomLayer, List<Vector2> worldObjectPositions)
    {
        _sectorPathGenerator = new SectorPathGenerator(biomLayer);
        _globalPathGenerator = new GlobalPathGenerator();

        _sectors = new SectorGenerator().FullSectorDictionary();
        FullPointsOnEdgesList();

        List<Vector2> points = worldObjectPositions;
        
        foreach (Vector2 position in biomLayer.GetBiomsStructurePoints())
        {
            points.Add(position);
        }

        AddPoints(points);
    }

    public void AddPoints(List<Vector2> newPoints)
    {
        foreach (var points in SortPoints(newPoints))
        {
            AddPointsToSector(points.Key,points.Value);
        }
    }

    public void Generate()
    {
        _globalPathGenerator.GeneratePaths(_sectors,_pointsOnEdgesIndexes,_pointsOnEdges);
    }

    private void FullPointsOnEdgesList()
    {
        _pointsOnEdges = new List<Vector2>();
        _pointsOnEdgesIndexes = new Dictionary<Vector2, int>();

        foreach (var sector in _sectors.Values) 
        {
            _pointsOnEdges.AddRange(sector.LeftEdgePoints);
        }

        _pointsOnEdges.AddRange(_sectors[_sectors.Keys.Max()].RightEdgePoints);

        for (int i = 0; i < _pointsOnEdges.Count; i++)
        {
            _pointsOnEdgesIndexes.Add(_pointsOnEdges[i], i);
        }
    }

    private Dictionary<int, List<Vector2>> SortPoints(List<Vector2> points)
    {
        Dictionary<int, List<Vector2>> sortedPoints= new Dictionary<int, List<Vector2>>();

        int maxSectorIndex = _sectors.Keys.Max();

        foreach (Vector2 point in points)
        {
            int index = GetPointSector(point);

            if (sortedPoints.ContainsKey(index) == false)
            {
                if (_sectors.ContainsKey(index) == false)
                {
                    if ((index - 1) == maxSectorIndex)
                        index--;
                    else
                        throw new ArgumentOutOfRangeException(nameof(point), $"{point.x} {point.y}");
                }

                if (sortedPoints.ContainsKey(index) == false)
                    sortedPoints.Add(index, new List<Vector2>());
            }

            sortedPoints[index].Add(point);
        }

        return sortedPoints;
    }

    private int GetPointSector(Vector2 point)
    {
        return Mathf.FloorToInt(point.x / PathConfig.SectorEdgeLength);
    }

    private void AddPointsToSector(int index, List<Vector2> points)
    {
        _sectors[index].AddPoints(points,_sectorPathGenerator);
    }
}
