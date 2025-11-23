using UnityEngine;

public interface IQuestGiver
{
    public string Name { get; }

    public void QuestComplitted();
    public void QuestFailed();
}
