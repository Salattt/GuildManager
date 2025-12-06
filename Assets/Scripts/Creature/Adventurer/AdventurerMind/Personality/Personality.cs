using System.Collections.Generic;

public class Personality
{
    public IReadOnlyDictionary<int, float> Tendencys;

    public float TendencyToGreed { get; }
    public float TendencyToGod { get; }
    public float TendencyToMelee { get; }
    public int MaxTendencyId { get; }

    public Personality(float tendencyToGreed, float tendencyToGod, float tendencyToMelee)
    {
        Dictionary<int, float> tendencys = new Dictionary<int, float>();

        TendencyToGreed = tendencyToGreed;
        TendencyToGod = tendencyToGod;
        TendencyToMelee = tendencyToMelee;

        tendencys.Add(0, TendencyToGreed);
        tendencys.Add(1, TendencyToGod);
        tendencys.Add(2, TendencyToMelee);

        Tendencys = tendencys;
        MaxTendencyId = GetMaxTendencyId();
    }

    private int GetMaxTendencyId()
    {
        int maxTendencyId = 0;

        foreach (var tendency in Tendencys)
        {
            if (Tendencys[maxTendencyId] < tendency.Value)
            {
                maxTendencyId = tendency.Key;
            }
        }

        return maxTendencyId;
    }

    public static float operator *(Personality a, Personality b)
    { 
        float result = 0;

        foreach (var tendecy in a.Tendencys)
        {
            result += tendecy.Value * b.Tendencys[tendecy.Key];
        }

        return result;
    }
}
