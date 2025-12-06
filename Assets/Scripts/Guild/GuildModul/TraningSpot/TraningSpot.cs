using UnityEngine;

public class TraningSpot : ScriptableObject
{
    [SerializeField] private int _traningStatId;
    [SerializeField] private int _maxStatTrainingLvl;

    public int TrainingStatsId => _traningStatId;
    public int MaxTrainingLvl => _maxStatTrainingLvl;
}
