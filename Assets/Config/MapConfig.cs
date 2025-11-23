namespace MapCfg
{
    public static class MapConfig
    {
        public static int MapSize { get => 200; }
        public static float HalfMapSize { get => MapSize / (float)2; }
        public static float MinShapeScaleSize { get => 0.005f; }
        public static float MaxShapeScaleSize { get => 0.01f; }
        public static float MaxShapeTurningAngle { get => 90f; }
        public static int ShapesToLayer { get => 70; }
        public static float CheclPointsBoxWidth { get => 0.5f; }
        public static float MaxInaccurace { get => 0.00001f; }
        public static int CircleStructurePoint { get => 10; }
    }
}
