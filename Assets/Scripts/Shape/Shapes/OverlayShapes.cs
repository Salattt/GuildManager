using System.Collections.Generic;
using UnityEngine;
using MapShape;

public class OverlayShapes : Shape
{
    private List<TurnebleShape> _shapes;
    private List<Vector2> _structurePoint;

    public OverlayShapes(List<TurnebleShape> shapes) 
    {
        for (int i = 0; i < shapes.Count; i++)
        {
            if (shapes[i] == null)
            {
                shapes.RemoveAt(i);
                i--;
            }
        }

        _shapes = shapes;

        FullStructurePointsList();
    }

    public override bool CheckPointInclude(Vector2 point)
    {
        bool isPointInclude = true;

        foreach (var shape in _shapes) 
        { 
            if(shape.CheckPointInclude(point) == false)
                isPointInclude = false;
        }

        return isPointInclude;
    }

    public override List<Vector2> GetShapeStructurePoints()
    {
        return _structurePoint;
    }

    private void FullStructurePointsList()
    {
        _structurePoint = VerifyIntersectionPoint(CalculateIntersectionPointsFromEdges(FindShapesEdges()));
    }

    private Dictionary<Vector2, Vector2> FindShapesEdges()
    {
        Dictionary<Vector2, Vector2> edge = new Dictionary<Vector2, Vector2>();

        foreach (var shape in _shapes) 
        {
            foreach (var edgePoints in FindEdgesFromPoints(shape.GetShapeStructurePoints()))
            {
                edge.Add(edgePoints.Item1, edgePoints.Item2);
            }
        }

        foreach(var edgePoints in FindEdgesFromPoints(IslandShape.GetMapEndPoints()))
        {
            edge.Add(edgePoints.Item1, edgePoints.Item2);
        }

        return edge;
    }

    private List<(Vector2, Vector2)> FindEdgesFromPoints(List<Vector2> points)
    {
        List<(Vector2, Vector2)> edge = new List<(Vector2, Vector2)>();
        Vector2 pointBuffer;

        pointBuffer = points[0];

        while (points.Count > 1)
        {
            edge.Add((points[0], points[1]));
            points.RemoveAt(0);
        }

        edge.Add((points[0], pointBuffer));

        return edge;
    }

    private List<Vector2> CalculateIntersectionPointsFromEdges(Dictionary<Vector2, Vector2> edges) 
    { 
        List<Vector2> allEdgeIntersectionPoints = new List<Vector2>();
        List<Vector2> edgesStartingPoints = new List<Vector2>(edges.Keys);

        for (int i = 0; i < edgesStartingPoints.Count; i++)
        {
            for (int j = i + 1; j < edgesStartingPoints.Count; j++)
            {
                if (CalculateLineIntersection(edgesStartingPoints[i], edges[edgesStartingPoints[i]], edgesStartingPoints[j], edges[edgesStartingPoints[j]],
                    out Vector2 intersectionPoint))
                {
                    allEdgeIntersectionPoints.Add(intersectionPoint);
                }   
            }
        }

        return allEdgeIntersectionPoints;
    }

    private bool CalculateLineIntersection(Vector2 firstVectorStartingPoint,Vector2 firstVectorEndPoint, Vector2 secondVectorStartingPoint, 
        Vector2 secondVectorEndPoint, out Vector2 intersectionPoint)
    {
        intersectionPoint = Vector2.zero;

        float denominator = (firstVectorStartingPoint.x - firstVectorEndPoint.x) * (secondVectorStartingPoint.y - secondVectorEndPoint.y) -
            (firstVectorStartingPoint.y - firstVectorEndPoint.y) * (secondVectorStartingPoint.x - secondVectorEndPoint.x);
        
        if(Mathf.Abs(denominator) < Mathf.Epsilon)
        {
            return false;
        }

        if (firstVectorStartingPoint == secondVectorStartingPoint)
        {
            intersectionPoint = firstVectorStartingPoint;
            return true;
        }

        if (firstVectorStartingPoint == secondVectorEndPoint)
        {
            intersectionPoint = firstVectorStartingPoint;
            return true;
        }

        if (firstVectorEndPoint == secondVectorStartingPoint)
        {
            intersectionPoint = firstVectorEndPoint;
            return true;
        }

        if (firstVectorEndPoint == secondVectorEndPoint)
        {
            intersectionPoint = firstVectorEndPoint;
            return true;
        }

        float percentToIntersictionPointOnFirstVector = ((firstVectorStartingPoint.x - secondVectorStartingPoint.x) *
                    (secondVectorStartingPoint.y - secondVectorEndPoint.y) - (firstVectorStartingPoint.y - secondVectorStartingPoint.y) *
                    (secondVectorStartingPoint.x - secondVectorEndPoint.x)) / denominator;

        float percentToIntersictionPointOnSecondVector =  ((firstVectorStartingPoint.y - firstVectorEndPoint.y) *
            (firstVectorStartingPoint.x - secondVectorStartingPoint.x) - (firstVectorStartingPoint.x - firstVectorEndPoint.x) *
            (firstVectorStartingPoint.y - secondVectorStartingPoint.y)) / denominator;

        if(percentToIntersictionPointOnFirstVector >= 0 && percentToIntersictionPointOnFirstVector <= 1 &&
            percentToIntersictionPointOnSecondVector >= 0 && percentToIntersictionPointOnSecondVector <= 1)
        {
            intersectionPoint = firstVectorStartingPoint + percentToIntersictionPointOnFirstVector * (firstVectorEndPoint - firstVectorStartingPoint);
            return true;
        }

        return false;
    }

    private List<Vector2> VerifyIntersectionPoint(List<Vector2> intersectionPointsDraft)
    {
        List<Vector2> verifiedIntersectionPoints;

        verifiedIntersectionPoints = DeleteDuplicates(intersectionPointsDraft);
        verifiedIntersectionPoints = DeleteNotIncludedInShapesPoints(verifiedIntersectionPoints);

        return verifiedIntersectionPoints;
    }

    private List<Vector2> DeleteDuplicates(List<Vector2> intersectionPointsDraft) 
    {
        for (int i = 0; i < intersectionPointsDraft.Count; i++)
        {
            for (int j = i + 1; j < intersectionPointsDraft.Count; j++)
            {
                if (intersectionPointsDraft[i] == intersectionPointsDraft[j])
                {
                    intersectionPointsDraft.RemoveAt(j);
                    j--;
                }
            }
        }

        return intersectionPointsDraft;
    }

    private List<Vector2> DeleteNotIncludedInShapesPoints(List<Vector2> intersectionPointsDraft) 
    {
        for (int i = 0; i < intersectionPointsDraft.Count; i++)
        {
            if (CheckPointInclude(intersectionPointsDraft[i]) == false || IslandShape.VerifyPoint(intersectionPointsDraft[i]) == false)
            {
                intersectionPointsDraft.RemoveAt(i);
                i--;
            }
        }

        return intersectionPointsDraft;
    }
}
