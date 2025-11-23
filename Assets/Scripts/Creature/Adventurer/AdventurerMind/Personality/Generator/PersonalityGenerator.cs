using UnityEngine;

public class PersonalityGenerator : ScriptableObject
{
    [SerializeField] private float _tendencyToGreed;
    [SerializeField] private float _tendencyToGod;
    [SerializeField] private float _tendencyToMelee;

    public Personality Generate()
    {
        return new Personality(_tendencyToGreed,_tendencyToGod,_tendencyToMelee);
    }
}
