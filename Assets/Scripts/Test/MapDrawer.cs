using System.Collections.Generic;
using MapCfg;
using UnityEngine;

public class MapDrawer : MonoBehaviour
{
    [SerializeField] private WorldModul _world;
    [SerializeField] private float _resolution = 0.5f;

    public void Drow()
    {
        _world.GetBiomFromPoint(Vector2.zero, out Biom biom);
        return;
        List <Vector2> points = new List<Vector2>();

        points.Add(Vector2.zero);

        for (int i = 1; i < (MapConfig.MapSize /2) / _resolution; i++)
        {
            for (int j = 0; j < (MapConfig.MapSize / 2) / _resolution; j++)
            {
                points.Add(new Vector2(i * _resolution,j * _resolution));
                points.Add(new Vector2(-i * _resolution, j * _resolution));
                points.Add(new Vector2(-i * _resolution, -j * _resolution));
                points.Add(new Vector2(i * _resolution, -j * _resolution));
            }
        }

        foreach (Vector2 point in points)
        {
            if(_world.GetBiomFromPoint(point,out Biom bioms))
                Transform.Instantiate(biom.Object,new Vector3(point.x,0, point.y),transform.rotation);
        }
    }
}
