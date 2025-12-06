using UnityEngine;

public class Adventurer : Creature
{
    public int Rank {  get; private set; }
    private AdventurerMind _adventurerMind;

    public Adventurer(Race race, Class @class,AdventurerMind adventurerMind) : base(race, @class)
    {
        _adventurerMind = adventurerMind;
    }
}
