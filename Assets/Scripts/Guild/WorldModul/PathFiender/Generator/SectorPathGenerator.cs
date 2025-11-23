using PathCfg;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SectorPathGenerator
{
    private BiomLayer _biomLayer; 
    private List<Vector2> _pathPoints = new List<Vector2>();
    private Dictionary<int, List<(int, float)>> _edges;

    private int _checkPointsPerStep = PathConfig.CheckPointsPerStep; 
    private float _defoultCost = PathConfig.DefoultCost; 
    private float _costPerTravelHardnessLvl = PathConfig.CostPerTraveHardnessLvl;
    private int _neighborsCount = PathConfig.NeighborCount;

    public SectorPathGenerator(BiomLayer biomLayer) 
    { 
        _biomLayer = biomLayer; 
    }

    public Dictionary<(int, int), Path> GeneratePathsInSector(List<Vector2> points, out Dictionary<int, List<(int, float)>> edges)
    {
        _pathPoints.Clear();

        _pathPoints.AddRange(VerifiPoints(points)); 

        List<int> pointIndexes = new List<int>();

        for (int i = 0; i < _pathPoints.Count; i++) 
        { 
            pointIndexes.Add(i);
        }

        _edges = GetEdgesFromPathPoints(pointIndexes);

        Dictionary<(int, int), Path> asd =GetPathsFromEdges( pointIndexes);
        Debug.Log($"{_pathPoints.Count * (_pathPoints.Count -1)}   {asd.Count}");
        edges = _edges;
        return asd;
    }

    private List<Vector2> VerifiPoints(List<Vector2> points)
    {
        for (int i = 0; i < points.Count; i++) 
        {
            for (int j = i + 1; j < points.Count; j++) 
            {
                if (points[i] == points[j])
                {
                    points.RemoveAt(j);
                    j--;
                }
            }
        }

        return points;
    }

    private Dictionary<(int, int), Path> GetPathsFromEdges( List<int> pointIndexes) 
    {
        Dictionary<(int, int), Path> paths = new Dictionary<(int, int), Path>();

        for (int i = 0; i < pointIndexes.Count - 1; i++) 
        {
            foreach (var path in GetAllPathsStartedInPoint(i,pointIndexes))
                paths.Add(path.Key, path.Value);
        }

        return paths; 
    }

    private Dictionary<(int, int), Path> GetAllPathsStartedInPoint(int startPointIndexIndex, List<int> pointIndexes) 
    { 
        Dictionary<(int, int), Path> finalPaths = new Dictionary<(int, int), Path>(); 
        Dictionary<int, float> smollestPathCostToPoint = new Dictionary<int, float>(); 
        Dictionary<int, List<int>> pathToPoint = new Dictionary<int, List<int>>();
        List<int> pointToUseIndex = new List<int>();
        Path newPath;

        int currentPointIndex;
        float newDist;

        for (int i = 0; i < pointIndexes.Count; i++)
        {
            pointToUseIndex.Add(pointIndexes[i]);
            smollestPathCostToPoint.Add(pointIndexes[i], float.PositiveInfinity);
            pathToPoint.Add(pointIndexes[i], new List<int>());
        }

        foreach (var index in _edges[pointIndexes[startPointIndexIndex]])
        {
            smollestPathCostToPoint[index.Item1] = index.Item2;
            pathToPoint[index.Item1] = new List<int> { pointIndexes[startPointIndexIndex], index.Item1};
        }

        smollestPathCostToPoint[pointIndexes[startPointIndexIndex]] = 0;
        pointToUseIndex.Remove(pointToUseIndex[startPointIndexIndex]);

        while (pointToUseIndex.Count > 0)
        {
            currentPointIndex = GetPointIndexWithSmollestPathCost(pointToUseIndex,smollestPathCostToPoint);

            if (smollestPathCostToPoint[currentPointIndex] == float.PositiveInfinity)
            {
                (int, int, float) newEdge = GenerateEdgeBetweenClosestPair(
                    smollestPathCostToPoint.Keys.Where(x => smollestPathCostToPoint[x] != float.PositiveInfinity).ToList(),pointToUseIndex);

                _edges[newEdge.Item1].Add((newEdge.Item2,newEdge.Item3));
                _edges[newEdge.Item2].Add((newEdge.Item1,newEdge.Item3));
                pathToPoint[newEdge.Item2].AddRange(pathToPoint[newEdge.Item1]);
                pathToPoint[newEdge.Item2].Add(newEdge.Item2);

                smollestPathCostToPoint[newEdge.Item2] = smollestPathCostToPoint[newEdge.Item1] + newEdge.Item3;
                currentPointIndex = newEdge.Item2;
            }

            pointToUseIndex.Remove(currentPointIndex);

            foreach (var neighbor in _edges[currentPointIndex])
            {
                newDist = neighbor.Item2 + smollestPathCostToPoint[currentPointIndex];

                if (newDist < smollestPathCostToPoint[neighbor.Item1])
                {
                    smollestPathCostToPoint[neighbor.Item1] = newDist;

                    pathToPoint[neighbor.Item1].Clear();
                    pathToPoint[neighbor.Item1].AddRange(pathToPoint[currentPointIndex]);
                    pathToPoint[neighbor.Item1].Add(neighbor.Item1);
                }
            }
        }

        for (int i = startPointIndexIndex + 1;i < pointIndexes.Count;i++)
        {
            newPath = new Path(_edges, pathToPoint[pointIndexes[i]],_pathPoints);

            finalPaths.Add((pointIndexes[startPointIndexIndex], pointIndexes[i]), newPath);
            finalPaths.Add((pointIndexes[i], pointIndexes[startPointIndexIndex]), newPath.Reverse());
        }

        return finalPaths; 
    }

    private int GetPointIndexWithSmollestPathCost(List<int> pointsIndexes, Dictionary<int, float> pathsCost)
    { 
        int pointWithSmollestPathCost;
        
        pointWithSmollestPathCost = pointsIndexes[0]; 

        foreach (int pointIndex in pointsIndexes) 
        { 
            if (pathsCost[pointWithSmollestPathCost] > pathsCost[pointIndex]) 
                pointWithSmollestPathCost = pointIndex; 
        }

        return pointWithSmollestPathCost; 
    }

    private (int,int,float) GenerateEdgeBetweenClosestPair(List<int> firstGroup,List<int> secondGroup)
    {
        float minDistance = float.PositiveInfinity;
        float newDistance;
        int firstIndex = 0;
        int secondIndex = 0;

        foreach (int firstGroupIndex in firstGroup)
        {
            foreach(int secondGroupIndex in secondGroup)
            {
                newDistance = (_pathPoints[firstGroupIndex] - _pathPoints[secondGroupIndex]).sqrMagnitude;

                if (newDistance < minDistance)
                {
                    firstIndex = firstGroupIndex;
                    secondIndex = secondGroupIndex;
                    minDistance = newDistance;
                }
            }
        }

        return (firstIndex, secondIndex, GetEdge(firstIndex,secondIndex));
    }

    private Dictionary<int, List<(int, float)>> GetEdgesFromPathPoints(List<int> pathPointsIndex) 
    {
        Dictionary<int, List<(int, float)>> edges = new Dictionary<int, List<(int, float)>>();

        float edge;
        bool isEdgeAlreadyAdded;
        bool isEdgeAlreadyAddedReverse;

        for (int i = 0; i < pathPointsIndex.Count; i++)
        {
            edges.Add(pathPointsIndex[i], new List<(int, float)>());
        }

        for (int i = 0; i < pathPointsIndex.Count; i++) 
        {
            foreach(int index in GetNearestNeighbors(pathPointsIndex[i], _neighborsCount))
            {
                isEdgeAlreadyAdded = edges[pathPointsIndex[i]].Any(a => a.Item1 == index);
                isEdgeAlreadyAddedReverse = edges[index].Any(a => a.Item1 == pathPointsIndex[i]);

                if ((isEdgeAlreadyAdded && isEdgeAlreadyAddedReverse) == false)
                {
                    edge = GetEdge(pathPointsIndex[i], index);

                    if (isEdgeAlreadyAdded == false)
                        edges[pathPointsIndex[i]].Add((index, edge));

                    if (isEdgeAlreadyAddedReverse == false)
                        edges[index].Add((pathPointsIndex[i], edge));
                }
            }
        }

        return edges; 
    }

    private List<int> GetNearestNeighbors(int index, int neighborsCount)
    {
        Vector2 center = _pathPoints[index];
        List<(int , float )> distances = new(_pathPoints.Count - 1);

        for (int i = 0; i < _pathPoints.Count; i++)
        {
            if (i == index) 
                continue;

            float d = Vector2.SqrMagnitude(center - _pathPoints[i]);

            distances.Add((i, d));
        }

        distances.Sort((a, b) => a.Item2.CompareTo(b.Item2));

        List<int> nearest = new(neighborsCount);

        for (int i = 0; i < Mathf.Min(neighborsCount, distances.Count); i++)
            nearest.Add(distances[i].Item1);

        return nearest;
    }

    private float GetEdge(int pointAIndex, int pointBIndex)
    { 
        float magnitude = (_pathPoints[pointAIndex] - _pathPoints[pointBIndex]).magnitude;
        float cost = GetAverageCostFromCheckPoints(GetCheckPoints(_pathPoints[pointAIndex], _pathPoints[pointBIndex], magnitude)) * magnitude; 

        return cost; 
    }

    private float GetAverageCostFromCheckPoints(List<Vector2> checkPoints) 
    { 
        float cost = 0; 

        foreach (Vector2 point in checkPoints)
        { 
            if (_biomLayer.GetBiomFromPoint(point, out Biom biom)) 
                cost += _costPerTravelHardnessLvl * biom.TravelHardnessLvl; 
        } 

        cost += checkPoints.Count * _defoultCost;
        cost /= checkPoints.Count;

        return cost; 
    }

    private List<Vector2> GetCheckPoints(Vector2 pointA, Vector2 PointB, float magnitude) 
    { 
        List<Vector2> checkPoints = new List<Vector2>(); 
        int checkPointsQuantity = (Mathf.FloorToInt(magnitude) + 1) * _checkPointsPerStep; 

        for (int i = 0; i < checkPointsQuantity; i++) 
        { 
            checkPoints.Add(pointA + (((PointB - pointA) / checkPointsQuantity) * i)); 
        }

        return checkPoints; 
    }
}
