
namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        session.Delete<ShoppingCart>(userName);
        await session.SaveChangesAsync(cancellationToken);
    }

    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var cart = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
        return cart is null ? throw new BasketNotFoundException(userName) : cart;

    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        session.Store(basket);
        await session.SaveChangesAsync(cancellationToken);
        return basket;
    }
}

