namespace Shopping.Tests;

public class ShoppingCartTests
{
    [Fact]
    public void AddItem_AddAnAppleToAnEmptyCart_ItemCountShouldBeOne()
    {
        var cart = new ShoppingCart();
        cart.AddItem("Apple", 1.50m);
        Assert.Equal(1, cart.GetItemCount());
    }

    [Fact]
    public void RemoveItem_RemoveExistingItem_DecreasesItemCount()
    {
        var cart = new ShoppingCart();
        cart.AddItem("Apple", 1.50m);
        cart.AddItem("Orange", 1.30m);

        cart.RemoveItem("Apple");

        Assert.Equal(1, cart.GetItemCount());
    }

    [Fact]
    public void RemoveItem_RemoveNonExistentItem_DoesNotAlterItemCount()
    {
        var cart = new ShoppingCart();
        cart.AddItem("Apple", 1.50m);
        cart.AddItem("Orange", 1.30m);

        cart.RemoveItem("Banana");

        Assert.Equal(2, cart.GetItemCount());
    }

    [Fact]
    public void GetTotal_AddMultipleItems_EnsuresTotalPriceIsCorrect()
    {
        var cart = new ShoppingCart();
        cart.AddItem("Apple", 1.50m);
        cart.AddItem("Orange", 1.30m);
        cart.AddItem("Banana", 1.20m);

        Assert.Equal(4m, cart.GetTotal());
    }

    [Fact]
    public void GetTotal_RemoveAllCartItems_EnsuresTotalPriceIsZero()
    {
        var cart = new ShoppingCart();
        cart.AddItem("Apple", 1.50m);
        cart.AddItem("Orange", 1.30m);
        cart.AddItem("Banana", 1.20m);

        cart.RemoveItem("Apple");
        cart.RemoveItem("Orange");
        cart.RemoveItem("Banana");

        Assert.Equal(0.00m, cart.GetTotal());
    }

    [Fact]
    public void AddItem_AddDuplicateItems_CartShouldContainMultipleInstances()
    {
        var cart = new ShoppingCart();
        cart.AddItem("Apple", 1.50m);
        cart.AddItem("Apple", 1.50m);

        var itemCount = cart.GetItemCount();
        var total = cart.GetTotal();

        Assert.Equal(2, itemCount);
        Assert.Equal(3.00m, total);
    }

    [Fact]
    public void RemoveItem_RemoveFromEmptyCart_ItemCountShouldRemainZero()
    {
        var cart = new ShoppingCart();

        cart.RemoveItem("Apple");

        Assert.Equal(0, cart.GetItemCount());
    }

    [Fact]
    public void AddItem_AddItemWithZeroOrNegativePrice_ShouldNotAddItem()
    {
        var cart = new ShoppingCart();

        var ex1 = Assert.Throws<ArgumentOutOfRangeException>(() => cart.AddItem("Apple", 0.00m));
        Assert.Equal("price", ex1.ParamName);

        var ex2 = Assert.Throws<ArgumentOutOfRangeException>(() => cart.AddItem("Orange", -1.50m));
        Assert.Equal("price", ex2.ParamName);

        Assert.Equal(0, cart.GetItemCount());
        Assert.Equal(0.00m, cart.GetTotal());
    }

    [Fact]
    public void GetAllItems_EmptyCart_ShouldReturnEmptyList()
    {
        var cart = new ShoppingCart();
        var items = cart.GetListOfItems();
        Assert.Empty(items);
    }

    [Fact]
    public void GetAllItems_OneItemAdded_ShouldReturnOneItem()
    {
        var cart = new ShoppingCart();
        cart.AddItem("Apple", 1.50m);
        var items = cart.GetListOfItems();
        Assert.Single(items);
        Assert.Equal("Apple", items[0].Item);
        Assert.Equal(1.50m, items[0].Price);
    }

    [Fact]
    public void GetAllItems_MultipleItemsAdded_ShouldReturnAllItems()
    {
        var cart = new ShoppingCart();
        cart.AddItem("Apple", 1.50m);
        cart.AddItem("Orange", 1.20m);
        cart.AddItem("Banana", 0.99m);
        var items = cart.GetListOfItems();
        Assert.Equal(3, items.Count);
        Assert.Contains(items, i => i.Item == "Apple" && i.Price == 1.50m);
        Assert.Contains(items, i => i.Item == "Orange" && i.Price == 1.20m);
        Assert.Contains(items, i => i.Item == "Banana" && i.Price == 0.99m);
    }
}
