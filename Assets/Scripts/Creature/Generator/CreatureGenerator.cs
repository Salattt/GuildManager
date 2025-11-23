using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureGenerator : ScriptableObject
{
    [SerializeField] protected Race Race;
    [SerializeField] protected Class Class;
}
