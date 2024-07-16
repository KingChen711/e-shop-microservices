using Basket.API.Dtos;

namespace Basket.API.Basket.CheckoutBasket;

public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/basket/checkout",
                async (BasketCheckoutDto basketCheckoutDto, ISender sender) =>
                {
                    var command = new CheckoutBasketCommand(basketCheckoutDto);

                    await sender.Send(command);

                    return Results.NoContent();
                }
            )
            .WithName("CheckoutBasket")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Checkout Basket")
            .WithDescription("Checkout Basket");
        ;
    }
}
