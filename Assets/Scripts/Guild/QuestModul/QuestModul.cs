using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestModul : MonoBehaviour
{
    [SerializeField] private List<EnemyGeneratorForSettelments> _enemyGenneratorsForSettelments;

    private QuestGenerator _generator;
    private QuestHolder _holder;

    public Action<Reward> RewardAppear;

    private void Start()
    {
        _generator = new QuestGenerator(_enemyGenneratorsForSettelments);
        _holder = new QuestHolder();
    }

    private void OnEnable()
    {
        _holder.RewardAppear += RewardAppear;
    }

    private void OnDisable()
    {
        _holder.RewardAppear -= RewardAppear;
    }

    public void GenerateQuest(IFractionQuestGiver questGiver)
    {
        _holder.AddQuest(_generator.GenerateQuest(questGiver),questGiver);
    }

    public void GenerateQuest(ISettelmentQuestGiver questGiver)
    {
        _holder.AddQuest(_generator.GenerateQuest(questGiver), questGiver);
    }
}