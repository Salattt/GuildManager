using UnityEngine;

public interface ISettelmentQuestGiver : IQuestGiver
{
    public int HazardLvl {  get;}
    public float Welth {  get;}

    public Vector2 Position {  get;}
}
