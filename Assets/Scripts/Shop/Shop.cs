using System.Collections.Generic;

public class Shop<T>
{
    Dictionary<T, int> _goods;

    public Shop()
    {
        _goods = new Dictionary<T, int>();
    }

    public void AddGood(T good,int value)
    {
        if (_goods.ContainsKey(good))
        {
            _goods[good] = value;
            return;
        }

        _goods.Add(good, value);
    }

    public IReadOnlyDictionary<T,int> GetGoods()
    {
        return _goods;
    }
}
