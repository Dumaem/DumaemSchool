using DumaemSchool.Core.Commands.Base;
using DumaemSchool.Core.Exceptions;
using LanguageExt.Common;
using MediatR;

namespace DumaemSchool.Database.PipelineBehaviors;

public class TransactPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
    where TRequest : ITransactRequest
{
    private readonly ApplicationContext _context;

    public TransactPipelineBehavior(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next,
        CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await next();
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch (InformationException ie)
        {
            await transaction.RollbackAsync(cancellationToken);
            return new Result<TResponse>(ie);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}