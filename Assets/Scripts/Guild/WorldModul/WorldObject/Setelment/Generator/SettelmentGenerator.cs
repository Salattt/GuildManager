using System.Collections.Generic;
using UnityEngine;

public class SettelmentGenerator : MonoBehaviour
{
    [SerializeField] private List<SingleSettelmentGenerator> _startingSettelments;

    [SerializeField] private RandomSettelmentGenerator _randomSettelmentGenerator;

    public Settelment GenerateRandomSettelment(Vector2 position,Biom biom)
    {
        return _randomSettelmentGenerator.Generate(position,biom);
    }

    public List<Settelment> GenerateStartingSettelment()
    {
        List<Settelment> settelments = new List<Settelment>();

        foreach(var generator in _startingSettelments)
        {
            settelments.Add(generator.Generate());
        }

        return settelments;
    }
}
