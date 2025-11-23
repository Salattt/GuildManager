using System.Collections.Generic;
using UnityEngine;
using MapCfg;

public abstract class Shape
{
    protected float MapSize { get => MapConfig.MapSize; }
    protected float HalfMapSize {  get => MapConfig.HalfMapSize; }

    public abstract bool CheckPointInclude(Vector2 point);
    public abstract List<Vector2> GetShapeStructurePoints();
}
