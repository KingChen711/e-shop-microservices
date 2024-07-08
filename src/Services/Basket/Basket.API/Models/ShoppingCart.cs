namespace Basket.API.Models;

public class ShoppingCart
{
    public string Username { get; set; } = null!;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public ShoppingCart(string userName)
    {
        Username = userName;
    }

    public ShoppingCart()
    {

    }
}

