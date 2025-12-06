using System.Collections.Generic;
using UnityEngine;

public class GuildModul : MonoBehaviour
{
    private Dictionary<int, List<Quest>> _sortedByRankQuests;
    private Dictionary<int, Rank> _ranks;
    private List<TraningSpot> _trainingSpots;

    public IReadOnlyList<TraningSpot> TraningSpots => _trainingSpots;
}
