using System.Collections.Generic;
using UnityEngine;
using MapCfg;

public class ShapeTester : MonoBehaviour
{
    private List<TurnebleShape> _shapes;

    public void Test()
    {
        List<int> angles = new List<int>();
        List<Vector2> points;

        for (int i = 0; i <= MapConfig.MaxShapeTurningAngle * 2; i++)
        {
            bool isAllPointInclude = true;
            Circle shape = new Circle(0, i * 0.5f, new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Random.Range(MapConfig.MinShapeScaleSize, MapConfig.MaxShapeScaleSize));

            points = shape.GetShapeStructurePoints();

            foreach (var point in points)
            {
                if (shape.CheckPointInclude(point) == false)
                {
                    isAllPointInclude = false;
                    Debug.Log("pos - " + shape.Position + ". point - " + point + "  " + (shape._radius - (shape.Position - point).magnitude));
                }
            }

            if (isAllPointInclude == false)
            {
                angles.Add(i);
            }
        }

        if (angles.Count == 0)
            Debug.Log("good");
        else
            Debug.Log("Bad");
    }
}
