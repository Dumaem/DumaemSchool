using DumaemSchool.Core.Commands.Lesson;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DumaemSchool.Database.Handlers.Lesson;

public sealed class CheckLessonCreatedCommandHandler : IRequestHandler<CheckLessonCreatedCommand, bool>
{
    private readonly ApplicationContext _context;

    public CheckLessonCreatedCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CheckLessonCreatedCommand request, CancellationToken cancellationToken)
    {
        return await _context
            .Lessons
            .FirstOrDefaultAsync(x => x.ScheduleId == request.ScheduleId
                                      && x.Date == request.CreationDate, cancellationToken: cancellationToken) is not null;
    }
}