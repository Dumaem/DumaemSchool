using DumaemSchool.Core.Queries.Lesson;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers.Lesson;

public sealed class LessonInfoQueryHandler : IRequestHandler<LessonInfoQuery, Core.Models.Lesson?>
{
    private readonly ILessonRepository _repository;

    public LessonInfoQueryHandler(ILessonRepository repository)
    {
        _repository = repository;
    }

    public async Task<Core.Models.Lesson?> Handle(LessonInfoQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetLessonInfo(request.LessonId);
    }
}
