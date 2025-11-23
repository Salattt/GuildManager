using System.Collections.Generic;
using UnityEngine;
using QuestCfg;

public class QuestGenerator 
{
    private List<EnemyGeneratorForSettelments> _enemies;

    public QuestGenerator(List<EnemyGeneratorForSettelments> enemies) 
    { 
        _enemies = enemies;
    }

    public EnemyGroupQuest GenerateQuest(ISettelmentQuestGiver questGiver)
    {
        return new EnemyGroupQuest(0,new Reward(),questGiver.Position,GetEnemies(questGiver.HazardLvl,
            questGiver.HazardLvl * questGiver.Welth * QuestConfig.ProductOfWelthAndHazardLvlToHazardPointsRatio));
    }

    public ItemQuest GenerateQuest(IFractionQuestGiver questGiver)
    {
        IReadOnlyList<Item> questItems = questGiver.GetQuestItems();

        return new ItemQuest(0, new Reward(), questItems[Random.Range(0, questItems.Count)]);
    }

    private List<Enemy> GetEnemies(int minHazardLvl, float hazardPoints) 
    { 
        List<Enemy> enemies = new List<Enemy>();
        List<EnemyGeneratorForSettelments> possibleEnemies = _enemies.FindAll(x => x.MinHazardLvl <= minHazardLvl);

        while (hazardPoints > 0)
        {
            EnemyGeneratorForSettelments newEnemy = possibleEnemies[Random.Range(0, possibleEnemies.Count)];

            enemies.Add(newEnemy.Generate());
            hazardPoints -= newEnemy.HazardPoints;
        }

        return enemies;
    }
}
