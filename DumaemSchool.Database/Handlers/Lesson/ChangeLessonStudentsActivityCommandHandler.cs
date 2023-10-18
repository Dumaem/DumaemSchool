using DumaemSchool.Core.Commands.Lesson;
using DumaemSchool.Database.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DumaemSchool.Database.Handlers.Lesson;

public sealed class ChangeLessonStudentsActivityCommandHandler : IRequestHandler<ChangeLessonStudentActivityCommand>
{
    private readonly ILessonRepository _repository;

    public ChangeLessonStudentsActivityCommandHandler(ILessonRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(ChangeLessonStudentActivityCommand request, CancellationToken cancellationToken)
    {
        await _repository.ChangeLessonStudentActivity(request.LessonId, request.StudentId, request.Mark);
    }
}