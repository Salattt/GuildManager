using System.Collections.Generic;
using UnityEngine;

public class RandomSettelmentGenerator : MonoBehaviour
{
    [SerializeField] private List<string> _possibleNames;

    public Settelment Generate(Vector2 position,Biom biom)
    {
        return new Settelment(position, _possibleNames[Random.Range(0,_possibleNames.Count)], biom);
    }
}
