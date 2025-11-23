using System;
using System.Collections.Generic;
using UnityEngine;

public class RankModul : MonoBehaviour
{
    private Dictionary<int, Rank> _rankHolder;
    private List<int> _rankPosition;

    private RankGenerator _rankGenerator;

    public Action RankHolderUpdated;

    public IReadOnlyList<int> RankPosition => _rankPosition;

    public void GenerateNewRank(string name)
    {
        Rank newRank = _rankGenerator.Generate(name);

        _rankHolder.Add(newRank.Id, newRank);
        _rankPosition.Add(newRank.Id);
    }

    public void UpdateRankPosition(List<int> rankPosition)
    {
        _rankPosition = rankPosition;
    }
}
