using UnityEngine;

[CreateAssetMenu(fileName = "NewRace", menuName = "Scriptable Objects/Race")]
public class Race : ScriptableObject
{
    [SerializeField] private CreatureModificator[] _creatureModificators;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
}
