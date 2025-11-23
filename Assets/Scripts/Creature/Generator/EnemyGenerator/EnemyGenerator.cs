using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyGenerator : CreatureGenerator
{
    [SerializeField] protected List<Item> Reward;
    [SerializeField] protected List<int> RewardChance;

    private void OnValidate()
    {
        if (Reward.Count != RewardChance.Count)
            throw new System.Exception("Reward and RewardChance list count not equivalent!");
    }

    public abstract Enemy Generate();
}
