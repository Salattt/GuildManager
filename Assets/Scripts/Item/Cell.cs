public class Cell 
{
    public int Count {  get; private set; }
    public Item Item { get;}

    public Cell(Item item)
    {
        Item = item;
        Count = 0;
    }

    public void Add(int count)
    {
        Count += count;
    }

    public bool TryGet(int count)
    {
        if(Count <= count)
        {
            Count -= count;
            return true;
        }

        return false;
    }
}
