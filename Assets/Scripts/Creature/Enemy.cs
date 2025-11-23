using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{
    private List<Item> _reward;

    public Enemy(Race race, Class @class,List<Item> reward) : base(race, @class)
    {
        _reward = reward;
    }
}
