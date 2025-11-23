using System;
using System.Collections.Generic;

public interface IFractionQuestGiver : IQuestGiver
{
    public IReadOnlyList<Item> GetQuestItems();
}
