using UnityEngine;

public class SkillType : ScriptableObject
{
    [SerializeField] private Skill _startingSkill;
    [SerializeField] private bool _isStartingSkillExist;
}
