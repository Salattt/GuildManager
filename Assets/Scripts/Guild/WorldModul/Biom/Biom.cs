using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBiom", menuName = "Scriptable Objects/Biom")]
public class Biom : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _hazardLvl;
    [SerializeField] private int _visibilityHardnessLvl;
    [SerializeField] private int _travelHardnessLvl;
    [SerializeField] private int _fogLvl;
    [SerializeField] public Object Object;

    public int HazardLvl => _hazardLvl;
    public int VisibilityHardnessLvl => _visibilityHardnessLvl;
    public int TravelHardnessLvl => _travelHardnessLvl;
    public int FogLvl => _fogLvl;
}
