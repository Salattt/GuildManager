using System;
using System.Collections.Generic;
using UnityEngine;

public class FractionModul : MonoBehaviour
{
    [SerializeField] private List<FractionGenerator> _generators;

    private List<Fraction> _fractions;

    public Action<IFractionQuestGiver> NewQuestAvaible;

    private void OnDisable()
    {
        UnsubscribeOnEvents();
    }

    public void Generate()
    {
        _fractions = new List<Fraction>();

        foreach (var generator in _generators) 
        {
            _fractions.Add(generator.Generate());
        }

        SubscribeOnEvents();
    }

    private void SubscribeOnEvents()
    {
        foreach(Fraction fraction in _fractions)
        {
            fraction.NewQuestAvaible += NewQuestAvaible;
        }
    }

    private void UnsubscribeOnEvents()
    {
        if(_fractions != null)

        foreach (Fraction fraction in _fractions)
        {
            fraction.NewQuestAvaible -= NewQuestAvaible;
        }
    }
}
