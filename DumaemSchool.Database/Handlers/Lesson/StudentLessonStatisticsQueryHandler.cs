using DumaemSchool.Core.OutputModels;
using DumaemSchool.Core.Queries;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers.Lesson;

public sealed class
    StudentLessonStatisticsQueryHandler : IRequestHandler<StudentLessonStatisticsQuery,
        ListDataResult<StudentLessonStatistics>>
{
    private readonly ILessonRepository _repository;

    public StudentLessonStatisticsQueryHandler(ILessonRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListDataResult<StudentLessonStatistics>> Handle(StudentLessonStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.ListLessonStatistics(request.Param);
    }
}