using BuildingBlocks.Exceptions;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviours;

public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validateResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failure = validateResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if (!failure.Any()) return await next();

        var errors = new Dictionary<string, string>();
        failure.ForEach(f =>
        {
            errors.Add(f.PropertyName, f.ErrorMessage);
        });
        throw new UnprocessableEntityException(errors);
    }
}
