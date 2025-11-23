public class RankGenerator
{
    private int _currentId;

    public RankGenerator(int startingId)
    {
        _currentId = startingId;
    }

    public Rank Generate(string name)
    {
        return new Rank(_currentId++,name);
    }
}
