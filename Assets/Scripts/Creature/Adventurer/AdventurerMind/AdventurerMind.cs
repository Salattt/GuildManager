using System.Collections.Generic;
using UnityEngine;

public class AdventurerMind
{
    private Personality _personality;
    private Class _classGoal;
    private IStats _statsToUpgrade;
    private IReadOnlyDictionary<int, TraningSpot> _trainingSpots; 

    public void SetClassGoal(Class currentClass)
    {
        Class classGoal = null;
        float tendecyMultiplication = 0;

        foreach (Class @class in currentClass.PossibleNextClasses)
        {
            float newTendencyMultiplication = _personality * @class.RequiredPersonality;

            if(newTendencyMultiplication > tendecyMultiplication)
            {
                classGoal = @class;
                tendecyMultiplication = newTendencyMultiplication;
            }
        }
        
        _classGoal = classGoal;
    }

    public void CheckStatsToUpgrade(IStats stats)
    {
        _statsToUpgrade = stats - _classGoal.RequiredStats;
    }

    public void ChooseToDo()
    {
        if (TryTrain())
            return;

        if (TryEarnMoney())
            return;
    }

    private bool TryTrain()
    {

    }

    private bool TryEarnMoney()
    {

    }
}
