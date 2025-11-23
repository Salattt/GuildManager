using UnityEngine;

public class Weapon : Item, IAttack
{
    [SerializeField] private float _damage;
    [SerializeField] private int _minDistance;
    [SerializeField] private int _maxDistance;
    [SerializeField] private DamageType _damageType;

    public float Damage => _damage;

    public int MaxDistance => _maxDistance;

    public int MinDistance => _minDistance;

    public DamageType DamageType => _damageType;
}
