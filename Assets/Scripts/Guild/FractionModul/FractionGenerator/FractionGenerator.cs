using System.Collections.Generic;
using UnityEngine;

public class FractionGenerator : ScriptableObject
{
    [SerializeField] private List<Item> _itemToSell;
    [SerializeField] private List<int> _fractionWelthToSellItem;
    [SerializeField] private List<CreatureModificator> _modificatorToSale;
    [SerializeField] private List<int> _fractionWelthToSellModificator;
    [SerializeField] private List<Item> _questItems;
    [SerializeField] private List<int> _fractionWelthToQuestItemAppear;

    [SerializeField] private string _name;
    [SerializeField] private string _description;

    private void OnValidate()
    {
        if (_itemToSell.Count != _fractionWelthToSellItem.Count)
            throw new System.Exception();

        if(_modificatorToSale.Count != _fractionWelthToSellModificator.Count)
            throw new System.Exception();

        if(_questItems.Count != _fractionWelthToQuestItemAppear.Count)
            throw new System.Exception();
    }

    public Fraction Generate()
    {
        Dictionary<Item, int> itemToSell = GenerateDictionary<Item>(_itemToSell,_fractionWelthToSellItem);
        Dictionary<CreatureModificator,int> modificatorToSell = GenerateDictionary<CreatureModificator>(_modificatorToSale,_fractionWelthToSellModificator);
        Dictionary<Item,int> QuestItem = GenerateDictionary<Item>(_questItems,_fractionWelthToQuestItemAppear);

        return new Fraction(_name,_description,itemToSell,modificatorToSell,QuestItem);
    }

    private Dictionary<T,int> GenerateDictionary<T>(List<T> keys,List<int> values)
    {
        Dictionary<T,int> newDictionaty = new Dictionary<T,int>();

        for (int i = 0; i < keys.Count; i++) 
        { 
            newDictionaty.Add(keys[i], values[i]);
        }

        return newDictionaty;
    }
}
