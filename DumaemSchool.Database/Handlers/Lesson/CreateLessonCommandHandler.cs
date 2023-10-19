using DumaemSchool.Core.Commands.Lesson;
using DumaemSchool.Database.Repositories;
using LanguageExt.Common;
using MediatR;

namespace DumaemSchool.Database.Handlers.Lesson;

public sealed class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, Result<Core.Models.Lesson>>
{
    private readonly ILessonRepository _repository;

    public CreateLessonCommandHandler(ILessonRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Core.Models.Lesson>> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        return await _repository.CreateLesson(request.Lesson);
    }
}