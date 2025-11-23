public class Stats : IStats
{
    public float Strength { get; private set; }
    public float Dexterety { get; private set; }
    public float BaseDefense { get; private set; }
    public float Willpower { get; private set; }
    public float Intellegence { get; private set; }
    public float Thougnes { get; private set; }

    public Stats(float strength, float dexterety, float baseDefense, float willpower, float intellegence, float thougnes)
    {
        Strength = strength;
        Dexterety = dexterety;
        BaseDefense = baseDefense;
        Willpower = willpower;
        Intellegence = intellegence;
        Thougnes = thougnes;
    }
}
