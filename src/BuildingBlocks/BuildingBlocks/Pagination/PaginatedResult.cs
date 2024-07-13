namespace BuildingBlocks.Pagination;

public class PaginatedResult<TEntity>(int page, int pageSize, long count, IEnumerable<TEntity> data)
    where TEntity : class
{
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
    public long Count { get; } = count;
    public IEnumerable<TEntity> Data { get; } = data;
}
