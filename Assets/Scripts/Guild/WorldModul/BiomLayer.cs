using System.Collections.Generic;
using UnityEngine;

public class BiomLayer
{
    private List<BiomShapeHolder> _bioms = new List<BiomShapeHolder>();

    public void AddBiom(BiomShapeHolder shapeHolder)
    {
        _bioms.Add(shapeHolder);
    }

    public bool GetBiomFromPoint(Vector2 point, out Biom biom)
    {
        biom = null;
        bool isBiomFiended = false;

        foreach (BiomShapeHolder shapeHolder in _bioms)
        {
            if (shapeHolder.CheckPointInclude(point))
            {
                isBiomFiended = true;
                biom = shapeHolder.Biom;
                break;
            }
        }

        return isBiomFiended;
    }

    public List<Vector2> GetBiomsStructurePoints()
    {
        List<Vector2> points = new List<Vector2>();

        foreach (BiomShapeHolder biomHolder in _bioms)
            points.AddRange(biomHolder.GetShapeStructurePoints());

        return points;
    }
}
