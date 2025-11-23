using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlobalPathGenerator
{
    public Dictionary<(int, int), Path> GeneratePaths(Dictionary<int, Sector> sectors, Dictionary<Vector2, int> pointsOnEdgeIndexes, List<Vector2> pointsOnEdges)
    {
        Dictionary<int, List<int>> sortedPoints = GetSortedPointsOnEdge(sectors, pointsOnEdgeIndexes);

        return GetPaths(sectors,pointsOnEdges,sortedPoints);
    }

    private Dictionary<(int, int), Path> GetPaths(Dictionary<int, Sector> sectors, List<Vector2> pointsOnEdges, Dictionary<int, List<int>> sortedPoints)
    {
        Dictionary<(int, int), Path> paths = new Dictionary<(int, int), Path>();

        foreach(var path in GetPathsInRange(sectors, pointsOnEdges, sortedPoints, sortedPoints.Keys.Min(), 0))
        {
            paths.Add(path.Key, path.Value);
        }

        foreach (var path in GetPathsInRange(sectors, pointsOnEdges, sortedPoints, 0, sortedPoints.Keys.Max()))
        {
            paths.Add(path.Key, path.Value);
        }

        return paths;
    }

    private Dictionary<(int, int), Path> GetPathsInRange(Dictionary<int, Sector> sectors, List<Vector2> pointsOnEdges, Dictionary<int, List<int>> sortedPoints, int start, int end)
    {
        Dictionary<(int, int), Path> paths = new Dictionary<(int, int), Path>();
        Dictionary<int, List<(int, float)>> connections = GetConnections(sectors, pointsOnEdges, sortedPoints, start, end);
        Dictionary<int, List<int>> pathToPoint;
        Dictionary<int, float> distanceToPoint;
        List<int> pointToUse;

        int currentPoint;

        for (int i = start; i < end; i++) 
        {
            foreach (int index in sortedPoints[i])
            {
                pointToUse = new List<int>();
                distanceToPoint = new Dictionary<int, float>();
                pathToPoint = new Dictionary<int, List<int>>();

                for (int j = i + 1; j <= end - 1; j++)
                {
                    foreach(int k in sortedPoints[j])
                    {
                        pointToUse.Add(k);
                        distanceToPoint.Add(k, float.PositiveInfinity);
                    }
                }

                foreach(int k in sortedPoints[end])
                {
                    distanceToPoint.Add(k, float.PositiveInfinity);
                }

                foreach (var k in connections[index])
                {
                    distanceToPoint[k.Item1] = k.Item2;
                    pathToPoint[k.Item1] = new List<int> { index,k.Item1 };
                }

                while(pointToUse.Count > 0)
                {
                    currentPoint = GetPointWithSmollestPathCost(distanceToPoint, pointToUse);

                    pointToUse.Remove(currentPoint);

                    foreach(var neighbor in connections[currentPoint])
                    {
                        float newDist = distanceToPoint[currentPoint] + neighbor.Item2;

                        if (distanceToPoint[neighbor.Item1] < newDist)
                        {
                            distanceToPoint[neighbor.Item1] = newDist;
                            pathToPoint[neighbor.Item1] = pathToPoint[currentPoint];
                            pathToPoint[neighbor.Item1].Add(neighbor.Item1);
                        }
                    }
                }

                foreach(var path in pathToPoint)
                {
                    Path newPath = sectors[i].GetPath(pointsOnEdges[path.Value[0]], pointsOnEdges[path.Value[1]]);

                    for (int j = 1; j < path.Value.Count - 1; j++)
                    {
                        newPath = newPath + sectors[i + j].GetPath(pointsOnEdges[path.Value[j]], pointsOnEdges[path.Value[j + 1]]);
                    }

                    paths.Add((path.Value[0], path.Key), newPath);
                    paths.Add((path.Key, path.Value[0]), newPath.Reverse());
                }
            }
        }

        return paths;
    }

    private int GetPointWithSmollestPathCost(Dictionary<int, float> distanceToPoint, List<int> points)
    {
        float minDist = float.PositiveInfinity;
        int pointWithMinDist = points[0];

        foreach(int point in points)
        {
            if (distanceToPoint[point] < minDist)
            {
                minDist = distanceToPoint[point];
                pointWithMinDist = point;
            }
        }

        return pointWithMinDist;
    }

    private Dictionary<int, List<(int, float)>> GetConnections(Dictionary<int, Sector> sectors, List<Vector2> pointsOnEdges, Dictionary<int, List<int>> sortedPoints,int start,int end)
    {
        Dictionary<int, List<(int, float)>> connections= new Dictionary<int, List<(int, float)>>();

        for (int i = start; i < end; i++)
        {
            foreach(int startPointIndex in sortedPoints[i])
            {
                connections.Add(startPointIndex, new List<(int, float)>());

                foreach(int endPointIndex in sortedPoints[i + 1])
                {
                    connections[startPointIndex].Add((endPointIndex, sectors[i].GetPath(pointsOnEdges[startPointIndex], pointsOnEdges[endPointIndex]).Cost));
                }
            }
        }

        return connections;
    }

    private Dictionary<int,List<int>> GetSortedPointsOnEdge(Dictionary<int, Sector> sectors, Dictionary<Vector2, int> pointsOnEdgeIndexes)
    {
        Dictionary<int, List<int>> sortedPoints = new Dictionary<int, List<int>>();

        foreach (var sector in sectors) 
        {
            sortedPoints.Add(sector.Key, new List<int>());

            foreach (var point in sector.Value.LeftEdgePoints)
            {
                sortedPoints[sector.Key].Add(pointsOnEdgeIndexes[point]);
            }
        }

        return sortedPoints;
    }
}
