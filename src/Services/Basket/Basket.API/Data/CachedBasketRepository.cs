namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repo, IDistributedCache cache)
        : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(
            string userName,
            CancellationToken cancellationToken = default
        )
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);

            if (!string.IsNullOrEmpty(cachedBasket))
            {
                var result = JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);

                if (result is not null)
                    return result;

                throw new InternalServerException(
                    "Cached basket is missing or deserialized failure"
                );
            }

            var basket = await repo.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(
                userName,
                JsonSerializer.Serialize(basket),
                cancellationToken
            );
            return basket;
        }

        public async Task DeleteBasket(
            string userName,
            CancellationToken cancellationToken = default
        )
        {
            await repo.DeleteBasket(userName, cancellationToken);
            await cache.RemoveAsync(userName, cancellationToken);
        }

        public async Task<ShoppingCart> StoreBasket(
            ShoppingCart basket,
            CancellationToken cancellationToken = default
        )
        {
            await repo.StoreBasket(basket, cancellationToken);
            await cache.SetStringAsync(
                basket.Username,
                JsonSerializer.Serialize(basket),
                cancellationToken
            );
            return basket;
        }
    }
}
