using DumaemSchool.Core.OutputModels;
using DumaemSchool.Core.Queries;
using DumaemSchool.Core.Queries.Lesson;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers.Lesson;

public sealed class LessonDatesQueryHandler : IRequestHandler<LessonDatesQuery, IEnumerable<LessonDate>>
{
    private readonly ILessonRepository _repository;

    public LessonDatesQueryHandler(ILessonRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LessonDate>> Handle(LessonDatesQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.ListSectionLessonDates(request.SectionId);
    }
}