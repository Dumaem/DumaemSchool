using DumaemSchool.Core.Commands.Lesson;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DumaemSchool.Database.Handlers.Lesson;

public sealed class
    ChangeLessonStudentsActivityCommandHandler : IRequestHandler<ChangeLessonStudentsActivityCommand>
{
    private readonly ApplicationContext _context;

    public ChangeLessonStudentsActivityCommandHandler(ApplicationContext context)
    {
        _context = context;
    }

    public async Task Handle(ChangeLessonStudentsActivityCommand request, CancellationToken cancellationToken)
    {
        foreach (var studentActivityInfo in request.Activity)
        {
            var activity =
                await _context.Activities.FirstOrDefaultAsync(
                    x => x.LessonId == studentActivityInfo.LessonId && x.StudentId == studentActivityInfo.StudentId,
                    cancellationToken: cancellationToken);
            if (activity is not null)
            {
                activity.Mark = (int)studentActivityInfo.ActivityMark;
                _context.Update(activity);
            }
            else
            {
                _context.Activities.Add(new Activity
                {
                    StudentId = studentActivityInfo.StudentId,
                    LessonId = studentActivityInfo.LessonId,
                    Mark = (int)studentActivityInfo.ActivityMark
                });
            }

            var attendance = await _context.Attendances.FirstOrDefaultAsync(
                x => x.LessonId == studentActivityInfo.LessonId && x.StudentId == studentActivityInfo.StudentId,
                cancellationToken);
            if (!studentActivityInfo.WasAttended)
            {
                if (attendance is null)
                {
                    _context.Attendances.Add(new Attendance
                    {
                        LessonId = studentActivityInfo.LessonId,
                        StudentId = studentActivityInfo.StudentId
                    });
                }
            }
            else
            {
                if (attendance is not null)
                {
                    _context.Remove(attendance);
                }
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}