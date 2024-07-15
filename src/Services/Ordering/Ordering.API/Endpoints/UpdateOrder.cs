using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto Order);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders/{id:guid}", async (Guid id, UpdateOrderRequest request, ISender sender) =>
            {
                var command = new UpdateOrderCommand(Order: request.Order with { Id = id });

                await sender.Send(command);

                return Results.NoContent();
            })
            .WithName("UpdateOrder")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Order")
            .WithDescription("Update Order");
    }
}
