using System.CodeDom.Compiler;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void StartTest()
    {
        foreach (Vector2 points in new OverlayShapes(new List<TurnebleShape> { new Square(1, 45, new Vector2(1, 0), 0.005f) }).GetShapeStructurePoints())
        {
            Debug.Log(points);
        }
    }
}
