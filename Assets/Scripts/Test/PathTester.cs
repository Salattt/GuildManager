using UnityEngine;
using System.Collections.Generic;

public class PathTester : MonoBehaviour
{
    [SerializeField] Biom biom;

    public void TestPath()
    {
        BiomLayer bl = new BiomLayer();

        bl.AddBiom(new BiomShapeHolder(biom,new OverlayShapes(new List<TurnebleShape> {new Circle(1,0,Vector2.zero,0.01f) })));
        SectorPathGenerator pg = new SectorPathGenerator(bl);

        

        //foreach (var pair in pg.Generate(new List<Vector2>()))
        //{
        //    Debug.Log(pair.Key.Item1 + "  " + pair.Key.Item2 + "  " + pair.Value.Cost);
        //}
    } 
}
