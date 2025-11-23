using UnityEngine;

public class SingleSettelmentGenerator : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Vector2 _position;
    [SerializeField] private Biom _biom;

    public Settelment Generate()
    {
        return new Settelment(_position, _name, _biom);
    }
}
