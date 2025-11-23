using System;

public abstract class Quest 
{
    private float _timeLimit;
    public Reward Reward { get; }

    public Action<Quest> QuestComplited;
    public Action<Quest> QuestFailed;

    public Quest(float timeLimit,Reward reward)
    {
        Reward = reward;
        _timeLimit = timeLimit;
    }

    public void Update(float deltaTime) 
    { 
        _timeLimit -= deltaTime;

        if (_timeLimit < 0)
            QuestFailed.Invoke(this);
    }
}
