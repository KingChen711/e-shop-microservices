namespace Basket.API.Basket.DeleteBasket;

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete(
                "/basket/{userName}",
                async (string userName, ISender sender) =>
                {
                    var command = new DeleteBasketCommand(userName);

                    await sender.Send(command);

                    return Results.NoContent();
                }
            )
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
    }
}
