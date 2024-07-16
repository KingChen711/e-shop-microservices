using Basket.API.Dtos;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto Basket) : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.Basket).NotNull().WithMessage("Basket should not be null");
        RuleFor(x => x.Basket.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class CheckoutBasketCommandHandler(IBasketRepository repo, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(
        CheckoutBasketCommand command,
        CancellationToken cancellationToken
    )
    {
        var basket = await repo.GetBasket(command.Basket.UserName, cancellationToken);

        if (basket is null)
        {
            return new CheckoutBasketResult(false);
        }

        var eventMessage = command.Basket.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repo.DeleteBasket(command.Basket.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}
