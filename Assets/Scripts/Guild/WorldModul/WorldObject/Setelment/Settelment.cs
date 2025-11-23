using UnityEngine;
using SettelmentCfg;
using System;

public class Settelment : WorldObject , ISettelmentQuestGiver
{
    private float _questTimer;

    public Action<ISettelmentQuestGiver> QuestAppeared;

    public int HazardLvl {  get; }
    public float Welth { get; private set; }
    public new Vector2 Position => base.Position;

    public Settelment(Vector2 position, string name,Biom biom) : base(position, name)
    {
        Welth = 0;
        _questTimer = 0;
        HazardLvl = (biom.HazardLvl + biom.TravelHardnessLvl + biom.VisibilityHardnessLvl) + (biom.FogLvl * SettelmentConfig.FogLvlMultiplicator);
    }

    public override void Update(float deltaTime)
    {
        UpdateWelth(deltaTime);
        UpdateQuestTimer(deltaTime);
        IsQeustAppered();
    }

    private void UpdateWelth(float deltaTime)
    {
        Welth += deltaTime * HazardLvl * SettelmentConfig.WelthGrowByHazardLvlCoefficent;
    }

    private void UpdateQuestTimer(float deltaTime)
    {
        _questTimer += HazardLvl * deltaTime * SettelmentConfig.QuestTimerGrowByHazardLvlCoefficent;
    }

    private void IsQeustAppered()
    {
        if (_questTimer < 1)
            return;

        if(UnityEngine.Random.Range(0,1) < _questTimer - 1)
        {
            QuestAppeared.Invoke(this);
        }
    }

    public void QuestComplitted()
    {
        throw new NotImplementedException();
    }

    public void QuestFailed()
    {
        throw new NotImplementedException();
    }
}
