using Basket.API.Data;

namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string Username) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);

internal class GetBasketQueryHandler(IBasketRepository repo)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(
        GetBasketQuery query,
        CancellationToken cancellationToken
    )
    {
        var cart = await repo.GetBasket(query.Username, cancellationToken);
        return new GetBasketResult(cart);
    }

}

