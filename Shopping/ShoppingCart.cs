namespace Shopping;

public class ShoppingCart
{
    private readonly List<(string Item, decimal Price)> _items = new();

    public void AddItem(string item, decimal price)
    {
        if(price <= 0)
        {
            throw new ArgumentOutOfRangeException("price","Price must be greater than 0");
        }
        _items.Add((item, price));
    }

    public void RemoveItem(string item)
    {
        var existingItem = _items.FirstOrDefault(i => i.Item == item);
        if (existingItem != default)
        {
            _items.Remove(existingItem);
        }
    }

    public decimal GetTotal()
    {
        return _items.Sum(i => i.Price);
    }

    public int GetItemCount()
    {
        return _items.Count;
    }

    public List<(string Item, decimal Price)> GetListOfItems()
    {
        return _items;
    }
}