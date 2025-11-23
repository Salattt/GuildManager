public abstract class Creature 
{
    private readonly Race _race; 
    protected Class Class;
    protected Stats Stats;

    public string Name { get; private set; }

    public IStats StatsValues => Stats;

    protected Creature(Race race, Class @class)
    {
        _race = race;
        Class = @class;
    }
}
