namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0")
            .LessThanOrEqualTo(50).WithMessage("Page size must be less than or equal to 50");
    }
}

internal class GetProductsQueryHandler(IQuerySession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(
        GetProductsQuery query,
        CancellationToken cancellationToken
    )
    {
        var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber, query.PageSize, cancellationToken);

        return new GetProductsResult(products);
    }

}

