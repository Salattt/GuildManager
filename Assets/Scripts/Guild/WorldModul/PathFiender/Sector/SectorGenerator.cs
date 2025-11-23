using MapCfg;
using PathCfg;
using MapShape;
using System.Collections.Generic;
using UnityEngine;

public class SectorGenerator
{
    public Dictionary<int, Sector> FullSectorDictionary()
    {
        Dictionary<int, Sector>  sectors = new Dictionary<int, Sector>();
        Dictionary<int,List<Vector2>> pointsOnSectorsEdges;

        List<int> indexes = GenerateIndexes();
        pointsOnSectorsEdges = GeneratePointsOnEdges(indexes);

        indexes.Add(0);

        foreach(int index in indexes)
        {
            if (pointsOnSectorsEdges.ContainsKey(index + 1) == false)
                continue;

            sectors.Add(index, new Sector(pointsOnSectorsEdges[index], pointsOnSectorsEdges[index + 1]));
        }

        return sectors;
    }

    private Dictionary<int, List<Vector2>> GeneratePointsOnEdges(List<int> indexes)
    {
        Dictionary<int,List<Vector2>> points = new Dictionary<int, List<Vector2>>();

        float rangeBetweenPoints = MapConfig.MapSize / (float)PathConfig.SectorPointOnEdge ;

        points.Add(0,GenerateZeroVectorsPoint());

        foreach (int index in indexes) 
        {
            points.Add(index,new List<Vector2>());
            points[index].Add(new Vector2(index * PathConfig.SectorEdgeLength,0));

            float cicleQuantity = (MapConfig.HalfMapSize *
                IslandShape.GenerateMaxYfromX((index * PathConfig.SectorEdgeLength) / MapConfig.HalfMapSize)) / rangeBetweenPoints;

            for (int i = 1; (i < cicleQuantity) || (Mathf.Approximately(cicleQuantity,i)) ; i++)
            {
                points[index].Add(new Vector2(index * PathConfig.SectorEdgeLength, i * rangeBetweenPoints));
                points[index].Add(new Vector2(index * PathConfig.SectorEdgeLength, -i * rangeBetweenPoints));
            }
        }

        return points;
    }

    private List<Vector2> GenerateZeroVectorsPoint()
    {
        return new List<Vector2> { Vector2.zero};
    }

    private List<int> GenerateIndexes()
    {
        List<int> indexes = new List<int>();

        int sectorInMap = (MapConfig.MapSize / PathConfig.SectorEdgeLength) / 2;

        for (int i = 1; i <= sectorInMap; i++)
        {
            indexes.Add(i);
            indexes.Add(-i);
        }

        return indexes;
    }
}
