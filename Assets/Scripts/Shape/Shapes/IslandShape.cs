using System.Collections.Generic;
using UnityEngine;
using MapCfg;

namespace MapShape
{
    public static class IslandShape
    {
        public static Vector2 RightPoint => new Vector2(MapConfig.HalfMapSize, 0);
        public static Vector2 UpPoint => new Vector2(0, MapConfig.HalfMapSize);
        public static Vector2 LeftPoint => new Vector2(-MapConfig.HalfMapSize, 0);
        public static Vector2 DownPoint => new Vector2(0, -MapConfig.HalfMapSize);

        public static bool VerifyPoint(Vector2 point)
        {
            if ((Mathf.Abs(point.x) <= MapConfig.HalfMapSize || Mathf.Approximately(Mathf.Abs(point.x), MapConfig.HalfMapSize)) 
                && (-Mathf.Abs(point.x) + MapConfig.HalfMapSize >= Mathf.Abs(point.y) ||
                Mathf.Approximately((-Mathf.Abs(point.x) + MapConfig.HalfMapSize), Mathf.Abs(point.y))))
            {
                return true;
            }

            return false;
        }

        public static List<Vector2> GetMapEndPoints()
        {
            return new List<Vector2> { new Vector2(MapConfig.HalfMapSize , 0),new Vector2(0,MapConfig.HalfMapSize),
                new Vector2(-MapConfig.HalfMapSize, 0), new Vector2(0,-MapConfig.HalfMapSize) };
        }

        public static float GenerateMaxYfromX(float x)
        {
            return -Mathf.Abs(x) + 1;
        }
    }
}
