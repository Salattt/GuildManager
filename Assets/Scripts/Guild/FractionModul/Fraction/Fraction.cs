using System;
using System.Collections.Generic;

public class Fraction : IFractionQuestGiver
{
    private Dictionary<Item, int> _itemToSell;
    private Dictionary<Item, int> _questItems;
    private Dictionary<CreatureModificator, int> _modificatorsToSell;
    private List<Item> _avaibleQuestItems;

    private Shop<Item> _itemShop;
    private Shop<CreatureModificator> _modificatorsShop;
    private int _welth;

    public Action<IFractionQuestGiver> NewQuestAvaible;

    public string Name { get;}
    public string Description { get;}

    public Fraction(string name,string description,Dictionary<Item,int> itemsToSell,Dictionary<CreatureModificator,int> modificatorToSell, Dictionary<Item, int> questItems)
    {
        _itemToSell = itemsToSell;
        _modificatorsToSell = modificatorToSell;
        _questItems = questItems;
        _avaibleQuestItems = new List<Item>();
        _itemShop = new Shop<Item>();
        _modificatorsShop = new Shop<CreatureModificator>();
        _welth = 0;
        Name = name;
        Description = description;
    }

    public IReadOnlyList<Item> GetQuestItems()
    {
        return _avaibleQuestItems;
    }

    public void QuestComplitted()
    {
        AddWelth(0);

        NewQuestAvaible.Invoke(this);
    }

    public void QuestFailed()
    {
        AddWelth(0);

        NewQuestAvaible.Invoke(this);
    }

    private void AddWelth(int addingWelth)
    {
        if (addingWelth <= 0)
            throw new ArgumentException(nameof(addingWelth));

        _welth += addingWelth;

        UpdateWelthDepentedDictionares();
    }

    private void UpdateWelthDepentedDictionares()
    {
        AddGoodsToShop<Item>(_itemToSell,_itemShop);
        AddGoodsToShop<CreatureModificator>(_modificatorsToSell,_modificatorsShop);
        AddQuestItems();
    }

    private void AddQuestItems()
    {
        List<Item> itemsToRemove=new List<Item>();

        foreach(var item in _questItems)
        {
            if(item.Value <= _welth)
            {
                _avaibleQuestItems.Add(item.Key);
                itemsToRemove.Add(item.Key);
            }
        }

        foreach(Item item in itemsToRemove)
        {
            _questItems.Remove(item);
        }
    }

    private void AddGoodsToShop<T>(Dictionary<T,int> goodsDictionary,Shop<T> shop)
    {
        List<T> goodsToRemove = new List<T>();

        foreach (var good in goodsDictionary)
        {
            if (_welth >= good.Value)
            {
                shop.AddGood(good.Key, good.Value);
                goodsToRemove.Add(good.Key);
            }
        }

        foreach (var item in goodsToRemove) 
        {
            goodsDictionary.Remove(item);
        }
    }
}
