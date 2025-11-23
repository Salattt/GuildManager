using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorForSettelments : EnemyGenerator
{
    [SerializeField] private int _minHazardLvl;
    [SerializeField] private int _hazardPoints;

    public int HazardPoints => _hazardPoints;
    public int MinHazardLvl => _minHazardLvl;

    public override Enemy Generate()
    {
        List<Item> reward = new List<Item>();

        for (int i = 0; i < Reward.Count;i++)
        {
            if(Random.Range(0,100) < RewardChance[i])
                reward.Add(Reward[i]);
        }

        return new Enemy(Race, Class, reward);
    }
}
