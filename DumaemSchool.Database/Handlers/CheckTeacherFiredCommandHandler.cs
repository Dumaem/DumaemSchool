using DumaemSchool.Core.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DumaemSchool.Database.Handlers;

public sealed class CheckTeacherFiredCommandHandler : IRequestHandler<CheckTeacherFiredCommand, bool>
{
    private readonly ApplicationContext _context;

    public CheckTeacherFiredCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CheckTeacherFiredCommand request,
        CancellationToken cancellationToken)
    {
        return (await _context
                   .Teachers
                   .FirstOrDefaultAsync(x => x.Id == request.TeacherId, cancellationToken: cancellationToken))
               ?.IsDeleted
               ?? false;
    }
}