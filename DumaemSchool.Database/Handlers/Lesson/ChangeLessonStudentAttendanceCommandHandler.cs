using DumaemSchool.Core.Commands.Lesson;
using DumaemSchool.Database.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DumaemSchool.Database.Handlers.Lesson;

public sealed class ChangeLessonStudentsAttendanceCommandHandler : IRequestHandler<ChangeLessonStudentAttendanceCommand>
{
    private readonly ILessonRepository _repository;

    public ChangeLessonStudentsAttendanceCommandHandler(ILessonRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(ChangeLessonStudentAttendanceCommand request, CancellationToken cancellationToken)
    {
        await _repository.ChangeLessonStudentAttendance(request.LessonId, request.StudentId, request.WasAttended);
    }
}