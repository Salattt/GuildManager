using System.Collections.Generic;
using UnityEngine;

public class BiomShapeHolder : Shape
{
    public Biom Biom { get;}
    private OverlayShapes _biomShape;

    public BiomShapeHolder(Biom biom, OverlayShapes biomShape)
    {
        Biom = biom;
        _biomShape = biomShape;
    }

    public override bool CheckPointInclude(Vector2 point)
    {
        return _biomShape.CheckPointInclude(point);
    }

    public override List<Vector2> GetShapeStructurePoints()
    {
        return _biomShape.GetShapeStructurePoints();
    }
}
