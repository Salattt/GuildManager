using System;
using System.Collections.Generic;

public class QuestHolder
{
    private Dictionary<Quest, IQuestGiver> _questHolder;

    public Action<Reward> RewardAppear; 

    public QuestHolder()
    {
        _questHolder = new Dictionary<Quest, IQuestGiver>();
    }

    public void AddQuest(Quest quest,IQuestGiver questGiver)
    {
        _questHolder.Add(quest, questGiver);

        quest.QuestComplited += OnQuestComplited;
        quest.QuestFailed += OnQuestFailed;
    }

    private void OnQuestComplited(Quest quest)
    {
        quest.QuestComplited -= OnQuestComplited;
        quest.QuestFailed -= OnQuestFailed;

        _questHolder[quest].QuestComplitted();
        _questHolder.Remove(quest);

        RewardAppear.Invoke(quest.Reward);
    }

    private void OnQuestFailed(Quest quest)
    {
        quest.QuestComplited -= OnQuestComplited;
        quest.QuestFailed -= OnQuestFailed;

        _questHolder[quest].QuestFailed();
        _questHolder.Remove(quest);
    }
}
