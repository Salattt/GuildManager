using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupQuest : Quest
{
    private Vector2 _targetPosition;
    private List<Enemy> _enemy;
    private float _information;

    public EnemyGroupQuest(float timeLimit,Reward reward, Vector2 targetPosition,List<Enemy> enemy) : base(timeLimit,reward)
    {

    }

    public void SetUpRangers()
    {

    }

    public void SetUpWarriors()
    {

    }
}
