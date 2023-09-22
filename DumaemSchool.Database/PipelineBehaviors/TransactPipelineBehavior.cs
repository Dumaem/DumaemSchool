using DumaemSchool.Core.Commands.Base;
using MediatR;

namespace DumaemSchool.Database.PipelineBehaviors;

public class TransactPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactRequest
{
    private readonly ApplicationContext _context;

    public TransactPipelineBehavior(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await next();
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            return default;
        }
    }
}