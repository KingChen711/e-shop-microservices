namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(
   ShoppingCart Cart
) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string Username);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart)
            .NotNull().WithMessage("Cart should not be null");

        RuleFor(x => x.Cart.Username)
            .NotEmpty().WithMessage("Username is required");
    }
}

internal class StoreBasketCommandHandler(IBasketRepository repo)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(
        StoreBasketCommand command,
        CancellationToken cancellationToken
    )
    {
        var cart = command.Cart;
        await repo.StoreBasket(cart, cancellationToken);
        return new StoreBasketResult(cart.Username);
    }

}

