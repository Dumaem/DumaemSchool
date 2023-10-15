using DumaemSchool.Core.Commands.Lesson;
using Microsoft.EntityFrameworkCore;

namespace DumaemSchool.Database.Handlers.Lesson;

public sealed class CheckLessonTeacherCommandHandler
{
    private readonly ApplicationContext _context;

    public CheckLessonTeacherCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CheckLessonTeacherCommand request, CancellationToken cancellationToken)
    {
        return await _context
            .Lessons
            .FirstOrDefaultAsync(x => x.Date == request.Date
                                      && x.TeacherId == request.TeacherId,
                cancellationToken: cancellationToken) is not null;
    }
}