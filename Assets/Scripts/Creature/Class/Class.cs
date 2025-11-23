using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Class", menuName = "Scriptable Objects/Class")]
public class Class : ScriptableObject
{
    [SerializeField] private List<Class> _possibleNextClasses;
    [SerializeField] private List<Ability> _abilities;

    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private readonly Item _requiredItem;
    [SerializeField] private StatsGenerator _requiredStatsGenerator;
    [SerializeField] private PersonalityGenerator _personalityGenerator;
    private Stats _requiredStats;
    private Personality _personality;

    public IReadOnlyList<Class> PossibleNextClasses;
    public IReadOnlyList<Ability> Abilities;
    public IStats RequiredStats => _requiredStats;
    public Personality RequiredPersonality => _personality;

    private void OnEnable()
    {
        _requiredStats = _requiredStatsGenerator.Generate();
        _personality = _personalityGenerator.Generate();
    }
}
