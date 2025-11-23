using UnityEngine;

public class ItemQuest : Quest
{
    private Item _requiredItem;

    public ItemQuest(float timeLimit, Reward reward, Item requiredItem) : base(timeLimit, reward)
    {
        _requiredItem = requiredItem;
    }
}
