public interface IStats 
{
    public float Strength { get;  }
    public float Dexterety { get; }
    public float BaseDefense { get; }
    public float Willpower { get;  }
    public float Intellegence { get;}
    public float Thougnes { get;  }

    public static bool operator >=(IStats a,IStats b)
    {
        bool isBigger = true;
        isBigger &= a.Strength >= b.Strength;
        isBigger &= a.Dexterety >= b.Dexterety;
        isBigger &= a.BaseDefense >= b.BaseDefense;
        isBigger &= a.Willpower >= b.Willpower;
        isBigger &= a.Intellegence >= b.Intellegence;
        isBigger &= a.Thougnes >= b.Thougnes;

        return isBigger;
    }

    public static bool operator <=(IStats a, IStats b) 
    {
        bool isSmoller = true;
        isSmoller &= a.Strength <= b.Strength;
        isSmoller &= a.Dexterety <= b.Dexterety;
        isSmoller &= a.BaseDefense <= b.BaseDefense;
        isSmoller &= a.Willpower <= b.Willpower;
        isSmoller &= a.Intellegence <= b.Intellegence;
        isSmoller &= a.Thougnes <= b.Thougnes;

        return isSmoller;
    }

    public static IStats operator -(IStats a,IStats b)
    {
        float strength = (a.Strength - b.Strength >= 0) ? a.Strength - b.Strength : 0;
        float dexterety = (a.Dexterety - b.Dexterety >= 0) ? a.Dexterety - b.Dexterety : 0;
        float baseDefense = (a.BaseDefense - b.BaseDefense >= 0) ? a.BaseDefense - b.BaseDefense : 0;
        float willpower = (a.Willpower - b.Willpower >= 0) ? a.Willpower - b.Willpower : 0;
        float intellegence = (a.Intellegence - b.Intellegence >= 0) ? a.Intellegence - b.Intellegence : 0;
        float thougnes = (a.Thougnes - b.Thougnes >= 0) ? a.Thougnes - b.Thougnes : 0;

        return new Stats(strength,dexterety,baseDefense,willpower,intellegence,thougnes);
    }
}
