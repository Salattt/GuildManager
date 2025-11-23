using UnityEngine;

public class StatsGenerator : ScriptableObject
{
    [SerializeField] private float _strength;
    [SerializeField] private float _dexterety;
    [SerializeField] private float _baseDefense;
    [SerializeField] private float _willpower;
    [SerializeField] private float _intellegence;
    [SerializeField] private float _thougnes;

    public Stats Generate()
    {
        return new Stats(_strength,_dexterety,_baseDefense,_willpower,_intellegence,_thougnes);
    }
}
