namespace PathCfg
{
    public static class PathConfig
    {
        public static int CheckPointsPerStep { get => 4;}
        public static int MaxCheckPooints {  get => 100;}
        public static float DefoultCost {  get => 1f;}
        public static float CostPerTraveHardnessLvl { get => 0.2f;}
        public static int SectorEdgeLength { get => 20; }
        public static int SectorPointOnEdge {  get => 200; }
        public static int NeighborCount { get => 10; }
    }
}
